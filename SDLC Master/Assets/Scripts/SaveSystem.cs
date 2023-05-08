using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    public static SaveSystem instance;
    public GameObject UsernameForm;

    void Awake()
    {
        // If there is an instance, and it's not me, delete myself.
        if (instance != null && instance != this) 
            Destroy(this); 
        else 
            instance = this; 

    }

    private void Start() {
        if(ProjectManager.instance.CheckProjectInProgress()){
            SaveSystem.instance.Load();
        }
        else if(PlayerPrefs.GetInt("isLoad") == 1){
            SaveSystem.instance.Load();
            GlobalVariable.isLoad = false;
            UsernameForm.SetActive(false);
        }
    }


    public void Save()
    {
        // FileHandler.SaveToJSON<TimeManager> (TimeManager.instance, "timesave.json");   
        TimeManager.instance.Save(); 
        GameManager.instance.Save(); 
        StaffManager.instance.Save();
        ProjectManager.instance.Save();
        WaterFallManager.instance.Save();
        AgileManager.instance.Save();
        BuildManager.instance.Save();
        Debug.Log("Game Saved!");
    }

    public void Load()
    {
        // var dataToLoad = FileHandler.ReadFromJSON<T> ("timesave.json");
        // TimeManager.instance.Load(dataToLoad);
        TimeManager.instance.Load();
        GameManager.instance.Load();
        StaffManager.instance.Load();
        ProjectManager.instance.Load();
        WaterFallManager.instance.Load();
        AgileManager.instance.Load();
        BuildManager.instance.Load();
        Debug.Log("Game Loaded!");

        // GameManager.instance.Resume();
    }
}
