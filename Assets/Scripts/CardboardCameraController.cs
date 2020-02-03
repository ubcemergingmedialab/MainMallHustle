using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardboardCameraController : MonoBehaviour {

    private Camera cardboardCamera;

	// Use this for initialization
	void Start () {
       
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private Vector3 GetMoveSpeed(float x, float y) { 
        float xMove = Mathf.Min(Mathf.Abs(y * 10), 3);
        float yMove = Mathf.Min(Mathf.Abs(x * 10), 3);

        if (x >= 0)
        {
            yMove *= -1;
        }
        if (y < 0)
        {
            xMove *= -1;
        }

        return new Vector3(xMove, yMove, 0f);
    }
}
