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
    }

    public void LoadGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        GlobalVariable.isLoad = true;
        // StartCoroutine(LoadSave());
        // GameObject.Find("GameManager").GetComponent<SaveSystem>().Load();
        // FindObjectOfType<SaveSystem>().Load();
        // Debug.Log(GameObject.Find("GameManager"));
    }

    // IEnumerator LoadSave(){
    //     yield return new WaitForSeconds(1);
    //     // FindObjectOfType<SaveSystem>().Load();\
    //     Debug.Log(GameObject.Find("GameManager"));
    // }
}
