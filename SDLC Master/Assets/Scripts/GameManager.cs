using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameState {PLAY, PAUSE}
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public List<Project> currentProjects;
    public int startDate = 1;
    private float date;
    public Text datePrefab;
    public Text monthPrefab;
    public Text yearPrefab;
    private string[] months = {"Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"};
    private int monthIndex = 0;
    private int money;
    public Text moneyPrefab;
    public GameState gameState;
    public GameObject pauseScreen;
    public GameObject staffToAssign;
    // Start is called before the first frame update
    void Start()
    {
        // If there is an instance, and it's not me, delete myself.
        if (instance != null && instance != this) 
            Destroy(this); 
        else 
            instance = this; 

        date = startDate;
        FindObjectOfType<AudioManager>().Play("BGM");
        money = 5000;

        gameState = GameState.PLAY;
        currentProjects = new List<Project>();
    }

    // Update is called once per frame
    void Update()
    {
        date += Time.deltaTime;
        datePrefab.text = Mathf.Round(date).ToString();
        monthPrefab.text = months[monthIndex].ToString();
        if(date > 30)
        {
            monthIndex += 1;
            date = 1;
        }
        if(monthIndex > 11)
        {
            date = 1;
            monthIndex = 0;
        }

        moneyPrefab.text = money.ToString();

        if(Input.GetButtonDown("Cancel"))
        {
            if(gameState == GameState.PLAY) Pause();
            else Resume();
        }
    }

    public int getMoney()
    {
        return money;
    }

    public void setMoney(int num)
    {
        money += num;
    }

    public void Pause()
    {
        Time.timeScale = 0;
        gameState = GameState.PAUSE;
        pauseScreen.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1;
        gameState = GameState.PLAY;
        pauseScreen.SetActive(false);
    }
}
