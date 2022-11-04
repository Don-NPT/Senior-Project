using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectManager : MonoBehaviour
{
    public static ProjectManager instance;
    public List<Project> currentProjects;
    // Start is called before the first frame update
    void Start()
    {
        if (instance != null && instance != this) 
            Destroy(this); 
        else 
            instance = this;

        currentProjects = new List<Project>();
    }

    void FixedUpdate()
    {
        foreach(var project in currentProjects)
        {
            if(project.state == Project.Status.READY)
            {
                project.state = Project.Status.DOING;
                project.phase = Project.Phases.ANALYSIS;
                ProjectHUD.instance.UpdateList();
                foreach(var staff in TeamManager2.instance.teams[project.teamIndex])
                {
                    staff.gameObject.GetComponent<StaffController>().AssignWork();
                }
                StartCoroutine(UpdateProgress(project));
            }
        }
    }

    IEnumerator UpdateProgress(Project project) {
        while(project.currentPoints < project.finishPoints || project.phase != Project.Phases.DEPLOYMENT)
        {
            yield return new WaitForSeconds(2.5f);
            project.currentPoints += 5;
            Debug.Log(project.currentPoints);
            if(project.currentPoints >= project.finishPoints && project.phase != Project.Phases.DEPLOYMENT)
            {
                project.currentPoints = 0;
                NextPhase(project);
            }
        }
        Debug.Log("FINISHED!!");
        project.state = Project.Status.FINISHED;
        ProjectHUD.instance.UpdateList();
        GameManager.instance.AddMoney(project.reward);
    }

    public void NextPhase(Project project)
    {
        switch(project.phase)
        {
            case Project.Phases.ANALYSIS:
                project.phase = Project.Phases.DESIGN;
                break;
            case Project.Phases.DESIGN:
                project.phase = Project.Phases.CODING;
                break;
            case Project.Phases.CODING:
                project.phase = Project.Phases.TESTING;
                break;
            case Project.Phases.TESTING:
                project.phase = Project.Phases.DEPLOYMENT;
                break;
        }
    }

    public int getNumProject()
    {
        return currentProjects.Count;
    }
}
