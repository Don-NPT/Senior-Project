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
    public Transform[] teamPrefabs;
    private int teamPrefabIndex;
    private GameObject[] team1Item;
    private GameObject[] team2Item;
    private GameObject[] team3Item;
    private GameObject[] team4Item;
    GameObject[] staffs;
    // Start is called before the first frame update
    void Start()
    {
        // If there is an instance, and it's not me, delete myself.
        if (instance != null && instance != this) 
            Destroy(this); 
        else 
            instance = this; 

        teamPrefabIndex = 0;
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
            SetPositionInfo(staffMiniItem[i], staffs[i].GetComponent<StaffProperties>());
            
            if(staffs[i].GetComponent<StaffProperties>().inTeam == true)
                staffMiniItem[i].SetActive(false);
        }
    }

    public void SelectStaff(string staffName)
    {
        //add team member
        foreach(var staff in staffs)
        {
            if(staff.GetComponent<StaffProperties>().fname.ToString() == staffName)
            {
                TeamManager2.instance.teams[teamPrefabIndex].Add(staff.GetComponent<StaffProperties>());
                staff.GetComponent<StaffProperties>().inTeam = true;
            }
        }

        //set staff to invisible
        foreach(var item in staffMiniItem)
        {
            if(item.GetComponentsInChildren<TextMeshProUGUI>()[0].text.ToString() == staffName)
            {
                item.SetActive(false);
            }
        }
        UpdateTeam();
    }

    public void UnselectStaff(string staffName)
    {
        // Debug.Log(teams[0].Count);
        Debug.Log(staffName);
        //remove team member
        foreach(var staff in staffs)
        {
            if(staff.GetComponent<StaffProperties>().fname.ToString() == staffName)
            {
                TeamManager2.instance.teams[0].Remove(staff.GetComponent<StaffProperties>());
                TeamManager2.instance.teams[1].Remove(staff.GetComponent<StaffProperties>());
                TeamManager2.instance.teams[2].Remove(staff.GetComponent<StaffProperties>());
                TeamManager2.instance.teams[3].Remove(staff.GetComponent<StaffProperties>());
                staff.GetComponent<StaffProperties>().inTeam = false;
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

    public void SelectTeam(int index)
    {
        teamPrefabIndex = index;

        //manage outline
        foreach(var prefab in teamPrefabs)
        {
            prefab.gameObject.GetComponent<Outline>().enabled = false;
        }
        teamPrefabs[index].gameObject.GetComponent<Outline>().enabled = true;
    }

    void UpdateTeam()
    {
        ClearTeamItem();
        
        if(TeamManager2.instance.teams[0] != null)
        {
            team1Item = new GameObject[TeamManager2.instance.getSize(0)];
            for(int i=0; i<TeamManager2.instance.getSize(0); i++)
            {
                team1Item[i] = (GameObject)Instantiate(staffMemberItemPrefab);
                team1Item[i].transform.SetParent(teamPrefabs[0]);
                team1Item[i].transform.localScale = new Vector3(1, 1, 1);

                team1Item[i].GetComponentsInChildren<TextMeshProUGUI>()[0].text = TeamManager2.instance.teams[0][i].fname;
                SetPositionInfo(team1Item[i], staffs[i].GetComponent<StaffProperties>());
            }
        }

        if(TeamManager2.instance.teams[1] != null)
        {
            team2Item = new GameObject[TeamManager2.instance.getSize(1)];
            for(int i=0; i<TeamManager2.instance.getSize(1); i++)
            {
                team2Item[i] = (GameObject)Instantiate(staffMemberItemPrefab);
                team2Item[i].transform.SetParent(teamPrefabs[1]);
                team2Item[i].transform.localScale = new Vector3(1, 1, 1);

                team2Item[i].GetComponentsInChildren<TextMeshProUGUI>()[0].text = TeamManager2.instance.teams[1][i].fname;
                SetPositionInfo(team2Item[i], staffs[i].GetComponent<StaffProperties>());
            }
        }

        if(TeamManager2.instance.teams[2] != null)
        {
            team3Item = new GameObject[TeamManager2.instance.getSize(2)];
            for(int i=0; i<TeamManager2.instance.getSize(2); i++)
            {
                team3Item[i] = (GameObject)Instantiate(staffMemberItemPrefab);
                team3Item[i].transform.SetParent(teamPrefabs[2]);
                team3Item[i].transform.localScale = new Vector3(1, 1, 1);

                team3Item[i].GetComponentsInChildren<TextMeshProUGUI>()[0].text = TeamManager2.instance.teams[2][i].fname;
                SetPositionInfo(team3Item[i], staffs[i].GetComponent<StaffProperties>());
            }
        }

        if(TeamManager2.instance.teams[3] != null)
        {
            team4Item = new GameObject[TeamManager2.instance.getSize(3)];
            for(int i=0; i<TeamManager2.instance.getSize(3); i++)
            {
                team4Item[i] = (GameObject)Instantiate(staffMemberItemPrefab);
                team4Item[i].transform.SetParent(teamPrefabs[3]);
                team4Item[i].transform.localScale = new Vector3(1, 1, 1);

                team4Item[i].GetComponentsInChildren<TextMeshProUGUI>()[0].text = TeamManager2.instance.teams[3][i].fname;
                SetPositionInfo(team4Item[i], staffs[i].GetComponent<StaffProperties>());
            }
        }
    }

    void SetPositionInfo(GameObject prefab, StaffProperties staffProperties)
    {
        switch(staffProperties.position)
            {
                case "Analyst":
                    prefab.GetComponentsInChildren<TextMeshProUGUI>()[1].text = staffProperties.analysis.ToString();
                    prefab.GetComponentsInChildren<Image>()[1].sprite = positionLogo[0];
                    break;
                case "Designer":
                    prefab.GetComponentsInChildren<TextMeshProUGUI>()[1].text = staffProperties.design.ToString();
                    prefab.GetComponentsInChildren<Image>()[1].sprite = positionLogo[1];
                    break;
                case "Programmer":
                    prefab.GetComponentsInChildren<TextMeshProUGUI>()[1].text = staffProperties.coding.ToString();
                    prefab.GetComponentsInChildren<Image>()[1].sprite = positionLogo[2];
                    break;
                case "Tester":
                    prefab.GetComponentsInChildren<TextMeshProUGUI>()[1].text = staffProperties.testing.ToString();
                    prefab.GetComponentsInChildren<Image>()[1].sprite = positionLogo[3];
                    break;
            }
    }

    void ClearTeamItem()
    {
        if(team1Item != null)
        {
            foreach(GameObject item in team1Item)
            {
                Destroy(item);
            }
        }
        if(team2Item != null)
        {
            foreach(GameObject item in team2Item)
            {
                Destroy(item);
            }
        }
        if(team3Item != null)
        {
            foreach(GameObject item in team3Item)
            {
                Destroy(item);
            }
        }
        if(team4Item != null)
        {
            foreach(GameObject item in team4Item)
            {
                Destroy(item);
            }
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
