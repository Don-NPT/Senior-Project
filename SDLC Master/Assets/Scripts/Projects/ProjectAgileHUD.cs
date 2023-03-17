using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class ProjectAgileHUD : MonoBehaviour
{
    public static ProjectAgileHUD instance;
    public Transform parent;
    public int projectIndex;
    public GameObject agileHudTab;
    public GameObject agileHudDetail;
    public GameObject submitBTN;
    public GameObject sprintReviewUI;
    public Transform taskContent;
    public GameObject taskItemPrefab;

    private Project project;
    private GameObject[] taskItem;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null && instance != this) 
            Destroy(this); 
        else 
            instance = this;
    }

    void OnEnable() {
        project = ProjectManager.instance.currentProject;

        if(project != null){
             // Setup project hud tab
            agileHudTab.SetActive(true);

            // Setup text info
            agileHudTab.GetComponentsInChildren<TextMeshProUGUI>()[0].text = ProjectManager.instance.currentProject.pjName;
            agileHudTab.GetComponentsInChildren<TextMeshProUGUI>()[1].text = ProjectManager.instance.currentProject.state.ToString();
        }

        ClearContent();
        Debug.Log(project.sprintList);
        taskItem = new GameObject[project.sprintList[0].taskList.Count];
        for(int i=0; i<project.sprintList[0].taskList.Count; i++){
            KitchenObjectSO task = project.sprintList[0].taskList[i];
            taskItem[i] = (GameObject) Instantiate(taskItemPrefab);
            taskItem[i].transform.SetParent(taskContent);
            taskItem[i].transform.localScale = Vector3.one;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(agileHudDetail.activeSelf && ProjectManager.instance.currentProject != null)
        {
            AgileHUDUpdate();
        }
    }

    public void ShowFinishBTN(Project project)
    {
        if(agileHudDetail.activeSelf){
            submitBTN.SetActive(true);
            submitBTN.GetComponent<Button>().onClick.AddListener(delegate {
                Debug.Log("Submit");
                GameManager.instance.Play();
                gameObject.SetActive(false);
                sprintReviewUI.SetActive(true);
            });
        }
    }

    void AgileHUDUpdate(){
        agileHudDetail.GetComponentsInChildren<TextMeshProUGUI>()[0].text = "โมเดล: Agile (Sprint " + "" + ")";
    }

    void ClearContent(){
        foreach(Transform child in taskContent){
            Destroy(child.gameObject);
        }
    }
}
