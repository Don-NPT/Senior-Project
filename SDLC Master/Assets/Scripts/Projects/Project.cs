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
    public int scale;
    public int estimateDaysInPhase;
    public float finishPoints;
    [HideInInspector]
    public float currentPoints;
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
    

}
