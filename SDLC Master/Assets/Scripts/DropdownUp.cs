using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropdownUp : MonoBehaviour
{
    [SerializeField] private GameObject list;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Press()
    {
        if(list.activeSelf){
            list.SetActive(false);
        }else{
            list.SetActive(true);
        }
    }
}
