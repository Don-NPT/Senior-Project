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

    private void OnEnable() {
        projectOldItem = new GameObject[ProjectManager.instance.getNumOldProject()];
  
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
