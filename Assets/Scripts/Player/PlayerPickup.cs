using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerPickup : MonoBehaviour {
	public AudioSource[] sounds;
	public AudioSource noise1;
	public AudioSource noise2;


    private PlayerHealth _ph;
    private PlayerShoot _pa;
    public int stonesPickedup = 0;
    public bool isDone = false;

	// Use this for initialization
	void Start () {
        _ph = GetComponent<PlayerHealth>();
        _pa = GetComponent<PlayerShoot>();
		sounds = GetComponents<AudioSource>();
		noise1 = sounds[0];
		noise2 = sounds[1];

	}

    public void addHealth(int amount) {
        _ph.addHealth(amount);
    }

    public void resetAmmo() {
        _pa.resetHeat();
    }

    public void improveRecharge() {
        _pa.coolDownPerUpdate += 2f;
    }

    public void improvePerShot() {
        _pa.heatPerShot -= .5f;
    }

    void OnTriggerEnter(Collider other) {
        if(other.tag == "Stone") {
            stonesPickedup += 1;

			noise2.Play();
            Destroy(other.gameObject);
        }
    }

}
