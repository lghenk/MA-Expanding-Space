using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDamage : MonoBehaviour {

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Enemy") {
			other.gameObject.GetComponent<EnemyHealth>().HurtEnemy(1);
			AudioSource enemyHit = GameObject.FindGameObjectWithTag ("Enemy_Hit").GetComponent<AudioSource> ();
			enemyHit.Play();
			Destroy (gameObject);	
		}

        if (other.gameObject.tag == "Chest Pickup") {
            other.gameObject.GetComponent<ChestPickup>().damage(1);
            Destroy(gameObject);
        }

        if (other.tag == "Tiles/Walls") {
            Destroy(gameObject);
        }
    }

}

