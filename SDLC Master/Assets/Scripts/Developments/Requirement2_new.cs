using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Requirement2_new : MonoBehaviour
{
    TextMeshProUGUI TMP;
    int index;
    // Start is called before the first frame update
    private void Start() {
        TMP = GetComponentsInChildren<TextMeshProUGUI>()[1];
    }
    void onEnable()
    {
        index = 0;
        TMP.text = ProjectManager.instance.currentProject.requirement2_new[index].word;
    }
    public void Functional()
    {
        index++;
        Debug.Log("index: "+ index + ", Length:" + ProjectManager.instance.currentProject.requirement2_new.Length);
        if(index < ProjectManager.instance.currentProject.requirement2_new.Length)
        {
            TMP.text = ProjectManager.instance.currentProject.requirement2_new[index].word;
        }else{
            gameObject.SetActive(false);
        }
    }

    public void NonFunctional()
    {
        index++;
         Debug.Log("index: "+ index + ", Length:" + ProjectManager.instance.currentProject.requirement2_new.Length);
        if(index < ProjectManager.instance.currentProject.requirement2_new.Length)
        {
            TMP.text = ProjectManager.instance.currentProject.requirement2_new[index].word;
        }else{
            gameObject.SetActive(false);
        }
    }
}
