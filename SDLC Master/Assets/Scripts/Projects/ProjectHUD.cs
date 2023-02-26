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
    GameObject submitBTN;
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
            hudDetailItem.GetComponentsInChildren<Slider>()[0].DOValue(DevelopmentManager.instance.currentDayInPhase, 0.3f).Play();
            hudDetailItem.GetComponentsInChildren<TextMeshProUGUI>()[1].text = "ขั้นตอน: " + ProjectManager.instance.currentProjects[projectIndex].phase.ToString();
            hudDetailItem.GetComponentsInChildren<TextMeshProUGUI>()[8].text = "เหลือเวลา: " + (ProjectManager.instance.currentProjects[projectIndex].deadline - DevelopmentManager.instance.currentDayUsed);

            // if(ProjectManager.instance.currentProjects[projectIndex].phase == Project.Phases.ANALYSIS)
                hudDetailItem.GetComponentsInChildren<TextMeshProUGUI>()[3].text = DevelopmentManager.instance.currentQualityEachPhase[0] + "/" + ProjectManager.instance.currentProjects[projectIndex].requireAnalysis;
                hudDetailItem.GetComponentsInChildren<Slider>()[1].DOValue(DevelopmentManager.instance.currentQualityEachPhase[0],0.3f).Play();
            // if(ProjectManager.instance.currentProjects[projectIndex].phase == Project.Phases.DESIGN)
                hudDetailItem.GetComponentsInChildren<TextMeshProUGUI>()[4].text = DevelopmentManager.instance.currentQualityEachPhase[1] + "/" + ProjectManager.instance.currentProjects[projectIndex].requireDesign;
                hudDetailItem.GetComponentsInChildren<Slider>()[2].DOValue(DevelopmentManager.instance.currentQualityEachPhase[1],0.3f).Play();
            // if(ProjectManager.instance.currentProjects[projectIndex].phase == Project.Phases.CODING)
                hudDetailItem.GetComponentsInChildren<TextMeshProUGUI>()[5].text = DevelopmentManager.instance.currentQualityEachPhase[2] + "/" + ProjectManager.instance.currentProjects[projectIndex].requireCoding;
                hudDetailItem.GetComponentsInChildren<Slider>()[3].DOValue(DevelopmentManager.instance.currentQualityEachPhase[2],0.3f).Play();
            // if(ProjectManager.instance.currentProjects[projectIndex].phase == Project.Phases.TESTING)
                hudDetailItem.GetComponentsInChildren<TextMeshProUGUI>()[6].text = DevelopmentManager.instance.currentQualityEachPhase[3] + "/" + ProjectManager.instance.currentProjects[projectIndex].requireTesting;
                hudDetailItem.GetComponentsInChildren<Slider>()[4].DOValue(DevelopmentManager.instance.currentQualityEachPhase[3],0.3f).Play();
            // if(ProjectManager.instance.currentProjects[projectIndex].phase == Project.Phases.DEPLOYMENT)
                hudDetailItem.GetComponentsInChildren<TextMeshProUGUI>()[7].text = DevelopmentManager.instance.currentQualityEachPhase[4] + "/" + ProjectManager.instance.currentProjects[projectIndex].requireDeployment;
                hudDetailItem.GetComponentsInChildren<Slider>()[5].DOValue(DevelopmentManager.instance.currentQualityEachPhase[4],0.3f).Play();
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
                ShowDetail(index);
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
            submitBTN = hudDetailItem.transform.GetChild(5).gameObject;
            submitBTN.SetActive(false);

            // setup text info
            hudDetailItem.GetComponentsInChildren<TextMeshProUGUI>()[0].text = ProjectManager.instance.currentProjects[index].model.modelName;
            hudDetailItem.GetComponentsInChildren<TextMeshProUGUI>()[1].text = ProjectManager.instance.currentProjects[index].phase.ToString();
            hudDetailItem.GetComponentsInChildren<Slider>()[0].maxValue = DevelopmentManager.instance.DayEachPhase;
            hudDetailItem.GetComponentsInChildren<Slider>()[1].value = 0;
            hudDetailItem.GetComponentsInChildren<Slider>()[2].value = 0;
            hudDetailItem.GetComponentsInChildren<Slider>()[3].value = 0;
            hudDetailItem.GetComponentsInChildren<Slider>()[4].value = 0;
            hudDetailItem.GetComponentsInChildren<Slider>()[5].value = 0;
            hudDetailItem.GetComponentsInChildren<Slider>()[1].maxValue = ProjectManager.instance.currentProjects[index].requireAnalysis;
            hudDetailItem.GetComponentsInChildren<Slider>()[2].maxValue = ProjectManager.instance.currentProjects[index].requireDesign;
            hudDetailItem.GetComponentsInChildren<Slider>()[3].maxValue = ProjectManager.instance.currentProjects[index].requireCoding;
            hudDetailItem.GetComponentsInChildren<Slider>()[4].maxValue = ProjectManager.instance.currentProjects[index].requireTesting;
            hudDetailItem.GetComponentsInChildren<Slider>()[5].maxValue = ProjectManager.instance.currentProjects[index].requireDeployment;
            return;
        }
        Destroy(hudDetailItem);
    }

    public void ShowFinishBTN(Project project)
    {
        if(hudDetailItem != null){
            submitBTN.SetActive(true);
            submitBTN.GetComponent<Button>().onClick.AddListener(delegate {
                Debug.Log("Submit");
                GameManager.instance.AddMoney(project.reward);
                GameManager.instance.Play();
                ProjectManager.instance.ViewProjectSummary();
                DestroyAllItem();
            });
        }
    }

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
