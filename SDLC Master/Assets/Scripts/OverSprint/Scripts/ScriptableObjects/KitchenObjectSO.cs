using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu()]
[System.Serializable]
public class KitchenObjectSO : ScriptableObject
{
    
    public Transform prefeb;
    public Sprite sprite;
    public string objectName;
    public int dayToFinish;
    public int requireQuality;
    public int size;
    [HideInInspector] public int quality;
    [HideInInspector] public int dayUsed;
    [HideInInspector] public bool isComplete;
    [HideInInspector] public bool fromPreviousSprint;

}

[System.Serializable]
public class Task
{
    
    public Transform prefeb;
    public Sprite sprite;
    public string objectName;
    public int dayToFinish;
    public int requireQuality;
    public int size;
    [HideInInspector] public int quality;
    [HideInInspector] public int dayUsed;
    [HideInInspector] public bool isComplete;
    [HideInInspector] public bool fromPreviousSprint;

    public Task(Transform _prefab, Sprite _sprite, string _objectName, int _dayToFinish, int _requireQuality, int _size){
        prefeb = _prefab;
        sprite = _sprite;
        objectName = _objectName;
        dayToFinish = _dayToFinish;
        requireQuality = _requireQuality;
        size = _size;
    }

}

