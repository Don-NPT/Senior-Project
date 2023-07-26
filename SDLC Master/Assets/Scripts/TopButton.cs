using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TopButton : MonoBehaviour
{
    public Color normalColor;
    public Color selectedColor;
    List<GameObject> buttons;
    // Start is called before the first frame update
    void Awake()
    {
        buttons = new List<GameObject>();
        foreach (Transform child in transform)
        {
            GameObject childObject = child.gameObject;
            buttons.Add(childObject);
            Debug.Log("test");
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(GameManager.instance.gameState == GameState.PLAY){
            buttons[0].GetComponentsInChildren<Image>()[0].color = normalColor;
            buttons[1].GetComponentsInChildren<Image>()[0].color = selectedColor;
            buttons[2].GetComponentsInChildren<Image>()[0].color = normalColor;

        }else if(GameManager.instance.gameState == GameState.PAUSE){
            buttons[0].GetComponentInChildren<Image>().color = selectedColor;
            buttons[1].GetComponentInChildren<Image>().color = normalColor;
            buttons[2].GetComponentInChildren<Image>().color = normalColor;

        }else if(GameManager.instance.gameState == GameState.FASTFORWARD){
            buttons[0].GetComponentInChildren<Image>().color = normalColor;
            buttons[1].GetComponentInChildren<Image>().color = normalColor;
            buttons[2].GetComponentInChildren<Image>().color = selectedColor;

        }
    }
}
