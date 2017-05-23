using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent (typeof (NavMeshAgent))]

public class EnemyController : MonoBehaviour {
	NavMeshAgent pathfinder;
	Transform target;
    Animator animController;

    private Vector3 lastpos;

	void Start () {
		pathfinder = GetComponent<NavMeshAgent> ();
		target = GameObject.FindGameObjectWithTag ("Player").transform;
        pathfinder.updateRotation = false;
        animController = transform.GetComponentInChildren<Animator>();
	}


	void Update () {
		pathfinder.SetDestination (target.position);

	}
}
