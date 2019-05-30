using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedStart : MonoBehaviour {

    // Use this for initialization
    public float timeNeedToPause;
    public GameObject countDown;
	void Start () {
        StartCoroutine("Delay");
	}
	
    public IEnumerator Delay()
    {
        Time.timeScale = 0;
        float pauseTime = Time.realtimeSinceStartup + timeNeedToPause;
        while (Time.realtimeSinceStartup < pauseTime)
        {
            yield return 0;
        }
        
        Time.timeScale = 1;
    }

}
