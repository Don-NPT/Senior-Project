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

}
