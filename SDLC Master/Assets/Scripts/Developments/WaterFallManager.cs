using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterFallManager : MonoBehaviour
{
    public static WaterFallManager instance;

    public GameObject[] requirementUIs;
    public GameObject[] designUIs;

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
    private int phaseIndex;

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
            requirementUIs[0].SetActive(true);
            ProjectHUD.instance.UpdateList();
            foreach(var staff in StaffManager.instance.getAllStaff())
            {
                staff.gameObject.GetComponent<StaffController>().AssignWork();
            }
            StartCoroutine(UpdateProgress());
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

            progress += project.staffEachPhase[phaseIndex];
            qualityEachPhase[phaseIndex] += project.statEachPhase[phaseIndex];
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
        switch(project.phase)
        {
            case Project.Phases.ANALYSIS:
                currentWorkAmount = project.designWork;
                staffPosition = "Designer";
                requirementUIs[0].SetActive(false);
                requirementUIs[1].SetActive(false);
                designUIs[0].SetActive(true);
                project.phase = Project.Phases.DESIGN;
                break;
            case Project.Phases.DESIGN:
                currentWorkAmount = project.codingWork;
                staffPosition = "Programmer";
                designUIs[0].SetActive(false);
                designUIs[1].SetActive(false);
                GameManager.instance.Play();
                project.phase = Project.Phases.CODING;
                break;
            case Project.Phases.CODING:
                currentWorkAmount = project.testingWork;
                staffPosition = "Tester";
                project.phase = Project.Phases.TESTING;
                break;
            case Project.Phases.TESTING:
                currentWorkAmount = project.deploymentWork;
                staffPosition = "Programmer";
                project.phase = Project.Phases.DEPLOYMENT;
                break;
            case Project.Phases.DEPLOYMENT:
                currentWorkAmount = 0;
                staffPosition = "";
                project.phase = Project.Phases.MAINTENANCE;
                break;
        }
        StartCoroutine(UpdateProgress());
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
