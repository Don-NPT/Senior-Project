using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum GameState {PLAY, PAUSE, FASTFORWARD}
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private int money;
    public TextMeshProUGUI moneyPrefab;
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

        FindObjectOfType<AudioManager>().Play("BGM");
        money = 5000;

        gameState = GameState.PLAY;
        
    }

    // Update is called once per frame
    void Update()
    {
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

    public void AddMoney(int num)
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
