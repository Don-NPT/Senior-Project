using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class ProjectHUD : MonoBehaviour
{
    public static ProjectHUD instance;
    GameManager gameManager = GameManager.instance;
    GameObject[] projectHudItem;
    public GameObject uiPrefab;
    public Transform parent;
    public GameObject hudDetail;
    public GameObject hudDetailItem;
    public int projectIndex;
    // Start is called before the first frame update
    void Start()
    {
        if (instance != null && instance != this) 
            Destroy(this); 
        else 
            instance = this;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(hudDetailItem != null)
        {
            // hudDetailItem.GetComponentInChildren<Slider>().value = ProjectManager.instance.currentProjects[projectIndex].currentPoints;
            hudDetailItem.GetComponentsInChildren<Slider>()[0].DOValue(DevelopmentManager.instance.currentDayInPhase, 0.3f).Play();
            hudDetailItem.GetComponentsInChildren<TextMeshProUGUI>()[1].text = "ขั้นตอน: " + ProjectManager.instance.currentProjects[projectIndex].phase.ToString();
            hudDetailItem.GetComponentsInChildren<TextMeshProUGUI>()[3].text = "เหลือเวลา: " + (ProjectManager.instance.currentProjects[projectIndex].deadline - DevelopmentManager.instance.currentDayUsed);

            if(ProjectManager.instance.currentProjects[projectIndex].phase == Project.Phases.ANALYSIS)
                hudDetailItem.GetComponentsInChildren<Slider>()[1].DOValue(DevelopmentManager.instance.currentQuality,0.3f).Play();
            if(ProjectManager.instance.currentProjects[projectIndex].phase == Project.Phases.DESIGN)
                hudDetailItem.GetComponentsInChildren<Slider>()[2].DOValue(DevelopmentManager.instance.currentQuality,0.3f).Play();
            if(ProjectManager.instance.currentProjects[projectIndex].phase == Project.Phases.CODING)
                hudDetailItem.GetComponentsInChildren<Slider>()[3].DOValue(DevelopmentManager.instance.currentQuality,0.3f).Play();
            if(ProjectManager.instance.currentProjects[projectIndex].phase == Project.Phases.TESTING)
                hudDetailItem.GetComponentsInChildren<Slider>()[4].DOValue(DevelopmentManager.instance.currentQuality,0.3f).Play();
            if(ProjectManager.instance.currentProjects[projectIndex].phase == Project.Phases.DEPLOYMENT)
                hudDetailItem.GetComponentsInChildren<Slider>()[5].DOValue(DevelopmentManager.instance.currentQuality,0.3f).Play();
        }
    }

    public void UpdateList() {
        if(ProjectManager.instance.getNumProject() > 0)
        {
            DestroyAllItem();
            projectHudItem = new GameObject[ProjectManager.instance.getNumProject()];
            for(int i=0; i<ProjectManager.instance.getNumProject(); i++)
            {
                // Setup project hud tab
                projectHudItem[i] = (GameObject)Instantiate(uiPrefab);
                projectHudItem[i].transform.SetParent(parent);
                projectHudItem[i].transform.localScale = new Vector3(1, 1, 1);
                int index = i;
                projectHudItem[i].GetComponent<Button>().onClick.AddListener(() => {ShowDetail(index);});

                // Setup text info
                projectHudItem[i].GetComponentsInChildren<TextMeshProUGUI>()[0].text = ProjectManager.instance.currentProjects[i].pjName;
                projectHudItem[i].GetComponentsInChildren<TextMeshProUGUI>()[1].text = ProjectManager.instance.currentProjects[i].state.ToString();
            }
        }
    }

    public void ShowDetail(int index)
    {
        if(hudDetailItem == null){
            projectIndex = index;
            // Setup project detail hud
            hudDetailItem = (GameObject)Instantiate(hudDetail);
            hudDetailItem.transform.SetParent(parent);
            hudDetailItem.transform.SetSiblingIndex(index+1);
            hudDetailItem.transform.localScale = new Vector3(1, 1, 1);

            // setup text info
            hudDetailItem.GetComponentsInChildren<TextMeshProUGUI>()[0].text = ProjectManager.instance.currentProjects[index].model.modelName;
            hudDetailItem.GetComponentsInChildren<TextMeshProUGUI>()[1].text = ProjectManager.instance.currentProjects[index].phase.ToString();
            hudDetailItem.GetComponentsInChildren<Slider>()[0].maxValue = DevelopmentManager.instance.DayEachPhase;
            hudDetailItem.GetComponentsInChildren<Slider>()[1].value = 0;
            hudDetailItem.GetComponentsInChildren<Slider>()[2].value = 0;
            hudDetailItem.GetComponentsInChildren<Slider>()[3].value = 0;
            hudDetailItem.GetComponentsInChildren<Slider>()[4].value = 0;
            hudDetailItem.GetComponentsInChildren<Slider>()[5].value = 0;
            hudDetailItem.GetComponentsInChildren<Slider>()[1].maxValue = 50;
            hudDetailItem.GetComponentsInChildren<Slider>()[2].maxValue = 50;
            hudDetailItem.GetComponentsInChildren<Slider>()[3].maxValue = 50;
            hudDetailItem.GetComponentsInChildren<Slider>()[4].maxValue = 50;
            hudDetailItem.GetComponentsInChildren<Slider>()[5].maxValue = 50;
            return;
        }
        Destroy(hudDetailItem);
    }

    // public void UpdateSlider(int index, int phaseQuality, int days)
    // {
    //     if(hudDetailItem != null){
    //         hudDetailItem.GetComponentsInChildren<Slider>()[index+1].DOValue(phaseQuality, days);
    //         // hudDetailItem.GetComponentsInChildren<Slider>()[1].value = 5;
    //         // hudDetailItem.GetComponentsInChildren<Slider>()[index+1].value = phaseQuality;
    //     }
    // }

    void DestroyAllItem()
    {
        if(projectHudItem != null){
            foreach(GameObject child in projectHudItem)
            {
                Destroy(child);
            }
        }
        if(hudDetailItem != null){
            Destroy(hudDetailItem);
        } 
    }

}
