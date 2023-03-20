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
    
    public List<KitchenObjectSO> taskList;
    public string sprintName;

    public SprintTask (List<KitchenObjectSO> list, string name){
        taskList = list;
        sprintName = name;
    }

}
