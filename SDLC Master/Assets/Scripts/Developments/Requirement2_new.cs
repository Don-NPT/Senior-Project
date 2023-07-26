using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class Requirement2_new : MonoBehaviour
{
    TextMeshProUGUI TMP;
    public float pointCorrect;
    public float pointWrong;
    public int index;
    public GameObject bg;
    private float calculateQuality = 0;
    private Project project;

    void OnEnable()
    {
        TMP = GetComponentsInChildren<TextMeshProUGUI>()[1];
        project = ProjectManager.instance.currentProject;
        project.requirement2Answer = new List<string>();
        project.requirement2Point = 0;
        index = 0;
        TMP.text = project.requirement2[index].word;
    }
    public void Functional()
    {
        CheckAnswer("Functional");
    }

    public void NonFunctional()
    {
        CheckAnswer("Non-Functional");
    }

    void CheckAnswer(string answer)
    {
        project.requirement2Answer.Add(answer);
        bool isCorrect; 
        if(answer == "Functional"){
            isCorrect = true;
        }else{
            isCorrect = false;
        }
        if(isCorrect == project.requirement2[index].isCorrect){
                FindObjectOfType<AudioManager>().Play("Purchase");

                Color color = Color.green;
                color.a = 0;
                bg.GetComponent<Image>().color = color;
                bg.SetActive(true);
                bg.GetComponent<Image>().DOFade(0.2f, 0.3f).OnComplete(()=> bg.GetComponent<Image>().DOFade(0, 0.3f).OnComplete(()=> bg.SetActive(false)));
                Vector3 originalScale = transform.localScale;
                // Animate the scale of the GameObject
                transform.DOScale(Vector3.one * 1.05f, 0.2f)
                    .SetEase(Ease.OutQuad)
                    .OnComplete(() => transform.localScale = Vector3.one);
                calculateQuality = ((float)StaffManager.instance.getSumStaffStat("Analyst")/((float)(project.scale * 15))) * pointCorrect;
                Debug.Log("Analyst staff stat ="+(float)StaffManager.instance.getSumStaffStat("Analyst"));
                Debug.Log("Quality raw Analyst * ans = "+calculateQuality);
                calculateQuality = calculateQuality * SkillManager.instance.GetQualityBonus();
                Debug.Log("QualityCorrect ="+calculateQuality);
                WaterFallManager.instance.qualityEachPhase[0] += calculateQuality;
                project.requirement2Point += calculateQuality;
            }else{
                FindObjectOfType<AudioManager>().Play("Warning");
                float shakeDuration = 0.5f;
                float shakeStrength = 7.0f;
                int shakeVibrato = 10;
                float shakeRandomness = 90.0f;

                Color color = Color.red;
                color.a = 0;
                bg.GetComponent<Image>().color = color;
                bg.SetActive(true);
                bg.GetComponent<Image>().DOFade(0.2f, 0.3f).OnComplete(()=> bg.GetComponent<Image>().DOFade(0, 0.3f).OnComplete(()=> bg.SetActive(false)));

                transform.DOShakePosition(shakeDuration, shakeStrength, shakeVibrato, shakeRandomness);
                calculateQuality = ((float)StaffManager.instance.getSumStaffStat("Analyst")/((float)(project.scale * 15))) * pointWrong;
                Debug.Log("Analyst staff stat ="+(float)StaffManager.instance.getSumStaffStat("Analyst"));
                Debug.Log("Quality raw Analyst * ans = "+calculateQuality);
                calculateQuality = calculateQuality * SkillManager.instance.GetQualityBonus();
                Debug.Log("QualityWrong "+calculateQuality);
                WaterFallManager.instance.qualityEachPhase[0] += calculateQuality;
                project.requirement2Point += calculateQuality;
            }
        index++;
        if(index < project.requirement2.Length )
        {
            TMP.text = project.requirement2[index].word;
        }else{
            ProjectHUD.instance.WaterfallHUDUpdate();
            gameObject.SetActive(false);
            WaterFallManager.instance.NextPhase();
        }
    }
}
