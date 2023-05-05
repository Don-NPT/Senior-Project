using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class RecipeSO : ScriptableObject
{
    
    public List<KitchenObjectSO> kitchenObjectList;
    public string recipeName;

    public RecipeSO (List<KitchenObjectSO> list, string name){
        kitchenObjectList = list;
        recipeName = name;
    }

}

[System.Serializable]
public class SprintTask
{
    
    public List<Task> taskList;
    public string sprintName;

    public SprintTask (List<Task> list, string name){
        taskList = list;
        sprintName = name;
    }

    public int GetSumQuality(){
        int sum = 0;
        foreach(var task in taskList){
            sum += task.quality;
        }
        return sum;
    }

    public int GetSumRequireQuality(){
        int sum = 0;
        foreach(var task in taskList){
            sum += task.requireQuality;
        }
        return sum;
    }

    public int GetDuration(){
        int sum = 0;
        foreach(var task in taskList){
            sum += task.dayToFinish;
        }
        return sum;
    }

    public int GetNumFinishedTasks(){
        int sum = 0;
        foreach(var task in taskList){
            if(task.isComplete == true) sum += task.dayToFinish;
        }
        return sum;
    }

    public int GetNumAllTasks(){
        int sum = 0;
        foreach(var task in taskList){
            sum += task.dayToFinish;
        }
        return sum;
    }

}
