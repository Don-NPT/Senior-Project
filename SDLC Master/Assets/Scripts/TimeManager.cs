using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System;
using System.Collections;

[Serializable]
public class TimeManager : MonoBehaviour
{
    public static TimeManager instance;
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
    
    // Start is called before the first frame update
    void Start()
    {
        // If there is an instance, and it's not me, delete myself.
        if (instance != null && instance != this) 
            Destroy(this); 
        else 
            instance = this; 

        date = startDate;
        monthIndex = startMonthIndex;
        year = startYear;
        datePrefab.text = startDate.ToString();
        monthPrefab.text = "Jan";
        yearPrefab.text = "2022";
        datetime = date;
    }

    // Update is called once per frame
    void Update()
    {
        datePrefab.text = date.ToString();
        monthPrefab.text = months[monthIndex].ToString();
        yearPrefab.text = year.ToString();

        if(GameManager.instance.gameState != GameState.PAUSE)
        {
            datetime += Time.deltaTime * 0.1f;
            date = (int) Mathf.Floor(datetime);
            // datePrefab.text = Mathf.Round(datetime).ToString();
            // monthPrefab.text = months[monthIndex].ToString();
            if(date > 30)
            {
                monthIndex += 1;
                date = 1;
                datetime = date;
            }
            if(monthIndex > 11)
            {
                date = 1;
                datetime = date;
                monthIndex = 0;
                year += 1;
            }
        }
    }

    // public IEnumerator dayTimer(int days){
    //     dayCount = 0;
    //     while (dayCount < days)
    //     {
    //         yield return new WaitForSeconds(1);
    //         dayCount++;
    //     }
    // }

    public void Play()
    {
        GameManager.instance.gameState = GameState.PLAY;
    }
    public void FastForward()
    {
        GameManager.instance.gameState = GameState.FASTFORWARD;
    }

    public void Save()
    {
        TimeAdapter timeAdapter = new TimeAdapter();
        timeAdapter.Save(date, monthIndex, year);
    }

    public void Load()
    {
        TimeAdapter timeAdapter = new TimeAdapter();
        TimeAdapter saveData = timeAdapter.Load();
        date = saveData.date;
        monthIndex = saveData.monthIndex;
        year = saveData.year;

        datetime = date;
    }
}

[Serializable]
public class TimeAdapter
{
    public int date;
    public int monthIndex;
    public int year;

    public void Save(int _date, int _monthIndex, int _year)
    {
        date = _date;
        monthIndex = _monthIndex;
        year = _year;
        FileHandler.SaveToJSON<TimeAdapter> (this, "timesave.json");   
    }

    public TimeAdapter Load()
    {
        TimeAdapter dataToLoad = FileHandler.ReadFromJSON<TimeAdapter> ("timesave.json");
        return dataToLoad;
    }
}