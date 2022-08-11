using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Buildable", menuName = "ScriptableObject/Buildable", order = 1)]
public class BuildPreset : ScriptableObject
{
    public string buildName;
    public GameObject prefab;
    public GameObject previewPrefab;
    public Material material;
    public Mesh mesh;

    public int price;
}
