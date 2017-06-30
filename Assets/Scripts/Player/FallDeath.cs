using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class FallDeath : MonoBehaviour {
	private int maxFall = -10;

	
	// Update is called once per frame
	void Update(){
		if(transform.position.y <= maxFall)
		SceneManager.LoadScene ("GameOver");
	}
}
