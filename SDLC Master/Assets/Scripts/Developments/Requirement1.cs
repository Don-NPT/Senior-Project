using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Requirement1 : MonoBehaviour
{
    public Button[] buttons;
    public Outline[] outlines;
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

        buttons[0].gameObject.GetComponentInChildren<TextMeshProUGUI>().text = ProjectManager.instance.currentProjects[0].requirement1[0].word;
        buttons[1].gameObject.GetComponentInChildren<TextMeshProUGUI>().text = ProjectManager.instance.currentProjects[0].requirement1[1].word;
        buttons[2].gameObject.GetComponentInChildren<TextMeshProUGUI>().text = ProjectManager.instance.currentProjects[0].requirement1[2].word;
        buttons[3].gameObject.GetComponentInChildren<TextMeshProUGUI>().text = ProjectManager.instance.currentProjects[0].requirement1[3].word;
        buttons[4].gameObject.GetComponentInChildren<TextMeshProUGUI>().text = ProjectManager.instance.currentProjects[0].requirement1[4].word;
        buttons[5].gameObject.GetComponentInChildren<TextMeshProUGUI>().text = ProjectManager.instance.currentProjects[0].requirement1[5].word;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ButtonPress(int index)
    {
        if(index % 2 == 0)
        {
            outlines[index].enabled = true;
            outlines[index+1].enabled = !outlines[index].enabled;
        }
        else if(index % 2 == 1)
        {
            outlines[index].enabled = true;
            outlines[index-1].enabled = !outlines[index].enabled;
        }
    }
}
