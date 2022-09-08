using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ProjectCurrent : MonoBehaviour
{
    public GameObject projectCurrentItemPrefab;
    public Transform content;
    GameObject[] projectCurrentItem;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable() {
        projectCurrentItem = new GameObject[GameManager.instance.currentProjects.Count];

        for(int i=0; i<GameManager.instance.currentProjects.Count; i++)
        {
            // Spawn current project items
            projectCurrentItem[i] = (GameObject)Instantiate(projectCurrentItemPrefab);
            projectCurrentItem[i].transform.SetParent(content);
            projectCurrentItem[i].transform.localScale = new Vector3(1, 1, 1);

            // set text for project confirm
            TextMeshProUGUI[] projectCurrentText = projectCurrentItem[i].GetComponentsInChildren<TextMeshProUGUI>();
            projectCurrentText[0].text = GameManager.instance.currentProjects[i].pjName;
            projectCurrentText[1].text = GameManager.instance.currentProjects[i].reward.ToString();
        }
    }

    private void OnDisable() {
        foreach (Transform child in content.transform) {
            GameObject.Destroy(child.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
