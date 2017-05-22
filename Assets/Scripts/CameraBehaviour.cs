using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour {

    public float interpVelocity;
    public float minDistance;
    public float followDistance;
    public GameObject target;
    public Vector3 offset;
    Vector3 targetPos;

    // Use this for initialization
    void Start() {
        target = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (target) {
            Vector3 posNoZ = transform.position;
            Vector3 targetDirection = (target.transform.position - posNoZ);
            interpVelocity = targetDirection.magnitude * 5f;
            targetPos = transform.position + (targetDirection.normalized * interpVelocity * Time.deltaTime);
            targetPos.y = 5;
            transform.position = Vector3.Lerp(transform.position, targetPos + offset, 0.25f);
        }
    }
}
