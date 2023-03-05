using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class Requirement2_new : MonoBehaviour
{
    TextMeshProUGUI TMP;
    int index;
    private Project project;

    // Start is called before the first frame update
    private void Start() {
        
    }
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
        if(project.requirement2[index].isCorrect){
                FindObjectOfType<AudioManager>().Play("Purchase");
                Vector3 originalScale = transform.localScale;
                // Animate the scale of the GameObject
                transform.DOScale(Vector3.one * 1.05f, 0.2f)
                    .SetEase(Ease.OutQuad)
                    .OnComplete(() => transform.localScale = Vector3.one);
                WaterFallManager.instance.qualityEachPhase[0] += 5;
                project.requirement2Point += 5;
            }else{
                FindObjectOfType<AudioManager>().Play("Warning");
                float shakeDuration = 0.5f;
                float shakeStrength = 7.0f;
                int shakeVibrato = 10;
                float shakeRandomness = 90.0f;
                transform.DOShakePosition(shakeDuration, shakeStrength, shakeVibrato, shakeRandomness);
                WaterFallManager.instance.qualityEachPhase[0] -= 3;
                project.requirement2Point -= 3;
            }
        if(index < project.requirement2.Length -1)
        {
            
            index++;
            TMP.text = project.requirement2[index].word;
        }else{
            gameObject.SetActive(false);
        }
    }
}
