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
    public GameObject agileHudTab;
    public GameObject agileHudDetail;
    GameObject submitBTN;
    int dayUsed;
    Coroutine timer;
    // Start is called before the first frame update
    void Awake()
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
            WaterfallHUDUpdate();
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
            dayUsed = 0;
            timer = StartCoroutine(StartTimer());
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
            hudDetail.GetComponentsInChildren<Slider>()[1].maxValue = ProjectManager.instance.currentProject.requireAnalysis;
            hudDetail.GetComponentsInChildren<Slider>()[2].maxValue = ProjectManager.instance.currentProject.requireDesign;
            hudDetail.GetComponentsInChildren<Slider>()[3].maxValue = ProjectManager.instance.currentProject.requireCoding;
            hudDetail.GetComponentsInChildren<Slider>()[4].maxValue = ProjectManager.instance.currentProject.requireTesting;
            hudDetail.GetComponentsInChildren<Slider>()[5].maxValue = ProjectManager.instance.currentProject.requireDeployment;
            return;
        }else{
            hudDetail.SetActive(false);
        }
    }

    IEnumerator StartTimer(){
        // for(int i=0; i<ProjectManager.instance.currentProject.deadline; i++){
        //     yield return new WaitForSeconds(1);
        //     dayUsed += 1;
        // }
        while(true){
            yield return new WaitForSeconds(1);
            dayUsed += 1;
        }
    }

    public void ShowFinishBTN(Project project)
    {
        if(hudDetail.activeSelf){
            submitBTN.SetActive(true);
            submitBTN.GetComponent<Button>().onClick.RemoveAllListeners();
            submitBTN.GetComponent<Button>().onClick.AddListener(delegate {
                Debug.Log("Submit");
                GameManager.instance.AddMoney(project.reward);
                GameManager.instance.LevelUp();
                SkillManager.instance.AddSkillPoint(project.skillPointReward);
                GameManager.instance.Play();
                StopCoroutine(timer);
                ProjectSummary.instance.ViewProjectSummary(ProjectManager.instance.oldProject[ProjectManager.instance.oldProject.Count-1]);
                HideHUD();
            });
        }
    }

    void HideHUD()
    {
        uiPrefab.SetActive(false);
        hudDetail.SetActive(false);
    }

    public void WaterfallHUDUpdate()
    {
        // hudDetail.GetComponentsInChildren<Slider>()[0].maxValue = WaterFallManager.instance.currentWorkAmount;
        // hudDetail.GetComponentsInChildren<Slider>()[0].DOValue(WaterFallManager.instance.progress, 0.3f).Play();
        if(WaterFallManager.instance.phaseIndex == 0 && FindObjectOfType<Requirement2_new>() != null){
            hudDetail.GetComponentsInChildren<Slider>()[0].maxValue = ProjectManager.instance.currentProject.requirement2.Length;
            hudDetail.GetComponentsInChildren<Slider>()[0].DOValue(FindObjectOfType<Requirement2_new>().index, 0.3f).Play();
        }
        if(WaterFallManager.instance.phaseIndex == 1 && FindObjectOfType<KeyInput>() != null){
            hudDetail.GetComponentsInChildren<Slider>()[0].maxValue = ProjectManager.instance.currentProject.keyInput.Length;
            hudDetail.GetComponentsInChildren<Slider>()[0].DOValue(FindObjectOfType<KeyInput>().index, 0.3f).Play();
        }
        if(WaterFallManager.instance.phaseIndex == 2){
            hudDetail.GetComponentsInChildren<Slider>()[0].maxValue = 5;
            hudDetail.GetComponentsInChildren<Slider>()[0].DOValue(ProjectManager.instance.currentProject.balloonAnswer.Count, 0.3f).Play();
        }
        if(WaterFallManager.instance.phaseIndex == 3){
            hudDetail.GetComponentsInChildren<Slider>()[0].maxValue = 5;
            hudDetail.GetComponentsInChildren<Slider>()[0].DOValue(ProjectManager.instance.currentProject.balloon2Answer.Count, 0.3f).Play();
        }
        if(WaterFallManager.instance.phaseIndex == 4){
            hudDetail.GetComponentsInChildren<Slider>()[0].maxValue = WaterFallManager.instance.currentWorkAmount;
            hudDetail.GetComponentsInChildren<Slider>()[0].DOValue( WaterFallManager.instance.progress, 0.3f).Play();
        }
        
        hudDetail.GetComponentsInChildren<TextMeshProUGUI>()[1].text = "ขั้นตอน: " + ProjectManager.instance.currentProject.phase.ToString();
        // hudDetail.GetComponentsInChildren<TextMeshProUGUI>()[8].text = "เหลือเวลา: " + (ProjectManager.instance.currentProject.deadline - DevelopmentManager.instance.currentDayUsed);

        hudDetail.GetComponentsInChildren<TextMeshProUGUI>()[3].text = (int)WaterFallManager.instance.qualityEachPhase[0] + "/" + ProjectManager.instance.currentProject.requireAnalysis;
        hudDetail.GetComponentsInChildren<Slider>()[1].DOValue(WaterFallManager.instance.qualityEachPhase[0],0.3f).Play();

        hudDetail.GetComponentsInChildren<TextMeshProUGUI>()[4].text = (int)WaterFallManager.instance.qualityEachPhase[1] + "/" + ProjectManager.instance.currentProject.requireDesign;
        hudDetail.GetComponentsInChildren<Slider>()[2].DOValue(WaterFallManager.instance.qualityEachPhase[1],0.3f).Play();

        hudDetail.GetComponentsInChildren<TextMeshProUGUI>()[5].text = (int)WaterFallManager.instance.qualityEachPhase[2] + "/" + ProjectManager.instance.currentProject.requireCoding;
        hudDetail.GetComponentsInChildren<Slider>()[3].DOValue(WaterFallManager.instance.qualityEachPhase[2],0.3f).Play();

        hudDetail.GetComponentsInChildren<TextMeshProUGUI>()[6].text = (int)WaterFallManager.instance.qualityEachPhase[3] + "/" + ProjectManager.instance.currentProject.requireTesting;
        hudDetail.GetComponentsInChildren<Slider>()[4].DOValue(WaterFallManager.instance.qualityEachPhase[3],0.3f).Play();

        hudDetail.GetComponentsInChildren<TextMeshProUGUI>()[7].text = (int)WaterFallManager.instance.qualityEachPhase[4] + "/" + ProjectManager.instance.currentProject.requireDeployment;
        hudDetail.GetComponentsInChildren<Slider>()[5].DOValue(WaterFallManager.instance.qualityEachPhase[4],0.3f).Play();

        hudDetail.GetComponentsInChildren<TextMeshProUGUI>()[8].text = "เหลือเวลา: " + (ProjectManager.instance.currentProject.deadline - dayUsed);
        if((ProjectManager.instance.currentProject.deadline - dayUsed) < 0){
            hudDetail.GetComponentsInChildren<TextMeshProUGUI>()[8].color = Color.red;
        }else{
            hudDetail.GetComponentsInChildren<TextMeshProUGUI>()[8].color = Color.white;
        }
    }
}
