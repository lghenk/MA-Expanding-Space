using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent (typeof (NavMeshAgent))]
public class EnemyController : MonoBehaviour {
	private NavMeshAgent pathfinder;
	public Transform target;
    public bool isGodZilla;
    public bool isStatic;
    private Animator animController;
    private PlayerHealth _ph;
    private bool roaming = false;
    private Vector3 roamingTarget = Vector3.zero;
    private float roamingCooldownMax = 10;
    private float roamingCooldownCur = 0;
    public float shootCooldownMax = 5;
    private float shootCooldownCur = 0;

    private EnemyShoot _es;
    private Vector3 lastpos;

    void Start() {
        pathfinder = GetComponent<NavMeshAgent>();
        if(GameObject.FindGameObjectWithTag("Player"))
            target = GameObject.FindGameObjectWithTag("Player").transform;

        pathfinder.updateRotation = false;
        animController = transform.GetComponentInChildren<Animator>();

        if(target != null && target.GetComponent<PlayerHealth>())
            _ph = target.GetComponent<PlayerHealth>();

        if(transform.GetChild(0).GetComponent<EnemyShoot>()) {
            _es = transform.GetChild(0).GetComponent<EnemyShoot>();
        }
	}


    void Update() {
        if(target == null) {
            if (GameObject.FindGameObjectWithTag("Player")) {
                target = GameObject.FindGameObjectWithTag("Player").transform;
                if (target.GetComponent<PlayerHealth>())
                    _ph = target.GetComponent<PlayerHealth>();
            }
                
            return;
        }

        if(pathfinder == null) {
            if(GetComponent<NavMeshAgent>())
                pathfinder = GetComponent<NavMeshAgent>();
            return;
        }

        var state = animController.GetCurrentAnimatorStateInfo(0);
        
        if(state.IsName("Enemy_hit_left") || state.IsName("Enemy_hit_front") || state.IsName("Enemy_hit_right") ) {
            pathfinder.isStopped = true;
        } else {
            pathfinder.isStopped = false;
        }
        
        if (Vector3.Distance(transform.position, target.position) < 30) {
            roaming = false;
            pathfinder.isStopped = false;
            pathfinder.SetDestination(target.position);
        } else {
            pathfinder.isStopped = true;
            roaming = true;
        }

        if (roaming == true) {
            pathfinder.isStopped = false;

            if(Vector3.Distance(roamingTarget, transform.position) < 2) {
                roamingCooldownCur = 0;
            }

            if (roamingCooldownCur <= 0) {
                Vector3 randomDirection = Random.insideUnitSphere * 20;
                randomDirection += transform.position;

                NavMeshHit myNavHit;
                if (NavMesh.SamplePosition(randomDirection, out myNavHit, 100, -1)) {
                    Vector3 finalPosition = myNavHit.position;
                    roamingTarget = finalPosition;
                    pathfinder.SetDestination(roamingTarget);
                    roamingCooldownCur = roamingCooldownMax;
                }            
            } else {
                roamingCooldownCur -= Time.deltaTime;
            }
        }

        if (_ph != null &&_ph.isDead || isStatic) {
            pathfinder.isStopped = true;
        }

        // Before we allow the enemy to shoot check the following things: is it static and does it have a shoot script
        // Or shootscript exists, AI is not stopped. And Distance between player is less than 10. Also we check if the shoot cooldown is less than or equals than 0
        if(((isStatic && _es != null) || (_es != null && !pathfinder.isStopped)) && (Vector3.Distance(transform.position, target.position) < 10 && shootCooldownCur <= 0)) {
            _es.shoot();
            shootCooldownCur = shootCooldownMax;
            animController.SetTrigger("shot");
            pathfinder.isStopped = true;
			AudioSource Enemy_Shoot = GetComponent<AudioSource> ();
			Enemy_Shoot.Play ();
        }

        if(shootCooldownCur > 0) {
            shootCooldownCur -= Time.deltaTime;
        }

        if(!isStatic) { // If player is not static we get their velocity and determine their rotation based on that
            float ang = Quaternion.LookRotation(pathfinder.velocity).eulerAngles.y;
            if (ang >= 315 || ang <= 45) {
                animController.SetInteger("direction", 1);
            } else if (ang < 315 && ang >= 225) {
                animController.SetInteger("direction", 0);
            } else if (ang < 225 && ang >= 135) {
                animController.SetInteger("direction", 3);
            } else if (ang < 135 && ang > 45) {
                animController.SetInteger("direction", 2);
            }
        } else { // If the player is static we determine their rotation based on where the player is located
            float ang = Quaternion.FromToRotation(Vector3.forward, target.transform.position - transform.position).eulerAngles.y;
            if (ang >= 315 || ang <= 45) {
                animController.SetInteger("direction", 1);
            } else if (ang < 315 && ang >= 225) {
                animController.SetInteger("direction", 0);
            } else if (ang < 225 && ang >= 135) {
                animController.SetInteger("direction", 3);
            } else if (ang < 135 && ang > 45) {
                animController.SetInteger("direction", 2);
            }
        }
        
    }
}
