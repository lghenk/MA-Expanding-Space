using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CutScene : MonoBehaviour {

    public int curScene = 0;
    public Sprite[] scenes;
    public Image image;
	
	// Update is called once per frame
	void Update () {
		if(Input.anyKeyDown) {
            curScene += 1;
            if(curScene > scenes.Length - 1) {
                SceneManager.LoadScene("LevelGenerator");
                return;
            }

            image.sprite = scenes[curScene];
        }
	}
}
