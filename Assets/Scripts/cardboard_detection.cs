using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class cardboard_detection : MonoBehaviour {

    // Use this for initialization
    public GameObject notification;
    public GameObject loadingBar;
    private Camera cardboardDetectCamera;

    void Start () {
        notification.GetComponent<Text>().text = "Please put your phone into cardboard with your home button to the left.";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.deviceOrientation == DeviceOrientation.LandscapeRight)
        {
            StartCoroutine(LoadDevice("Cardboard"));
            MainMallManager.switched = true;
            MainMallManager.CurrentState = MainMallManager.GameStates.Main;
        }
    }
    

    // For loading the cardboard device
    public IEnumerator LoadDevice(string newDevice)
    {

        if (String.Compare(XRSettings.loadedDeviceName, newDevice, true) != 0)
        {
            XRSettings.LoadDeviceByName(newDevice);
            yield return null;
        }
        XRSettings.enabled = true;
    }

    // If want switch between scenes, use the following function
    //public IEnumerator LoadYourAsyncScene()
    //{

    //    AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Main Scene");
    //    notification.GetComponent<Text>().text = " ";


    //    while (!asyncLoad.isDone)
    //    {   
    //        loadingBar.GetComponent<Text>().text = "Loading progress: " + (Mathf.RoundToInt(asyncLoad.progress * 100)) + "%";
    //        yield return null;
    //    }
    //}
}
