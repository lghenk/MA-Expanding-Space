using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour {

    private Rigidbody _rb;
    private int speed = 7;
    private Animator animatorBody;
    
    private PlayerHealth _ph;
    private PlayerPickup _pp;

    public ParticleSystem walkParticle;
	// Use this for initialization
	void Start () {
        _rb = gameObject.GetComponent<Rigidbody>();
        animatorBody = gameObject.GetComponent<Animator>();
        _ph = GetComponent<PlayerHealth>();
        _pp = GetComponent<PlayerPickup>();
        Time.timeScale = 1;
	}
	
	// Update is called once per frame
	void Update () {
        if (_ph.isDead)
            return;

        Vector3 pos = transform.position;

		if(Input.GetKey(KeyCode.W)) {
            pos.z += speed * Time.deltaTime;
            animatorBody.SetInteger("direction", 1);
        }

        if (Input.GetKey(KeyCode.S)) {
            pos.z += -speed * Time.deltaTime;
            animatorBody.SetInteger("direction", 3);
        }

        if (Input.GetKey(KeyCode.A)) {
            pos.x += -speed * Time.deltaTime;
            animatorBody.SetInteger("direction", 0);
        }

        if (Input.GetKey(KeyCode.D)) {
            pos.x += speed * Time.deltaTime;
            animatorBody.SetInteger("direction", 2);
        }

        if(Input.anyKey) {
            if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)) {
                animatorBody.SetBool("isWalking", true);
                walkParticle.Play();
            }
        } else {
            animatorBody.SetBool("isWalking", false);
            walkParticle.Stop();
        }

        _rb.MovePosition(pos);
    }

    private void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.tag == "Spawn Area" && _pp.stonesPickedup == 5) {
            SceneManager.LoadScene("WinScreen");
        }
    }
}
