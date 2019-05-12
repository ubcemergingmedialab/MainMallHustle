using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Controls the camera when its in 3rd person or 1st person
 * 
 */
public class CameraController_3rd : MonoBehaviour {
    public GameObject player;
	public Transform vrCamera;
	public bool thirdPerson;
	private float CameraRise = 0f;

	private Vector3 offset;

    
	// Use this for initialization
	void Start () {
		// thirdPerson = GameObject.Find ("Options").GetComponent<Options> ().thirdPersonSelect;
        offset = (transform.position - player.transform.position); 
	}

	void LateUpdate () {
        /*
         * If camera is in thirdPerson:
         *  camera should follow behind the ball (like a third person camera)
         */
		if (thirdPerson) {
			player.GetComponent<MeshRenderer> ().enabled = true;
			transform.position = player.transform.position - offset.magnitude * vrCamera.forward + new Vector3 (0f, CameraRise, 0f);
            vrCamera.transform.LookAt(player.transform);
		} else {
			player.GetComponent<MeshRenderer> ().enabled = false;
			transform.position = player.transform.position + offset;
		}
	}
}
