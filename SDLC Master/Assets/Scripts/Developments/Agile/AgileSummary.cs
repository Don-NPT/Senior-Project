using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AgileSummary : MonoBehaviour
{
    public static AgileSummary instance;
    public TextMeshProUGUI titleTotalQuality;
    public Transform qualityList;
    public GameObject sprintPanel;
    public Transform footerPanel;
    public Transform rightPanel;
    public GameObject detailPrefab;
    public Color[] qualityColors;
    private GameObject detail;
    private OldProject project;

    void Awake()
    {
        if (instance != null && instance != this) 
            Destroy(this); 
        else 
            instance = this;
    }
    
    // private void OnEnable() {
    //     if(ProjectManager.instance.currentProject != null){
    //         project = ProjectManager.instance.ol;
    //         SetupLeftPanel();
    //         SetupRightPanel();
    //     }
    // }

    public void ShowOldProject(int index){
        gameObject.SetActive(true);
        project = ProjectManager.instance.oldProject[index];

        SetupLeftPanel();
        SetupRightPanel();
    }

    void SetupLeftPanel(){
        titleTotalQuality.text = GetProjectQuality() + " / " + GetProjectRequireQuality();

        // Delete existing sprint rows
        foreach(Transform child in qualityList){
            Destroy(child.gameObject);
        }

        // Spawn sprint rows
        GameObject[] sprintRow = new GameObject[project.sprintList.Count];
        for(int i=0; i<project.sprintList.Count; i++){
            sprintRow[i] = (GameObject) Instantiate(sprintPanel);
            sprintRow[i].transform.SetParent(qualityList);
            sprintRow[i].transform.localScale = Vector3.one;

            sprintRow[i].GetComponentsInChildren<TextMeshProUGUI>()[0].text = "Sprint " + (i+1);
            sprintRow[i].GetComponentsInChildren<TextMeshProUGUI>()[1].text = project.sprintList[i].GetSumQuality() + " / " + project.sprintList[i].GetSumRequireQuality();
            sprintRow[i].GetComponentInChildren<Slider>().maxValue = project.sprintList[i].GetSumRequireQuality();
            sprintRow[i].GetComponentInChildren<Slider>().value = project.sprintList[i].GetSumQuality();

            float diff = (float)project.sprintList[i].GetSumQuality()/project.sprintList[i].GetSumRequireQuality();

            if(diff >= 0.7) sprintRow[i].GetComponentsInChildren<Image>()[2].color = qualityColors[0];
            else if(diff >= 0.5) sprintRow[i].GetComponentsInChildren<Image>()[2].color = qualityColors[1];
            else sprintRow[i].GetComponentsInChildren<Image>()[2].color = qualityColors[2];

            int index = i+1;
            sprintRow[i].GetComponentInChildren<Button>().onClick.AddListener(delegate { ShowDetail(project.sprintList[index-1], index); });
        }

        // Setup Footer
        footerPanel.GetComponentsInChildren<TextMeshProUGUI>()[1].text = GetProjectQuality() + " / " + GetProjectRequireQuality();
        float diff2 = (float)GetProjectQuality()/GetProjectRequireQuality();
        if(diff2 >= 0.7) footerPanel.GetComponentsInChildren<Image>()[2].color = qualityColors[0];
        else if(diff2 >= 0.5) footerPanel.GetComponentsInChildren<Image>()[2].color = qualityColors[1];
        else footerPanel.GetComponentsInChildren<Image>()[2].color = qualityColors[2];
        footerPanel.GetComponentInChildren<Slider>().maxValue = GetProjectRequireQuality();
        footerPanel.GetComponentInChildren<Slider>().value = GetProjectQuality();

        footerPanel.GetComponentsInChildren<TextMeshProUGUI>()[2].text = "ใช้เวลาทั้งหมด: " + GetProjectDuration() + " วัน";
        footerPanel.GetComponentsInChildren<TextMeshProUGUI>()[3].text = "เสร็จ " + GetNumTaskFinished() + "/" + GetNumTaskAll() + " งาน"; 
    }

    void SetupRightPanel(){
        rightPanel.GetComponentsInChildren<TextMeshProUGUI>()[0].text = project.pjName;
        rightPanel.GetComponentsInChildren<TextMeshProUGUI>()[3].text = "+" + project.finalReward.ToString("N0");
        rightPanel.GetComponentsInChildren<TextMeshProUGUI>()[5].text = "-" + project.expense.ToString("N0");
        rightPanel.GetComponentsInChildren<TextMeshProUGUI>()[7].text = StaffManager.instance.getAllStaffStat() + " หน่วย";
        rightPanel.GetComponentsInChildren<TextMeshProUGUI>()[9].text = StaffManager.instance.GetStaffbyID(project.PO_id).analysis + " หน่วย";
    }

    void ShowDetail(SprintTask sprint, int index)
    {
        if(detail == null)
        {
            detail = (GameObject) Instantiate(detailPrefab);
            detail.transform.SetParent(qualityList);
            detail.transform.localScale = Vector3.one;
            
        }

        if(!detail.activeSelf){
            detail.transform.SetSiblingIndex(index);
            detail.SetActive(true);
            detail.GetComponentsInChildren<TextMeshProUGUI>()[1].text = sprint.GetDuration() + " วัน";
            detail.GetComponentsInChildren<TextMeshProUGUI>()[3].text = sprint.taskList.Count + " งาน";
            // detail.GetComponentsInChildren<TextMeshProUGUI>()[5].text = project.staffEachPhase[index-1].ToString() + " คน";
            // detail.GetComponentsInChildren<TextMeshProUGUI>()[7].text = project.statEachPhase[index-1].ToString() + " หน่วย";
        }
        else{
            detail.SetActive(false);
        }
        
    }

    // private void OnDisable() {
    //     if(ProjectManager.instance.currentProject != null){
    //         ProjectManager.instance.oldProject.Add(project);
    //         ProjectManager.instance.currentProject = null;
    //     }
    // }

    private void OnDisable() {
        GameManager.instance.Play();
    }

    int GetProjectQuality(){
        int sum = 0;
        foreach(var sprint in project.sprintList){
            sum += sprint.GetSumQuality();            
        }
        return sum;
    }

    int GetProjectRequireQuality(){
        int sum = 0;
        foreach(var sprint in project.sprintList){
            sum += sprint.GetSumRequireQuality();            
        }
        return sum;
    }

    int GetProjectDuration(){
        int sum = 0;
        foreach(var sprint in project.sprintList){
            sum += sprint.GetDuration();            
        }
        return sum;
    } 

    int GetNumTaskFinished(){
        int sum = 0;
        foreach(var sprint in project.sprintList){
            sum = sprint.GetNumFinishedTasks();           
        }
        return sum;
    }

    int GetNumTaskAll(){
        int sum = 0;
        foreach(var sprint in project.sprintList){
            sum = sprint.GetNumAllTasks();           
        }
        return sum;
    }

}
