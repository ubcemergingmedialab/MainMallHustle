using UnityEngine;
using UnityEngine.UI;

/*
 * Main Script handling updating the timer for the game +/-
 */
public class TimerScript : MonoBehaviour {

    public Text timerText;
    public Text addTimeText;
    public Text removeTimeText;

    public float Initialtime;
    public float timeMultiplier;

    private string minutesString;
    private string secondsString;
    private float minutes;
    private float seconds;
    private float removeTimeTextTime;
    private float addTimeTextTime;
    private float timeTextMax;

    public float timeBonus = 5f;
    public float timePenalty = 10f;

    private float time;

    private static TimerScript instance;
    public static TimerScript Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

    }

    // Use this for initialization
    void Start ()
    {
        timeTextMax = 1f;
        addTimeText.text = "";
        removeTimeText.text = "";
        resetTime();
	}

    public void resetTime() {
        time = Initialtime;
        SetTime();
    }
	
	// Update is called once per frame
	void Update ()
    {
        time -= Time.deltaTime * timeMultiplier;

        if (minutes <= 0 && seconds <= 0) // If time's up, end the game
        {
            GameOver();
        }
        else // Do the following if time isn't up and the player isn't in the colliding process
        {
            SetTime();
        }

        if (addTimeTextTime >= timeTextMax) // If the AddTimeTextTime duration is overdue, reset time and text
        {
            addTimeText.text = "";
            addTimeTextTime = 0f;
        }
        else // Else increase AddTimeTextTime duration
        {
            addTimeTextTime += Time.deltaTime;
        }

        if (removeTimeTextTime >= timeTextMax) // If the RemoveTimeTextTime duration is overdue, reset time and text
        {
            removeTimeText.text = "";
            removeTimeTextTime = 0f;
        }
        else // Else increase RemoveTextTimeText duration
        {
            removeTimeTextTime += Time.deltaTime;
        }
    }

    void GameOver() // Game over procedures
    {
        MainMallManager.Instance.isOnTime(false);
    }

    void SetTime() // Get minutes and seconds, and set them as text
    {
        minutes = Mathf.Floor(time / 60);
        seconds = Mathf.Floor(time % 60);

        minutesString = minutes.ToString("00");
        secondsString = seconds.ToString("00");

        timerText.text = minutesString + ":" + secondsString;
    }

    public void addBonus(){
        time += timeBonus;
        addTimeText.text = "+ " + timeBonus.ToString();
        addTimeTextTime = 0f;
        SetTime();
    }

    public void addPenalty(){
        time -= timePenalty;
        removeTimeText.text = "- " + timePenalty.ToString();
        removeTimeTextTime = 0f;    
        SetTime();
    }
}
