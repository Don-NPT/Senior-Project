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
    GameObject[] balloons = new GameObject[10];
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
        
        
    }
    private void FixedUpdate() {
        // if(DevelopmentManager.instance.currentPhase == Project.Phases.CODING && isStarted == false)
        // {
        //     isStarted = true;
        //     Debug.Log("Starting Balloon Boom");
        //     StartCoroutine(SpawnBalloon());
        // }
        // else if(DevelopmentManager.instance.currentPhase == Project.Phases.TESTING)
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

    IEnumerator SpawnBalloon()
    {
        for(int i=0; i<10; i++)
        {
            float xPos = Random.Range(0, Screen.width);
            float yPos = 0;
            Vector3 spawnPosition = new Vector3(xPos, yPos, 0f);
            balloons[i] = (GameObject) Instantiate(balloonPrefab, spawnPosition, Quaternion.identity);
            balloons[i].transform.SetParent(canvas.transform);
            balloons[i].transform.position = spawnPosition;

            // Set text
            int index = Random.Range(0, ProjectManager.instance.currentProjects[0].balloons.Length-1);
            balloons[i].GetComponentInChildren<TextMeshProUGUI>().text = ProjectManager.instance.currentProjects[0].balloons[index].word;
            balloons[i].GetComponent<Balloon>().index = index;
            yield return new WaitForSeconds(1);
        }
        
    }
}
