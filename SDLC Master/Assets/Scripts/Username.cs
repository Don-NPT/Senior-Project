using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Username : MonoBehaviour
{
    public TMP_InputField inputField;
    public GameObject UsernamePage;
    public TMP_Text MyUsername;

    // Update is called once per frame
    public void SaveUsername()
    {
        MyUsername.text = inputField.text + " lv." + GameManager.instance.GetLevel();

        UsernamePage.SetActive(false);
        GameManager.instance.username = inputField.text;
    }
}
