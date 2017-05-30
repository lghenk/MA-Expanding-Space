using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {
	private int startingHealth;
	public int currentHealth;
    private PlayerMovement _pm;
    private Animator animController;
    public bool isDead = false;

	public void addHealth(int amount){
		currentHealth += amount;
	}

	// Use this for initialization
	void Start () {
		startingHealth = 10;
		currentHealth = startingHealth;

        _pm = GetComponent<PlayerMovement>();
        animController = GetComponent<Animator>();

    }

	public void HurtPlayer(int damageAmount)
	{
		currentHealth -= damageAmount;

		if (currentHealth <=0) {
            isDead = true;
            animController.SetBool("isDead", true);
		}

	}



}
