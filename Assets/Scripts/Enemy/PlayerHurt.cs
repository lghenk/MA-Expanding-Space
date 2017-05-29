using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHurt : MonoBehaviour {

	public int damageToGive;

    public void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player") 
		{
			other.gameObject.GetComponent<PlayerHealth>().HurtPlayer(damageToGive);

		}
	}
}