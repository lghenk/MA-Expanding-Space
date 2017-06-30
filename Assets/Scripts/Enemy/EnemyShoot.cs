using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour {

    [Range(1, 100)] public int damage = 1;
    private EnemyController _ec;
    public GameObject projectile;

    public void shoot() {
        if (transform.parent && _ec == null) {
            if(transform.parent.GetComponent<EnemyController>()) {
                _ec = transform.parent.GetComponent<EnemyController>();
            }
        }

        if (_ec.target == null)
            return;

        GameObject go = Instantiate(projectile, transform.position, transform.rotation);
        go.GetComponent<ProjectileDamagePlayer>().damage = damage;
        go.transform.LookAt(_ec.target.transform);
        go.transform.eulerAngles = new Vector3(90, go.transform.eulerAngles.y, 0);	
        Destroy(go, 5);
    }
}

