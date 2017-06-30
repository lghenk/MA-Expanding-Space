using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestPickup : MonoBehaviour {
	
    private string[] possiblePickups = { "health", "ammo", "fasterRecharge", "betterShotCharge" };
    private int chestHealth = 2;
    [Range(1, 1000)] public float maxCooldown = 60;
    private float curCooldown = 0;

    private Animator animController;

    private PlayerPickup _player;
    [Header("Tagger")]
    public GameObject PickupTagger;
    public Sprite health;
    public Sprite ammo;
    public Sprite fasterCooldown;
    public Sprite lessHeatUsage;

	// Use this for initialization
	void Start () {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPickup>();
        animController = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (_player == null) {
            if (GameObject.FindGameObjectWithTag("Player")) {
                _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPickup>();
            }
            return;
        }

        if (curCooldown > 0) {
            curCooldown -= Time.deltaTime;

            if(curCooldown <= 0) {
                chestHealth = 2;
                animController.SetInteger("state", 0);
            }
        }
	}

    public void damage(int amount) {
        if (_player == null) {
            if (GameObject.FindGameObjectWithTag("Player")) {
                _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPickup>();
            }
            return;
        }

        chestHealth -= amount;

        if(chestHealth == 2) {
            animController.SetInteger("state", 0);
        } else if(chestHealth == 1) {
            animController.SetInteger("state", 1);
        } else if(chestHealth == 0) {
            animController.SetInteger("state", 2);
            curCooldown = maxCooldown;

            GameObject go = Instantiate(PickupTagger, transform.position, Quaternion.identity);
            go.transform.localEulerAngles = new Vector3(90f, 0f, 0f);
            SpriteRenderer sr = go.GetComponent<SpriteRenderer>();

            string pickup = possiblePickups[Random.Range(0, possiblePickups.Length)];
            if(pickup == "health") {
                sr.sprite = health;
                _player.addHealth(Random.Range(10, 25));
            } else if(pickup == "ammo") {
                sr.sprite = ammo;
                _player.resetAmmo();
            } else if (pickup == "fasterRecharge") {
                sr.sprite = fasterCooldown;
                _player.improveRecharge();
            } else if (pickup == "betterShotCharge") {
                sr.sprite = lessHeatUsage;
                _player.improvePerShot();
            }

            Destroy(go, 1);
        }
    }   
}
