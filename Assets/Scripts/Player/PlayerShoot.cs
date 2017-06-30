using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShoot : MonoBehaviour {
	[Range(0, 1000)] public float maxHeat = 100;
    public float currentHeat = 0;
    [Range(0, 100)] public float heatPerShot = 5f;
    [Range(0, 100)] public float coolDownPerUpdate = 3f;

    [Tooltip("If the gun overheats the user has a wait penalty until this % is hit.")]
    [Range(0, 1000)] public float overHeatAllowShoot = 75;
    public bool coolingDown = false;
    public bool shoot = false;
    public LayerMask mask;

    public GameObject projectile;
    private PlayerHealth _ph;

    private Transform head;
    private Animator animatorHead;
    private PlayerPickup _pp;

    public enum Direction {
        north,
        south,
        west,
        east
    }

    void Start() {
        _ph = GetComponent<PlayerHealth>();
        _pp = GetComponent<PlayerPickup>();
        for (var i = 0; i < transform.childCount; i++) {
            if (transform.GetChild(i).name == "Head") {
                head = transform.GetChild(i);
            }
        }

        if (head)
            animatorHead = head.GetComponent<Animator>();
    }

    public void resetHeat() {
        currentHeat = 0;
    }

    private void Update() {
        if(_ph.isDead)
            animatorHead.SetBool("is_dead", true);

        if (_pp.isDone)
            return;

        if (currentHeat != 0) {
            currentHeat -= coolDownPerUpdate * Time.deltaTime;

            if(currentHeat <= overHeatAllowShoot)
                coolingDown = false;

            if (currentHeat <= 0) {
                currentHeat = 0;
                coolingDown = false;
            }
        }
        
        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            animatorHead.SetInteger("direction", 1);
            Shoot(Direction.north);
        } else if (Input.GetKeyDown(KeyCode.DownArrow)) {
            animatorHead.SetInteger("direction", 3);
            Shoot(Direction.south);
        } else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            animatorHead.SetInteger("direction", 0);
            Shoot(Direction.west);
        } else if (Input.GetKeyDown(KeyCode.RightArrow)) {
            animatorHead.SetInteger("direction", 2);
            Shoot(Direction.east);
        } 
    }

    public void Shoot(Direction dir) {
        if (_ph.isDead || currentHeat >= maxHeat || coolingDown)
            return;

        AudioSource shoot = GetComponent<AudioSource>();
        shoot.Play();

        currentHeat += (heatPerShot > 0) ? heatPerShot : 0;

        if (currentHeat >= maxHeat) {
            currentHeat = maxHeat;
            coolingDown = true;
        }

        Vector3 pos = transform.GetChild(0).position;
        Vector3 rot = Vector3.zero;

        if (dir == Direction.north) {
            pos = transform.GetChild(0).position;
            rot = Vector3.zero;
        } else if (dir == Direction.south) {
            pos = transform.GetChild(3).position;
            rot = new Vector3(0, 180, 0);
        } else if (dir == Direction.west) {
            pos = transform.GetChild(2).position;
            rot = new Vector3(0, 270, 0);
        } else if (dir == Direction.east) {
            pos = transform.GetChild(1).position;
            rot = new Vector3(0, 90, 0);
        }

        GameObject go = Instantiate(projectile, pos, transform.rotation);

        rot.x = 90;
        go.transform.eulerAngles = rot;

        Destroy(go, 5);
    }
}
