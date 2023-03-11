using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgileManager : MonoBehaviour
{
    public static AgileManager instance;
    public GameObject[] agileUI;

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
            agileUI[0].SetActive(true);
            // StartCoroutine(UpdateProgress());
        }
    }
}
