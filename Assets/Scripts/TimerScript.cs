using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/*
 * Main Script handling updating the timer for the game +/-
 */
public class TimerScript : MonoBehaviour {

    public GameObject player2;
    // public GameObject canvas;

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

    private Coroutine removeBonusCo;
    private Coroutine removePenaltyCo;

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
        ResetTime();
	}

    public void ResetTime() {
        addTimeText.text = "";
        removeTimeText.text = "";
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
    }

    void GameOver() // Game over procedures
    {
        addTimeText.text = "";
        removeTimeText.text = "";
        MainMallManager.Instance.isOnTime(false);
        //
        player2.GetComponent<Rigidbody>().Sleep();
        //
        
        // gameObject.SetActive(false);
    }

    void SetTime() // Get minutes and seconds, and set them as text
    {
        minutes = Mathf.Floor(time / 60);
        seconds = Mathf.Floor(time % 60);

        minutesString = minutes.ToString("00");
        secondsString = seconds.ToString("00");

        timerText.text = minutesString + ":" + secondsString;
    }

    public void AddBonus(){
        time += timeBonus;
        addTimeText.text = "+ " + timeBonus.ToString();
        if (removeBonusCo != null)
        {
            StopCoroutine(removeBonusCo);
        }
        removeBonusCo = StartCoroutine(RemoveBonusText());
        SetTime();
    }

    public void AddPenalty(){
        time -= timePenalty;
        removeTimeText.text = "- " + timePenalty.ToString();
        if (removePenaltyCo != null)
        {
            StopCoroutine(removePenaltyCo);
        }
        removePenaltyCo = StartCoroutine(RemovePenaltyText());
        SetTime();
    }

    private IEnumerator RemovePenaltyText()
    {
        yield return new WaitForSeconds(timeTextMax);
        removeTimeText.text = "";
    }

    private IEnumerator RemoveBonusText()
    {
        yield return new WaitForSeconds(timeTextMax);
        addTimeText.text = "";
    }
}
