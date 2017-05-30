using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent (typeof (NavMeshAgent))]

public class EnemyController : MonoBehaviour {
	NavMeshAgent pathfinder;
	Transform target;
    Animator animController;
    PlayerHealth _ph;

    private Vector3 lastpos;

	void Start () {
		pathfinder = GetComponent<NavMeshAgent> ();
		target = GameObject.FindGameObjectWithTag ("Player").transform;
        pathfinder.updateRotation = false;
        animController = transform.GetComponentInChildren<Animator>();
        _ph = target.GetComponent<PlayerHealth>();
	}


    void Update() {
        var state = animController.GetCurrentAnimatorStateInfo(0);

        pathfinder.SetDestination(target.position);
        if(state.IsName("Enemy_hit_left") || state.IsName("Enemy_hit_front") || state.IsName("Enemy_hit_right") ) {
            pathfinder.isStopped = true;
        } else {
            pathfinder.isStopped = false;
        }

        if(Vector3.Distance(transform.position, target.position) < 30) {
            pathfinder.isStopped = false;
        } else {
            pathfinder.isStopped = true;
        }

        if(_ph.isDead) {
            pathfinder.isStopped = true;
        }

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
}
