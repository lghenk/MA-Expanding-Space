﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour {

    public float speed = 15;
	
	// Update is called once per frame
	void Update () {
        transform.position += transform.up * speed * Time.deltaTime;
	}

}

