using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ToprightUI : MonoBehaviour
{
    void FixedUpdate()
    {
        TextMeshProUGUI[] texts = GetComponentsInChildren<TextMeshProUGUI>();
    
        texts[0].text = StaffManager.instance.getSumStaffStat("Analyst").ToString();
        texts[1].text = StaffManager.instance.getSumStaffStat("Designer").ToString();
        texts[2].text = StaffManager.instance.getSumStaffStat("Programmer").ToString();
        texts[3].text = StaffManager.instance.getSumStaffStat("Tester").ToString();
    }
}
