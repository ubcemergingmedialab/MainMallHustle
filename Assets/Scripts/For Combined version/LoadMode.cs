using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadMode : MonoBehaviour {
	public float gazeTime = 2f;

    private float timer;
    private bool gazeAt;

	private bool isGameMode = false;
	private string modeName;
	
	// Update is called once per frame
	void Update () {
		if (gazeAt) {
            timer += Time.deltaTime;

			if (timer >= gazeTime) {
                if (modeName.Contains("Tour")){
					isGameMode = false;
				}
				else if (modeName.Contains("End")){	
					MainMallManager.Instance.returnToMainMenu(modeName);
					return;
				}
				else{
					isGameMode = true;
				}
				MainMallManager.Instance.loadMode(isGameMode);
            }
	    }
    }
	
	public void PointerEnter() {
        gazeAt = true;
		modeName = this.name;
    }

    public void PointerExit()
    {
        gazeAt = false;
    }
}
