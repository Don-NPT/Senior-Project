using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterFallManager : MonoBehaviour
{
    public static WaterFallManager instance;
    public int pointWrong;
    private float calculateQuality;

    public GameObject[] requirementUIs;
    public GameObject[] designUIs;
    public GameObject[] detailminigames;

    [HideInInspector]
    public float[] qualityEachPhase;
    [HideInInspector]
    public int currentWorkAmount;
    [HideInInspector]
    public int progress;
    [HideInInspector]
    public Project project;

    private string staffPosition;
    public int phaseIndex;

    void Awake()
    {
        // If there is an instance, and it's not me, delete myself.
        if (instance != null && instance != this) 
            Destroy(this); 
        else 
            instance = this; 

    }

    public void InitiateWaterfall()
    {
        Debug.Log("Start Waterfall Development");

        project = ProjectManager.instance.currentProject;
        Debug.Log("Project id = " + project.projectId);
        if(project != null)
        {
            project.state = Project.Status.DOING;
            project.phase = Project.Phases.ANALYSIS;
            currentWorkAmount = project.analysisWork;
            staffPosition = "Analyst";
            phaseIndex = 0;
            qualityEachPhase = new float[6];  
            project.staffEachPhase = new int[6];
            project.statEachPhase = new int[6];
            project.startDates = new System.DateTime[6];
            project.startDates[0] = TimeManager.instance.currentDate;
            project.staffEachPhase[phaseIndex] = StaffManager.instance.getTotalStaffbyPosition(staffPosition);
            project.statEachPhase[phaseIndex] = StaffManager.instance.getSumStaffStat(staffPosition);
            if(detailminigames[0].GetComponent<Tutorial>().Checktutorial()==true){
                detailminigames[0].GetComponent<Tutorial>().Open();
            }else{
                requirementUIs[0].SetActive(true);
            }
            ProjectHUD.instance.UpdateList();
            foreach(var staff in StaffManager.instance.getAllStaff())
            {
                staff.gameObject.GetComponent<StaffController>().AssignWork();
            }
            // StartCoroutine(UpdateProgress());
        }
    }

    IEnumerator UpdateProgress()
    {
        progress = 0;
        // int TotalTime = (int) Mathf.Round(project.getWorkAmountbyIndex(phaseIndex) / StaffManager.instance.getTotalStaffbyPosition(staffPosition));
        project.staffEachPhase[phaseIndex] = StaffManager.instance.getTotalStaffbyPosition(staffPosition);
        project.statEachPhase[phaseIndex] = StaffManager.instance.getSumStaffStat(staffPosition);

        while(progress < currentWorkAmount)
        {
            yield return new WaitForSeconds(1);

            if(GameManager.instance.gameState != GameState.PAUSE){
                progress += project.staffEachPhase[phaseIndex];
                Debug.Log("Progress "+progress);
                calculateQuality = ((float)StaffManager.instance.getSumStaffStat("Programmer")/((float)(project.scale * 15))) * pointWrong;
                calculateQuality = calculateQuality * SkillManager.instance.GetQualityBonus();
                qualityEachPhase[phaseIndex] += calculateQuality;
            }
        }
        if(project.phase != Project.Phases.DEPLOYMENT)
            NextPhase();
        else
            FinishProject();
    }

    public void NextPhase()
    {
        project.finishDates[phaseIndex] = TimeManager.instance.currentDate;
        phaseIndex++;
        project.startDates[phaseIndex] = TimeManager.instance.currentDate;
        project.staffEachPhase[phaseIndex] = StaffManager.instance.getTotalStaffbyPosition(staffPosition);
        project.statEachPhase[phaseIndex] = StaffManager.instance.getSumStaffStat(staffPosition);

        switch(project.phase)
        {
            case Project.Phases.ANALYSIS:
                currentWorkAmount = project.designWork;
                staffPosition = "Designer";
                requirementUIs[0].SetActive(false);
                if(detailminigames[1].GetComponent<Tutorial>().Checktutorial()==true){
                    detailminigames[1].GetComponent<Tutorial>().Open();
                }else{
                    designUIs[0].SetActive(true);
                    GameManager.instance.Play();
                }
                //designUIs[0].SetActive(true);
                project.phase = Project.Phases.DESIGN;
                break;
            case Project.Phases.DESIGN:
                currentWorkAmount = project.codingWork;
                staffPosition = "Programmer";
                designUIs[0].SetActive(false);
                if(detailminigames[2].GetComponent<Tutorial>().Checktutorial()==true){
                    detailminigames[2].GetComponent<Tutorial>().Open();
                }else if(detailminigames[2].GetComponent<Tutorial>().Checktutorial()==false){
                    detailminigames[4].GetComponent<Tutorial>().Open();
                }else{
                    GameManager.instance.Play();
                    BalloonBoom.instance.InitiateBalloonDev();
                }
                project.phase = Project.Phases.CODING;
                break;
            case Project.Phases.CODING:
                currentWorkAmount = project.testingWork;
                staffPosition = "Tester";
                BalloonBoom.instance.Stop();
                if(detailminigames[3].GetComponent<Tutorial>().Checktutorial()==true){
                    detailminigames[3].GetComponent<Tutorial>().Open();
                }else if(detailminigames[3].GetComponent<Tutorial>().Checktutorial()==false){
                    detailminigames[5].GetComponent<Tutorial>().Open();
                }else{
                    GameManager.instance.Play();
                    BalloonBoom.instance.InitiateBalloonTest();
                }
                project.phase = Project.Phases.TESTING;
                break;
            case Project.Phases.TESTING:
                currentWorkAmount = project.deploymentWork;
                staffPosition = "Programmer";
                BalloonBoom.instance.Stop();
                project.phase = Project.Phases.DEPLOYMENT;
                StartCoroutine(UpdateProgress());
                break;
            case Project.Phases.DEPLOYMENT:
                currentWorkAmount = 0;
                staffPosition = "";
                project.phase = Project.Phases.MAINTENANCE;
                break;
        }
        // StartCoroutine(UpdateProgress());
    }

    void FinishProject()
    {
        Debug.Log("FINISHED!!");
        project.state = Project.Status.FINISHED;

        project.actualAnalysis = (int) Mathf.Round(qualityEachPhase[0]);
        project.actualDesign = (int) Mathf.Round(qualityEachPhase[1]);
        project.actualCoding = (int) Mathf.Round(qualityEachPhase[2]);
        project.actualTesting = (int) Mathf.Round(qualityEachPhase[3]);
        project.actualDeployment = (int) Mathf.Round(qualityEachPhase[4]);

        project.finishDates[phaseIndex] = TimeManager.instance.currentDate;

        // project.dayUsed = totalDayToFinished;
        ProjectManager.instance.FinishProject(project);
        // ProjectManager.instance.currentProjects.Remove(project);
        // ProjectHUD.instance.UpdateList();
        ProjectHUD.instance.ShowFinishBTN(project);
        GameManager.instance.Play();
    }

    public void Save()
    {
        WaterfallAdapter waterfallAdapter = new WaterfallAdapter();
        if(qualityEachPhase != null && project != null){
            waterfallAdapter.Save(qualityEachPhase,currentWorkAmount,progress,project.projectId,staffPosition,phaseIndex);
        }else{
            waterfallAdapter.Save(null,currentWorkAmount,0,-1,staffPosition,-1);
        }
    }

    public void Load()
    {
        WaterfallAdapter waterfallAdapter = new WaterfallAdapter();
        WaterfallAdapter saveData = waterfallAdapter.Load();

        if(ProjectManager.instance.currentProject.model.modelName == "Waterfall"){
            qualityEachPhase = saveData.qualityEachPhase;
            currentWorkAmount = saveData.currentWorkAmount;
            progress = 0;
            if(saveData.projectIndex != -1) {
                project = ProjectManager.instance.GetProjectbyId(saveData.projectIndex);
                ProjectHUD.instance.UpdateList();
            }
            staffPosition = saveData.staffPosition;
            phaseIndex = saveData.phaseIndex;

            
            // Reset current phase quality
            if(qualityEachPhase != null && qualityEachPhase.Length != 0) {qualityEachPhase[phaseIndex] = 0;}

            switch(phaseIndex){
                case 0:
                    requirementUIs[0].SetActive(true);
                    break;
                case 1:
                    designUIs[0].SetActive(true);
                    break;
                case 2:
                    BalloonBoom.instance.InitiateBalloonDev();
                    break;
                case 3:
                    BalloonBoom.instance.InitiateBalloonTest();
                    break;
                case 4:
                    StartCoroutine(UpdateProgress());
                    break;
            }
        }
    }

}

[System.Serializable]
public class WaterfallAdapter
{
    public float[] qualityEachPhase;
    public int currentWorkAmount;
    public int progress;
    public int projectIndex;
    public string staffPosition;
    public int phaseIndex;

    public void Save(float[] _qualityEachPhase, int _currentWorkAmount, int _progress, int _projectIndex, string _staffPosition, int _phaseIndex)
    {
        qualityEachPhase = _qualityEachPhase;
        currentWorkAmount = _currentWorkAmount;
        progress = _progress;
        projectIndex = _projectIndex;
        staffPosition = _staffPosition;
        phaseIndex = _phaseIndex;

        FileHandler.SaveToJSON<WaterfallAdapter> (this, "WaterfallAdapterSave.json");   
    }

    public WaterfallAdapter Load()
    {
        WaterfallAdapter dataToLoad = FileHandler.ReadFromJSON<WaterfallAdapter> ("WaterfallAdapterSave.json");
        return dataToLoad;
    }
}
