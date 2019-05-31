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
    public float yOffset;

    private Vector3 offset;

    
	// Use this for initialization
	void Start () {
        // thirdPerson = GameObject.Find ("Options").GetComponent<Options> ().thirdPersonSelect;
        player.GetComponent<MeshRenderer>().enabled = false;
        offset = new Vector3(0.0f, yOffset, 0.0f); 
	}

	void LateUpdate () {
        // updates every frame so camera follows player
        Vector3 playerInfo = player.transform.transform.position;
        vrCamera.position = player.transform.position + offset;
	}
}
