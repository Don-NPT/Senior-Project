using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    private bool hasOpen = false;

    public bool Checktutorial() {
        if(hasOpen==false){
          return true;
        }else{
            return false;
        }
    }
    public void Open(){
        GameManager.instance.Pause();
        gameObject.SetActive(true);
        hasOpen=true;
    }

    private void OnDisable() {
        GameManager.instance.Play();
    }

}
