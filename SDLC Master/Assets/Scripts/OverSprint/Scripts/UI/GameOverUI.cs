using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour {

    [SerializeField] private Button BackToMainButton;
    [SerializeField] private Transform sprintListUI;
    [SerializeField] private GameObject sprintPanelPrefab;

    private void Start() {
        KitchenGameManager.Instance.OnStateChanged += KitchenGameManager_OnStateChanged;
        Hide();
    }

    private void KitchenGameManager_OnStateChanged(object sender, System.EventArgs e) {
        if (KitchenGameManager.Instance.IsGameOver()) {
            Show();
            AgileManager.instance.project.sprintList = DeliveryManager.Instance.sprintList;

            foreach(Transform child in sprintListUI){
                Destroy(child.gameObject);
            }

            GameObject[] sprintPanel = new GameObject[DeliveryManager.Instance.sprintList.Count];
            int i=0;
            foreach(var tasklist in DeliveryManager.Instance.sprintList){
                sprintPanel[i] = (GameObject) Instantiate(sprintPanelPrefab);
                sprintPanel[i].transform.SetParent(sprintListUI);
                sprintPanel[i].transform.localScale = Vector3.one;

                sprintPanel[i].GetComponentInChildren<TextMeshProUGUI>().text = tasklist.sprintName;

                int j=0;
                foreach(Transform child in sprintPanel[i].GetComponentInChildren<GridLayoutGroup>().transform){
                    if(j < tasklist.taskList.Count){
                        child.GetComponent<Image>().sprite = tasklist.taskList[j].sprite;
                    }else{
                        child.gameObject.SetActive(false);
                    }
                    j++;
                }
                i++;
            }

            BackToMainButton.onClick.AddListener(() => { 
                AgileManager.instance.phaseIndex = 1;
                ProjectManager.instance.currentProject.sprintList = DeliveryManager.Instance.sprintList;
                AgileManager.instance.Save();
                GlobalVariable.isLoad = true;
                SceneManager.LoadScene("SampleScene");
                DeliveryManager.Instance.ClearNewRecipeList();      
            });
        } else {
            Hide();
        }
    }

    private void Show() {
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }

}