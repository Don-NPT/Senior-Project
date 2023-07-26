using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Design : MonoBehaviour
{
    public GameObject toolUsedUI;
    public GameObject toolExistUI;
    Button[] toolUsedBtn;
    Button[] toolExistBtn;
    private Project project;

    private void OnEnable() {
        toolUsedBtn = toolUsedUI.GetComponentsInChildren<Button>();
        toolExistBtn = toolExistUI.GetComponentsInChildren<Button>();

        toolUsedBtn[0].onClick.AddListener(() => ToolUsedClick(0));
        toolUsedBtn[1].onClick.AddListener(() => ToolUsedClick(1));
        toolUsedBtn[2].onClick.AddListener(() => ToolUsedClick(2));
        toolUsedBtn[3].onClick.AddListener(() => ToolUsedClick(3));

        toolExistBtn[0].onClick.AddListener(() => ToolExistClick(0));
        toolExistBtn[1].onClick.AddListener(() => ToolExistClick(1));
        toolExistBtn[2].onClick.AddListener(() => ToolExistClick(2));
        toolExistBtn[3].onClick.AddListener(() => ToolExistClick(3));

        foreach(var button in toolUsedBtn)
        {
            button.gameObject.SetActive(false);
        }
        
        project = ProjectManager.instance.currentProject;
        project.designAnswer = new List<bool>();

        foreach(var button in toolUsedBtn)
        {
            button.gameObject.SetActive(false);
        }
    }

    void ToolUsedClick(int index)
    {
        toolUsedBtn[index].gameObject.SetActive(!toolUsedBtn[index].gameObject.activeSelf);
        toolExistBtn[index].gameObject.SetActive(!toolUsedBtn[index].gameObject.activeSelf);
    }

    void ToolExistClick(int index)
    {
        toolExistBtn[index].gameObject.SetActive(!toolExistBtn[index].gameObject.activeSelf);
        toolUsedBtn[index].gameObject.SetActive(!toolExistBtn[index].gameObject.activeSelf);
    }

    public void Confirm()
    {
        foreach(var button in toolUsedBtn)
        {
            if(button.gameObject.activeSelf){
                project.designAnswer.Add(true);
            }
        }
    }
}
