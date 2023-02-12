using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Design1 : MonoBehaviour
{
    public Button[] buttons;
    public Outline[] outlines;
    public GameObject KeyInputUI;
    
    // Start is called before the first frame update
    void Start()
    {
        buttons = GetComponentsInChildren<Button>();
        outlines = GetComponentsInChildren<Outline>();

        buttons[0].onClick.AddListener(() => ButtonPress(0));
        buttons[1].onClick.AddListener(() => ButtonPress(1));
        buttons[2].onClick.AddListener(() => ButtonPress(2));
        buttons[3].onClick.AddListener(() => ButtonPress(3));
        buttons[4].onClick.AddListener(() => ButtonPress(4));
        buttons[5].onClick.AddListener(() => ButtonPress(5));

        buttons[0].gameObject.GetComponentInChildren<TextMeshProUGUI>().text = ProjectManager.instance.currentProjects[0].requirement2[0].word;
        buttons[1].gameObject.GetComponentInChildren<TextMeshProUGUI>().text = ProjectManager.instance.currentProjects[0].requirement2[1].word;
        buttons[2].gameObject.GetComponentInChildren<TextMeshProUGUI>().text = ProjectManager.instance.currentProjects[0].requirement2[2].word;
        buttons[3].gameObject.GetComponentInChildren<TextMeshProUGUI>().text = ProjectManager.instance.currentProjects[0].requirement2[3].word;
    }

    public void InitiateKeyInput()
    {
        KeyInputUI.SetActive(true);
    }

    void ButtonPress(int index)
    {
        outlines[index].enabled = !outlines[index].enabled;
    }
}
