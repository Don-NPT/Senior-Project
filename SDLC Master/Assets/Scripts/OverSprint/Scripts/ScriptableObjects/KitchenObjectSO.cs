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
    [HideInInspector] public int quality;
    [HideInInspector] public int dayUsed;
    [HideInInspector] public bool isComplete;

}
