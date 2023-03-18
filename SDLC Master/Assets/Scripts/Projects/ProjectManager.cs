using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectManager : MonoBehaviour
{
    public static ProjectManager instance;
    public Project[] allProjects;
    public List<Project> currentProjects;
    public Project currentProject;
    public List<Project> oldProject;
    
    void Awake() {
        if (instance != null && instance != this) 
            Destroy(this); 
        else 
            instance = this;
    }
    void Start()
    {
        currentProjects = new List<Project>();
        oldProject = new List<Project>();
    }

    public void FinishProject(Project project)
    {
        currentProject = null;
        oldProject.Add(project);
    }

    public int getNumOldProject()
    {
        return oldProject.Count;
    }

    public Project GetProjectbyId(int index)
    {
        foreach(Project project in allProjects){
            if(project.projectId == index){
                return project;
            }
        }
        return null;
    }

    public bool CheckProjectInProgress()
    {
        foreach(Project project in allProjects){
            if(project.inProgress == true){
                return true;
            }
        }
        return false;
    }

    public void Save()
    {
        ProjectManagerAdapter gameAdapter = new ProjectManagerAdapter();
        gameAdapter.Save(currentProject, oldProject);
    }

    public void Load()
    {
        ProjectManagerAdapter projectManagerAdapter = new ProjectManagerAdapter();
        ProjectManagerAdapter saveData = projectManagerAdapter.Load();
        currentProject = saveData.currentProject;
        oldProject = saveData.oldProject;
    }
    
}

[System.Serializable]
public class ProjectManagerAdapter
{
    public Project currentProject;
    public List<Project> oldProject;

    public void Save(Project _currentProject, List<Project> _oldProject)
    {
        currentProject = _currentProject;
        oldProject = _oldProject;
        FileHandler.SaveToJSON<ProjectManagerAdapter> (this, "ProjectManagerSave.json");   
    }

    public ProjectManagerAdapter Load()
    {
        ProjectManagerAdapter dataToLoad = FileHandler.ReadFromJSON<ProjectManagerAdapter> ("ProjectManagerSave.json");
        return dataToLoad;
    }
}