using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour{
	
    public GameObject mainMenuHolder;
    public GameObject optionsMenuHolder;
    public Toggle[] resolutionToggles;
    public Toggle fullscreenToggle;
    public int[] screenWidths;
    int activeScreenResIndex;

    void Start() {
	    for (int i = 0; i < resolutionToggles.Length; i++) {
		    resolutionToggles [i].isOn = i == activeScreenResIndex;
	    }
    }

    public void Play() {
	    SceneManager.LoadScene ("Cutscene");
    }

    public void Quit() {
		SceneManager.LoadScene ("MainMenu");
    }

	public void Replay(){
	    SceneManager.LoadScene ("LevelGenerator");
	}

    public void OptionsMenu() {
	    mainMenuHolder.SetActive (false);
	    optionsMenuHolder.SetActive (true);
    }

    public void MainMenu() {
	    mainMenuHolder.SetActive (true);
	    optionsMenuHolder.SetActive (false);
    }

    public void SetScreenResolution(int i) {
	    if (resolutionToggles [i].isOn) {
		    activeScreenResIndex = i;
		    float aspectRatio = 16 / 9f;
		    Screen.SetResolution (screenWidths [i], (int)(screenWidths [i] / aspectRatio), false);		
	    }
    }

    public void SetFullscreen(bool isFullscreen) {
	    for (int i = 0; i < resolutionToggles.Length; i++) {
		    resolutionToggles [i].interactable = !isFullscreen;
	    }

	    if (isFullscreen) {
		    Resolution[] allResolutions = Screen.resolutions;
		    Resolution maxResolution = allResolutions [allResolutions.Length - 1];
		    Screen.SetResolution (maxResolution.width, maxResolution.height, true);
	    } else {
		    SetScreenResolution (activeScreenResIndex);
	    }	
    }
}