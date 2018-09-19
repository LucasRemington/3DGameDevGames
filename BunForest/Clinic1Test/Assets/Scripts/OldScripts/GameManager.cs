using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static int Health;
	public static int Score;
	public Text healthCount;
	public Text scoreCount;

	public int waveCount;

	void Start () {
		Health = 10;
		Score = 0;
	}
	

	void Update () {
		healthCount.text = "Health: " + Health.ToString ();
		scoreCount.text = "Score: " + Score.ToString ();
	}
}
