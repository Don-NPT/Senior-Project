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
    public List<KitchenObjectSO> AllTasks;
    
    void Awake() {
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
        List<int> oldProjectId = new List<int>();
        foreach(var project in oldProject){
            oldProjectId.Add(project.projectId);
        }
        gameAdapter.Save(currentProject.projectId, oldProjectId.ToArray());
    }

    public void Load()
    {
        ProjectManagerAdapter projectManagerAdapter = new ProjectManagerAdapter();
        ProjectManagerAdapter saveData = projectManagerAdapter.Load();
 
        foreach(var project in allProjects){
            if(project.projectId == saveData.currentProject) currentProject = project;
            foreach(var old in saveData.oldProject){
                if(project.projectId == old) oldProject.Add(project);
            }
        }
    }
    
}

[System.Serializable]
public class ProjectManagerAdapter
{
    public int currentProject;
    public int[] oldProject;

    public void Save(int _currentProject, int[] _oldProject)
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