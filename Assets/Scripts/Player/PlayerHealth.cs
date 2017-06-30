using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour {
	public int maxY = -10;

    [Header("Health Data")]
    public int maxHealth = 10;
    public int startingHealth = 5;
	public int currentHealth;

    public bool isDead = false;
    private Animator animController;
    public GameObject hitParticle;

    [Header("UI Components")]
    public GameObject healthHolder;
    private List<Image> healthUIList = new List<Image>();

    [Header("UI Sprites")]
    public Sprite heart;
    public Sprite brokenHeart;

    public void addHealth(int amount){
        currentHealth += amount;
        if (currentHealth > maxHealth) {
            currentHealth = maxHealth;
        }	
	}

    private PlayerPickup _pp;
	// Use this for initialization
	void Start () {
		currentHealth = startingHealth;
        animController = GetComponent<Animator>();
        _pp = GetComponent<PlayerPickup>();
	}

	public void HurtPlayer(int damageAmount) {
        if (_pp.isDone)
            return;

		currentHealth -= damageAmount;
        GameObject particle = Instantiate(hitParticle, transform.position, Quaternion.identity);
        particle.transform.rotation = Quaternion.Euler(90, 0, 0);
        Destroy(particle, 2);

        if (currentHealth <=0) {
            isDead = true;
            animController.SetBool("isDead", true);
            StartCoroutine(openDeath());
		}
	}

    IEnumerator openDeath() {
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene("GameOver");
    }

    private void OnGUI() {
        for(int i = 0; i < healthUIList.Count; i++) {
            if(currentHealth >= i+1) {
                healthUIList[i].sprite = heart;
            } else {
                healthUIList[i].sprite = brokenHeart;
            }
        }
    }
	public void Update() {
		if (transform.position.y <= maxY) {
			isDead = true;
			StartCoroutine (openDeath ());
		}
	}
}
