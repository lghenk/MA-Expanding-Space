using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health_UP : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void OnTriggerEnter (Collider other) {
		if (other.gameObject.tag == "Player") {
			other.gameObject.GetComponent<PlayerHealth> ().addHealth (1);
			Destroy (gameObject);
		}
	}
}
