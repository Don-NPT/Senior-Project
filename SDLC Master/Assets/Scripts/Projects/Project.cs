using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Project", menuName = "ScriptableObject/Project", order = 2)]
[System.Serializable]
public class Project : ScriptableObject
{
    [Header("Project Info")]
    public int projectId;
    public string pjName;
    [TextArea(2, 10)]
    public string description;
    [TextArea(5, 10)]
    public string requirement;
    public int reward;
    public int expense;
    public int deadline;    
    public int scale;
    public bool inProgress;
    
    [Header("Work Amount")]
    public int analysisWork;
    public int designWork;
    public int codingWork;
    public int testingWork;
    public int deploymentWork;

    [Header("Require Quality")]
    public int requireAnalysis;
    public int requireDesign;
    public int requireCoding;
    public int requireTesting;
    public int requireDeployment;

    [Header("Words")]

    public Word[] requirement1 = new Word[6];
    public Word[] requirement2 = new Word[4];
    // public Word[] requirement2_new;
    public Word[] balloons = new Word[5];
    public Keyword[] keyInput = new Keyword[2];

    [Header("Agile Tasks")]
    public int smallTasks;
    public int mediumTasks;
    public int largeTasks;
    public int giantTasks;

    [HideInInspector] public int actualAnalysis;
    [HideInInspector] public int actualDesign;
    [HideInInspector] public int actualCoding;
    [HideInInspector] public int actualTesting;
    [HideInInspector] public int actualDeployment;

    [HideInInspector] public SDLCModel model;
    [HideInInspector] public int teamIndex;
    [HideInInspector] public enum Status {READY, DOING, PAUSE, FINISHED};
    [HideInInspector] public Status state;
    [HideInInspector] public enum Phases {ANALYSIS, DESIGN, CODING, TESTING, DEPLOYMENT, MAINTENANCE};
    [HideInInspector] public Phases phase;
    [HideInInspector] public int[] staffEachPhase;
    [HideInInspector] public int[] statEachPhase;
    [HideInInspector] public int dayUsed;
    [HideInInspector] public int estimateDaysInPhase;
    [HideInInspector] public DateTime[] startDates = new DateTime[6];
    [HideInInspector] public DateTime[] finishDates = new DateTime[6];
    [HideInInspector] public List<string> requirement1Answer;
    [HideInInspector] public int requirement1Point;
    [HideInInspector] public List<string> requirement2Answer;
    [HideInInspector] public int requirement2Point;
    [HideInInspector] public List<bool> designAnswer;
    [HideInInspector] public int designPoint;
    [HideInInspector] public List<bool> keyInputPass;
    [HideInInspector] public int keyInputPoint;
    [HideInInspector] public int balloonPoint;
    [HideInInspector] public List<string> balloonAnswer;
    [HideInInspector] public int balloon2Point;
    [HideInInspector] public List<string> balloon2Answer;

    // Properties for Agile gameplay
    [HideInInspector] public List<SprintTask> sprintList; 

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

    public int getAllRequireQuality()
    {
        int result = requireAnalysis + requireDesign + requireCoding + requireTesting + requireDeployment;
        return result;
    }

    public int getTimeUsed(int index)
    {
        int result = (finishDates[index] - startDates[index]).Days;
        return result;
    }

    public int getOverallTimeUsed()
    {
        int result = 0;
        for (int i=0; i<6 ; i++)
        {
            result += (finishDates[i] - startDates[i]).Days;
        }
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
