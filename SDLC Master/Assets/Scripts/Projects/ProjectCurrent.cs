using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ProjectCurrent : MonoBehaviour
{
    public GameObject projectCurrentItemPrefab;
    public Transform content;
    GameObject[] projectCurrentItem;


    private void OnEnable() {
        // projectCurrentItem = new GameObject[ProjectManager.instance.getNumProject()];

        // for(int i=0; i<ProjectManager.instance.getNumProject(); i++)
        // {
        //     // Spawn current project items
        //     projectCurrentItem[i] = (GameObject)Instantiate(projectCurrentItemPrefab);
        //     projectCurrentItem[i].transform.SetParent(content);
        //     projectCurrentItem[i].transform.localScale = new Vector3(1, 1, 1);

        //     // set text for project confirm
        //     TextMeshProUGUI[] projectCurrentText = projectCurrentItem[i].GetComponentsInChildren<TextMeshProUGUI>();
        //     projectCurrentText[0].text = ProjectManager.instance.currentProjects[i].pjName;
        //     projectCurrentText[1].text = ProjectManager.instance.currentProjects[i].reward.ToString();
        // }
    }

    private void OnDisable() {
        // foreach (Transform child in content.transform) {
        //     GameObject.Destroy(child.gameObject);
        // }
    }

}
