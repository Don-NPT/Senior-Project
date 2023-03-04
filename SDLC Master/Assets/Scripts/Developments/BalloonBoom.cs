using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BalloonBoom : MonoBehaviour
{
    public static BalloonBoom instance;
    public GameObject balloonPrefab;
    public Transform canvas;
    public bool isStarted = false;
    private Coroutine spawnBalloon;
    GameObject[] balloons;
    // Start is called before the first frame update
    void Start()
    {
        // If there is an instance, and it's not me, delete myself.
        if (instance != null && instance != this) 
            Destroy(this); 
        else 
            instance = this;
 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Initiate() {
        if(ProjectManager.instance.currentProject != null){
            // if(ProjectManager.instance.currentProject.phase == Project.Phases.CODING && isStarted == false)
            // {
                // isStarted = true;
                Debug.Log("Starting Balloon Boom");
                ProjectManager.instance.currentProject.balloonPoint = 0;
                spawnBalloon = StartCoroutine(SpawnBalloon());
            // }
            // else if(ProjectManager.instance.currentProject.phase == Project.Phases.TESTING)
            // {
            //     isStarted = false;
            //     Debug.Log("Stopping Balloon Boom");
            //     StopCoroutine(SpawnBalloon());
            //     foreach (var balloon in balloons)
            //     {
            //         if(balloon.gameObject)
            //             Destroy(balloon.gameObject);
            //     }
            // }
        }
        
    }
    public void Stop() {
        // isStarted = false;
        if(spawnBalloon != null)
        {
            Debug.Log("Stopping Balloon Boom");
            StopCoroutine(spawnBalloon);
            foreach (var balloon in balloons)
            {
                Destroy(balloon);
            }
        }
    }

    IEnumerator SpawnBalloon()
    {
        balloons = new GameObject[30];

        for(int i=0; i<30; i++)
        {
            float xPos = Random.Range(0, Screen.width);
            float yPos = 0;
            Vector3 spawnPosition = new Vector3(xPos, yPos, 0f);
            balloons[i] = (GameObject) Instantiate(balloonPrefab, spawnPosition, Quaternion.identity);
            balloons[i].transform.SetParent(canvas.transform);
            balloons[i].transform.position = spawnPosition;

            // Set text
            int index = Random.Range(0, ProjectManager.instance.currentProject.balloons.Length-1);
            balloons[i].GetComponentInChildren<TextMeshProUGUI>().text = ProjectManager.instance.currentProject.balloons[index].word;
            balloons[i].GetComponent<Balloon>().index = index;
            yield return new WaitForSeconds(1);
        }
        
    }
}
