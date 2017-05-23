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


<<<<<<< HEAD
	void Update () {
		if (target != null) {
			pathfinder.SetDestination (target.position);
		}
	}
=======
    void Update() {

        pathfinder.SetDestination(target.position);
        float ang = Quaternion.LookRotation(pathfinder.velocity).eulerAngles.y;

        if(ang >= 315 || ang <= 45) {
            animController.SetInteger("direction", 1);
        } else if(ang < 315 && ang >= 225){
            animController.SetInteger("direction", 0);
        } else if(ang < 225 && ang >= 135) {
            animController.SetInteger("direction", 3);
        } else if(ang < 135 && ang > 45) {
            animController.SetInteger("direction", 2);
        }
    }
>>>>>>> origin/master
}
