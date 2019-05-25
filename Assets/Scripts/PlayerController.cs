using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public TimerScript timer;

	public float DASHSPEED = 3000f;
	public float DASHTIME = 100f;

	public float speed;         //Move Speed from Camera Tilt
	public float speedMultiplier;

    

	private Rigidbody rb;
	private float force;
	private bool dashing;
	private float dashTimer;

	public GameObject vrCamera;

   

	/*
	 * TODO
	 * - find a way to keep camera rotation above 0 (might not be an issue once we switch to 1st person)
	 */

	void Start()
	{
		rb = GetComponent<Rigidbody>();
		force = speed * speedMultiplier;
		Physics.gravity = new Vector3(0, -200.0F, 0);
		dashing = false;
		dashTimer = 0;
    }

	void FixedUpdate()
	{
		updateDash();
		// TODO: fix this so that it does not rely on generic phone input but rather device specific input
		if (Input.GetButton("Fire1"))
			moveCamera();
	}

	//Moving the Player Object
	void moveCamera()
	{
		Vector3 forward;
		forward = vrCamera.transform.TransformDirection(Vector3.forward);

		rb.AddForce(forward * force);
		if (dashing)
		{
			rb.AddForce(forward * DASHSPEED);
		}
	}


	//For Iteraction with Pickup Items
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Collectable"))
		{
            MainMallManager.Instance.addEatten(other.gameObject);
			timer.addBonus();
			other.gameObject.SetActive(false);
			startDash();
		}
		
		Vector3 forward = vrCamera.transform.TransformDirection(Vector3.forward);
		if (other.gameObject.CompareTag("Obstacle")) // Don't collide if dashing
		{
			timer.addPenalty();
			/* 
			TODO: rb.AddForce(forward * force * other.GetComponent<Obstacle>().force );
			
			The line above was suppose to call a class called Obstacle attached to the objects that would contain different values for each obstacle
			BUT there seems to be an issue now hence why I used -50 for now
			*/ 
			rb.AddForce(forward * force * -50);
		}
		
		if(other.gameObject.CompareTag("Wall")){
			// Most of the Wall tags are for colliders so they do not have Obstacle script attached
			rb.AddForce(forward * force * -15); 
		}

		if(other.gameObject.CompareTag("End")){
            

			MainMallManager.Instance.isOnTime(true);
		}
	}

	private void updateDash() {
		if (dashing)
		{
			dashTimer -= Time.deltaTime;
			if (dashTimer <= 0)
			{
				dashing = false;
				gameObject.layer = LayerMask.NameToLayer("Default"); // Set player's layer to default, which collide with obstacle
				gameObject.GetComponent<Collider>().enabled = !dashing; // Reset collider
			}
		}
	}

	private void startDash() {
		dashing = true;
		dashTimer += DASHTIME;
		gameObject.layer = LayerMask.NameToLayer("Dashing"); // Set player's layer to dashing, which does not collide with obstacle
	}
}