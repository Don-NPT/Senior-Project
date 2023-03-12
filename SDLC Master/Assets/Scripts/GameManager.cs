using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using DG.Tweening;

public enum GameState {PLAY, PAUSE, FASTFORWARD}
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private int money;
    public TextMeshProUGUI moneyPrefab;
    public GameState gameState;
    public GameObject pauseScreen;
    public GameObject staffToAssign;
    public bool panelOpen = false;
    public Transform canvasTransform;
    public GameObject moneyNotificationPrefab;
    int previousDay;
    // Start is called before the first frame update
    void Start()
    {
        // If there is an instance, and it's not me, delete myself.
        if (instance != null && instance != this) 
            Destroy(this); 
        else 
            instance = this; 

        money = 10000;

        gameState = GameState.PLAY;
        previousDay = 1;
        
    }

    // Update is called once per frame
    void Update()
    {
        moneyPrefab.text = money.ToString();

        if(Input.GetButtonDown("Cancel"))
        {
            if(panelOpen == false)
            {
                if(gameState == GameState.PLAY) Pause();
                else Resume();
            }else{
                panelOpen = false;
            }
        }
    }

    private void FixedUpdate() {
        if (previousDay != TimeManager.instance.currentDate.Day) {
            previousDay = TimeManager.instance.currentDate.Day;

            // If it's the 1st day of the month, call the PaySalary method
            if (previousDay == 1) {
                PaySalary();
            }
        }
    }

    void PaySalary()
    {
        int totalSalary = 0;
        GameObject[] staffs = GameObject.FindGameObjectsWithTag("Staff");
        foreach(var staff in staffs)
        {
            totalSalary += staff.GetComponent<StaffProperties>().wage;
        }
        AddMoney(-totalSalary);
    }

    public int getMoney()
    {
        return money;
    }

    public void AddMoney(int num)
    {
        money += num;
        moneyPrefab.transform.DOPunchScale (new Vector3 (0.2f, 0.2f, 0.2f), .25f);

        GameObject moneyNotification = (GameObject)Instantiate(moneyNotificationPrefab);
        moneyNotification.transform.SetParent(canvasTransform);
        moneyNotification.transform.localScale = Vector3.one;
        moneyNotification.transform.position = moneyPrefab.transform.position + (Vector3.up * 50);
        Destroy(moneyNotification, 3f);
    }

    public void Pause()
    {
        Time.timeScale = 0;
        gameState = GameState.PAUSE;
        //pauseScreen.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1;
        gameState = GameState.PLAY;
        pauseScreen.SetActive(false);
    }

    public void Stop()
    {
        Time.timeScale = 0;
        gameState = GameState.PAUSE;
    }

    public void Play()
    {
        Time.timeScale = 1;
        gameState = GameState.PLAY;
    }

    public void FastForward()
    {
        Time.timeScale = 5;
        gameState = GameState.FASTFORWARD;
    }

    public void Save()
    {
        GameAdapter gameAdapter = new GameAdapter();
        gameAdapter.Save(money);
    }

    public void Load()
    {
        GameAdapter gameAdapter = new GameAdapter();
        GameAdapter saveData = gameAdapter.Load();
        money = saveData.money;
    }
}

[Serializable]
public class GameAdapter
{
    public int money;

    public void Save(int _money)
    {
        money = _money;
        FileHandler.SaveToJSON<GameAdapter> (this, "gamesave.json");   
    }

    public GameAdapter Load()
    {
        GameAdapter dataToLoad = FileHandler.ReadFromJSON<GameAdapter> ("gamesave.json");
        return dataToLoad;
    }
}
