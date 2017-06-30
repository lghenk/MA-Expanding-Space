using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChargeImage : MonoBehaviour {
    [Header("UI Component")]
    public Image heatBar;

    private PlayerShoot _ps;

    private void Start() {
        heatBar = GetComponent<Image>();
        //_ps = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerShoot>();
    }

    private void OnGUI() {
        //chargeText.text = Mathf.Floor(currentCharge) + "%";
        if(_ps == null) {
            if(GameObject.FindGameObjectWithTag("Player")) {
                _ps = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerShoot>();
            }
            return;
        }

        heatBar.fillAmount = _ps.currentHeat / 100;
    }
}
