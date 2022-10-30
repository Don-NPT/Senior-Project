using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamManager2 : MonoBehaviour
{
    public static TeamManager2 instance;
    public List<StaffProperties>[] teams = new List<StaffProperties>[4];
    // Start is called before the first frame update
    void Start()
    {
        if (instance != null && instance != this) 
            Destroy(this); 
        else 
            instance = this; 

        teams[0] = new List<StaffProperties>();
        teams[1] = new List<StaffProperties>();
        teams[2] = new List<StaffProperties>();
        teams[3] = new List<StaffProperties>();
    }

    public int getSize(int index)
    {
        return teams[index].Count;
    }
}
