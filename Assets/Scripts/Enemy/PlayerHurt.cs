using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHurt : MonoBehaviour {

    [Tooltip("When the player touches an enemy, This is how much damage the player should receive")]
    [Range(1,100)] public int damageToGive;

    public void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player") 
		{
			AudioSource hit = GetComponent<AudioSource> ();
			hit.Play ();
			other.gameObject.GetComponent<PlayerHealth>().HurtPlayer(damageToGive);
		}
	}
}