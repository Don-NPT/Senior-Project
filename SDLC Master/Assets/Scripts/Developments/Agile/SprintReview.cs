using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SprintReview : MonoBehaviour
{
    public GameObject[] taskRows;
    public TextMeshProUGUI feedback;
    private Project project;
    private string[] feedbackStrings;
    private void OnEnable() {
        project = ProjectManager.instance.currentProject;
        int sprintIndex = AgileManager.instance.sprintIndex;

        GetComponentsInChildren<TextMeshProUGUI>()[0].text = "Sprint " + (sprintIndex+1) + " Review";

        List<KitchenObjectSO> smallTasks = new List<KitchenObjectSO>();
        List<KitchenObjectSO> mediumTasks = new List<KitchenObjectSO>();
        List<KitchenObjectSO> largeTasks = new List<KitchenObjectSO>();
        List<KitchenObjectSO> giantTasks = new List<KitchenObjectSO>();

        int[] totalQualities = new int[4];

        foreach(var task in project.sprintList[sprintIndex].taskList){
            if(task.objectName == "งานขนาดเล็ก"){
                smallTasks.Add(task);
                totalQualities[0] += task.quality;
            }
            else if(task.objectName == "งานขนาดกลาง"){
                mediumTasks.Add(task);
                totalQualities[1] += task.quality;
            }
            else if(task.objectName == "งานขนาดใหญ่"){
                largeTasks.Add(task);
                totalQualities[2] += task.quality;
            }
            else if(task.objectName == "งานแบบเบิ้มๆ"){
                giantTasks.Add(task);
                totalQualities[3] += task.quality;
            }
        }

        // Showw all rows
        taskRows[0].SetActive(true);
        taskRows[1].SetActive(true);
        taskRows[2].SetActive(true);
        taskRows[3].SetActive(true);

        // Setup Quantity
        taskRows[0].GetComponentsInChildren<TextMeshProUGUI>()[0].text = "งานขนาดเล็ก (x" + smallTasks.Count + ")";
        taskRows[1].GetComponentsInChildren<TextMeshProUGUI>()[0].text = "งานขนาดกลาง (x" + mediumTasks.Count + ")";
        taskRows[2].GetComponentsInChildren<TextMeshProUGUI>()[0].text = "งานขนาดใหญ่ (x" + largeTasks.Count + ")";
        taskRows[3].GetComponentsInChildren<TextMeshProUGUI>()[0].text = "งานแบบเบิ้มๆ (x" + giantTasks.Count + ")";

        // Setup Each Quality
        if(smallTasks.Count > 0) taskRows[0].GetComponentsInChildren<TextMeshProUGUI>()[1].text = GetTaskQuality(smallTasks) + " / " + smallTasks[0].requireQuality;
        if(mediumTasks.Count > 0) taskRows[1].GetComponentsInChildren<TextMeshProUGUI>()[1].text = GetTaskQuality(mediumTasks) + " / " + mediumTasks[0].requireQuality;
        if(largeTasks.Count > 0) taskRows[2].GetComponentsInChildren<TextMeshProUGUI>()[1].text = GetTaskQuality(largeTasks) + " / " + largeTasks[0].requireQuality;
        if(giantTasks.Count > 0) taskRows[3].GetComponentsInChildren<TextMeshProUGUI>()[1].text = GetTaskQuality(giantTasks) + " / " + giantTasks[0].requireQuality;

        // Setup Total Quality
        if(smallTasks.Count > 0) taskRows[0].GetComponentsInChildren<TextMeshProUGUI>()[2].text = totalQualities[0] + " / " + (smallTasks[0].requireQuality * smallTasks.Count);
        if(mediumTasks.Count > 0) taskRows[1].GetComponentsInChildren<TextMeshProUGUI>()[2].text = totalQualities[1] + " / " + (mediumTasks[0].requireQuality * mediumTasks.Count);
        if(largeTasks.Count > 0) taskRows[2].GetComponentsInChildren<TextMeshProUGUI>()[2].text = totalQualities[2] + " / " + (largeTasks[0].requireQuality * largeTasks.Count);
        if(giantTasks.Count > 0) taskRows[3].GetComponentsInChildren<TextMeshProUGUI>()[2].text = totalQualities[3] + " / " + (giantTasks[0].requireQuality * giantTasks.Count);

        // Hide row if no tasks
        if(smallTasks.Count == 0) taskRows[0].SetActive(false);
        if(mediumTasks.Count == 0) taskRows[1].SetActive(false);
        if(largeTasks.Count == 0) taskRows[2].SetActive(false);
        if(giantTasks.Count == 0) taskRows[3].SetActive(false);
    
        int sumActual = project.sprintList[sprintIndex].GetSumQuality();
        int sumRequire = project.sprintList[sprintIndex].GetSumRequireQuality();

        float diff = (float)sumActual/sumRequire;
        Debug.Log(sumActual);
        Debug.Log(sumRequire);
        Debug.Log(diff);

        if(diff >= 1){
            feedback.text = "สุดยอดมากคร้าบ สาธุ";
        }
        else if(diff >= 0.7){
            feedback.text = "ทำดีมาก ทำต่อไปนะคร้าบ";
        }
        else if(diff >= 0.5){
            feedback.text = "ใช้ได้ ไม่เลวทีเดียว";
        }
        else if(diff >= 0.3){
            feedback.text = "เห...ทำได้แค่นี้หรอ";
        }
        else{
            feedback.text = "เลือกงานเมื่อพร้อมนะครับ\nพนักงานคุณดูไม่พร้อมเลย";
        }
        
    }

    int GetTaskQuality(List<KitchenObjectSO> tasks){
        int maxQuality = 0;
        foreach(var task in tasks){
            if(task.quality != 0 && task.quality > maxQuality) maxQuality = task.quality;
        }
        return maxQuality;
    }
}
