using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class ProjectPicker : MonoBehaviour
{
    public Transform rightContent;
    public Project[] projects;
    public SDLCModel[] models;
    public GameObject[] projectItems;
    public GameObject[] modelItems;
    public GameObject[] teamItems;
    public GameObject projectDetail;
    public GameObject projectConfirm;
    public GameObject modelConfirm;
    public GameObject teamConfirm;
    private Project selectedProject;
    private SDLCModel selectedModel;
    private List<StaffProperties> selectedTeam;

    public void OnEnable() {
        // set texts for 3 projects
        for(int i=0; i<projectItems.Length;i++){
            TextMeshProUGUI[] textMeshPros = projectItems[i].GetComponentsInChildren<TextMeshProUGUI>();
            textMeshPros[0].text = projects[i].pjName;
            textMeshPros[1].text = projects[i].description;
            textMeshPros[2].text = "ผลตอบแทน: " + projects[i].reward.ToString() + " บาท";
        }
        // reset outline if no selectedProject
        if(!selectedProject)
        {
            foreach(var projectItem in projectItems)
            {
                projectItem.GetComponent<Outline>().enabled = false;
            }
        }
        // reset outline if no selectedModel
        if(!selectedModel)
        {
            foreach(var modelItem in modelItems)
            {
                modelItem.GetComponent<Outline>().enabled = false;
            }
        }
    }

    public void SeeProjectDetail(int index)
    {
        // set text for project detail
        TextMeshProUGUI[] projectDetailText = projectDetail.GetComponentsInChildren<TextMeshProUGUI>();
        projectDetailText[0].text = projects[index].pjName;
        projectDetailText[1].text = projects[index].description;
        projectDetailText[3].text = "Analysis: " + projects[index].requireAnalysis.ToString();
        projectDetailText[4].text = "Design: " + projects[index].requireDesign.ToString();
        projectDetailText[5].text = "Coding: " + projects[index].requireCoding.ToString();
        projectDetailText[6].text = "Testing: " + projects[index].requireTesting.ToString();
        projectDetailText[7].text = "Deployment: " + projects[index].requireDeployment.ToString();
        projectDetailText[8].text = "ผลตอบแทน: " + projects[index].reward.ToString() + " บาท";
        projectDetailText[9].text = "ต้องการภายใน: " + projects[index].deadline.ToString() + " วัน";
    }

    public void selectProject(int index)
    {
        selectedProject = projects[index];

        // set outline for selected project
        for(int i=0; i<projectItems.Length; i++)
        {
            if(i == index) projectItems[index].GetComponent<Outline>().enabled = true;
            else projectItems[i].GetComponent<Outline>().enabled = false;
        }

        // set text for project confirm
        TextMeshProUGUI[] projectConfirmText = projectConfirm.GetComponentsInChildren<TextMeshProUGUI>();
        projectConfirmText[0].text = selectedProject.pjName;
        projectConfirmText[1].text = selectedProject.description;
        projectConfirmText[2].text = "ผลตอบแทน: " + selectedProject.reward.ToString() + " บาท";

        print(selectedProject);
    }

    public void SelectModel(int index)
    {
        selectedModel = models[index];

        // set outline for selected model
        for(int i=0; i<modelItems.Length; i++)
        {
            if(i == index) modelItems[index].GetComponent<Outline>().enabled = true;
            else modelItems[i].GetComponent<Outline>().enabled = false;
        }

        // set text for model confirm
        TextMeshProUGUI modelConfirmText = modelConfirm.GetComponentInChildren<TextMeshProUGUI>();
        modelConfirmText.text = selectedModel.modelName;
        Image[] modelConfirmImage = modelConfirm.GetComponentsInChildren<Image>();
        modelConfirmImage[1].sprite = selectedModel.image;

        print(selectedModel);
    }

    public void SelectTeam(int index)
    {
        selectedTeam = TeamManager2.instance.teams[index];
        foreach(var item in teamItems)
        {
            item.GetComponent<Outline>().enabled = false;
        }
        teamItems[index].GetComponent<Outline>().enabled = true;

        // set text for model confirm
        TextMeshProUGUI[] teamConfirmText = teamConfirm.GetComponentsInChildren<TextMeshProUGUI>();
        teamConfirmText[0].text = "Team " + (index+1);
        teamConfirmText[1].text = "จำนวนสมาชิก: " + TeamManager2.instance.getSize(index);
    }

    public void ConfirmProject()
    {
        GameManager.instance.currentProjects.Add(selectedProject);
        ProjectHUD.instance.UpdateList();
        selectedProject = null;
        selectedModel = null;
    }

    public void CloseRightPanel()
    {
        foreach(Transform child in rightContent)
        {
            child.gameObject.SetActive(false);
        }
    }
}

