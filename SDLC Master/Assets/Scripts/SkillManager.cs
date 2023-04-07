using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;

    

    private void Awake() {
        if (instance != null && instance != this) 
            Destroy(this); 
        else 
            instance = this; 
    }
}
