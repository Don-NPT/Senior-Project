using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Project", menuName = "ScriptableObject/Project", order = 2)]
public class Project : ScriptableObject
{
    public string pjName;
    public string description;
    public int requireAnalysis;
    public int requireDesign;
    public int requireCoding;
    public int requireTesting;
    public int requireDeployment;
    public int reward;
    public int deadline;
}
