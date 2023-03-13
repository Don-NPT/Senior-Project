using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevelopmentManager : MonoBehaviour
{
    public static DevelopmentManager instance;
    public int currentDayUsed;
    public int totalDayToFinished;
    public int currentDayInPhase;
    public int DayEachPhase;
    int[] totalDayEachPhase;
    public int[] qualityEachPhase;
    public int[] currentQualityEachPhase;
    int totalQuality;
    public int currentQuality;
    public Project.Phases currentPhase;
    public GameObject[] requirementUIs;
    public GameObject[] designUIs;

    void Start()
    {
        // If there is an instance, and it's not me, delete myself.
        if (instance != null && instance != this) 
            Destroy(this); 
        else 
            instance = this; 

    }

    void FixedUpdate()
    {
        foreach(var project in ProjectManager.instance.currentProjects)
        {
            if(project.state == Project.Status.READY)
            {
                project.state = Project.Status.DOING;
                project.phase = Project.Phases.ANALYSIS;
                requirementUIs[0].SetActive(true);
                ProjectHUD.instance.UpdateList();
                foreach(var staff in TeamManager2.instance.teams[project.teamIndex])
                {
                    staff.gameObject.GetComponent<StaffController>().AssignWork();
                }
                ComputeTotalTime(TeamManager2.instance.teams[project.teamIndex], project);
                ComputeQuality(TeamManager2.instance.teams[project.teamIndex], project);
                StartCoroutine(UpdateProgress(project));
            }
        }
    }

    void ComputeTotalTime(List<StaffProperties> teams, Project project)
    {
        totalDayEachPhase = new int[5];
        totalDayEachPhase[0] = getTotalDays(teams, project, "Analyst");
        totalDayEachPhase[1] = getTotalDays(teams, project, "Designer");
        totalDayEachPhase[2] = getTotalDays(teams, project, "Programmer");
        totalDayEachPhase[3] = getTotalDays(teams, project, "Tester");
        totalDayEachPhase[4] = getTotalDays(teams, project, "Programmer");

        Debug.Log("analysisDays " + totalDayEachPhase[0]);
        Debug.Log("designDays " + totalDayEachPhase[1]);
        Debug.Log("codingDays " + totalDayEachPhase[2]);
        Debug.Log("testingDays " + totalDayEachPhase[3]);
        Debug.Log("deployDays " + totalDayEachPhase[4]);

        totalDayToFinished = totalDayEachPhase[0] + totalDayEachPhase[1] + totalDayEachPhase[2] + totalDayEachPhase[3] + totalDayEachPhase[4];
        Debug.Log("Total Days to Finish:  " + totalDayToFinished);
    }

    int getTotalDays(List<StaffProperties> teams, Project project, string position)
    {
        float teamCapability = 0;
        int dayToFinish = 0;
        foreach(var staff in teams)
        {
            if(staff.position == position)
            {
                float staffCapability = staff.stamina / (project.scale * 10);
                teamCapability += staffCapability;
            }
        }
        dayToFinish = (int) Mathf.Round(project.estimateDaysInPhase * ((100 - teamCapability)/100));
        return dayToFinish;
    }

    IEnumerator UpdateProgress(Project project) {    
        currentQualityEachPhase = new int[5];    
        currentDayUsed = 0;
        ProjectHUD.instance.ShowFinishBTN(project);
        for(int i=0; i<totalDayEachPhase.Length; i++)
        {
            currentPhase = project.phase;
            DayEachPhase = totalDayEachPhase[i];
            currentDayInPhase = 0;
            currentQuality = 0;
            currentQualityEachPhase[i] = 0;
            List<GameObject> workingStaffs = TeamManager2.instance.getStaffbyPhase(project.phase, TeamManager2.instance.teams[project.teamIndex]);
            while(currentDayInPhase < totalDayEachPhase[i])
            {
                yield return new WaitForSeconds(10);
                currentDayInPhase++;
                currentDayUsed++;
                currentQuality += (int) Mathf.Round(totalQuality/DayEachPhase);
                currentQualityEachPhase[i] += (int) Mathf.Round(qualityEachPhase[i]/DayEachPhase);
                foreach(var staff in workingStaffs)
                {
                    staff.GetComponent<StaffController>().state = StaffState.WORKING;
                }
            }
            foreach(var staff in workingStaffs)
            {
                yield return new WaitForSeconds(1);
                staff.GetComponent<StaffController>().state = StaffState.WAITING;
            }
            currentDayInPhase = 0;
            project.phase = NextPhase(project);
        }
        FinishProject(project);
    }

    void FinishProject(Project project)
    {
        Debug.Log("FINISHED!!");
        project.state = Project.Status.FINISHED;

        project.actualAnalysis = currentQualityEachPhase[0];
        project.actualDesign = currentQualityEachPhase[1];
        project.actualCoding = currentQualityEachPhase[2];
        project.actualTesting = currentQualityEachPhase[3];
        project.actualDeployment = currentQualityEachPhase[4];
        project.dayUsed = totalDayToFinished;
        ProjectManager.instance.FinishProject(project);
        // ProjectManager.instance.currentProjects.Remove(project);
        // ProjectHUD.instance.UpdateList();
        ProjectHUD.instance.ShowFinishBTN(project);
    }

    void ComputeQuality(List<StaffProperties> teams, Project project)
    {
        qualityEachPhase = new int[5];
        qualityEachPhase[0] = getQualityEachPhase(teams, project, "Analyst");
        qualityEachPhase[1] = getQualityEachPhase(teams, project, "Designer");
        qualityEachPhase[2] = getQualityEachPhase(teams, project, "Programmer");
        qualityEachPhase[3] = getQualityEachPhase(teams, project, "Tester");
        qualityEachPhase[4] = getQualityEachPhase(teams, project, "Programmer");

        Debug.Log("analysis quality " + qualityEachPhase[0]);
        Debug.Log("design quality " + qualityEachPhase[1]);
        Debug.Log("coding quality " + qualityEachPhase[2]);
        Debug.Log("testing quality " + qualityEachPhase[3]);
        Debug.Log("deploy quality " + qualityEachPhase[4]);

        totalQuality = qualityEachPhase[0] + qualityEachPhase[1] + qualityEachPhase[2] + qualityEachPhase[3] + qualityEachPhase[4];
        Debug.Log("Total Quality:  " + totalQuality);
    }

    int getQualityEachPhase(List<StaffProperties> teams, Project project, string position)
    {
        float teamCapability = 0;
        
        float staffCapability = 0;

        foreach(var staff in teams)
        {
            
            if(staff.position == position)
            {
                switch(position)
                {
                    case "Analyst":
                        staffCapability = (staff.analysis * staff.stamina) / project.scale;
                        Debug.Log(staff.analysis);
                        Debug.Log(staffCapability);
                        teamCapability += staffCapability;
                        break;
                    case "Designer":
                        staffCapability = (staff.design * staff.stamina) / project.scale;
                        teamCapability += staffCapability;
                        break;
                    case "Programmer":
                        staffCapability = (staff.coding * staff.stamina) / project.scale;
                        teamCapability += staffCapability;
                        break;
                    case "Tester":
                        staffCapability = (staff.testing * staff.stamina) / project.scale;
                        teamCapability += staffCapability;
                        break;
                }
                
            }
        }
        int quality = (int) Mathf.Round(teamCapability * 0.25f);
        // quality = (int) Mathf.Round(project.estimateDaysInPhase * ((100 - teamCapability)/100));
        return (int)quality;
    }

    public Project.Phases NextPhase(Project project)
    {
        switch(project.phase)
        {
            case Project.Phases.ANALYSIS:
                requirementUIs[0].SetActive(false);
                requirementUIs[1].SetActive(false);
                designUIs[0].SetActive(true);
                return Project.Phases.DESIGN;
            case Project.Phases.DESIGN:
                designUIs[0].SetActive(false);
                designUIs[1].SetActive(false);
                GameManager.instance.Play();
                return Project.Phases.CODING;
            case Project.Phases.CODING:
                return Project.Phases.TESTING;
            case Project.Phases.TESTING:
                return Project.Phases.DEPLOYMENT;
        }
        return project.phase;
    }

    public void Save()
    {
        DevelopmentAdapter gameAdapter = new DevelopmentAdapter();
        gameAdapter.Save(instance);
    }

    public void Load()
    {
        DevelopmentAdapter developmentAdapter = new DevelopmentAdapter();
        DevelopmentAdapter saveData = developmentAdapter.Load();
        instance = saveData.instance;
    }
}

[System.Serializable]
public class DevelopmentAdapter
{
    public DevelopmentManager instance;

    public void Save(DevelopmentManager _instance)
    {
        instance = _instance;
        FileHandler.SaveToJSON<DevelopmentAdapter> (this, "DevelopmentManagerSave.json");   
    }

    public DevelopmentAdapter Load()
    {
        DevelopmentAdapter dataToLoad = FileHandler.ReadFromJSON<DevelopmentAdapter> ("ProjectManagerSave.json");
        return dataToLoad;
    }
}
