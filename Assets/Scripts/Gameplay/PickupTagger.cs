using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupTagger : MonoBehaviour {	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1 * Time.deltaTime);
	}
}
