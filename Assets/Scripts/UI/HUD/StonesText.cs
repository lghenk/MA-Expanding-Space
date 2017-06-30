using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StonesText : MonoBehaviour {

    public BoardCreator generatedBoard;
    public PlayerPickup playerPickup;
    public Text text;

	// Use this for initialization
	void Start () {
        generatedBoard = GameObject.FindGameObjectWithTag("GameManager").GetComponent<BoardCreator>();
        text = GetComponent<Text>();
	}

    private void OnGUI() {
        if (playerPickup == null) {
            if (GameObject.FindGameObjectWithTag("Player")) {
                playerPickup = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPickup>();
            }
            return;
        }

        if(text == null) {
            if(GetComponent<Text>()) {
                text = GetComponent<Text>();
            } else {
                return;
            }
        }

        text.text = playerPickup.stonesPickedup + "/" + generatedBoard.numStomes;
    }
}
