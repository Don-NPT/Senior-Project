using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SDLCModel", menuName = "ScriptableObject/SDLCModel", order = 2)]
public class SDLCModel : ScriptableObject
{
    public string modelName;
    public Sprite image;
    public string description;
}
