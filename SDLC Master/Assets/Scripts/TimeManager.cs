using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System;
using System.Collections;

[Serializable]
public class TimeManager : MonoBehaviour
{
    public static TimeManager instance;
    public DateTime currentDate;
    private Coroutine dayCoroutine;
    private int date;
    private int monthIndex;
    private int year;

    public int dayCount;
    
    [SerializeField] int startDate = 1;
    [SerializeField] int startMonthIndex = 0;
    [SerializeField] int startYear = 2022;
    [SerializeField] TextMeshProUGUI datePrefab;
    [SerializeField] TextMeshProUGUI monthPrefab;
    [SerializeField] TextMeshProUGUI yearPrefab;
    private float datetime;
    private string[] months = {"Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"};
    
    private void Awake() {
        // If there is an instance, and it's not me, delete myself.
        if (instance != null && instance != this) 
            Destroy(this); 
        else 
            instance = this; 
    }
    void Start()
    {

        date = startDate;
        monthIndex = startMonthIndex;
        year = startYear;
        datePrefab.text = startDate.ToString();
        monthPrefab.text = "Jan";
        yearPrefab.text = "2022";
        datetime = date;

        currentDate = new DateTime(2022, 1, 1);
        dayCoroutine = StartCoroutine(IncreaseDay());
    }

    IEnumerator IncreaseDay()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            currentDate = currentDate.AddDays(1);
        }
    }

    void StartDayCoroutine()
    {
        if (dayCoroutine == null)
        {
            dayCoroutine = StartCoroutine(IncreaseDay());
        }
    }

    void StopDayCoroutine()
    {
        if (dayCoroutine != null)
        {
            StopCoroutine(dayCoroutine);
            dayCoroutine = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        datePrefab.text = currentDate.Day.ToString();
        monthPrefab.text = months[currentDate.Month-1].ToString();
        yearPrefab.text = currentDate.Year.ToString();

    }

    public void Play()
    {
        StartDayCoroutine();
        // GameManager.instance.gameState = GameState.PLAY;
        GameManager.instance.Play();
    }
    public void Pause()
    {
        StopDayCoroutine();
        GameManager.instance.gameState = GameState.PAUSE;
    }
    public void FastForward()
    {
        GameManager.instance.gameState = GameState.FASTFORWARD;
    }

    public void Save()
    {
        TimeAdapter timeAdapter = new TimeAdapter();
        timeAdapter.Save(currentDate.Day, currentDate.Month, currentDate.Year);
    }

    public void Load()
    {
        TimeAdapter timeAdapter = new TimeAdapter();
        TimeAdapter saveData = timeAdapter.Load();
        date = saveData.date;
        monthIndex = saveData.month;
        year = saveData.year;
        currentDate= new DateTime(saveData.year, saveData.month, saveData.date);

        datetime = date;
    }
}

[Serializable]
public class TimeAdapter
{
    public int date;
    public int month;
    public int year;

    public void Save(int _date, int _month, int _year)
    {
        date = _date;
        month = _month;
        year = _year;
        FileHandler.SaveToJSON<TimeAdapter> (this, "timesave.json");   
    }

    public TimeAdapter Load()
    {
        TimeAdapter dataToLoad = FileHandler.ReadFromJSON<TimeAdapter> ("timesave.json");
        return dataToLoad;
    }
}