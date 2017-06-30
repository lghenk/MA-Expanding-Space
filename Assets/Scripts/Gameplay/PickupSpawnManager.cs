using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PickupSpawnManager : MonoBehaviour {

    public GameObject stonePrefab;
    public GameObject chestPrefab;

    private GameObject[] spawners;

    private List<GameObject> stones = new List<GameObject>();
    private List<GameObject> chests = new List<GameObject>();

    [Header("Spawn Data")]
    public int numberStones = 5;
    public int chestPercentage = 50;
    public int emptyPercentage = 30;

	// Use this for initialization
	void Start () {
        spawners = GameObject.FindGameObjectsWithTag("Pickup Spawner");

        for(var i = 0; i < numberStones; i++) {
            GameObject Spawner = spawners[Random.Range(0, spawners.Length)];
            if (stones.Contains(Spawner) == false) {
                stones.Add(Spawner);
                GameObject go = Instantiate(stonePrefab);
                go.transform.position = Spawner.transform.position;
            } else {
                i--;
            }
        }

        for (var i = 0; i < 10; i++) {
            GameObject Spawner = spawners[Random.Range(0, spawners.Length)];
            if (stones.Contains(Spawner) || chests.Contains(Spawner)) {
                i--;
            } else {
                chests.Add(Spawner);
                GameObject go = Instantiate(chestPrefab);
                go.transform.position = Spawner.transform.position;
            }
        }
	}
}
