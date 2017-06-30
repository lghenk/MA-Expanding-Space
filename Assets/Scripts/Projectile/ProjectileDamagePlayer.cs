using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDamagePlayer : MonoBehaviour {

    public int damage = 1;

    private void OnTriggerEnter(Collider collider) {
        if (collider.tag == "Player") {
            collider.gameObject.GetComponent<PlayerHealth>().HurtPlayer(damage);
			AudioSource PlayerHurt = GetComponent<AudioSource> ();
			PlayerHurt.Play ();
			Destroy (gameObject);
        }

        if (collider.tag == "Tiles/Walls") {
            Destroy(gameObject);
        }
    }
}

