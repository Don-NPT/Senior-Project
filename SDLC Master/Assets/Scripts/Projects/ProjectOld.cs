using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class ProjectOld : MonoBehaviour
{
    public GameObject projectOldItemPrefab;
    public Transform content;
    public GameObject waterfallSummary;
    public GameObject agileSummary;
    GameObject[] projectOldItem;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable() {
        projectOldItem = new GameObject[ProjectManager.instance.getNumOldProject()];
        
        // var oldProjects = ProjectManager.instance.oldProject.GroupBy(p => p.projectId)
        //                       .Select(group => group.Last())
        //                       .ToList();

        for(int i=0; i<ProjectManager.instance.oldProject.Count; i++)
        {
            // Spawn Old project items
            projectOldItem[i] = (GameObject)Instantiate(projectOldItemPrefab);
            projectOldItem[i].transform.SetParent(content);
            projectOldItem[i].transform.localScale = new Vector3(1, 1, 1);

            // set text for project confirm
            TextMeshProUGUI[] projectOldText = projectOldItem[i].GetComponentsInChildren<TextMeshProUGUI>();
            OldProject project =  ProjectManager.instance.oldProject[i];
            projectOldText[0].text = project.pjName;

            if(project.model.modelName == "Waterfall"){
                projectOldText[2].text = project.getAllActualQuality() + "/" + project.getAllRequireQuality();
                Slider slider = projectOldItem[i].GetComponentInChildren<Slider>();
                slider.maxValue = project.getAllRequireQuality();
                slider.value = project.getAllActualQuality();
            }else{
                projectOldText[2].text = GetAgileProjectQuality(project) + "/" + GetAgileProjectRequireQuality(project);
                Slider slider = projectOldItem[i].GetComponentInChildren<Slider>();
                slider.maxValue = GetAgileProjectRequireQuality(project);
                slider.value = GetAgileProjectQuality(project);
            }

            int index = i;
                projectOldItem[i].GetComponent<Button>().onClick.AddListener(() => ShowOldProjectDetail(index));
        }
    }

    private void OnDisable() {
        foreach (Transform child in content.transform) {
            GameObject.Destroy(child.gameObject);
        }
    }

    void ShowOldProjectDetail(int index)
    {
        content.transform.parent.gameObject.SetActive(false);
        if(ProjectManager.instance.oldProject[index].model.modelName == "Waterfall"){
            ProjectSummary.instance.ViewOldProjectSummary(ProjectManager.instance.oldProject[index]);
        }else{
            agileSummary.SetActive(true);
            AgileSummary.instance.ShowOldProject(index);
        }

        // TextMeshProUGUI[] texts = projectOldDetail.GetComponentsInChildren<TextMeshProUGUI>();
        // Project project =  ProjectManager.instance.oldProject[index];
        // texts[0].text =  ProjectManager.instance.oldProject[index].pjName;
        // texts[1].text =  "โมเดลการทำงาน: " + project.model.modelName;
        // texts[2].text =  "ระยะเวลาที่กำหนด: " + project.deadline + " วัน";
        // texts[3].text =  "ระยะเวลาที่ใช้ไป: " + project.dayUsed + " วัน";
        // texts[5].text =  "คะแนนโดยรวม: " + project.getAllActualQuality() + "/" + project.getAllRequireQuality();
        // texts[6].text =  "Analysis: " + project.actualAnalysis + "/" + project.requireAnalysis;
        // texts[7].text =  "Design: " + project.actualDesign + "/" + project.requireDesign;
        // texts[8].text =  "Coding: " + project.actualCoding + "/" + project.requireCoding;
        // texts[9].text =  "Testing: " + project.actualTesting + "/" + project.requireTesting;
        // texts[10].text =  "Deploying: " + project.actualDeployment + "/" + project.actualDeployment;
        // texts[11].text =  "เงินตอบแทน: " + project.reward + " บาท";
        // texts[12].text =  "ค่าใช้จ่าย: " + 5000 + " บาท";
    }

    int GetAgileProjectQuality(OldProject project){
        int sum = 0;
        foreach(var sprint in project.sprintList){
            sum += sprint.GetSumQuality();            
        }
        return sum;
    }

    int GetAgileProjectRequireQuality(OldProject project){
        int sum = 0;
        foreach(var sprint in project.sprintList){
            sum += sprint.GetSumRequireQuality();            
        }
        return sum;
    }
}
