using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    public static SaveSystem instance;

    void Start()
    {
        // If there is an instance, and it's not me, delete myself.
        if (instance != null && instance != this) 
            Destroy(this); 
        else 
            instance = this; 

        if(ProjectManager.instance.CheckProjectInProgress()){
            SaveSystem.instance.Load();
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

        // GameManager.instance.Resume();
    }
}
