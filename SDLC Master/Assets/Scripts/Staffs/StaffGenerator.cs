using System;
using System.Collections;
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
    string[] firstnames = {"สมศักด์", "เพ็นศรี", "พบทอง", "ตุ๊ตู่", "แดง", "ณโดช", "ยิ่งศักดิ์", "ปังปอนด์", "กล้วยไม้", "แกง", "ธอร์", "โลกิ", "ยุงบินวน"};
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
            
           int abilityIndex = -1;
            if (tempStaff[index].position.ToString() == "Analyst")
                abilityIndex = 0;
            else if (tempStaff[index].position.ToString() == "Designer")
                abilityIndex = 1;
            else if (tempStaff[index].position.ToString() == "Programmer")
                abilityIndex = 2;
            else if (tempStaff[index].position.ToString() == "Tester")
                abilityIndex = 3;
            
            switch (abilityIndex)
            {
                case 0:
                    rightText[1].text = "ความสามารถ : " + tempStaff[index].analysis.ToString();
                    break;
                case 1:
                    rightText[1].text = "ความสามารถ : " + tempStaff[index].design.ToString();
                    break;
                case 2:
                    rightText[1].text = "ความสามารถ : " + tempStaff[index].coding.ToString();
                    break;
                case 3:
                    rightText[1].text = "ความสามารถ : " + tempStaff[index].testing.ToString();
                    break;
                default:
                    rightText[1].text = "ความสามารถ : ? ";
                    break;
            }
            
            rightText[2].text = "ตำแหน่ง: " + tempStaff[index].position.ToString();
            rightText[3].text = "ค่าจ้าง: " + tempStaff[index].wage.ToString() + " บาท/เดือน";
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
            tempStaff[i].id = Guid.NewGuid().ToString();
            tempStaff[i].fname = firstnames[UnityEngine.Random.Range(0, 9)];
            while(CheckStaffName(tempStaff[i].fname) == false)
            {
                tempStaff[i].fname = firstnames[UnityEngine.Random.Range(0, 9)];
            }
            tempStaff[i].coding = UnityEngine.Random.Range(1, 10);
            tempStaff[i].design = UnityEngine.Random.Range(1, 10);
            tempStaff[i].testing = UnityEngine.Random.Range(1, 10);
            tempStaff[i].analysis = UnityEngine.Random.Range(1, 10);
            tempStaff[i].position = positions[UnityEngine.Random.Range(0, 4)];
            tempStaff[i].wage = UnityEngine.Random.Range(1000, 5000);
            tempStaff[i].stamina = 100;
        }
        
        // Show generated staff on the left side content
        leftText = leftContent.GetComponentsInChildren<TextMeshProUGUI>();
        leftText[0].text = tempStaff[0].fname;
        leftText[1].text = tempStaff[1].fname;
        leftText[2].text = tempStaff[2].fname;
        
    }

    public void HireStaff()
    {
        bool canHire = true;
        if(StaffManager.instance.getTotalStaff() >= StaffManager.instance.maxStaff[GameManager.instance.GetLevel()-1]){
            GameManager.instance.ToggleNotification("จำนวนพนักงานถึงขีดจำกัดแล้ว (สามารถเพิ่มเลเวลเพื่อเพิ่มขีดจำกัดได้)");
            canHire = false;
        }

        if(index >= 0 && canHire)
        {
            // Create staff as GameObject outside of the screen
            GameObject staff = (GameObject)Instantiate(staffPrefab, new Vector3(5000,5000,5000), Quaternion.identity);

            // Tranfer the generated stat to the new staff
            staff.GetComponent<StaffProperties>().id = tempStaff[index].id;
            staff.GetComponent<StaffProperties>().fname = tempStaff[index].fname;
            staff.GetComponent<StaffProperties>().coding = tempStaff[index].coding;
            staff.GetComponent<StaffProperties>().design = tempStaff[index].design;
            staff.GetComponent<StaffProperties>().testing = tempStaff[index].testing;
            staff.GetComponent<StaffProperties>().analysis = tempStaff[index].analysis;
            staff.GetComponent<StaffProperties>().position = tempStaff[index].position;
            staff.GetComponent<StaffProperties>().wage = tempStaff[index].wage;
            staff.GetComponent<StaffProperties>().stamina = tempStaff[index].stamina;

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

    bool CheckStaffName(string name)
    {
        bool result = true;
        GameObject[] staffs = GameObject.FindGameObjectsWithTag("Staff");
        for(int i=0; i<staffs.Length; i++)
        {
            if(staffs[i].GetComponent<StaffProperties>().fname == name)
            {
                result = false;
            }
        }
        return result;
    }
}
