using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class HealthScript : MonoBehaviour {

    private PlayerHealth _ph;
    private Image img;

    private void Awake() {
        img = GetComponent<Image>();
    }

    private void OnGUI() {
        if (_ph == null) {
            if (GameObject.FindGameObjectWithTag("Player")) {
                _ph = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
            }
            return;
        }

        img.fillAmount = _ph.currentHealth / 100f;
    }
}
