using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using HTC.UnityPlugin.Vive;

public class PlayerController2 : MonoBehaviour {

    public GameObject canvas;
    public TimerScript timer;
    

    public float speed = 10f;
    public float dashSpeed = 20f;
    public float dashTime = 1f;
    
    private Rigidbody rb;
    private bool isDashing;
    private float dashTimer;

    public GameObject Camera;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        isDashing = false;
        dashTimer = 0;
    }

    void FixedUpdate()
    {
        UpdateDash();
        #region Inputs
        if ((ViveInput.GetPress(HandRole.RightHand, ControllerButton.Trigger)
            || ViveInput.GetPress(HandRole.LeftHand, ControllerButton.Trigger)) ||
            (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) ||
            Input.GetKey(KeyCode.Mouse0))
        {
           // Debug.Log("Moving");
            MovePlayer2();
        }
        #endregion
    }

    void UpdateDash()
    {
        if (isDashing)
        {
            dashTimer -= Time.deltaTime;
            if (dashTimer <= 0)
            {
                isDashing = false;
                gameObject.layer = LayerMask.NameToLayer("Default"); // set player's layer to default, which collides with obstacles
                gameObject.GetComponent<Collider>().enabled = !isDashing; // reset collider
            }
        }
    }

    // Moves the player object
    void MovePlayer2()
    {
        Vector3 forward = Camera.transform.forward;
        rb.AddForce(forward * speed);
        // if the player is dashing from consumables, add an extra force
        if (isDashing)
        {
            rb.AddForce(forward * dashSpeed);
        }
    }

    // For interaction with pickup items
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Collectable"))
        {
            timer.AddBonus();
            other.gameObject.SetActive(false);
            MainMallManager.Instance.addEatten(other.gameObject);
            StartDash();
        }

        Vector3 forward = transform.forward;
        if (other.gameObject.CompareTag("Obstacle")) // Don't collide if dashing
        {
            timer.AddPenalty();
            /* 
			TODO: rb.AddForce(forward * force * other.GetComponent<Obstacle>().force );
			
			The line above was suppose to call a class called Obstacle attached to the objects that would contain different values for each obstacle
			BUT there seems to be an issue now hence why I used -50 for now
			*/
            rb.AddForce(forward * speed * -50);
        }

        if (other.gameObject.CompareTag("Wall"))
        {
            // Most of the Wall tags are for colliders so they do not have Obstacle script attached
            rb.AddForce(forward * speed * -15);
        }

        if (other.gameObject.CompareTag("End"))
        {
            timer.ResetTime();
            MainMallManager.Instance.isOnTime(true);

            //
            rb.Sleep();
           
        }
    }

    void StartDash()
    {
        isDashing = true;
        dashTimer += dashTime;
        gameObject.layer = LayerMask.NameToLayer("Dashing"); // set player's layer to dashing, so that it does not collide with obstacles
    }
}
