using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TeamManager : MonoBehaviour
{
    public static TeamManager instance;
    public Transform selectionSlider;
    private GameObject[] staffMiniItem;
    public GameObject staffMiniItemPrefab;
    public GameObject staffMemberItemPrefab;
    public Sprite[] positionLogo;
    public List<StaffProperties> team1;
    public Transform[] teams;
    private GameObject[] teamItem;
    GameObject[] staffs;
    // Start is called before the first frame update
    void Start()
    {
        // If there is an instance, and it's not me, delete myself.
        if (instance != null && instance != this) 
            Destroy(this); 
        else 
            instance = this; 

        team1 = new List<StaffProperties>();
    }

    void FixedUpdate()
    {
        
    }

    void OnEnable()
    {
        // find all staffs
        staffs = GameObject.FindGameObjectsWithTag("Staff");
        staffMiniItem = new GameObject[staffs.Length];

        // create staff item UI
        for(int i=0; i<staffs.Length; i++)
        {
            staffMiniItem[i] = (GameObject)Instantiate(staffMiniItemPrefab);
            staffMiniItem[i].transform.SetParent(selectionSlider);
            staffMiniItem[i].transform.localScale = new Vector3(1, 1, 1);

            staffMiniItem[i].GetComponentsInChildren<TextMeshProUGUI>()[0].text = staffs[i].GetComponent<StaffProperties>().fname;
            SetPositionInfo(i, staffMiniItem, staffs[i].GetComponent<StaffProperties>());
            
        }
    }

    public void SelectStaff(string staffName)
    {
        //add team member
        foreach(var staff in staffs)
        {
            if(staff.GetComponent<StaffProperties>().fname.ToString() == staffName)
            {
                team1.Add(staff.GetComponent<StaffProperties>());
            }
        }
        UpdateTeam();
    }

    public void UnselectStaff(string staffName)
    {
        // Debug.Log(team1.Count);
        Debug.Log(staffName);
        //remove team member
        foreach(var staff in staffs)
        {
            if(staff.GetComponent<StaffProperties>().fname.ToString() == staffName)
            {
                team1.Remove(staff.GetComponent<StaffProperties>());
            }
        }

        //set staff to visible
        foreach(var item in staffMiniItem)
        {
            if(item.GetComponentsInChildren<TextMeshProUGUI>()[0].text.ToString() == staffName)
            {
                item.SetActive(true);
            }
        }

        UpdateTeam();
    }

    void UpdateTeam()
    {
        if(teamItem != null)
            ClearTeamItem();
        if(team1 != null)
        {
            teamItem = new GameObject[team1.Count];
            for(int i=0; i<team1.Count; i++)
            {
                teamItem[i] = (GameObject)Instantiate(staffMemberItemPrefab);
                teamItem[i].transform.SetParent(teams[0]);
                teamItem[i].transform.localScale = new Vector3(1, 1, 1);

                teamItem[i].GetComponentsInChildren<TextMeshProUGUI>()[0].text = staffs[i].GetComponent<StaffProperties>().fname;
                SetPositionInfo(i, teamItem, staffs[i].GetComponent<StaffProperties>());
            }
        }
    }

    void SetPositionInfo(int index, GameObject[] prefab, StaffProperties staffProperties)
    {
        switch(staffProperties.position)
            {
                case "Analyst":
                    prefab[index].GetComponentsInChildren<TextMeshProUGUI>()[1].text = staffProperties.analysis.ToString();
                    prefab[index].GetComponentsInChildren<Image>()[1].sprite = positionLogo[0];
                    break;
                case "Designer":
                    prefab[index].GetComponentsInChildren<TextMeshProUGUI>()[1].text = staffProperties.design.ToString();
                    prefab[index].GetComponentsInChildren<Image>()[1].sprite = positionLogo[1];
                    break;
                case "Programmer":
                    prefab[index].GetComponentsInChildren<TextMeshProUGUI>()[1].text = staffProperties.coding.ToString();
                    prefab[index].GetComponentsInChildren<Image>()[1].sprite = positionLogo[2];
                    break;
                case "Tester":
                    prefab[index].GetComponentsInChildren<TextMeshProUGUI>()[1].text = staffProperties.social.ToString();
                    prefab[index].GetComponentsInChildren<Image>()[1].sprite = positionLogo[3];
                    break;
            }
    }

    void ClearTeamItem()
    {
        foreach(GameObject item in teamItem)
        {
            Destroy(item);
        }
    }

    void OnDisable()
    {
        foreach(GameObject item in staffMiniItem)
        {
            Destroy(item);
        }
    }
}
