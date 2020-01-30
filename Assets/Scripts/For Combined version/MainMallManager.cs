using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMallManager : MonoBehaviour {
    // Game related variables
	[SerializeField] private GameObject gameMode;
	[SerializeField] private GameObject endScene;

    // Tour related variables
	[SerializeField] private GameObject tourMode;

	[SerializeField] private GameObject baseLevel;
	[SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject cardboardDetectionMenu;

    private static MainMallManager instance;
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject Collectables;
    private Vector3 playerPos;

    // Game state related variables
    public enum GameStates
    {
        WaitingForRotation,
        Main,
        Game
    }
    public static GameStates CurrentState = GameStates.WaitingForRotation;
    public static bool switched = false;

    // List of Eatten objects
    private List<GameObject> eatten = new List<GameObject>();

    public static MainMallManager Instance
    {
        get { return instance; }
    }

    private void Start()
    {
        playerPos = new Vector3(Player.transform.position.x, Player.transform.position.y, Player.transform.position.z);
    }

    void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

    }

    // Every frame, check the current game state and whether we have just switched from our last game state
    private void Update()
    {
        switch (CurrentState)
        {
            case GameStates.WaitingForRotation:
                //If rotation change currentstate to main
                if (switched)
                {
                    switched = false;
                }
                break;

            case GameStates.Main:
                if (switched)
                {
                    cardboardDetectionMenu.SetActive(false);
                    Destroy(cardboardDetectionMenu);
                    mainMenu.SetActive(true);
                    switched = false;
                }
                break;

            case GameStates.Game:
                if (switched)
                {
                    switched = false;
                }
                break;
        }
    }

    public void loadMode(bool isGameMode){
		mainMenu.SetActive(false);		
		baseLevel.SetActive(true);

		if (isGameMode){
			gameMode.SetActive(true);
            Player.transform.position = playerPos;
            TimerScript.Instance.ResetTime();
		}
		else{
			tourMode.SetActive(true);
		}
	}

    public void returnToMainMenu(string name){
        if (name == "End")
            endScene.SetActive(false);
            
        mainMenu.SetActive(true);
    }

    // Game related functions
    public void isOnTime(bool onTime)
    {
        foreach (GameObject obj in eatten)
        {
            obj.SetActive(true);
        }

        baseLevel.SetActive(false);
        gameMode.SetActive(false);
        mainMenu.SetActive(false);
        endScene.SetActive(true);


        endScene.GetComponent<EndSceneManager>().loadEndScene(onTime);
    }

    //Add Eaten objects to list
    public void addEatten(GameObject obj) {
        eatten.Add(obj);
    }
    // Tour related functions
}
