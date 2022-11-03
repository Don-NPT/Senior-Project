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
        while(project.currentPoints < project.finishPoints)
        {
            yield return new WaitForSeconds(2.5f);
            project.currentPoints += 3;
            Debug.Log(project.currentPoints);
        }
        Debug.Log("FINISHED!!");
        project.state = Project.Status.FINISHED;
        ProjectHUD.instance.UpdateList();
        GameManager.instance.AddMoney(project.reward);
    }

    public int getNumProject()
    {
        return currentProjects.Count;
    }
}
