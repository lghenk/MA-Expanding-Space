using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestPickup : MonoBehaviour {

    public Sprite closedChest;
    public Sprite almostOpenChest;
    public Sprite openChest;

    private string[] possiblePickups = { "health", "ammo" };
    private SpriteRenderer _sr;
    private int chestHealth = 2;
    private float maxCooldown = 120;
    private float curCooldown = 0;

    private PlayerPickup _player;

	// Use this for initialization
	void Start () {
        _sr = GetComponent<SpriteRenderer>();
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPickup>();
	}
	
	// Update is called once per frame
	void Update () {
		if(curCooldown > 0) {
            curCooldown -= maxCooldown;

            if(curCooldown <= 0) {
                chestHealth = 2;
                _sr.sprite = closedChest;
            }
        }
	}

    public void damage(int amount) {
        chestHealth -= amount;

        if(chestHealth == 2) {
            _sr.sprite = closedChest;
        } else if(chestHealth == 1) {
            _sr.sprite = almostOpenChest;
        } else if(chestHealth == 0) {
            _sr.sprite = closedChest;
            curCooldown = maxCooldown;

            string pickup = possiblePickups[Random.Range(0, possiblePickups.Length)];
            if(pickup == "health") {
                _player.addHealth(1);
            } else if(pickup == "ammo") {
                _player.resetAmmo();
            }
        }
    }   
}
