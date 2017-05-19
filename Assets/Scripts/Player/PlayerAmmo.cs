using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAmmo : MonoBehaviour {
	public int currentAmmo;	
	public Text uitext;


	
	// Update is called once per frame
	public void Shoot () {
		if(uitext != null){
			uitext.text = currentAmmo.ToString ();
	}
}
}
