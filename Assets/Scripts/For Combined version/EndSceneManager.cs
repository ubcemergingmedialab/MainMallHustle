using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndSceneManager : MonoBehaviour {
	[SerializeField] public GameObject photoSphere;
    [SerializeField] public Material onTimeMat;
    [SerializeField] public Material lateMat;

	[SerializeField] public GameObject button;
	[SerializeField] public Material buttonOnTimeMat;
	[SerializeField] public Material buttonLateMat;

	public void loadEndScene(bool onTime){
		if (onTime){
            photoSphere.GetComponent<Renderer>().material = onTimeMat;
			button.GetComponent<Renderer>().material = buttonOnTimeMat;
        }
        else{
            photoSphere.GetComponent<Renderer>().material = lateMat;
			button.GetComponent<Renderer>().material = buttonLateMat;
        }
	}
}
