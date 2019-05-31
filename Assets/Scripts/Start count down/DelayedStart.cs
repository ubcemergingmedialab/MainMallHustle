using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedStart : MonoBehaviour {

    // Use this for initialization
    public float timeNeedToPause;
    public GameObject timer;
	void Start () {
        timer.SetActive(false);
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
        timer.SetActive(true);
        Time.timeScale = 1;
    }

}
