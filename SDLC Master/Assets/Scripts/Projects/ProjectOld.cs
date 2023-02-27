using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProjectOld : MonoBehaviour
{
    public GameObject projectOldItemPrefab;
    public Transform content;
    public GameObject projectOldDetail;
    GameObject[] projectOldItem;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable() {
        projectOldItem = new GameObject[ProjectManager.instance.getNumOldProject()];

        for(int i=0; i<ProjectManager.instance.getNumOldProject(); i++)
        {
            // Spawn Old project items
            projectOldItem[i] = (GameObject)Instantiate(projectOldItemPrefab);
            projectOldItem[i].transform.SetParent(content);
            projectOldItem[i].transform.localScale = new Vector3(1, 1, 1);

            // set text for project confirm
            TextMeshProUGUI[] projectOldText = projectOldItem[i].GetComponentsInChildren<TextMeshProUGUI>();
            Project project =  ProjectManager.instance.oldProject[i];
            projectOldText[0].text = ProjectManager.instance.oldProject[i].pjName;
            projectOldText[2].text = project.getAllActualQuality() + "/" + project.getAllWorkAmount();
            Slider slider = projectOldItem[i].GetComponentInChildren<Slider>();
            slider.maxValue = project.getAllWorkAmount();
            slider.value = project.getAllActualQuality();
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
        projectOldDetail.SetActive(true);

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

    // Update is called once per frame
    void Update()
    {
        
    }
}
