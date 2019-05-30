using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class cardboard_detection : MonoBehaviour {

    // Use this for initialization
    public GameObject notification;
    public GameObject Canvas;
    public GameObject UICamera;
    
	void Start () {
      

            notification.GetComponent<Text>().text = "Please put your phone into cardboard with your home button to the left.";

	}
	
	// Update is called once per frame
	void Update () {
        if (Input.deviceOrientation == DeviceOrientation.LandscapeRight)
        {
            Canvas.SetActive(false);
            UICamera.SetActive(false);
            StartCoroutine(LoadDevice("cardboard"));
            SceneManager.LoadScene("Main Scene");

        }
	}
    public IEnumerator LoadDevice(string newDevice)
    {

        if (String.Compare(XRSettings.loadedDeviceName, newDevice, true) != 0)
        {
            XRSettings.LoadDeviceByName(newDevice);
            yield return null;
            
        }
        XRSettings.enabled = true;
    }
}
