using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropdownUp : MonoBehaviour
{
    [SerializeField] private GameObject list;
    
    public void Press()
    {
        if(list.activeSelf){
            list.SetActive(false);
        }else{
            list.SetActive(true);
        }
    }
}
