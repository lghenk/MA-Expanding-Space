using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDamage : MonoBehaviour {
	public 

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Enemy") 
		{
			other.gameObject.GetComponent<EnemyHealth>().HurtEnemy(1);
			Destroy (gameObject);

		}
	}
}

