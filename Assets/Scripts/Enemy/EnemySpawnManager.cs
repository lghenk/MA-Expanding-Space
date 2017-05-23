using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour {

    private GameObject[] _spawners;
    private GameObject player;
    private float maxCooldown = 3;
    private float curCooldown = 0;
    private float maxSpawnDistance = 20;
    private List<GameObject> av_spawners = new List<GameObject>();

    public GameObject enemy;

	// Use this for initialization
	void Start () {
        _spawners = GameObject.FindGameObjectsWithTag("Enemy Spawner");
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
        int numEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;

        if(curCooldown <= 0) {
            curCooldown = maxCooldown;
            av_spawners.Clear();
            foreach(var go in _spawners) {
                if(Vector3.Distance(go.transform.position, player.transform.position) <= maxSpawnDistance) {
                    av_spawners.Add(go);
                }
            }

            GameObject _detach = av_spawners[Random.Range(0, av_spawners.Count)];
            Instantiate(enemy, _detach.transform.position, _detach.transform.rotation);
        } else {
            curCooldown -= Time.deltaTime;
        }
    }
}
