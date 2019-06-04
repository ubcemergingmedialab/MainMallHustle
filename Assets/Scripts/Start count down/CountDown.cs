using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CountDown : MonoBehaviour {
    

    public GameObject StartCounter;
    public GameObject backgroundSound;
    public int maxTime;
    public string textAfterCountDown;

    
    public AudioClip BeepForSecondClip;
    public AudioClip BeepForGoClip;
    
    // Use this for initialization
    void Start () {
       
       StartCoroutine(TimeCountDown(maxTime)); 
        
	}

    public IEnumerator TimeCountDown(int maximumTime)
    {   
        int time = maximumTime;
        AudioSource countDownAudios = backgroundSound.GetComponent<AudioSource>();

        while (time>0)
        {
            
            StartCounter.GetComponent<Text>().text = time.ToString();
            countDownAudios.clip = BeepForSecondClip;
            countDownAudios.Play();
            yield return new WaitForSecondsRealtime(1);
            time--;
        }
        StartCounter.GetComponent<Text>().text = textAfterCountDown;
        countDownAudios.clip = BeepForGoClip;
        countDownAudios.Play();
        yield return new WaitForSecondsRealtime(1);
        StartCounter.GetComponent<Text>().text = " ";
        yield return 0;
    }
}
