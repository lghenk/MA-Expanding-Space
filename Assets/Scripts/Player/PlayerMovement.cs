using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour {

    private Rigidbody _rb;
    private int speed = 10;

	// Use this for initialization
	void Start () {
        _rb = gameObject.GetComponent<Rigidbody>();
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

        _rb.MovePosition(pos);
    }

    public void lookAt(Vector3 point) {
        transform.LookAt(point);
        transform.eulerAngles = new Vector3(90, transform.eulerAngles.y, 0);
    }
}
