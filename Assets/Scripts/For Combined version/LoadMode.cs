using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadMode : MonoBehaviour {
	private float gazeTime = 7.0f;

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

        if (gazeAt)
        {
            if (modeName.Contains("End"))
            {
                timer += Time.deltaTime;
            }
            else
            {
                timer += 10.0f * Time.deltaTime;
            }

            if (timer >= gazeTime)
            {
                if (modeName.Contains("Tour"))
                {
                    isGameMode = false;
                }
                else if (modeName.Contains("End"))
                {
                    timer = timer - gazeTime;
                    gazeAt = false;
                    MainMallManager.Instance.returnToMainMenu(modeName);

                    return;
                }
                else
                {
                    isGameMode = true;
                }
                timer = timer - gazeTime;
                gazeAt = false;
                MainMallManager.CurrentState = MainMallManager.GameStates.Game;
                MainMallManager.switched = true;
                MainMallManager.Instance.loadMode(isGameMode);
            }

        }
    }
	

    // Called automatically when pointer detects an object
	public void PointerEnter() {
        //Debug.Log("Pointer entered");
        gazeAt = true;
		modeName = this.name;
    }

    // Called automatically when pointer exits an object
    public void PointerExit()
    {
        //Debug.Log("Pointe exited");
        gazeAt = false;
    }
}
