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
    public Transform parentPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable() {
        staffs = GameObject.FindGameObjectsWithTag("Staff");
        staffItem = new GameObject[staffs.Length];

        for(int i=0;i<staffs.Length;i++)
        {
            // Create staff list
            staffItem[i] = (GameObject)Instantiate(uiPrefab);
            staffItem[i].transform.SetParent(parentPanel);
            staffItem[i].transform.localScale = new Vector3(1, 1, 1);

            // Set staff name on the list
            string name = staffs[i].GetComponent<StaffProperties>().fname + " " + staffs[i].GetComponent<StaffProperties>().lname;
            staffItem[i].GetComponentsInChildren<TextMeshProUGUI>()[0].text = name;
            
            // Set staff wage on the list
            // string wage = staffs[i].GetComponent<StaffProperties>().wage.ToString();
            // staffItem[i].GetComponentsInChildren<TextMeshProUGUI>()[1].text = "ค่าจ้าง/เดือน: " + wage;

            // Set assign button according the isAssign
            if(staffs[i].GetComponent<StaffProperties>().isAssign)
            {
                staffItem[i].GetComponentInChildren<Button>().gameObject.SetActive(false);
            }else{
                staffItem[i].GetComponentInChildren<Button>().gameObject.SetActive(true);
            }
        }
    }

    private void OnDisable() {
        for(int i=0;i<staffs.Length;i++)
        {
            Destroy(staffItem[i]);
        }
    }
}
