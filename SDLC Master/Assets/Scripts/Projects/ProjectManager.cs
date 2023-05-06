using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ProjectManager : MonoBehaviour
{
    public static ProjectManager instance;
    public Project[] allProjects;
    public List<Project> currentProjects;
    public Project currentProject;
    public List<OldProject> oldProject;
    public List<KitchenObjectSO> AllTasks;
    
    void Awake() {
        if (instance != null && instance != this) 
            Destroy(this); 
        else 
            instance = this;

        currentProjects = new List<Project>();
        oldProject = new List<OldProject>();
    }

    public void FinishProject(Project project)
    {
        currentProject = null;
        oldProject.Add(new OldProject(project));
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

        int id = -1;
        if(currentProject != null){
            id = currentProject.projectId;
        }
        
        // List<int> oldProjectId = new List<int>();
        // foreach(var project in oldProject){
        //     oldProjectId.Add(project.projectId);
        // }
        gameAdapter.Save(id, oldProject.ToArray());
    }

    public void Load()
    {
        ProjectManagerAdapter projectManagerAdapter = new ProjectManagerAdapter();
        ProjectManagerAdapter saveData = projectManagerAdapter.Load();
 
        foreach(var project in allProjects){
            if(project.projectId == saveData.currentProject) currentProject = project;
            // foreach(var old in saveData.oldProject){
            //     if(project.projectId == old) oldProject.Add(project);
            // }
        }
        oldProject = saveData.oldProject.ToList();
    }
    
}

[System.Serializable]
public class ProjectManagerAdapter
{
    public int currentProject;
    public OldProject[] oldProject;

    public void Save(int _currentProject, OldProject[] _oldProject)
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