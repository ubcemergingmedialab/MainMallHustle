using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Rotate the object the script is attached to
 */
public class Rotator_Upright: MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
        //transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
        transform.Rotate(new Vector3(0, 45, 0) * Time.deltaTime);
    }
}
