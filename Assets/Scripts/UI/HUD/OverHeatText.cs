using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OverHeatText : MonoBehaviour {
    [Header("UI Components")]
    public GameObject overHeatText;
    private PlayerShoot _ps;

    private void OnGUI() {
        if (_ps == null) {
            if(GameObject.FindGameObjectWithTag("Player")) {
                _ps = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerShoot>();
            }
            return;
        }    
        
        if(overHeatText == null) {
            overHeatText = transform.GetChild(0).gameObject;
            return;
        }

        if (_ps.coolingDown) {
            overHeatText.SetActive(true);
        } else {
            overHeatText.SetActive(false);
        }
    }
}
