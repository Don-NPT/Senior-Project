using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SprintRetrospective : MonoBehaviour
{
    public GameObject[] taskRows;
    public TextMeshProUGUI thisSprint;
    public TextMeshProUGUI allSprint;
    private Project project;
    private void OnEnable() {
        project = ProjectManager.instance.currentProject;
        int sprintIndex = AgileManager.instance.sprintIndex;

        GetComponentsInChildren<TextMeshProUGUI>()[0].text = "Sprint " + (sprintIndex+1) + " Review";

        List<KitchenObjectSO> smallTasks = new List<KitchenObjectSO>();
        List<KitchenObjectSO> mediumTasks = new List<KitchenObjectSO>();
        List<KitchenObjectSO> largeTasks = new List<KitchenObjectSO>();
        List<KitchenObjectSO> giantTasks = new List<KitchenObjectSO>();

        int[] totalDayUsed = new int[4];

        foreach(var task in project.sprintList[sprintIndex].taskList){
            if(task.objectName == "งานขนาดเล็ก"){
                smallTasks.Add(task);
                totalDayUsed[0] += task.dayUsed;
            }
            else if(task.objectName == "งานขนาดกลาง"){
                mediumTasks.Add(task);
                totalDayUsed[1] += task.dayUsed;
            }
            else if(task.objectName == "งานขนาดใหญ่"){
                largeTasks.Add(task);
                totalDayUsed[2] += task.dayUsed;
            }
            else if(task.objectName == "งานแบบเบิ้มๆ"){
                giantTasks.Add(task);
                totalDayUsed[3] += task.dayUsed;
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
        if(smallTasks.Count > 0) taskRows[0].GetComponentsInChildren<TextMeshProUGUI>()[1].text = smallTasks[0].dayUsed + " วัน";
        if(mediumTasks.Count > 0) taskRows[1].GetComponentsInChildren<TextMeshProUGUI>()[1].text = mediumTasks[0].dayUsed + " วัน";
        if(largeTasks.Count > 0) taskRows[2].GetComponentsInChildren<TextMeshProUGUI>()[1].text = largeTasks[0].dayUsed + " วัน";
        if(giantTasks.Count > 0) taskRows[3].GetComponentsInChildren<TextMeshProUGUI>()[1].text = giantTasks[0].dayUsed + " วัน";

        // Setup Total Quality
        if(smallTasks.Count > 0) taskRows[0].GetComponentsInChildren<TextMeshProUGUI>()[2].text = totalDayUsed[0] + " วัน";
        if(mediumTasks.Count > 0) taskRows[1].GetComponentsInChildren<TextMeshProUGUI>()[2].text = totalDayUsed[1] + " วัน";
        if(largeTasks.Count > 0) taskRows[2].GetComponentsInChildren<TextMeshProUGUI>()[2].text = totalDayUsed[2] + " วัน";
        if(giantTasks.Count > 0) taskRows[3].GetComponentsInChildren<TextMeshProUGUI>()[2].text = totalDayUsed[3] + " วัน";

        // Hide row if no tasks
        if(smallTasks.Count == 0) taskRows[0].SetActive(false);
        if(mediumTasks.Count == 0) taskRows[1].SetActive(false);
        if(largeTasks.Count == 0) taskRows[2].SetActive(false);
        if(giantTasks.Count == 0) taskRows[3].SetActive(false);

        int sumComplete = 0;
        foreach(var task in project.sprintList[sprintIndex].taskList){
            if(task.isComplete) sumComplete++;
        }
        thisSprint.text = sumComplete + " / " + project.sprintList[sprintIndex].taskList.Count;

        sumComplete = 0;
        for(int i=sprintIndex; i>=0; i--){
            foreach(var task in project.sprintList[i].taskList){
                if(task.isComplete) sumComplete++;
            }
        }

        int sumTasks = 0;
        foreach(var sprint in project.sprintList){
            foreach(var task in sprint.taskList){
                sumTasks++;
            }
        }

        allSprint.text = sumComplete + " / " + sumTasks;
    }
}
