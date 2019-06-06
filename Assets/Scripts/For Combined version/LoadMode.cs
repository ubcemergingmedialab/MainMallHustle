using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadMode : MonoBehaviour {
	public float gazeTime = 5000f;

    private float timer;
    private bool gazeAt;

	private bool isGameMode = true;
	private string modeName;
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButton("Fire1")) {
            modeName = this.name;
            gazeAt = modeName.Contains("End") || gazeAt;
        }

        if (gazeAt) {
            Debug.Log("Gazeat activated");
            timer += Time.deltaTime;
            if (timer >= gazeTime) {
                if (modeName.Contains("Tour")){
					isGameMode = false;
				}
				else if (modeName.Contains("End")){
                    timer = 0;
                    gazeAt = false;
                    MainMallManager.Instance.returnToMainMenu(modeName);
                    
					return;
				}
				else{
					isGameMode = true;
				}
                timer = 0;
                gazeAt = false;
                MainMallManager.CurrentState = MainMallManager.GameStates.Game;
                MainMallManager.switched = true;
				MainMallManager.Instance.loadMode(isGameMode);
            }
	    }
    }
	


	public void PointerEnter() {
        Debug.Log("Pointer entered");
        gazeAt = true;
		modeName = this.name;
    }

    public void PointerExit()
    {
        Debug.Log("Pointe exited");
        gazeAt = false;
    }
}
