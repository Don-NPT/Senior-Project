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
        if(PlayerPrefs.GetString("Username") == "" || PlayerPrefs.GetString("Username") == null)
        {
            UsernamePage.SetActive(true);
        }else
        {

            MyUsername.text = PlayerPrefs.GetString("Username");

            UsernamePage.SetActive(false);
        }
    }

    // Update is called once per frame
    public void SaveUsername()
    {

        PlayerPrefs.SetString("Username" , inputField.text);

        MyUsername.text = inputField.text;

        UsernamePage.SetActive(false);
        GameManager.instance.username = inputField.text;
    }
}