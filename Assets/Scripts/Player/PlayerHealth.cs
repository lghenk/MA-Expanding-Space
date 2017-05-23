using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {
	private int startingHealth;
	public int currentHealth;

	public void addHealth(int amount){
		currentHealth += amount;
	}

	// Use this for initialization
	void Start () {
		startingHealth = 10;
		currentHealth = startingHealth;

	}

	public void HurtPlayer(int damageAmount) {
		currentHealth -= damageAmount;

		if (currentHealth <=0)
		{
			
			Application.LoadLevel ("DeathScreen");
		}
	}
}
