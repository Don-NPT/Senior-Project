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

    [Header("Answer Blocks")]
    public Transform requirement1;
    public Transform requirement2;
    public Transform design;
    public Transform keyInput;
    public Transform balloonBoom;
    public Transform balloonBoom2;
    public Color correctColor;
    public Color wrongColor;
    public GameObject requirement2_text;
    public GameObject answerButton;
    public GameObject answerRow;
    
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

    public void ViewProjectSummary(Project project)
    {
        projectSummaryUI.SetActive(true);

        projectName.text = project.pjName;
        overallQuality.text = project.getAllActualQuality() + "/" + project.getAllRequireQuality();
        model.text = "รุปแบบการทำงาน: " + project.model.modelName;

        SetupQuality(project);
        SetupAnswer(project);

        analysisQuality.GetComponentInChildren<Button>().onClick.AddListener(delegate { ShowDetail(project, 1); });
        designQuality.GetComponentInChildren<Button>().onClick.AddListener(delegate { ShowDetail(project, 2); });
        codingQuality.GetComponentInChildren<Button>().onClick.AddListener(delegate { ShowDetail(project, 3); });
        testingQuality.GetComponentInChildren<Button>().onClick.AddListener(delegate { ShowDetail(project, 4); });
        deploymentQuality.GetComponentInChildren<Button>().onClick.AddListener(delegate { ShowDetail(project, 5); });

        reward.text = "+" + project.finalReward.ToString("N0");
        expense.text = "-" + project.expense.ToString("N0");
        timeUsed.text = project.getOverallTimeUsed() + " / " + project.deadline + " วัน";
        Debug.Log(string.Join(", ", project.requirement1Answer));
        Debug.Log(string.Join(", ", project.requirement2Answer));
        // Debug.Log(string.Join(", ", project.designAnswer));
        Debug.Log(string.Join(", ", project.keyInputPass));
        Debug.Log(project.balloonPoint);
    }

    void ShowDetail(Project project, int index)
    {
        if(!detailUI.activeSelf)
        {
            detailUI.transform.SetSiblingIndex(index);
            detailUI.SetActive(true);

            detailUI.GetComponentsInChildren<TextMeshProUGUI>()[1].text = project.getTimeUsed(index-1) + " วัน";
            detailUI.GetComponentsInChildren<TextMeshProUGUI>()[3].text = project.staffEachPhase[index-1].ToString() + " คน";
            detailUI.GetComponentsInChildren<TextMeshProUGUI>()[5].text = project.statEachPhase[index-1].ToString() + " หน่วย";

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

    void SetupAnswer(Project project)
    {
        // Setup Requirement 1
        // SetupRequirement1(project);

        // Setup Requirement 2
        SetupRequirement2(project);

        // Setup KeyInput
        SetupKeyInput(project);

        // Setup BalloonBoom
        SetupBalloonBoom(project);

        // Setup BalloonBoom2
        SetupBalloonBoom2(project);
    }

    void SetupRequirement1(Project project)
    {
        List<string> requirement1Answer = project.requirement1Answer;
        for(int i=0; i<requirement1Answer.Count; i++){
            requirement1.GetComponentsInChildren<TextMeshProUGUI>()[i].text = requirement1Answer[i];
            foreach(var word in project.requirement1){
                if(requirement1Answer[i] == word.word && word.isCorrect){
                    requirement1.GetComponentsInChildren<Image>()[i+1].color = correctColor;
                }else if(requirement1Answer[i] == word.word){
                    requirement1.GetComponentsInChildren<Image>()[i+1].color = wrongColor;
                }
            }
        }
    }

    void SetupRequirement2(Project project)
    {
        List<string> requirement2Answer = project.requirement2Answer;
        GameObject[] rows = new GameObject[project.requirement2Answer.Count];

        foreach(Transform child in requirement2){
            Destroy(child.gameObject);
        }

        for(int i=0; i<requirement2Answer.Count; i++){
            rows[i] = (GameObject)Instantiate(answerRow);
            rows[i].transform.SetParent(requirement2);
            rows[i].transform.localScale = Vector3.one;
            rows[i].GetComponentsInChildren<TextMeshProUGUI>()[0].text = project.requirement2[i].word;

            rows[i].GetComponentsInChildren<TextMeshProUGUI>()[1].text = requirement2Answer[i];
            
            if(requirement2Answer[i] == "Functional" && project.requirement2[i].isCorrect){
                rows[i].GetComponentInChildren<Image>().color = correctColor;
            }
            else if(requirement2Answer[i] == "Functional" && project.requirement2[i].isCorrect == false){
                rows[i].GetComponentInChildren<Image>().color = wrongColor;
            }
            else if(requirement2Answer[i] == "Non-Functional" && project.requirement2[i].isCorrect){
                rows[i].GetComponentInChildren<Image>().color = wrongColor;
            }else if(requirement2Answer[i] == "Non-Functional" && project.requirement2[i].isCorrect == false){
                rows[i].GetComponentInChildren<Image>().color = correctColor;
            }
        }
    }

    void SetupKeyInput(Project project)
    {
        GameObject[] rows = new GameObject[project.keyInput.Length];

        foreach(Transform child in keyInput){
            Destroy(child.gameObject);
        }

        for(int i=0; i<project.keyInput.Length; i++){
            rows[i] = (GameObject)Instantiate(answerRow);
            rows[i].transform.SetParent(keyInput);
            rows[i].transform.localScale = Vector3.one;
            rows[i].GetComponentsInChildren<TextMeshProUGUI>()[0].text = project.keyInput[i].hint;

            rows[i].GetComponentsInChildren<TextMeshProUGUI>()[1].text = project.keyInput[i].word;

            if(i < project.keyInputPass.Count && project.keyInputPass[i]){
                rows[i].GetComponentInChildren<Image>().color = correctColor;
            }else{
                rows[i].GetComponentInChildren<Image>().color = wrongColor;
            }
        }
    }

    void SetupBalloonBoom(Project project)
    {
        GameObject[] balloons = new GameObject[project.balloonAnswer.Count];

        foreach(Transform child in balloonBoom){
            Destroy(child.gameObject);
        }

        for(int i=0; i<project.balloonAnswer.Count; i++){
            balloons[i] = (GameObject)Instantiate(answerButton);
            balloons[i].transform.SetParent(balloonBoom);
            balloons[i].transform.localScale = Vector3.one;
            balloons[i].GetComponentInChildren<TextMeshProUGUI>().text = project.balloonAnswer[i];

            foreach(var word in project.balloons){
                if(project.balloonAnswer[i] == word.word && word.isCorrect){
                    balloons[i].GetComponentInChildren<Image>().color = correctColor;
                }else if(project.balloonAnswer[i] == word.word){
                    balloons[i].GetComponentInChildren<Image>().color = wrongColor;
                }
            }
        }
    }

    void SetupBalloonBoom2(Project project)
    {
        GameObject[] balloons = new GameObject[project.balloon2Answer.Count];

        foreach(Transform child in balloonBoom2){
            Destroy(child.gameObject);
        }

        for(int i=0; i<project.balloon2Answer.Count; i++){
            balloons[i] = (GameObject)Instantiate(answerButton);
            balloons[i].transform.SetParent(balloonBoom2);
            balloons[i].transform.localScale = Vector3.one;
            balloons[i].GetComponentInChildren<TextMeshProUGUI>().text = project.balloon2Answer[i];

            foreach(var word in project.balloons){
                if(project.balloon2Answer[i] == word.word && word.isCorrect){
                    balloons[i].GetComponentInChildren<Image>().color = wrongColor;
                }else if(project.balloon2Answer[i] == word.word){
                    balloons[i].GetComponentInChildren<Image>().color = correctColor;
                }
            }
        }
    }

    ///////////////////////////// Old Project /////////////////////////////
    public void ViewOldProjectSummary(OldProject project)
    {
        projectSummaryUI.SetActive(true);

        projectName.text = project.pjName;
        overallQuality.text = project.getAllActualQuality() + "/" + project.getAllRequireQuality();
        model.text = "รุปแบบการทำงาน: " + project.model.modelName;

        SetupOldQuality(project);
        SetupOldAnswer(project);

        analysisQuality.GetComponentInChildren<Button>().onClick.AddListener(delegate { ShowOldDetail(project, 1); });
        designQuality.GetComponentInChildren<Button>().onClick.AddListener(delegate { ShowOldDetail(project, 2); });
        codingQuality.GetComponentInChildren<Button>().onClick.AddListener(delegate { ShowOldDetail(project, 3); });
        testingQuality.GetComponentInChildren<Button>().onClick.AddListener(delegate { ShowOldDetail(project, 4); });
        deploymentQuality.GetComponentInChildren<Button>().onClick.AddListener(delegate { ShowOldDetail(project, 5); });

        reward.text = "+" + project.finalReward.ToString("N0");
        expense.text = "-" + project.expense.ToString("N0");
        timeUsed.text = project.dayUsed + " / " + project.deadline + " วัน";
        Debug.Log(string.Join(", ", project.requirement1Answer));
        Debug.Log(string.Join(", ", project.requirement2Answer));
        // Debug.Log(string.Join(", ", project.designAnswer));
        Debug.Log(string.Join(", ", project.keyInputPass));
        Debug.Log(project.balloonPoint);
    }

    void ShowOldDetail(OldProject project, int index)
    {
        if(!detailUI.activeSelf)
        {
            detailUI.transform.SetSiblingIndex(index);
            detailUI.SetActive(true);

            detailUI.GetComponentsInChildren<TextMeshProUGUI>()[1].text = project.dayUsedEachPhase[index-1] + " วัน";
            detailUI.GetComponentsInChildren<TextMeshProUGUI>()[3].text = project.staffEachPhase[index-1].ToString() + " คน";
            detailUI.GetComponentsInChildren<TextMeshProUGUI>()[5].text = project.statEachPhase[index-1].ToString() + " หน่วย";

        }else{
            detailUI.SetActive(false);
        }
        
    }

    void SetupOldQuality(OldProject project)
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

    void SetupOldAnswer(OldProject project)
    {
        // Setup Requirement 2
        SetupOldRequirement2(project);

        // Setup KeyInput
        SetupOldKeyInput(project);

        // Setup BalloonBoom
        SetupOldBalloonBoom(project);

        // Setup BalloonBoom2
        SetupOldBalloonBoom2(project);
    }

    void SetupOldRequirement2(OldProject project)
    {
        List<string> requirement2Answer = project.requirement2Answer;
        GameObject[] rows = new GameObject[project.requirement2Answer.Count];

        foreach(Transform child in requirement2){
            Destroy(child.gameObject);
        }

        for(int i=0; i<requirement2Answer.Count; i++){
            rows[i] = (GameObject)Instantiate(answerRow);
            rows[i].transform.SetParent(requirement2);
            rows[i].transform.localScale = Vector3.one;
            rows[i].GetComponentsInChildren<TextMeshProUGUI>()[0].text = project.requirement2[i].word;

            rows[i].GetComponentsInChildren<TextMeshProUGUI>()[1].text = requirement2Answer[i];
            
            if(requirement2Answer[i] == "Functional" && project.requirement2[i].isCorrect){
                rows[i].GetComponentInChildren<Image>().color = correctColor;
            }
            else if(requirement2Answer[i] == "Functional" && project.requirement2[i].isCorrect == false){
                rows[i].GetComponentInChildren<Image>().color = wrongColor;
            }
            else if(requirement2Answer[i] == "Non-Functional" && project.requirement2[i].isCorrect){
                rows[i].GetComponentInChildren<Image>().color = wrongColor;
            }else if(requirement2Answer[i] == "Non-Functional" && project.requirement2[i].isCorrect == false){
                rows[i].GetComponentInChildren<Image>().color = correctColor;
            }
        }
    }

    void SetupOldKeyInput(OldProject project)
    {
        GameObject[] rows = new GameObject[project.keyInput.Length];

        foreach(Transform child in keyInput){
            Destroy(child.gameObject);
        }

        for(int i=0; i<project.keyInput.Length; i++){
            rows[i] = (GameObject)Instantiate(answerRow);
            rows[i].transform.SetParent(keyInput);
            rows[i].transform.localScale = Vector3.one;
            rows[i].GetComponentsInChildren<TextMeshProUGUI>()[0].text = project.keyInput[i].hint;

            rows[i].GetComponentsInChildren<TextMeshProUGUI>()[1].text = project.keyInput[i].word;

            if(i < project.keyInputPass.Count && project.keyInputPass[i]){
                rows[i].GetComponentInChildren<Image>().color = correctColor;
            }else{
                rows[i].GetComponentInChildren<Image>().color = wrongColor;
            }
        }
    }

    void SetupOldBalloonBoom(OldProject project)
    {
        GameObject[] balloons = new GameObject[project.balloonAnswer.Count];

        foreach(Transform child in balloonBoom){
            Destroy(child.gameObject);
        }

        for(int i=0; i<project.balloonAnswer.Count; i++){
            balloons[i] = (GameObject)Instantiate(answerButton);
            balloons[i].transform.SetParent(balloonBoom);
            balloons[i].transform.localScale = Vector3.one;
            balloons[i].GetComponentInChildren<TextMeshProUGUI>().text = project.balloonAnswer[i];

            foreach(var word in project.balloons){
                if(project.balloonAnswer[i] == word.word && word.isCorrect){
                    balloons[i].GetComponentInChildren<Image>().color = correctColor;
                }else if(project.balloonAnswer[i] == word.word){
                    balloons[i].GetComponentInChildren<Image>().color = wrongColor;
                }
            }
        }
    }

    void SetupOldBalloonBoom2(OldProject project)
    {
        GameObject[] balloons = new GameObject[project.balloon2Answer.Count];

        foreach(Transform child in balloonBoom2){
            Destroy(child.gameObject);
        }

        for(int i=0; i<project.balloon2Answer.Count; i++){
            balloons[i] = (GameObject)Instantiate(answerButton);
            balloons[i].transform.SetParent(balloonBoom2);
            balloons[i].transform.localScale = Vector3.one;
            balloons[i].GetComponentInChildren<TextMeshProUGUI>().text = project.balloon2Answer[i];

            foreach(var word in project.balloons){
                if(project.balloon2Answer[i] == word.word && word.isCorrect){
                    balloons[i].GetComponentInChildren<Image>().color = wrongColor;
                }else if(project.balloon2Answer[i] == word.word){
                    balloons[i].GetComponentInChildren<Image>().color = correctColor;
                }
            }
        }
    }
}
