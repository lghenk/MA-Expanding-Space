using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FinishGameManager : MonoBehaviour {

    private PlayerPickup player;
    public BoardCreator _bc;
    public Image fadeImage;

    [Header("Got All Stones")]
    public UnityEvent goteem = new UnityEvent();
    private bool hasInvoked = false;


    public void Initialize() {
        hasInvoked = false;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPickup>();
        fadeImage.canvasRenderer.SetAlpha(0.0f);
    }

	// Update is called once per frame
	void Update () {
        if (player == null)
            return;

        if(player.stonesPickedup >= _bc.stones.Count && !hasInvoked) {
            goteem.Invoke();
            hasInvoked = true;
        }

        if(Vector3.Distance(_bc.getRocketPos(), player.transform.position) < 5 && player.stonesPickedup >= _bc.stones.Count) {
            print("Player Close To Rocket");
            player.isDone = true;
            fadeImage.CrossFadeAlpha(1.0f, 1.0f, true);
            StartCoroutine(winScreen());
        }
	}

    IEnumerator winScreen() {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("WinScreen");
    }
}
