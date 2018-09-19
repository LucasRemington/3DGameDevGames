using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {


	public int lastgateHP = 10;
	public AudioSource alarm;

	public GameObject[] enemies;
	public GameObject gates;
	public Transform[] spawnPoints; // 0-4 is the left lane, 5-9 is the right lane

	public static int gateHP = 10;
	public static int killScore = 0;
	public int currentWave = 1;
	int maxWaves = 10;
	int enemySpawn;
	int c = 0;
	int cc = 3;

	float time;
	float roundTime;
	float startTime;
	public float spawnTime = 5f;

	bool r1;
	bool t1;
	bool r2;
	bool t2;
	bool r3;
	bool t3;
	bool r4;
	bool t4;
	bool r5;
	bool t5;
	bool i;

	//private WaitForSeconds waveDuration = new WaitForSeconds(2.5f);
	public Text gateHealthcount;
	public Text killScorecount;
	public Text waveCount;
	public Text nextWave;
	public Text loseText;
	public GameObject player;

	void Start()
	{
		time = 0;
		currentWave = 0;
		waveCount.text = "Wave " + currentWave.ToString ();

	}

	void Update()
	{
		//gateHealthcount.text = "Health: " + gateHP.ToString ();
		waveCount.text = currentWave.ToString ();
		//nextWave.text = "Next wave starts in: 3";
		//killScorecount.text = "Score: " + killScore.ToString ();
		startTime += Time.deltaTime;
		enemySpawn = Mathf.RoundToInt(startTime);
		roundTime = time += Time.deltaTime;
		roundTime = Mathf.Round(roundTime * 100f) / 100;
		roundTime = Mathf.RoundToInt (roundTime);
		Debug.Log (roundTime);

		if (lastgateHP > gateHP) {
			alarm.Play();
			Debug.Log ("Alarm");
		}
		gateHealthcount.text = "Health: " + gateHP.ToString ();
		waveCount.text = "Wave " + currentWave.ToString ();
		killScorecount.text = "Score: " + killScore.ToString ();
		lastgateHP = gateHP;

		if (roundTime == 7) 
		{
			if (t1 == false) {
				StartCoroutine (ActualCountdown ());
				t1 = true;
			}
		}

		if (roundTime == 10) 
		{
			if (r1 == false)
			{
				r1 = true;
				Instantiate (enemies [0], spawnPoints [0].position, spawnPoints [0].rotation);
				Instantiate (enemies [0], spawnPoints [1].position, spawnPoints [1].rotation);
				Instantiate (enemies [1], spawnPoints [2].position, spawnPoints [2].rotation);
				Instantiate (enemies [1], spawnPoints [3].position, spawnPoints [3].rotation);
				currentWave += 1; 
			}
		}

		if (roundTime == 17) 
		{
			if (t2 == false) {
				StartCoroutine (ActualCountdown ());
				t2 = true;
			}
		}

		if (roundTime == 20) 
		{
			if (r2 == false)
			{
				r2 = true;
				Instantiate (enemies [1], spawnPoints [4].position, spawnPoints [4].rotation);
				Instantiate (enemies [1], spawnPoints [5].position, spawnPoints [5].rotation);
				Instantiate (enemies [0], spawnPoints [6].position, spawnPoints [6].rotation);
				Instantiate (enemies [0], spawnPoints [7].position, spawnPoints [7].rotation);
				currentWave += 1; 
				Invoke ("Countdown", 7);

			}
		}
		if (roundTime == 27) 
		{
			if (t3 == false) {
				StartCoroutine (ActualCountdown ());
				t3 = true;
			}
		}

		if (roundTime == 30) 
		{
			if (r3 == false)
			{
				r3 = true;
				Instantiate (enemies [1], spawnPoints [4].position, spawnPoints [4].rotation);
				Instantiate (enemies [0], spawnPoints [5].position, spawnPoints [5].rotation);
				Instantiate (enemies [2], spawnPoints [6].position, spawnPoints [6].rotation);
				Instantiate (enemies [2], spawnPoints [7].position, spawnPoints [7].rotation);
				currentWave += 1; 
				Invoke ("Countdown", 12);

			}
		}
		if (roundTime == 42) 
		{
			if (t4 == false) {
				StartCoroutine (ActualCountdown ());
				t4 = true;
			}
		}
		if (roundTime == 45) 
		{
			if (r4 == false)
			{
				r4 = true;
				Instantiate (enemies [1], spawnPoints [5].position, spawnPoints [5].rotation);
				Instantiate (enemies [1], spawnPoints [6].position, spawnPoints [6].rotation);
				Instantiate (enemies [0], spawnPoints [7].position, spawnPoints [7].rotation);
				Instantiate (enemies [0], spawnPoints [4].position, spawnPoints [4].rotation);
				Instantiate (enemies [1], spawnPoints [5].position, spawnPoints [5].rotation);
				Instantiate (enemies [1], spawnPoints [6].position, spawnPoints [6].rotation);
				Instantiate (enemies [0], spawnPoints [7].position, spawnPoints [7].rotation);
				Instantiate (enemies [0], spawnPoints [4].position, spawnPoints [4].rotation);
				currentWave += 1;

			}
		}

		if (roundTime == 87) 
		{
			if (t5 == false) {
				StartCoroutine (ActualCountdown ());
				t5 = true;
			}
		}
		if (roundTime == 90) 
		{
			if (r5 == false)
			{
				r5 = true;
				Instantiate (enemies [1], spawnPoints [5].position, spawnPoints [5].rotation);
				Instantiate (enemies [1], spawnPoints [6].position, spawnPoints [6].rotation);
				Instantiate (enemies [0], spawnPoints [7].position, spawnPoints [7].rotation);
				Instantiate (enemies [0], spawnPoints [4].position, spawnPoints [4].rotation);
				Instantiate (enemies [1], spawnPoints [5].position, spawnPoints [5].rotation);
				Instantiate (enemies [1], spawnPoints [6].position, spawnPoints [6].rotation);
				Instantiate (enemies [0], spawnPoints [7].position, spawnPoints [7].rotation);
				Instantiate (enemies [0], spawnPoints [4].position, spawnPoints [4].rotation);
				currentWave += 1;

			}
		}

		if (roundTime == 130 && i == false) 
		{
			i = true;
			StartCoroutine (InfiniteWave());
			InvokeRepeating ("Spawn", 3f, spawnTime);
			InvokeRepeating ("Spawn", 10f, 10f);
		}

		if (Input.GetKey ("escape")) {
			Application.Quit ();
		}

		if (Input.GetKey ("r")) {
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}

		if (gateHP < 1) {
			loseText.gameObject.SetActive(true);
			Destroy (player);
		}
	}

	IEnumerator FirstRound()
	{
		yield return new WaitForSeconds (3);
		Instantiate (enemies [0], spawnPoints [0].position, spawnPoints [0].rotation);
		Instantiate (enemies [0], spawnPoints [1].position, spawnPoints [0].rotation);
		Instantiate (enemies [0], spawnPoints [2].position, spawnPoints [0].rotation);
	}

	IEnumerator InfiniteWave()
	{
		nextWave.text = "Infinite wave is about to start!";
		yield return new WaitForSeconds (2);
		nextWave.text = "GLHF";
		yield return new WaitForSeconds (0.5f);
		nextWave.text = "";
	}

	IEnumerator ActualCountdown()
	{
		c = 0;
		cc = 3;
		while ( c <= 2)
		{
			nextWave.text = "Next wave starts in: " + cc;
			cc--;
			yield return new WaitForSeconds (1);
			c++;
		}

		if (c == 3) 
		{
			nextWave.text = "";
		}
	}

	void SpawnDecrease()
	{
		spawnTime /= 2;
	}

	void Spawn()
	{
		int spawnPointIndex = Random.Range (0, spawnPoints.Length);
		int enemiesIndex = Random.Range (0, enemies.Length);
		Instantiate (enemies [enemiesIndex], spawnPoints [spawnPointIndex].position, spawnPoints [spawnPointIndex].rotation);
	}
}
	

