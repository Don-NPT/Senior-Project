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
    public int dayUsed;
    public int scale;
    public int estimateDaysInPhase;
    public int actualAnalysis;
    public int actualDesign;
    public int actualCoding;
    public int actualTesting;
    public int actualDeployment;
    [HideInInspector]
    public SDLCModel model;
    [HideInInspector]
    public int teamIndex;
    [HideInInspector]
    public enum Status {READY, DOING, PAUSE, FINISHED};
    [HideInInspector]
    public Status state;
    [HideInInspector]
    public enum Phases {ANALYSIS, DESIGN, CODING, TESTING, DEPLOYMENT};
    [HideInInspector]
    public Phases phase;
    
    public int getAllRequireQuality()
    {
        int result = requireAnalysis + requireDesign + requireCoding + requireTesting + requireDeployment;
        return result;
    }

    public int getAllActualQuality()
    {
        int result = actualAnalysis + actualDesign + actualCoding + actualTesting + actualDeployment;
        return result;
    }
}
