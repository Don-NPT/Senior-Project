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
    [HideInInspector] public int finalReward;
    public int expense;
    public int skillPointReward;
    public int deadline;
    private int dayremain;
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
    [HideInInspector] public int[] dayUsedEachPhase;
    [HideInInspector] public int estimateDaysInPhase;
    [HideInInspector] public DateTime[] startDates = new DateTime[6];
    [HideInInspector] public DateTime[] finishDates = new DateTime[6];
    [HideInInspector] public DateTime[] currentDates = new DateTime[6];
    [HideInInspector] public List<string> requirement1Answer;
    [HideInInspector] public int requirement1Point;
    [HideInInspector] public List<string> requirement2Answer;
    [HideInInspector] public float requirement2Point;
    [HideInInspector] public List<bool> designAnswer;
    [HideInInspector] public float designPoint;
    [HideInInspector] public List<bool> keyInputPass;
    [HideInInspector] public float keyInputPoint;
    [HideInInspector] public float balloonPoint;
    [HideInInspector] public List<string> balloonAnswer;
    [HideInInspector] public float balloon2Point;
    [HideInInspector] public List<string> balloon2Answer;

    // Properties for Agile gameplay
    public List<SprintTask> sprintList;
    [HideInInspector] public string PO_id;

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
    public int getTimeRemain(){
        int result = 0;
        for (int i=0; i<6 ; i++)
        {
            result += (finishDates[i] - currentDates[i]).Days;
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

[System.Serializable]
public class OldProject {
    public int projectId;
    public string pjName;
    public string description;
    public int reward;
    public int finalReward;
    public int expense;
    public int skillPointReward;
    public int deadline;
    public int scale;
    public int requireAnalysis;
    public int requireDesign;
    public int requireCoding;
    public int requireTesting;
    public int requireDeployment;
    public Word[] requirement1;
    public Word[] requirement2;
    public Word[] balloons;
    public Keyword[] keyInput;
    public int smallTasks;
    public int mediumTasks;
    public int largeTasks;
    public int giantTasks;
    public int actualAnalysis;
    public int actualDesign;
    public int actualCoding;
    public int actualTesting;
    public int actualDeployment;
    public SDLCModel model;
    public int[] staffEachPhase;
    public int[] statEachPhase;
    public int dayUsed;
    public int[] dayUsedEachPhase;
    public List<string> requirement1Answer;
    public int requirement1Point;
    public List<string> requirement2Answer;
    public float requirement2Point;
    public List<bool> designAnswer;
    public float designPoint;
    public List<bool> keyInputPass;
    public float keyInputPoint;
    public float balloonPoint;
    public List<string> balloonAnswer;
    public float balloon2Point;
    public List<string> balloon2Answer;

    // Properties for Agile gameplay
    public List<SprintTask> sprintList;
    [HideInInspector] public string PO_id;

    public OldProject(Project project){
        projectId = project.projectId;
        pjName = project.pjName;
        description = project.description;
        reward = project.reward;
        finalReward = project.finalReward;
        expense = project.expense;
        skillPointReward = project.skillPointReward;
        deadline = project.deadline;
        scale = project.scale;
        requireAnalysis = project.requireAnalysis;
        requireDesign = project.requireDesign;
        requireCoding = project.requireCoding;
        requireTesting = project.requireTesting;
        requireDeployment = project.requireDeployment;
        requirement1 = project.requirement1;
        requirement2 = project.requirement2;
        balloons = project.balloons;
        keyInput = project.keyInput;
        smallTasks = project.smallTasks;
        mediumTasks = project.mediumTasks;
        largeTasks = project.largeTasks;
        giantTasks = project.giantTasks;
        actualAnalysis = project.actualAnalysis;
        actualDesign = project.actualDesign;
        actualCoding = project.actualCoding;
        actualTesting = project.actualTesting;
        actualDeployment = project.actualDeployment;
        model = project.model;
        staffEachPhase = project.staffEachPhase;
        statEachPhase = project.statEachPhase;
        dayUsed = project.dayUsed;
        dayUsedEachPhase = project.dayUsedEachPhase;
        requirement1Answer = project.requirement1Answer;
        requirement1Point = project.requirement1Point;
        requirement2Answer = project.requirement2Answer;
        requirement2Point = project.requirement2Point;
        designAnswer = project.designAnswer;
        designPoint = project.designPoint;
        keyInputPass = project.keyInputPass;
        keyInputPoint = project.keyInputPoint;
        balloonPoint = project.balloonPoint;
        balloonAnswer = project.balloonAnswer;
        balloon2Point = project.balloon2Point;
        balloon2Answer = project.balloon2Answer;
        sprintList = project.sprintList;
        PO_id = project.PO_id;
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
}
