using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class SprintRetrospective : MonoBehaviour
{
    public GameObject[] taskRows;
    public TextMeshProUGUI thisSprint;
    public TextMeshProUGUI allSprint;
    public GameObject[] postItBoxes;
    public GameObject postItPrefab;
    public string[] randomMad;
    public string[] randomSad;
    public string[] randomGlad;
    public string[] TooMuchTaskFeedback;
    public string[] OkTaskFeedback;
    public string[] goodQualityFeedbacks;
    public string[] lowQualityFeedbacks;
    private Project project;
    private int sprintIndex;
    private void OnEnable() {
        project = ProjectManager.instance.currentProject;
        sprintIndex = AgileManager.instance.sprintIndex;

        SetupLeft();
        SetupRight();
    }

    void SetupLeft(){
        GetComponentsInChildren<TextMeshProUGUI>()[0].text = "Sprint " + (sprintIndex+1) + " Retrospective";

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

    void SetupRight(){
        int numMad = UnityEngine.Random.Range(1, 4);
        int numSad = UnityEngine.Random.Range(1, 4);
        int numGlad = UnityEngine.Random.Range(1, 2);

        if(IsTaskOverload()){
            GameObject taskOverload = (GameObject) Instantiate(postItPrefab);
            taskOverload.transform.SetParent(postItBoxes[0].transform);
            taskOverload.transform.localScale = Vector3.one;
            taskOverload.GetComponentInChildren<TextMeshProUGUI>().text = TooMuchTaskFeedback[UnityEngine.Random.Range(0, TooMuchTaskFeedback.Length)];
        }else{
            GameObject taskok = (GameObject) Instantiate(postItPrefab);
            taskok.transform.SetParent(postItBoxes[2].transform);
            taskok.transform.localScale = Vector3.one;
            taskok.GetComponentInChildren<TextMeshProUGUI>().text = OkTaskFeedback[UnityEngine.Random.Range(0, OkTaskFeedback.Length)];
        }

        if(IsTooLowQuality()){
            GameObject lowQuality = (GameObject) Instantiate(postItPrefab);
            lowQuality.transform.SetParent(postItBoxes[1].transform);
            lowQuality.transform.localScale = Vector3.one;
            lowQuality.GetComponentInChildren<TextMeshProUGUI>().text = lowQualityFeedbacks[UnityEngine.Random.Range(0, lowQualityFeedbacks.Length)];
        }
        else{
            GameObject okQuality = (GameObject) Instantiate(postItPrefab);
            okQuality.transform.SetParent(postItBoxes[2].transform);
            okQuality.transform.localScale = Vector3.one;
            okQuality.GetComponentInChildren<TextMeshProUGUI>().text = goodQualityFeedbacks[UnityEngine.Random.Range(0, goodQualityFeedbacks.Length)];
        }

        List<string> madList = randomMad.ToList();
        Debug.Log("madlist = "+madList.Count);
        for(int i=0; i<numMad; i++){
            GameObject postIt = (GameObject) Instantiate(postItPrefab);
            postIt.transform.SetParent(postItBoxes[0].transform);
            postIt.transform.localScale = Vector3.one;
            int index = UnityEngine.Random.Range(0, madList.Count);
            Debug.Log("indexmad = "+index);
            postIt.GetComponentInChildren<TextMeshProUGUI>().text = madList[index];
            if (madList.Count > 0) {
                madList.RemoveAt(index);
            }
        }

        List<string> sadList = randomSad.ToList();
        Debug.Log("sadlist = "+sadList.Count);
        for(int i=0; i<numSad; i++){
            GameObject postIt = (GameObject) Instantiate(postItPrefab);
            postIt.transform.SetParent(postItBoxes[1].transform);
            postIt.transform.localScale = Vector3.one;
            int index = UnityEngine.Random.Range(0, sadList.Count);
            Debug.Log("indexsad = "+index);
            postIt.GetComponentInChildren<TextMeshProUGUI>().text = sadList[index];
            if (sadList.Count > 0) {
                sadList.RemoveAt(index);
            }
        }

        List<string> gladList = randomGlad.ToList();
        Debug.Log("gladlist = "+gladList.Count);
        for(int i=0; i<numGlad; i++){
            GameObject postIt = (GameObject) Instantiate(postItPrefab);
            postIt.transform.SetParent(postItBoxes[2].transform);
            postIt.transform.localScale = Vector3.one;
            int index = UnityEngine.Random.Range(0, gladList.Count);
            Debug.Log("indexglad = "+index);
            postIt.GetComponentInChildren<TextMeshProUGUI>().text = gladList[index];
            if (gladList.Count > 0) {
                gladList.RemoveAt(index);
            }
        }
    }

    private void OnDisable() {
        foreach(Transform item in postItBoxes[0].transform){
            Destroy(item.gameObject);
        }
        foreach(Transform item in postItBoxes[1].transform){
            Destroy(item.gameObject);
        }
        foreach(Transform item in postItBoxes[2].transform){
            Destroy(item.gameObject);
        }
    }

    bool IsTaskOverload(){
        foreach(var task in project.sprintList[sprintIndex].taskList){
            if(task.isComplete == false) return true;
        }
        return false;
    }

    bool IsTooLowQuality(){
        if(GetProjectQuality() < GetProjectRequireQuality()) return true;
        else return false;
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

}
