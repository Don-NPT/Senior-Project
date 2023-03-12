using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    public void Save()
    {
        // FileHandler.SaveToJSON<TimeManager> (TimeManager.instance, "timesave.json");   
        TimeManager.instance.Save(); 
        GameManager.instance.Save(); 
        StaffManager.instance.Save();
    }

    public void Load()
    {
        // var dataToLoad = FileHandler.ReadFromJSON<T> ("timesave.json");
        // TimeManager.instance.Load(dataToLoad);
        TimeManager.instance.Load();
        GameManager.instance.Load(); 
        StaffManager.instance.Load();

        GameManager.instance.Resume();
    }
}
