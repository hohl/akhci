﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float movementSpeed;
	public float jumpForce;

	private Rigidbody2D myRigidbody;

	// Use this for initialization
	void Start () {

		myRigidbody = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {

		int direction = 0;

		if (Input.acceleration.x > 0.1f || Input.GetKey("d")) {
			direction = 1;
		}
		else if (Input.acceleration.x < -0.1f || Input.GetKey("a")){
			direction -= 1;
		}
		Vector2 movement = new Vector2 (direction, 0);

		myRigidbody.AddForce (movement * movementSpeed);

		// Just for debugging reasons!
		if (Input.GetKeyDown (KeyCode.Space)) {
			myRigidbody.velocity = new Vector2 (myRigidbody.velocity.x, jumpForce);
		}
			
	}
}
