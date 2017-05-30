using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickup : MonoBehaviour {

    private PlayerHealth _ph;
    private PlayerAmmo _pa;

	// Use this for initialization
	void Start () {
        _ph = GetComponent<PlayerHealth>();
        _pa = GetComponent<PlayerAmmo>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void addHealth(int amount) {
        _ph.addHealth(amount);
    }

    public void resetAmmo() {
        //_pa.resetHeat();
    }

    void OnTriggerEnter(Collider other) {
        if(other.tag == "Stone") {
            print("Triggered Stone");
        }
    }

}
