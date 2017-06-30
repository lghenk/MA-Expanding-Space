using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChargeText : MonoBehaviour {
    [Header("UI Components")]
    public Text chargeText;

    [Header("UI Images")]
    public Sprite canShootSprite;
    public Sprite cantShootSprite;

    private PlayerShoot _ps;

    private void Start() {
        //_ps = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerShoot>();
        chargeText = GetComponent<Text>();
    }

    private void OnGUI() {
        if(_ps == null) {
            if(GameObject.FindGameObjectWithTag("Player")) {
                _ps = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerShoot>();
            }
            return;
        }

        chargeText.text = Mathf.Floor(_ps.currentHeat) + "%";
    }
}
