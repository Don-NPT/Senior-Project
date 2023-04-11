using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    // public Transform canvasTransform;
    // public GameObject moneyNotificationPrefab;
    public GameObject moneyNotification;
    public string username;
    
    public Color[] colors;
    public GameObject notificationUI;
    public string username;
    int previousDay;
    
    private void Awake() {
        // If there is an instance, and it's not me, delete myself.
        if (instance != null && instance != this) 
            Destroy(this); 
        else 
            instance = this; 
    }
    void Start()
    {
        money = 100000;

        gameState = GameState.PLAY;
        previousDay = 1;
        username = "";
    }

    // Update is called once per frame
    void Update()
    {
        moneyPrefab.text = money.ToString("C0");

        if(Input.GetButtonDown("Cancel"))
        {
            if(panelOpen == false)
            {
                if(gameState == GameState.PLAY) {
                    Pause();
                    pauseScreen.SetActive(true);
                }
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

    public int PayTrainStaff()
    {
        money -= 10000;
        return money;
    }

    public void AddMoney(int num)
    {
        money += num;
        moneyPrefab.transform.DOPunchScale (new Vector3 (0.2f, 0.2f, 0.2f), .25f);

        StartCoroutine(ShowNotification(num));
    }

    IEnumerator ShowNotification(int num){
        moneyNotification.SetActive(true);

        if(num > 0) {
            moneyNotification.GetComponentInChildren<TextMeshProUGUI>().text = "+" + num;
            moneyNotification.GetComponent<Image>().color = GameManager.instance.colors[0];
        }
        else if(num < 0) {
            moneyNotification.GetComponentInChildren<TextMeshProUGUI>().text = num.ToString("C0");
            moneyNotification.GetComponent<Image>().color = GameManager.instance.colors[1];
        }
        yield return new WaitForSeconds(1);
        moneyNotification.SetActive(false);
    }

    public void ToggleNotification(string content){
        notificationUI.GetComponent<PanelOpener>().OpenPanelPunch();
        notificationUI.GetComponentsInChildren<TextMeshProUGUI>()[0].text = content;
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
