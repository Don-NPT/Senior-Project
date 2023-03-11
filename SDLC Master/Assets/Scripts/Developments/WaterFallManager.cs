using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterFallManager : MonoBehaviour
{
    public static WaterFallManager instance;

    public GameObject[] requirementUIs;
    public GameObject[] designUIs;
    public GameObject[] detailminigames;

    [HideInInspector]
    public int[] qualityEachPhase;
    [HideInInspector]
    public int[] TotalQualityEachPhase;
    [HideInInspector]
    public int currentWorkAmount;
    [HideInInspector]
    public int progress;
    [HideInInspector]
    public Project project;

    private string staffPosition;
    public int phaseIndex;

    void Start()
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
        if(project != null)
        {
            project.state = Project.Status.DOING;
            project.phase = Project.Phases.ANALYSIS;
            currentWorkAmount = project.analysisWork;
            staffPosition = "Analyst";
            phaseIndex = 0;
            qualityEachPhase = new int[6];  
            TotalQualityEachPhase = new int[6]; 
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
                qualityEachPhase[phaseIndex] += project.statEachPhase[phaseIndex];
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
                }else{
                    GameManager.instance.Play();
                }
                BalloonBoom.instance.InitiateBalloonDev();
                project.phase = Project.Phases.CODING;
                break;
            case Project.Phases.CODING:
                currentWorkAmount = project.testingWork;
                staffPosition = "Tester";
                BalloonBoom.instance.Stop();
                if(detailminigames[3].GetComponent<Tutorial>().Checktutorial()==true){
                    detailminigames[3].GetComponent<Tutorial>().Open();
                }else{
                    GameManager.instance.Play();
                }
                BalloonBoom.instance.InitiateBalloonTest();
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

        project.actualAnalysis = qualityEachPhase[0];
        project.actualDesign = qualityEachPhase[1];
        project.actualCoding = qualityEachPhase[2];
        project.actualTesting = qualityEachPhase[3];
        project.actualDeployment = qualityEachPhase[4];

        project.finishDates[phaseIndex] = TimeManager.instance.currentDate;

        // project.dayUsed = totalDayToFinished;
        ProjectManager.instance.FinishProject(project);
        // ProjectManager.instance.currentProjects.Remove(project);
        // ProjectHUD.instance.UpdateList();
        ProjectHUD.instance.ShowFinishBTN(project);
        GameManager.instance.Play();
    }

}
