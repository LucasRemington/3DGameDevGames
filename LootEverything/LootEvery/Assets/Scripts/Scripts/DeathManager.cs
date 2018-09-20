using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathManager : MonoBehaviour {

	public Text gameOver;
	public PlayerControl PC;
	public Button menuReturn;
	public Image gOBackground;
	public Text buttonText;

	void Start () {
		gOBackground.enabled = false;
		gameOver.enabled = false;
		menuReturn.enabled = false;
		buttonText.enabled = false;
	}

	void Update () {
		if (PC.isDead == true) {
			gameOver.enabled = true;
			menuReturn.enabled = true;
			gOBackground.enabled = true;
			buttonText.enabled = true;
		}
	}

	public void OnClick () {
		SceneManager.LoadScene ("MainMenu");
	}
}
