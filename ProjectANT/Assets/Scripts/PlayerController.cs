﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float movementSpeed;
	public float jumpForce;
	public GameObject playerCamera;

	private CameraController cameraCont;
	private Rigidbody2D myRigidbody;

	// Use this for initialization
	void Start () {
		myRigidbody = GetComponent<Rigidbody2D> ();
		// should we create a gamecontroller for that
		cameraCont = playerCamera.GetComponent<CameraController>();
	}
	
	// Update is called once per frame
	void Update () {
		if (!cameraCont.IsGameOver())
		{
			int direction = 0;

			if (Input.acceleration.x > 0.1f || Input.GetKey("d"))
			{
				direction = 1;
			}
			else if (Input.acceleration.x < -0.1f || Input.GetKey("a"))
			{
				direction -= 1;
			}
			Vector2 movement = new Vector2(direction, 0);

			myRigidbody.AddForce(movement * movementSpeed);
		}
		else
		{
			myRigidbody.gameObject.SetActive(false);
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("End"))
		{
			Debug.Log("DEAD!!!!");
			cameraCont.SetGameOver(true);
		}
	}
}
