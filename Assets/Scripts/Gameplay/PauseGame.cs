﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PauseGame : MonoBehaviour {
	public Transform pause_Canvas;
	public Transform Buttons;
	public Transform options_Canvas;

	public Transform Player;
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Pause ();
		}
	}

	public void Back() {
			options_Canvas.gameObject.SetActive (false);
			Buttons.gameObject.SetActive (true);
	}
		

	public void Pause() {
		if (pause_Canvas.gameObject.activeInHierarchy == false) {
			pause_Canvas.gameObject.SetActive (true);
			Time.timeScale = 0;
			Player.GetComponent<PlayerMovement> ().enabled = false;
			Player.GetComponent<PlayerShoot> ().enabled = false;
		} else {
			pause_Canvas.gameObject.SetActive (false);
			Time.timeScale = 1;
			Player.GetComponent<PlayerMovement> ().enabled = true;
			Player.GetComponent<PlayerShoot> ().enabled = true;
		}


	}
	public void options() {
		if (options_Canvas.gameObject.activeInHierarchy == false) {
			Buttons.gameObject.SetActive (false);
			options_Canvas.gameObject.SetActive (true);
			Time.timeScale = 0;
			Player.GetComponent<PlayerMovement> ().enabled = false;
			Player.GetComponent<PlayerShoot> ().enabled = false;
		} else {
			options_Canvas.gameObject.SetActive (false);
			Time.timeScale = 1;
			Player.GetComponent<PlayerMovement> ().enabled = true;
			Player.GetComponent<PlayerShoot> ().enabled = true;
		}
	}
}
