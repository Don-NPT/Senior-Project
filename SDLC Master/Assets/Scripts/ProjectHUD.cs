using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectHUD : MonoBehaviour
{
    public static ProjectHUD instance;
    GameManager gameManager = GameManager.instance;
    GameObject[] projectHudItem;
    public GameObject uiPrefab;
    public Transform parent;
    public GameObject hudDetail;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateList() {
        if(GameManager.instance.currentProjects.Count > 0)
        {
            DestroyAllItem();
            projectHudItem = new GameObject[GameManager.instance.currentProjects.Count];
            for(int i=0; i<GameManager.instance.currentProjects.Count; i++)
            {
                projectHudItem[i] = (GameObject)Instantiate(uiPrefab);
                projectHudItem[i].transform.SetParent(parent);
                projectHudItem[i].transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }

    public void ShowDetail(int index)
    {
        if(hudDetail.activeSelf == true){
            hudDetail.SetActive(false);
            return;
        }
        // hudDetail = (GameObject)Instantiate(hudDetail);
        // hudDetail.transform.SetParent(parent);
        hudDetail.SetActive(true);
        hudDetail.transform.SetSiblingIndex(index+1);
        // hudDetail.transform.localScale = new Vector3(1, 1, 1);
    }

    void DestroyAllItem()
    {
        if(projectHudItem != null){
            foreach(GameObject child in projectHudItem)
            {
                Destroy(child);
            }
        } 
    }

}
