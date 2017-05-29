using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {
	public int damage;
	public int health;
	public int currentHealth;
    private Animator animController;

    // Use this for initialization
    void Start () {
		currentHealth = health;
        animController = GetComponent<Animator>();
    }

	public void HurtEnemy(int damage)
	{
		currentHealth -= damage;

        if (currentHealth <= 0) {
            Destroy(gameObject.transform.parent.gameObject);
        } else {
            animController.SetTrigger("hit");
        }
    }

}
