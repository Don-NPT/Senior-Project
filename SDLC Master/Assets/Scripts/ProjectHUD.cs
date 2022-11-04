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
    GameObject[] projectHudItem;
    public GameObject uiPrefab;
    public Transform parent;
    public GameObject hudDetail;
    private GameObject hudDetailItem;
    public int projectIndex;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(hudDetailItem != null)
        {
            // hudDetailItem.GetComponentInChildren<Slider>().value = ProjectManager.instance.currentProjects[projectIndex].currentPoints;
            hudDetailItem.GetComponentInChildren<Slider>().DOValue(ProjectManager.instance.currentProjects[projectIndex].currentPoints, 0.3f).Play();
            hudDetailItem.GetComponentsInChildren<TextMeshProUGUI>()[1].text = ProjectManager.instance.currentProjects[projectIndex].phase.ToString();
        }
    }

    public void UpdateList() {
        if(ProjectManager.instance.getNumProject() > 0)
        {
            DestroyAllItem();
            projectHudItem = new GameObject[ProjectManager.instance.getNumProject()];
            for(int i=0; i<ProjectManager.instance.getNumProject(); i++)
            {
                // Setup project hud tab
                projectHudItem[i] = (GameObject)Instantiate(uiPrefab);
                projectHudItem[i].transform.SetParent(parent);
                projectHudItem[i].transform.localScale = new Vector3(1, 1, 1);
                int index = i;
                projectHudItem[i].GetComponent<Button>().onClick.AddListener(() => {ShowDetail(index);});

                // Setup text info
                projectHudItem[i].GetComponentsInChildren<TextMeshProUGUI>()[0].text = ProjectManager.instance.currentProjects[i].pjName;
                projectHudItem[i].GetComponentsInChildren<TextMeshProUGUI>()[1].text = ProjectManager.instance.currentProjects[i].state.ToString();
            }
        }
    }

    public void ShowDetail(int index)
    {
        if(hudDetailItem == null){
            projectIndex = index;
            // Setup project detail hud
            hudDetailItem = (GameObject)Instantiate(hudDetail);
            hudDetailItem.transform.SetParent(parent);
            hudDetailItem.transform.SetSiblingIndex(index+1);
            hudDetailItem.transform.localScale = new Vector3(1, 1, 1);

            // setup text info
            hudDetailItem.GetComponentsInChildren<TextMeshProUGUI>()[0].text = ProjectManager.instance.currentProjects[index].model.modelName;
            hudDetailItem.GetComponentsInChildren<TextMeshProUGUI>()[1].text = ProjectManager.instance.currentProjects[index].phase.ToString();
            hudDetailItem.GetComponentInChildren<Slider>().maxValue = ProjectManager.instance.currentProjects[index].finishPoints;
            return;
        }
        Destroy(hudDetailItem);
    }

    void DestroyAllItem()
    {
        if(projectHudItem != null){
            foreach(GameObject child in projectHudItem)
            {
                Destroy(child);
            }
        }
        if(hudDetailItem != null){
            Destroy(hudDetailItem);
        } 
    }

}
