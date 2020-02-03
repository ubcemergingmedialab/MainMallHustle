using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController2 : MonoBehaviour {

    public GameObject Player2;
    public float xOffset = 0;
    public float yOffset = 0;
    public float zOffset = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void LateUpdate()
    {
        transform.SetPositionAndRotation(new Vector3(Player2.transform.position.x + xOffset, Player2.transform.position.y + yOffset, Player2.transform.position.z + zOffset), transform.rotation);
        //transform.position.Set(Player2.transform.position.x, Player2.transform.position.y + heightOffset, Player2.transform.position.z);
    }
}
