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
    public int quality;
    public int requireQuality;
    public int dayUsed;

}
