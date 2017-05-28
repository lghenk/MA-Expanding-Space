using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour {

    private Rigidbody _rb;
    private int speed = 10;
    private Animator animator;

	// Use this for initialization
	void Start () {
        _rb = gameObject.GetComponent<Rigidbody>();
        animator = gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

        Vector3 pos = transform.position;

		if(Input.GetKey(KeyCode.W)) {
            pos.z += speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.S)) {
            pos.z += -speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.A)) {
            pos.x += -speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.D)) {
            pos.x += speed * Time.deltaTime;
        }

        if(Input.anyKey) {
            if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)) {
                animator.SetBool("isWalking", true);
            }

        } else {
            animator.SetBool("isWalking", false);
        }

        _rb.MovePosition(pos);

        print("Direction: " + animator.GetInteger("direction") + " Is Walking: " + animator.GetBool("isWalking"));
    }

    public void lookAt(Vector3 point) {
        float ang = Quaternion.FromToRotation(Vector3.forward, point - transform.position).eulerAngles.y;
        
        if (ang >= 315 || ang <= 45) {
            animator.SetInteger("direction", 1);
        } else if (ang < 315 && ang >= 225) {
            animator.SetInteger("direction", 0);
        } else if (ang < 225 && ang >= 135) {
            animator.SetInteger("direction", 3);
        } else if (ang < 135 && ang > 45) {
            animator.SetInteger("direction", 2);
        }
    }
}
