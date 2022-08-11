using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProjectPicker : MonoBehaviour
{
    public GameObject ProjectMenu;

    public Sprite Sprite; //List of Sprites added from the Editor to be created as GameObjects at runtime
    public Transform ParentPanel; //Parent Panel you want the new Images to be children of
    public Font Font;
    public Project[] projects;
    public GameObject itemPrefab;

    // private string[] data = {"Visage", "Layer of Fear", "Fatal Frame"};

    public void ShowProjects() {

        for(int i=0; i<projects.Length;i++){
            GameObject item = (GameObject)Instantiate(itemPrefab);
            item.transform.SetParent(ParentPanel);
            item.transform.localScale = new Vector3(1, 1, 1);

            TMP_Text[] childs = item.GetComponentsInChildren<TMP_Text>();
            childs[0].text = projects[i].pjName;
            childs[1].text = projects[i].description;
            childs[2].text = projects[i].reward.ToString() + " บาท";
        }

        // for(int i=0; i<projects.Length;i++){
        // GameObject newObj = new GameObject();
        // newObj.AddComponent<Image>();
        // newObj.GetComponent<Image>().color = Color.white;
        // newObj.AddComponent<Button>();
        // newObj.transform.SetParent(ParentPanel);
        // newObj.transform.localScale = new Vector3(1, 1, 1);

        // GameObject text = new GameObject();
        // text.AddComponent<Text>();
        // text.GetComponent<Text>().text = projects[i].pjName;
        // text.GetComponent<Text>().font = Font;
        // text.GetComponent<Text>().fontSize = 24;
        // text.GetComponent<Text>().color = Color.black;
        // text.GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
        // text.GetComponent<RectTransform>().SetParent(newObj.transform);
        // text.transform.localScale = new Vector3(1, 1, 1);
        // }

    }

    public void OpenPanel()
    {
        if(ProjectMenu != null)
        {
            ProjectMenu.transform.localScale = Vector3.zero;
            ProjectMenu.SetActive(true);
            ProjectMenu.transform.DOScale(1, 0.3f).SetEase(Ease.OutQuad);
        }
    }

    public void ClosePanel()
    {
        if(ProjectMenu != null)
        {
            ProjectMenu.SetActive(false);
            foreach (Transform child in ParentPanel) {
                Destroy(child.gameObject);
            }
        }
    }
}

