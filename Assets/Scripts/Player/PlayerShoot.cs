using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour {

    public GameObject projectile;

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Shoot();
        }
    }

    public void Shoot() {
        GameObject go = Instantiate(projectile, transform.position, transform.rotation);
        Destroy(go, 20);
    }
}
