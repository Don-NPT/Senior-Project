using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;

    public GameObject skillMenu;
    public int skillPoint;
    public Transform qualityTree;
    public Transform speedTree;
    public TextMeshProUGUI skillPointText;
    public GameObject skillNotification;
    private bool[] qualityUnlock;
    private bool[] speedUnlock;

    private void Awake() {
        if (instance != null && instance != this) 
            Destroy(this); 
        else 
            instance = this; 
    }

    private void FixedUpdate() {
        if(qualityUnlock == null) { qualityUnlock = new bool[4]; }
        if(speedUnlock == null) { speedUnlock = new bool[4]; }
        for(int i=0; i<4; i++){
            qualityTree.GetComponentsInChildren<Outline>()[i].enabled = qualityUnlock[i];
            speedTree.GetComponentsInChildren<Outline>()[i].enabled = speedUnlock[i];
        }

        skillPointText.text = "แต้มสกิล: " + skillPoint;
    }

    public void OpenMenu(){
        if(qualityUnlock == null) { qualityUnlock = new bool[4]; }
        if(speedUnlock == null) { speedUnlock = new bool[4]; }

        for(int i=0; i<4; i++){
            int index = i;
            qualityTree.GetComponentsInChildren<Outline>()[i].enabled = qualityUnlock[i];
            qualityTree.GetComponentsInChildren<Outline>()[i].gameObject.GetComponent<Button>().onClick.AddListener(delegate { 
                LearnQualitySkill(index);
            });
        }
    }

    void LearnQualitySkill(int index){
        if(index == 0 && skillPoint >= CheckPrice(index)) {qualityUnlock[index] = true; AddSkillPoint(-CheckPrice(index));}
        else if(qualityUnlock[index-1] == true && skillPoint >= CheckPrice(index)){
            qualityUnlock[index] = true;
            AddSkillPoint(-CheckPrice(index));
        }
        else{
            AudioManager.instance.Play("Warning");
        }
    }

    int CheckPrice(int index){
        switch(index){
            case 0:
                return 2;
            case 1:
                return 4;
            case 2:
                return 6;
            case 3:
                return 8;
        }
        return 0;
    }

    public float GetQualityBonus(){
        int lastIndex = -1;
        foreach(bool unlock in qualityUnlock){
            if(unlock){
                lastIndex++;
            }
        }

        switch(lastIndex){
            case -1:
                return 1;
            case 0:
                return 1.05f;
            case 1:
                return 1.1f;
            case 2:
                return 1.15f;
            case 3:
                return 1.2f;
        }
        return 0;
    }

    public void AddSkillPoint(int point){
        skillPoint += point;
        StartCoroutine(ShowNotification(point));
    }

    IEnumerator ShowNotification(int point){
        skillNotification.SetActive(true);

        if(point > 0) {
            skillNotification.GetComponentInChildren<TextMeshProUGUI>().text = "+" + point;
            skillNotification.GetComponent<Image>().color = GameManager.instance.colors[0];
        }
        else if(point < 0) {
            skillNotification.GetComponentInChildren<TextMeshProUGUI>().text = point.ToString();
            skillNotification.GetComponent<Image>().color = GameManager.instance.colors[1];
        }
        yield return new WaitForSeconds(1);
        skillNotification.SetActive(false);
    }

    public void ResetSkill(){
        for(int i=0; i<4; i++){
            if(qualityUnlock[i]){
                AddSkillPoint(CheckPrice(i));
                qualityUnlock[i] = false;
            }
            if(speedUnlock[i]){
                AddSkillPoint(CheckPrice(i));
                speedUnlock[i] = false;
            }
        }
    }
}
