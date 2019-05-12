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

    public static MainMallManager Instance
    {
        get { return instance; }
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
        baseLevel.SetActive(false);
        gameMode.SetActive(false);
        endScene.SetActive(true);

        endScene.GetComponent<EndSceneManager>().loadEndScene(onTime);
    }
    // Tour related functions
}
