using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance;
    public int startDate = 1;
    private float date;
    public TextMeshProUGUI datePrefab;
    public TextMeshProUGUI monthPrefab;
    public TextMeshProUGUI yearPrefab;
    private string[] months = {"Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"};
    private int monthIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        // If there is an instance, and it's not me, delete myself.
        if (instance != null && instance != this) 
            Destroy(this); 
        else 
            instance = this; 

        date = startDate;
        datePrefab.text = startDate.ToString();
        monthPrefab.text = "Jan";
        yearPrefab.text = "2022";
    }

    // Update is called once per frame
    void Update()
    {   
        if(GameManager.instance.gameState == GameState.FASTFORWARD)
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
        }
    }

    public void Play()
    {
        GameManager.instance.gameState = GameState.PLAY;
    }
    public void FastForward()
    {
        GameManager.instance.gameState = GameState.FASTFORWARD;
    }
}
