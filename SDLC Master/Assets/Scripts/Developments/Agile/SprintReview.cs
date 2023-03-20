using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SprintReview : MonoBehaviour
{
    public GameObject[] taskRows;
    public TextMeshProUGUI feedback;
    private Project project;
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
        if(smallTasks.Count > 0) taskRows[0].GetComponentsInChildren<TextMeshProUGUI>()[1].text = smallTasks[0].quality + " / " + smallTasks[0].requireQuality;
        if(mediumTasks.Count > 0) taskRows[1].GetComponentsInChildren<TextMeshProUGUI>()[1].text = mediumTasks[0].quality + " / " + mediumTasks[0].requireQuality;
        if(largeTasks.Count > 0) taskRows[2].GetComponentsInChildren<TextMeshProUGUI>()[1].text = largeTasks[0].quality + " / " + largeTasks[0].requireQuality;
        if(giantTasks.Count > 0) taskRows[3].GetComponentsInChildren<TextMeshProUGUI>()[1].text = giantTasks[0].quality + " / " + giantTasks[0].requireQuality;

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
        
        feedback.text = "Hello World";
    }
}
