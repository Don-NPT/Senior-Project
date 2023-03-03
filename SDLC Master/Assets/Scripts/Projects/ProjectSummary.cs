using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProjectSummary : MonoBehaviour
{
    public static ProjectSummary instance;

    public GameObject projectSummaryUI;
    public GameObject detailUI;

    [Header("Project Info")]
    public TextMeshProUGUI projectName;
    public TextMeshProUGUI overallQuality;
    public TextMeshProUGUI model;

    [Header("Phase Quality")]
    public GameObject analysisQuality;
    public GameObject designQuality;
    public GameObject codingQuality;
    public GameObject testingQuality;
    public GameObject deploymentQuality;
    
    [Header("Others")]
    public TextMeshProUGUI reward;
    public TextMeshProUGUI expense;
    public TextMeshProUGUI timeUsed;

    // Start is called before the first frame update
    void Start()
    {
        if (instance != null && instance != this) 
            Destroy(this); 
        else 
            instance = this;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ViewProjectSummary(Project project)
    {
        projectSummaryUI.SetActive(true);

        projectName.text = project.pjName;
        overallQuality.text = project.getAllActualQuality() + "/" + project.getAllRequireQuality();
        model.text = "รุปแบบการทำงาน: " + project.model.modelName;

        SetupQuality(project);

        analysisQuality.GetComponentInChildren<Button>().onClick.AddListener(delegate { ShowDetail(project, 1); });
        designQuality.GetComponentInChildren<Button>().onClick.AddListener(delegate { ShowDetail(project, 2); });
        codingQuality.GetComponentInChildren<Button>().onClick.AddListener(delegate { ShowDetail(project, 3); });
        testingQuality.GetComponentInChildren<Button>().onClick.AddListener(delegate { ShowDetail(project, 4); });
        deploymentQuality.GetComponentInChildren<Button>().onClick.AddListener(delegate { ShowDetail(project, 5); });

        reward.text = project.reward.ToString();
        expense.text = project.expense.ToString();
        timeUsed.text = project.getOverallTimeUsed() + " / " + project.deadline + " วัน";
        Debug.Log(string.Join(", ", project.requirement1Answer));
        Debug.Log(string.Join(", ", project.requirement2Answer));
        Debug.Log(string.Join(", ", project.designAnswer));
        Debug.Log(project.keyInputPass);
        Debug.Log(project.balloonPoint);
    }

    void ShowDetail(Project project, int index)
    {
        if(!detailUI.activeSelf)
        {
            detailUI.transform.SetSiblingIndex(index);
            detailUI.SetActive(true);

            detailUI.GetComponentsInChildren<TextMeshProUGUI>()[1].text = project.getTimeUsed(index-1) + " วัน";
            detailUI.GetComponentsInChildren<TextMeshProUGUI>()[3].text = "??? บาท";
            detailUI.GetComponentsInChildren<TextMeshProUGUI>()[5].text = project.staffEachPhase[index-1].ToString() + " คน";
            detailUI.GetComponentsInChildren<TextMeshProUGUI>()[7].text = project.statEachPhase[index-1].ToString() + " หน่วย";

        }else{
            detailUI.SetActive(false);
        }
        
    }

    void SetupQuality(Project project)
    {
        analysisQuality.GetComponentsInChildren<TextMeshProUGUI>()[1].text = project.actualAnalysis + "/" + project.requireAnalysis;
        designQuality.GetComponentsInChildren<TextMeshProUGUI>()[1].text = project.actualDesign + "/" + project.requireDesign;
        codingQuality.GetComponentsInChildren<TextMeshProUGUI>()[1].text = project.actualCoding + "/" + project.requireCoding;
        testingQuality.GetComponentsInChildren<TextMeshProUGUI>()[1].text = project.actualTesting + "/" + project.requireTesting;
        deploymentQuality.GetComponentsInChildren<TextMeshProUGUI>()[1].text = project.actualDeployment + "/" + project.requireDeployment;

        analysisQuality.GetComponentInChildren<Slider>().maxValue = project.requireAnalysis;
        designQuality.GetComponentInChildren<Slider>().maxValue = project.requireDesign;
        codingQuality.GetComponentInChildren<Slider>().maxValue = project.requireCoding;
        testingQuality.GetComponentInChildren<Slider>().maxValue = project.requireTesting;
        deploymentQuality.GetComponentInChildren<Slider>().maxValue = project.requireDeployment;

        analysisQuality.GetComponentInChildren<Slider>().value = project.actualAnalysis;
        designQuality.GetComponentInChildren<Slider>().value = project.actualDesign;
        codingQuality.GetComponentInChildren<Slider>().value = project.actualCoding;
        testingQuality.GetComponentInChildren<Slider>().value = project.actualTesting;
        deploymentQuality.GetComponentInChildren<Slider>().value = project.actualDeployment;
    }
}
