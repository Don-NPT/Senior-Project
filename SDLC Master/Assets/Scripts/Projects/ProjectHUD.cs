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
    public GameObject uiPrefab;
    public Transform parent;
    public GameObject hudDetail;
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
        if(hudDetail.activeSelf && ProjectManager.instance.currentProject != null)
        {
            hudDetail.GetComponentsInChildren<Slider>()[0].maxValue = WaterFallManager.instance.currentWorkAmount;
            hudDetail.GetComponentsInChildren<Slider>()[0].DOValue(WaterFallManager.instance.progress, 0.3f).Play();
            hudDetail.GetComponentsInChildren<TextMeshProUGUI>()[1].text = "ขั้นตอน: " + ProjectManager.instance.currentProject.phase.ToString();
            // hudDetail.GetComponentsInChildren<TextMeshProUGUI>()[8].text = "เหลือเวลา: " + (ProjectManager.instance.currentProject.deadline - DevelopmentManager.instance.currentDayUsed);

            // if(ProjectManager.instance.currentProjects[projectIndex].phase == Project.Phases.ANALYSIS)
                hudDetail.GetComponentsInChildren<TextMeshProUGUI>()[3].text = WaterFallManager.instance.qualityEachPhase[0] + "/" + ProjectManager.instance.currentProject.analysisWork;
                hudDetail.GetComponentsInChildren<Slider>()[1].DOValue(WaterFallManager.instance.qualityEachPhase[0],0.3f).Play();
            // if(ProjectManager.instance.currentProjects[projectIndex].phase == Project.Phases.DESIGN)
                hudDetail.GetComponentsInChildren<TextMeshProUGUI>()[4].text = WaterFallManager.instance.qualityEachPhase[1] + "/" + ProjectManager.instance.currentProject.designWork;
                hudDetail.GetComponentsInChildren<Slider>()[2].DOValue(WaterFallManager.instance.qualityEachPhase[1],0.3f).Play();
            // if(ProjectManager.instance.currentProjects[projectIndex].phase == Project.Phases.CODING)
                hudDetail.GetComponentsInChildren<TextMeshProUGUI>()[5].text = WaterFallManager.instance.qualityEachPhase[2] + "/" + ProjectManager.instance.currentProject.codingWork;
                hudDetail.GetComponentsInChildren<Slider>()[3].DOValue(WaterFallManager.instance.qualityEachPhase[2],0.3f).Play();
            // if(ProjectManager.instance.currentProjects[projectIndex].phase == Project.Phases.TESTING)
                hudDetail.GetComponentsInChildren<TextMeshProUGUI>()[6].text = WaterFallManager.instance.qualityEachPhase[3] + "/" + ProjectManager.instance.currentProject.testingWork;
                hudDetail.GetComponentsInChildren<Slider>()[4].DOValue(WaterFallManager.instance.qualityEachPhase[3],0.3f).Play();
            // if(ProjectManager.instance.currentProjects[projectIndex].phase == Project.Phases.DEPLOYMENT)
                hudDetail.GetComponentsInChildren<TextMeshProUGUI>()[7].text = WaterFallManager.instance.qualityEachPhase[4] + "/" + ProjectManager.instance.currentProject.deploymentWork;
                hudDetail.GetComponentsInChildren<Slider>()[5].DOValue(WaterFallManager.instance.qualityEachPhase[4],0.3f).Play();
        }
    }

    public void UpdateList() {
        if(ProjectManager.instance.currentProject != null)
        {
            // Setup project hud tab
            uiPrefab.SetActive(true);
            // uiPrefab.GetComponent<Button>().onClick.AddListener(() => {ShowDetail();});

            // Setup text info
            uiPrefab.GetComponentsInChildren<TextMeshProUGUI>()[0].text = ProjectManager.instance.currentProject.pjName;
            uiPrefab.GetComponentsInChildren<TextMeshProUGUI>()[1].text = ProjectManager.instance.currentProject.state.ToString();
            
            hudDetail.SetActive(false);
            ShowDetail();
        }
    }

    public void ShowDetail()
    {
        if(hudDetail.activeSelf == false){
            // Setup project detail hud
            hudDetail.SetActive(true);

            submitBTN = hudDetail.transform.GetChild(5).gameObject;
            submitBTN.SetActive(false);

            // setup text info
            hudDetail.GetComponentsInChildren<TextMeshProUGUI>()[0].text = ProjectManager.instance.currentProject.model.modelName;
            hudDetail.GetComponentsInChildren<TextMeshProUGUI>()[1].text = ProjectManager.instance.currentProject.phase.ToString();
            hudDetail.GetComponentsInChildren<Slider>()[1].value = 0;
            hudDetail.GetComponentsInChildren<Slider>()[2].value = 0;
            hudDetail.GetComponentsInChildren<Slider>()[3].value = 0;
            hudDetail.GetComponentsInChildren<Slider>()[4].value = 0;
            hudDetail.GetComponentsInChildren<Slider>()[5].value = 0;
            hudDetail.GetComponentsInChildren<Slider>()[1].maxValue = ProjectManager.instance.currentProject.analysisWork;
            hudDetail.GetComponentsInChildren<Slider>()[2].maxValue = ProjectManager.instance.currentProject.designWork;
            hudDetail.GetComponentsInChildren<Slider>()[3].maxValue = ProjectManager.instance.currentProject.codingWork;
            hudDetail.GetComponentsInChildren<Slider>()[4].maxValue = ProjectManager.instance.currentProject.testingWork;
            hudDetail.GetComponentsInChildren<Slider>()[5].maxValue = ProjectManager.instance.currentProject.deploymentWork;
            return;
        }else{
            hudDetail.SetActive(false);
        }
    }

    public void ShowFinishBTN(Project project)
    {
        if(hudDetail.activeSelf){
            submitBTN.SetActive(true);
            submitBTN.GetComponent<Button>().onClick.AddListener(delegate {
                Debug.Log("Submit");
                GameManager.instance.AddMoney(project.reward);
                GameManager.instance.Play();
                ProjectManager.instance.ViewProjectSummary();
                HideHUD();
            });
        }
    }

    void HideHUD()
    {
        uiPrefab.SetActive(false);
        hudDetail.SetActive(false);
    }
}
