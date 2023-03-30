using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StaffList : MonoBehaviour
{
    GameObject[] staffs;
    public GameObject uiPrefab;
    GameObject[] staffItem;
    public Transform rightContent;
    public Transform leftContent;
    string positionToShow;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnEnable()
    {
        DestroyAllStaffItem();
        positionToShow = "All";

        ManageStaffItem();
    }

    public void CheckSelectedTab(int index)
    {
        ButtonManager button = leftContent.GetComponentsInChildren<ButtonManager>()[index];
        positionToShow = "";
        if(button.isSelected)
        {
            switch(button.name)
            {
                case "AllBtn":
                    positionToShow = "All";
                    break;
                case "AnalysisBtn":
                    positionToShow = "Analyst";
                    break;
                case "DesignBtn":
                    positionToShow = "Designer";
                    break;
                case "DevelopBtn":
                    positionToShow = "Programmer";
                    break;
                case "TestBtn":
                    positionToShow = "Tester";
                    break;
            }
        }
        ManageStaffItem();
    }

    public void ManageStaffItem() {
        DestroyAllStaffItem();
    
        staffs = GameObject.FindGameObjectsWithTag("Staff");
        staffItem = new GameObject[staffs.Length];
        Debug.Log(staffs.Length);
        for(int i=0;i<staffs.Length;i++)
        {
            if(staffs[i].GetComponent<StaffProperties>().position == positionToShow || positionToShow == "All")
            {
                SetupStaffItem(i);
            }
        }
    }

    void SetupStaffItem(int i)
    {
        // Create staff list
        staffItem[i] = (GameObject)Instantiate(uiPrefab);
        staffItem[i].transform.SetParent(rightContent);
        staffItem[i].transform.localScale = new Vector3(1, 1, 1);

        // Set staff name on the list
        string name = staffs[i].GetComponent<StaffProperties>().fname;
        staffItem[i].GetComponentsInChildren<TextMeshProUGUI>()[1].text = name;

        // Set logo and color
        staffItem[i].GetComponentsInChildren<Image>()[1].sprite = staffs[i].GetComponent<StaffProperties>().GetStaffLogo();
        staffItem[i].GetComponentsInChildren<Image>()[1].color = staffs[i].GetComponent<StaffProperties>().GetStaffColor();
        staffItem[i].GetComponentsInChildren<TextMeshProUGUI>()[0].text = staffs[i].GetComponent<StaffProperties>().GetStaffStat().ToString();
        staffItem[i].GetComponentsInChildren<TextMeshProUGUI>()[0].color = staffs[i].GetComponent<StaffProperties>().GetStaffColor();

        // Set assign button according the isAssign
        int index = i;
        if(staffs[i].GetComponent<StaffProperties>().isAssign)
        {
            staffItem[i].GetComponentInChildren<Button>().gameObject.SetActive(false);
            staffItem[i].GetComponentsInChildren<Button>()[0].onClick.AddListener(delegate { KickoutStaff(index); });
        }else{
            staffItem[i].GetComponentInChildren<Button>().gameObject.SetActive(true);
            staffItem[i].GetComponentsInChildren<Button>()[1].onClick.AddListener(delegate { KickoutStaff(index); });
        }

    }

    void KickoutStaff(int index){
        if(ProjectManager.instance.currentProject == null){
            // Debug.Log("Kick out staff '" + staffs[index].GetComponent<StaffProperties>().fname + "'");
            Destroy(staffs[index]);
            ManageStaffItem();
        }else{
            GameManager.instance.ToggleNotification("ไม่สามารถไล่พนักงานระหว่างทำโปรเจคได้");
        }
    }

    private void OnDisable() {
        DestroyAllStaffItem();
    }

    void DestroyAllStaffItem()
    {
        foreach(Transform child in rightContent)
        {
            Destroy(child.gameObject);
        }
    }
}
