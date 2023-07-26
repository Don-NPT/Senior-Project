using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        GlobalVariable.isLoad = false;
        PlayerPrefs.SetInt("isLoad", 0);
    }

    public void LoadGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        GlobalVariable.isLoad = true;
        PlayerPrefs.SetInt("isLoad", 1);
    }

}
