using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {
	public int health;
	public int currentHealth;
    public GameObject hitParticle;
    private Animator animController;
    private EnemyController _ec;

    // Use this for initialization
    void Start () {
        _ec = transform.parent.GetComponent<EnemyController>();
        float scale = 0;
        if(_ec.isGodZilla || _ec.isStatic) {
            scale = 1f;
            transform.localScale = new Vector3(scale, scale, 1);
        } else {
            scale = Random.Range(0.4f, 0.6f);
        }
        
        transform.localScale = new Vector3(scale, scale, 1);

        if(scale <= 0.5f) {
            currentHealth = 1;
        } else if (scale <= 0.6f) {
            currentHealth = 2;
        } else if (scale <= 0.7f) {
            currentHealth = 3;
        } else if(scale <= 0.8f) {
            currentHealth = 4;
        } else if (scale <= 0.9f) {
            currentHealth = 5;
        } else if (scale <= 1f) {
            currentHealth = 6;
        }

        animController = GetComponent<Animator>();
    }

	public void HurtEnemy(int damage)
	{
		currentHealth -= damage;
        if (currentHealth <= 0) {
            GameObject particle = Instantiate(hitParticle, transform.position, Quaternion.identity);
            particle.transform.rotation = Quaternion.Euler(90, 0, 0);
            Destroy(particle, 2);
            Destroy(gameObject.transform.parent.gameObject);
        } else {
            animController.SetTrigger("hit");
        }
    }

}
