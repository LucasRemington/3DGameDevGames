using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sign : MonoBehaviour {

	public bool canRead = false;
	//public GameObject obj;
	public Image signText;
	public Text signTextText;
	public PlayerControl PC;
	public int signNumber;

	void Start () {
		//obj = GetComponent <GameObject> ();
		signText.enabled = false;
		signTextText.enabled = false;
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.Mouse0) && canRead == true && PC.canReadSign == true) {
			PC.anim.SetTrigger ("ReadSign");
			PC.navMeshAgent.isStopped = true;
			Debug.Log ("good");
			signText.enabled = true;
		}
		if (Input.GetKeyDown (KeyCode.Mouse1) || PC.canReadSign == false) {
			signText.enabled = false;
		}

		if (signText.enabled == true) {
			signText.enabled = true;

		} else {
			//signText.enabled = false;
		}
		if (signText.enabled == true) {
			signTextText.enabled = true;
			/*if (signNumber == 1) {
				signTextText.text = "Welcome to Loot Everything! Move around with right click, and use left click to interact with nearby objects.";
			}
			if (signNumber == 2) {
				signTextText.text = "Walk over keys to collect them. When you have a key, clicking a door will open it.";
			}
			if (signNumber == 3) {
				signTextText.text = "If you get near a chest, you'll open it. Chests contain gold and sometimes keys.";
			}
			if (signNumber == 4) {
				signTextText.text = "Every 100 gold collected will increase your morale, making your abilities stronger.";
			}
			if (signNumber == 5) {
				signTextText.text = "Right click enemies to attack them, press space to block.";
			}
			if (signNumber == 6) {
				signTextText.text = "You can cast spells with QWE. Q boosts attack, W heals you, and E boosts speed.";
			}
			if (signNumber == 7) {
				signTextText.text = "This beautiful slime statue glistens wetly...";
			}
			if (signNumber == 8) {
				signTextText.text = "You can pick up cubes by left clicking, and drop them by clicking the cube. Place them on buttons to reveal secrets.";
			}
			if (signNumber == 9) {
				signTextText.text = "This hallway is VERY long.";
			}
			if (signNumber == 10) {
				signTextText.text = "You are now leaving the tutorial. Death awaits you, as it awaits us all. Good luck!";
			}*/
		} else {
			signTextText.enabled = false;
		}

	}

	void OnMouseEnter () {
		canRead = true;
	}

	void OnMouseExit () {
		canRead = false;
	}

}
