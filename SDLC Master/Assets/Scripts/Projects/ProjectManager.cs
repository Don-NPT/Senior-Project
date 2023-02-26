using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectManager : MonoBehaviour
{
    public static ProjectManager instance;
    public List<Project> currentProjects;
    public Project currentProject;
    public List<Project> oldProject;

    public GameObject ProjectSummary;
    // Start is called before the first frame update
    void Start()
    {
        if (instance != null && instance != this) 
            Destroy(this); 
        else 
            instance = this;

        currentProjects = new List<Project>();
        oldProject = new List<Project>();
    }

    public void FinishProject(Project project)
    {
        currentProject = null;
        oldProject.Add(project);
    }

    public int getNumProject()
    {
        return currentProjects.Count;
    }

    public int getNumOldProject()
    {
        return oldProject.Count;
    }

    public void ViewProjectSummary()
    {
        ProjectSummary.SetActive(true);
    }
}
