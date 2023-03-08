using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class SprintPlanning : MonoBehaviour
{
    Button button;
    public PlayableDirector director;
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponentInChildren<Button>();
        button.onClick.AddListener(() => StartCoroutine(ChangeScene()));
    }


    IEnumerator ChangeScene()
    {
        director.Play();   
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(2);
    }
}
