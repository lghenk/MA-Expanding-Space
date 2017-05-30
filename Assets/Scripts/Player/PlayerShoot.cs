using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour {

    public GameObject projectile;
    public LayerMask mask;
    private PlayerHealth _ph;

    private void Start() {
        _ph = GetComponent<PlayerHealth>();
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0) && !_ph.isDead) {
            Shoot();
        }
    }

    public void Shoot() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1000, mask)) {          
            GameObject go = Instantiate(projectile, transform.position, transform.rotation);
            go.transform.LookAt(hit.point);
            go.transform.eulerAngles = new Vector3(90, go.transform.eulerAngles.y, 0);
            Destroy(go, 5);
        }       
    }
}
