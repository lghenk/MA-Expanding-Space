using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour {

    private Rigidbody _rb;
    private int speed = 10;
    private Animator animator;
    private PlayerHealth _ph;
	// Use this for initialization
	void Start () {
        _rb = gameObject.GetComponent<Rigidbody>();
        animator = gameObject.GetComponent<Animator>();
        _ph = GetComponent<PlayerHealth>();
	}
	
	// Update is called once per frame
	void Update () {
        if (_ph.isDead)
            return;

        Vector3 pos = transform.position;

		if(Input.GetKey(KeyCode.W)) {
            pos.z += speed * Time.deltaTime;
            animator.SetInteger("direction", 1);
        }

        if (Input.GetKey(KeyCode.S)) {
            pos.z += -speed * Time.deltaTime;
            animator.SetInteger("direction", 3);
        }

        if (Input.GetKey(KeyCode.A)) {
            pos.x += -speed * Time.deltaTime;
            animator.SetInteger("direction", 0);
        }

        if (Input.GetKey(KeyCode.D)) {
            pos.x += speed * Time.deltaTime;
            animator.SetInteger("direction", 2);

        }

        if(Input.anyKey) {
            if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)) {
                animator.SetBool("isWalking", true);
            }

        } else {
            animator.SetBool("isWalking", false);
        }

        _rb.MovePosition(pos);
    }

    public void lookAt(Vector3 point) {
    }
}
