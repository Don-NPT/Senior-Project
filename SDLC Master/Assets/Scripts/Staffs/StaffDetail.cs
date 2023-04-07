using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StaffDetail : MonoBehaviour
{
    public static StaffDetail instance;
    // // Start is called before the first frame update
    // void Awake()
    // {
    //     // If there is an instance, and it's not me, delete myself.
    //     if (instance != null && instance != this) 
    //         Destroy(this); 
    //     else 
    //         instance = this;
    // }

    // public void ShowDetail(StaffProperties staff){
    //     GetComponent<PanelOpener>().OpenPanelPunch();

    //     GetComponentsInChildren<TextMeshProUGUI>()[0].text = staff.fname;
    //     GetComponentsInChildren<TextMeshProUGUI>()[1].text = "ค่าสถานะ: " + staff.GetStaffStat() + " หน่วย";
    //     GetComponentsInChildren<TextMeshProUGUI>()[2].text = "ตำแหน่ง: " + staff.position;
    //     GetComponentsInChildren<TextMeshProUGUI>()[3].text = "ค่าจ้าง: " + staff.wage + " บาท";
    // }
}
