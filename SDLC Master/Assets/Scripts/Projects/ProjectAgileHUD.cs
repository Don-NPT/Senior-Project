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
    public TextMeshProUGUI dayLeftText;

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
            agileHudTab.GetComponentsInChildren<TextMeshProUGUI>()[0].text = project.pjName;
            agileHudTab.GetComponentsInChildren<TextMeshProUGUI>()[1].text = project.state.ToString();
            GetComponentsInChildren<TextMeshProUGUI>()[3].text = "โมเดล: Agile (" + project.sprintList[AgileManager.instance.sprintIndex].sprintName + ")";

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
            Task task = project.sprintList[AgileManager.instance.sprintIndex].taskList[i];
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
                    taskItem[i].GetComponentsInChildren<Slider>()[0].maxValue = ProjectManager.instance.AllTasks[0].dayToFinish;
                    taskItem[i].GetComponentsInChildren<Slider>()[1].maxValue = ProjectManager.instance.AllTasks[0].requireQuality;
                    break;
                case "งานขนาดกลาง":
                    taskItem[i].GetComponentsInChildren<Slider>()[0].maxValue = ProjectManager.instance.AllTasks[1].dayToFinish;
                    taskItem[i].GetComponentsInChildren<Slider>()[1].maxValue = ProjectManager.instance.AllTasks[1].requireQuality;
                    break;
                case "งานขนาดใหญ่":
                    taskItem[i].GetComponentsInChildren<Slider>()[0].maxValue = ProjectManager.instance.AllTasks[2].dayToFinish;
                    taskItem[i].GetComponentsInChildren<Slider>()[1].maxValue = ProjectManager.instance.AllTasks[2].requireQuality;
                    break;
                case "งานแบบเบิ้มๆ":
                    taskItem[i].GetComponentsInChildren<Slider>()[0].maxValue = ProjectManager.instance.AllTasks[3].dayToFinish;
                    taskItem[i].GetComponentsInChildren<Slider>()[1].maxValue = ProjectManager.instance.AllTasks[3].requireQuality;
                    break;
            }
        }
        StartCoroutine(UpdateProgress(taskItem));
    }

    IEnumerator UpdateProgress(GameObject[] taskItem)
    {
        GameManager.instance.Play();
        TimeManager.instance.Play();
        int day = 0;
        int dayLeft = 14;
        dayLeftText.text = "เหลือเวลา: " + dayLeft.ToString() + " วัน";

        for(int i=0; i<project.sprintList[AgileManager.instance.sprintIndex].taskList.Count; i++){
            project.sprintList[AgileManager.instance.sprintIndex].taskList[i].dayUsed = 0;
            project.sprintList[AgileManager.instance.sprintIndex].taskList[i].isComplete = false;
        }

        for(int i=0; i<taskItem.Length; i++){
            if(day > 14 || dayLeft <= 0) break;
            
            int taskDay = 0;
            project.sprintList[AgileManager.instance.sprintIndex].taskList[i].dayUsed = 0;

            while(taskItem[i].GetComponentsInChildren<Slider>()[0].value < taskItem[i].GetComponentsInChildren<Slider>()[0].maxValue)
            {
                if(taskDay*StaffManager.instance.getTotalStaff() >= project.sprintList[AgileManager.instance.sprintIndex].taskList[i].dayToFinish || day > 14 || dayLeft <= 0){
                    break;
                }
                yield return new WaitForSeconds(1);

                if(GameManager.instance.gameState != GameState.PAUSE){
                    day++;
                    dayLeft--;
                    dayLeftText.text = "เหลือเวลา: " + dayLeft.ToString() + " วัน";
                    float progress = taskItem[i].GetComponentsInChildren<Slider>()[0].value + StaffManager.instance.getTotalStaff();
                    float quality = taskItem[i].GetComponentsInChildren<Slider>()[1].value + ((float)StaffManager.instance.getAllStaffStat()/((float)(project.scale * 15))) * SkillManager.instance.GetQualityBonus() * 180;
                    taskItem[i].GetComponentsInChildren<Slider>()[0].DOValue(progress, 0.3f);
                    taskItem[i].GetComponentsInChildren<Slider>()[1].DOValue(quality, 0.3f);
                    project.sprintList[AgileManager.instance.sprintIndex].taskList[i].dayUsed++;
                    // taskItem[i].GetComponentsInChildren<Slider>()[0].value += StaffManager.instance.getTotalStaff();
                    // Debug.Log(((float)StaffManager.instance.getAllStaffStat()/((float)(project.scale * 15))) * SkillManager.instance.GetQualityBonus() * 180);
                }  
                taskDay++;
            }

            project.sprintList[AgileManager.instance.sprintIndex].taskList[i].isComplete = true;
        }
        yield return new WaitForSeconds(0.3f);
        // project.sprintList[AgileManager.instance.sprintIndex].taskList[1].isComplete = true;
        submitBTN.SetActive(true);
        List<Task> tasks = project.sprintList[AgileManager.instance.sprintIndex].taskList;
        // Save quality
        for(int i=0; i<tasks.Count; i++){
            tasks[i].quality = (int) Mathf.Round(taskItem[i].GetComponentsInChildren<Slider>()[1].value);
            Debug.Log(tasks[i].size + " : " + tasks[i].quality);
            // Debug.Log(tasks[i].isComplete);
        }
        // Add unfinished tasks to next sprint
        foreach(var task in tasks){
            if(task.isComplete == false){
                task.fromPreviousSprint = true;
                project.sprintList[AgileManager.instance.sprintIndex+1].taskList.Add(task);
            }
        }
        GameManager.instance.Play();
        TimeManager.instance.Pause();
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
