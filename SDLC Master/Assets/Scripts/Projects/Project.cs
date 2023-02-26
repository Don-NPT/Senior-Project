using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Project", menuName = "ScriptableObject/Project", order = 2)]
[System.Serializable]
public class Project : ScriptableObject
{
    [Header("Project Info")]
    public string projectId;
    public string pjName;
    public string description;
    public int reward;
    public int deadline;
    public int dayUsed;
    public int scale;
    public int estimateDaysInPhase;
    [Header("Work Amount")]
    public int analysisWork;
    public int designWork;
    public int codingWork;
    public int testingWork;
    public int deploymentWork;
    [Header("Actual")]
    public int actualAnalysis;
    public int actualDesign;
    public int actualCoding;
    public int actualTesting;
    public int actualDeployment;
    [Header("Words")]

    public Word[] requirement1 = new Word[6];
    public Word[] requirement2_new = new Word[4];
    public Word[] requirement2 = new Word[4];
    public Word[] balloons = new Word[5];
    public Keyword[] keyInput = new Keyword[2];
    
    [HideInInspector]
    public SDLCModel model;
    [HideInInspector]
    public int teamIndex;
    [HideInInspector]
    public enum Status {READY, DOING, PAUSE, FINISHED};
    [HideInInspector]
    public Status state;
    [HideInInspector]
    public enum Phases {ANALYSIS, DESIGN, CODING, TESTING, DEPLOYMENT, MAINTENANCE};
    [HideInInspector]
    public Phases phase;
    
    public int getAllWorkAmount()
    {
        int result = analysisWork + designWork + codingWork + testingWork + deploymentWork;
        return result;
    }

    public int getAllActualQuality()
    {
        int result = actualAnalysis + actualDesign + actualCoding + actualTesting + actualDeployment;
        return result;
    }

    public int getWorkAmountbyIndex(int index)
    {
        switch(index)
        {
            case 0:
                return analysisWork;
            case 1:
                return designWork;
            case 2:
                return codingWork;
            case 3:
                return testingWork;
            case 4:
                return deploymentWork;
        }
        return 0;
    }
    
}

[System.Serializable]
public class Word {
    public string word;
    public bool isCorrect;

    public Word(){
        word = "Sometext";
        isCorrect = true;
    }
}

[System.Serializable]
public class Keyword {
    public string word;
    public string hint;
    public int showNum;
    public char[] additionalChar;

    public Keyword(){
        word = "Sometext";
        hint = "description";
    }
}