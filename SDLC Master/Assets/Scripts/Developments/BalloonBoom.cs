using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BalloonBoom : MonoBehaviour
{
    public static BalloonBoom instance;
    public GameObject balloonDevPrefab;
    public GameObject balloonTestPrefab;
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

    public void InitiateBalloonDev() {
        if(ProjectManager.instance.currentProject != null){
            Debug.Log("Starting Development Balloon Boom");
            ProjectManager.instance.currentProject.balloonPoint = 0;
            ProjectManager.instance.currentProject.balloonAnswer = new List<string>();
            spawnBalloon = StartCoroutine(SpawnBalloonDev());
        }
        
    }

    public void InitiateBalloonTest() {
        if(ProjectManager.instance.currentProject != null){
            Debug.Log("Starting Testing Balloon Boom");
            ProjectManager.instance.currentProject.balloon2Point = 0;
            ProjectManager.instance.currentProject.balloon2Answer = new List<string>();
            spawnBalloon = StartCoroutine(SpawnBalloonTest());
        }
        
    }

    public void Stop() {
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

    IEnumerator SpawnBalloonDev()
    {
        balloons = new GameObject[30];

        for(int i=0; i<30; i++)
        {
            float xPos = Random.Range(0, Screen.width);
            float yPos = 0;
            Vector3 spawnPosition = new Vector3(xPos, yPos, 0f);
            balloons[i] = (GameObject) Instantiate(balloonDevPrefab, spawnPosition, Quaternion.identity);
            balloons[i].transform.SetParent(canvas.transform);
            balloons[i].transform.position = spawnPosition;

            // Set text
            int index = Random.Range(0, ProjectManager.instance.currentProject.balloons.Length-1);
            balloons[i].GetComponentInChildren<TextMeshProUGUI>().text = ProjectManager.instance.currentProject.balloons[index].word;
            balloons[i].GetComponent<Balloon>().index = index;
            balloons[i].GetComponent<Balloon>().isDev = true;
            yield return new WaitForSeconds(1);
        }
        
    }

    IEnumerator SpawnBalloonTest()
    {
        balloons = new GameObject[30];

        for(int i=0; i<30; i++)
        {
            float xPos = Random.Range(0, Screen.width);
            float yPos = 0;
            Vector3 spawnPosition = new Vector3(xPos, yPos, 0f);
            balloons[i] = (GameObject) Instantiate(balloonTestPrefab, spawnPosition, Quaternion.identity);
            balloons[i].transform.SetParent(canvas.transform);
            balloons[i].transform.position = spawnPosition;

            // Set text
            int index = Random.Range(0, ProjectManager.instance.currentProject.balloons.Length-1);
            balloons[i].GetComponentInChildren<TextMeshProUGUI>().text = ProjectManager.instance.currentProject.balloons[index].word;
            balloons[i].GetComponent<Balloon>().index = index;
            balloons[i].GetComponent<Balloon>().isDev = false;
            yield return new WaitForSeconds(1);
        }
        
    }
}
