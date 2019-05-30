using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CountDown : MonoBehaviour {
    

    public GameObject StartCounter;
    public int maxTime;
    public string textAfterCountDown;
    // Use this for initialization
    void Start () {
       
       StartCoroutine(TimeCountDown(maxTime)); 
        
	}
	
	// Update is called once per frame
	void Update () {

    }

    public IEnumerator TimeCountDown(int maximumTime)
    {
        int time = maximumTime;
        while (time>0)
        {
            StartCounter.GetComponent<Text>().text = time.ToString();
            yield return new WaitForSecondsRealtime(1);
            time--;
        }
        StartCounter.GetComponent<Text>().text = textAfterCountDown;
        yield return new WaitForSecondsRealtime(1);
        StartCounter.GetComponent<Text>().text = " ";
        yield return 0;
    }
}
