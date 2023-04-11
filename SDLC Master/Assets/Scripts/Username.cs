using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Username : MonoBehaviour
{
    public TMP_InputField inputField;
    public GameObject UsernamePage;
    public TMP_Text MyUsername;
        
    // Start is called before the first frame update
    void Start()
    {
        if(GameManager.instance.username == "" || GameManager.instance.username == null)
        {
            UsernamePage.SetActive(true);
        }else
        {

            MyUsername.text = GameManager.instance.username;

            UsernamePage.SetActive(false);
        }
    }

    // Update is called once per frame
    public void SaveUsername()
    {

        MyUsername.text = inputField.text;

        UsernamePage.SetActive(false);
        GameManager.instance.username = inputField.text;
    }
}
