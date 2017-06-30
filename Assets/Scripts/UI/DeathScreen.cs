using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class DeathScreen : MonoBehaviour {
	private PlayerHealth health;

	void Start() {
		health = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerHealth> ();
	}

	void Update () {
		if (health.currentHealth <= 0) {
			SceneManager.LoadScene ("GameOver");
		}
	}
}
