using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {
	public int startingHealth;
	private int currentHealth;


	// Use this for initialization
	void Start () {
		currentHealth = startingHealth;

	}

	public void HurtPlayer(int damageAmount)
	{
		currentHealth -= damageAmount;

		if (currentHealth <=0)
		{
			
			Application.LoadLevel ("DeathScreen");
		}

	}



}
