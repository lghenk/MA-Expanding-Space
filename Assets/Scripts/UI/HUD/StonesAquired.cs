using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StonesAquired : MonoBehaviour {
    
    public string aquiredText = "Stones Aquired.\nReturn to your rocket";
    public Text text;

	void OnEnable() {
        StartCoroutine(AnimateText(aquiredText));
	}

    IEnumerator AnimateText(string strComplete) {
        text.text = "";
        for (int i = 0; i < strComplete.Length; i++) {
            text.text += strComplete[i];
            yield return new WaitForSeconds(0.05F);
        }
    }
}
