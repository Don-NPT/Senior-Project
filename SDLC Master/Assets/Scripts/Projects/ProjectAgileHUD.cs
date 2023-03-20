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

            // Hide finish button
            submitBTN.SetActive(false);
            submitBTN.GetComponent<Button>().onClick.AddListener(delegate {
                gameObject.SetActive(false);
                sprintReviewUI.SetActive(true);
            });
        }

        ClearContent();
        taskItem = new GameObject[project.sprintList[AgileManager.instance.sprintIndex].taskList.Count];
        for(int i=0; i<project.sprintList[AgileManager.instance.sprintIndex].taskList.Count; i++){
            KitchenObjectSO task = project.sprintList[AgileManager.instance.sprintIndex].taskList[i];
            // Spawn task item
            taskItem[i] = (GameObject) Instantiate(taskItemPrefab);
            taskItem[i].transform.SetParent(taskContent);
            taskItem[i].transform.localScale = Vector3.one;

            // Set text
            taskItem[i].GetComponentsInChildren<TextMeshProUGUI>()[0].text = "งานที่ " + (i+1);
            taskItem[i].GetComponentsInChildren<Image>()[0].sprite = task.sprite;

            // Set Slider
            taskItem[i].GetComponentsInChildren<Slider>()[0].value = 0;
            taskItem[i].GetComponentsInChildren<Slider>()[0].maxValue = 0;
            taskItem[i].GetComponentsInChildren<Slider>()[1].value = 0;
            taskItem[i].GetComponentsInChildren<Slider>()[1].maxValue = 0;

            switch(task.objectName){
                case "งานขนาดเล็ก":
                    taskItem[i].GetComponentsInChildren<Slider>()[0].maxValue = 10;
                    taskItem[i].GetComponentsInChildren<Slider>()[1].maxValue = 50;
                    break;
                case "งานขนาดกลาง":
                    taskItem[i].GetComponentsInChildren<Slider>()[0].maxValue = 25;
                    taskItem[i].GetComponentsInChildren<Slider>()[1].maxValue = 100;
                    break;
                case "งานขนาดใหญ่":
                    taskItem[i].GetComponentsInChildren<Slider>()[0].maxValue = 50;
                    taskItem[i].GetComponentsInChildren<Slider>()[1].maxValue = 250;
                    break;
                case "งานแบบเบิ้มๆ":
                    taskItem[i].GetComponentsInChildren<Slider>()[0].maxValue = 100;
                    taskItem[i].GetComponentsInChildren<Slider>()[1].maxValue = 500;
                    break;
            }
        }
        StartCoroutine(UpdateProgress(taskItem));
    }

    IEnumerator UpdateProgress(GameObject[] taskItem)
    {
        for(int i=0; i<taskItem.Length; i++){
            project.sprintList[AgileManager.instance.sprintIndex].taskList[i].dayUsed = 0;
            while(taskItem[i].GetComponentsInChildren<Slider>()[0].value < taskItem[i].GetComponentsInChildren<Slider>()[0].maxValue)
            {
                yield return new WaitForSeconds(1);

                if(GameManager.instance.gameState != GameState.PAUSE){
                    float progress = taskItem[i].GetComponentsInChildren<Slider>()[0].value + StaffManager.instance.getTotalStaff();
                    float quality = taskItem[i].GetComponentsInChildren<Slider>()[1].value + StaffManager.instance.getAllStaffStat();
                    taskItem[i].GetComponentsInChildren<Slider>()[0].DOValue(progress, 0.3f);
                    taskItem[i].GetComponentsInChildren<Slider>()[1].DOValue(quality, 0.3f);
                    project.sprintList[AgileManager.instance.sprintIndex].taskList[i].dayUsed++;
                    // taskItem[i].GetComponentsInChildren<Slider>()[0].value += StaffManager.instance.getTotalStaff();
                }
            }
        }
        submitBTN.SetActive(true);
        List<KitchenObjectSO> tasks = project.sprintList[AgileManager.instance.sprintIndex].taskList;
        for(int i=0; i<tasks.Count; i++){
            tasks[i].quality = (int) Mathf.Round(taskItem[i].GetComponentsInChildren<Slider>()[1].value);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // if(agileHudDetail.activeSelf && ProjectManager.instance.currentProject != null)
        // {
        //     AgileHUDUpdate();
        // }
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

    // void AgileHUDUpdate(){
    //     agileHudDetail.GetComponentsInChildren<TextMeshProUGUI>()[0].text = "โมเดล: Agile (Sprint " + "" + ")";
    // }

    void ClearContent(){
        foreach(Transform child in taskContent){
            Destroy(child.gameObject);
        }
    }
}
