using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTimer : MonoBehaviour {
	private float timer = 10f;

	void Update(){
		timer -= Time.deltaTime;
		if (timer <= 0f) {
		    AudioSource EnemySound = GetComponent<AudioSource> ();
			EnemySound.Play ();	
			timer = 10f;
		}
	}
}

