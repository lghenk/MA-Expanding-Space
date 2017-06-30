using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * A small script so we can easily add multiple enemies to the spawner 
 * And also set their spawn probability
 */

[System.Serializable]
public class Enemy {
    public GameObject enemy;
    [Range(0, 100)] public float probability;
}
