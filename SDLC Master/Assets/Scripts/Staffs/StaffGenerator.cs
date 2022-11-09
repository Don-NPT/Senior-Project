using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class StaffGenerator : MonoBehaviour
{
    public GameObject leftContent;
    public GameObject rightContent;

    TextMeshProUGUI[] rightText;
    TextMeshProUGUI[] leftText;
    Text[] info;

    Staff[] tempStaff;
    string[] firstnames = {"สมศักด์", "เพ็นศรี", "พบทอง", "ตุ๊ตู่", "แดง", "ณโดช", "ยิ่งศักดิ์", "ปังปอนด์", "กล้วยไม้"};
    string[] positions = {"Analyst", "Designer", "Programmer", "Tester"};
    public int index = 0;
    public GameObject staffPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Update right content according to index
        if(index >= 0)
        {
            rightText = rightContent.GetComponentsInChildren<TextMeshProUGUI>();
            rightText[0].text = tempStaff[index].fname;
            rightText[1].text = "Coding: " + tempStaff[index].coding.ToString();
            rightText[2].text = "Design: " + tempStaff[index].design.ToString();
            rightText[3].text = "Social: " + tempStaff[index].testing.ToString();
            rightText[4].text = "Analysis: " + tempStaff[index].analysis.ToString();
            rightText[5].text = "ตำแหน่ง: " + tempStaff[index].position.ToString();
            rightText[6].text = "ค่าจ้าง: " + tempStaff[index].wage.ToString() + " บาท/เดือน";
        }
        
    
    }

    private void OnEnable() {

        // Enable staff list
        Transform staffItem1 = leftContent.transform.GetChild(0);
        Transform staffItem2 = leftContent.transform.GetChild(1);
        Transform staffItem3 = leftContent.transform.GetChild(2);
        staffItem1.gameObject.SetActive(true);
        staffItem2.gameObject.SetActive(true);
        staffItem3.gameObject.SetActive(true);

        index = 0;
        tempStaff = new Staff[3];
        
        // Generate random stat for 3 staffs
        for(int i=0;i<3;i++)
        {
            tempStaff[i] = new Staff();
            tempStaff[i].fname = firstnames[Random.Range(0, 6)];
            tempStaff[i].coding = Random.Range(1, 20);
            tempStaff[i].design = Random.Range(1, 20);
            tempStaff[i].testing = Random.Range(1, 20);
            tempStaff[i].analysis = Random.Range(1, 20);
            tempStaff[i].position = positions[Random.Range(0, 4)];
            tempStaff[i].wage = Random.Range(1000, 5000);
        }
        
        // Show generated staff on the left side content
        leftText = leftContent.GetComponentsInChildren<TextMeshProUGUI>();
        leftText[0].text = tempStaff[0].fname;
        leftText[1].text = tempStaff[1].fname;
        leftText[2].text = tempStaff[2].fname;
        
    }

    public void HireStaff()
    {
        if(index >= 0)
        {
            // Create staff as GameObject outside of the screen
            GameObject staff = (GameObject)Instantiate(staffPrefab, new Vector3(5000,5000,5000), Quaternion.identity);

            // Tranfer the generated stat to the new staff
            staff.GetComponent<StaffProperties>().fname = tempStaff[index].fname;
            staff.GetComponent<StaffProperties>().coding = tempStaff[index].coding;
            staff.GetComponent<StaffProperties>().design = tempStaff[index].design;
            staff.GetComponent<StaffProperties>().testing = tempStaff[index].testing;
            staff.GetComponent<StaffProperties>().analysis = tempStaff[index].analysis;
            staff.GetComponent<StaffProperties>().position = tempStaff[index].position;
            staff.GetComponent<StaffProperties>().wage = tempStaff[index].wage;

            // Disable the selected item on the left content
            Transform staffItem = leftContent.transform.GetChild(index);
            staffItem.gameObject.SetActive(false);

            index = -1;
        }
    }

    public void SetIndex(int i)
    {
        index = i;
    }
}
