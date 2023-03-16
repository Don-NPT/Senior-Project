using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AgileManager : MonoBehaviour
{
    public static AgileManager instance;
    public GameObject[] agileUI;
    public int phaseIndex;

    [HideInInspector] public Project project;
    // Start is called before the first frame update
    void Start()
    {
        if (instance != null && instance != this) 
            Destroy(this); 
        else 
            instance = this; 
    }

    public void InitiateAgile()
    {
        Debug.Log("Start Agile Development");

        project = ProjectManager.instance.currentProject;
        if(project != null)
        {
            project.state = Project.Status.DOING;
            ProjectHUD.instance.UpdateList();
            foreach(var staff in StaffManager.instance.getAllStaff())
            {
                staff.gameObject.GetComponent<StaffController>().AssignWork();
            }
            phaseIndex = 0;
            agileUI[0].SetActive(true);
        }
    }

    public void Save()
    {
        AgileAdapter agileAdapter = new AgileAdapter();

        if(project != null){
            agileAdapter.Save(project.projectId, phaseIndex);
        }else{
            agileAdapter.Save(-1, -1);
        }
        
    }

    public void Load()
    {
        AgileAdapter agileAdapter = new AgileAdapter();
        AgileAdapter saveData = agileAdapter.Load();

        project = ProjectManager.instance.GetProjectbyId(saveData.projectIndex);
        phaseIndex = saveData.phaseIndex;

        ProjectHUD.instance.UpdateList();

        switch(phaseIndex){
            case 0:
                agileUI[0].SetActive(true);
                break;
            // case 1:
            //     designUIs[0].SetActive(true);
            //     break;
        }
    }
}

[System.Serializable]
public class AgileAdapter
{
    public int projectIndex;
    public int phaseIndex;

    public void Save(int _projectIndex, int _phaseIndex)
    {

        projectIndex = _projectIndex;
        phaseIndex = _phaseIndex;

        FileHandler.SaveToJSON<AgileAdapter> (this, "AgileAdapterSave.json");   
    }

    public AgileAdapter Load()
    {
        AgileAdapter dataToLoad = FileHandler.ReadFromJSON<AgileAdapter> ("AgileAdapterSave.json");
        return dataToLoad;
    }
}