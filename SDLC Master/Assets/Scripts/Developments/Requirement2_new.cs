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
        project.requirement2Answer = new List<bool>();
        index = 0;
        TMP.text = project.requirement2[index].word;
    }
    public void Functional()
    {
        project.requirement2Answer.Add(true);
        if(index < project.requirement2.Length)
        {
            if(project.requirement2[index].isCorrect){
                // float jumpHeight = 20f;
                // float jumpDuration = 0.5f;
                // transform.DOJump(transform.position + Vector3.up * jumpHeight, jumpHeight, 1, jumpDuration)
                //     .SetEase(Ease.InOutQuad)
                //     .OnComplete(() => {
                //         // Animate the return to the original position
                //         transform.DOMove(transform.position, jumpDuration)
                //             .SetEase(Ease.InOutQuad);
                //     });
            }

            index++;
            TMP.text = project.requirement2[index].word;
        }else{
            gameObject.SetActive(false);
        }
        
    }

    public void NonFunctional()
    {
        index++;
        project.requirement2Answer.Add(false);
        if(index < project.requirement2.Length)
        {
            TMP.text = project.requirement2[index].word;
        }else{
            gameObject.SetActive(false);
        }
        
    }
}
