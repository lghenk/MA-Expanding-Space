using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAiming : MonoBehaviour {

    private PlayerMovement _playerMovement;
    //private PlayerShoot _playerShoot;
    public LayerMask mask;


    void Awake() {
        _playerMovement = GetComponent<PlayerMovement>();
        //_playerShoot = GetComponent<PlayerShoot>();
        //mask = LayerMask.GetMask("FLOOR");
    }

    // Update is called once per frame
    void Update () {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1000, mask)) {
            print(hit.collider.name);       
            _playerMovement.lookAt(hit.point);
        }
        if (Input.GetMouseButtonDown(0)) {
            //_playerShoot.Shoot();
        }
    }
}
