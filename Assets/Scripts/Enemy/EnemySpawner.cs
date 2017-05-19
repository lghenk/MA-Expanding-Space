using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	public Transform[] spawnLocations;
	public GameObject[] Prefab;
	public GameObject[] Clone;

	void Start () {
		spawn ();
	}
	public void spawn (){
		//spawner 0
	Clone[0] = Instantiate(Prefab[0], spawnLocations[0].transform.position, Quaternion.Euler(0,0,0)) as GameObject;
	Clone[1] = Instantiate(Prefab[1], spawnLocations[0].transform.position, Quaternion.Euler(0,0,0)) as GameObject;
	Clone[2] = Instantiate(Prefab[2], spawnLocations[0].transform.position, Quaternion.Euler(0,0,0)) as GameObject;
		//spawner 1
	Clone[0] = Instantiate(Prefab[0], spawnLocations[1].transform.position, Quaternion.Euler(0,0,0)) as GameObject;
	Clone[1] = Instantiate(Prefab[1], spawnLocations[1].transform.position, Quaternion.Euler(0,0,0)) as GameObject;
	Clone[2] = Instantiate(Prefab[2], spawnLocations[1].transform.position, Quaternion.Euler(0,0,0)) as GameObject;
	}
}
