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
    private static MainMallManager instance;
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject Collectables;
    private Vector3 playerPos;

    public List<GameObject> eatten = new List<GameObject>();

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
	public void loadMode(bool isGameMode){
		mainMenu.SetActive(false);		
		baseLevel.SetActive(true);

		if (isGameMode){
			gameMode.SetActive(true);
            Player.transform.position = playerPos;
            TimerScript.Instance.resetTime();
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
