using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawnManager : MonoBehaviour {

    public bool allowSpawning = false;
    public float maxCooldown = 2;
    private float curCooldown = 0;
    public int maxEnemies = 50;

    [SerializeField] public Enemy[] enemies;

    public BoardCreator _bc;

    public void allowSpawn() {
        allowSpawning = true;
    }

	// Use this for initialization
	void Start () {
        _bc = GetComponent<BoardCreator>();
	}
	
	// Update is called once per frame
	void Update () {
        if(_bc.placeAble.Count == 0) {
            return;
        }

        if (!allowSpawning)
            return;

        int numEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;

        if(curCooldown <= 0 && numEnemies < maxEnemies) {
            curCooldown = maxCooldown;

            float selection = Random.Range(0, 100);
            Enemy selected = null;
            for(int i = 0; i < enemies.Length; i++) {
                if(selection <= enemies[i].probability) {
                    if (selected == null)
                        selected = enemies[i];

                    if (enemies[i].probability < selected.probability)
                        selected = enemies[i];
                }
            }

            Vector2 _detach = _bc.placeAble[Random.Range(0, _bc.placeAble.Count)];

            NavMeshHit myNavHit;
            if (NavMesh.SamplePosition(new Vector3(_detach.x, 0, _detach.y), out myNavHit, 100, -1)) {
                GameObject enemy = Instantiate(selected.enemy, myNavHit.position, Quaternion.identity);
                enemy.transform.eulerAngles = new Vector3(90, 0, 0);
            }       
        } else {
            curCooldown -= Time.deltaTime;
        }
    }
}
