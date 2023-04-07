using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tooltip : MonoBehaviour
{
    public string message;

    private void OnMouseEnter(){
        Debug.Log("ใช้ได้นะ");
        TooltipManager._instance.SetAndShowToolTip(message);
    }

    private void OnMouseExit() {
        TooltipManager._instance.HideToolTip();
    }
}
