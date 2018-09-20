using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonLerp : MonoBehaviour {

	public GameObject enabler1;
	public GameObject enabler2;

	public bool blockCooldown;

	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Block" && blockCooldown == false) {
			enableStuff ();
			blockCooldown = true;
			Debug.Log ("snap");
			StartCoroutine (cooldown ());
		}
	}

	void OnTriggerExit (Collider other)
	{
		if (other.tag == "Block" && blockCooldown == false) {
			enableStuff ();
			blockCooldown = true;
			StartCoroutine (cooldown ());
		}
	}

	void enableStuff () {
		if (enabler1.active == true) {
			enabler1.SetActive (false);
		} else {
			enabler1.SetActive (true);

			if (enabler2.active == true) {
				enabler2.SetActive (false);
			} else {
				enabler2.SetActive (true);
			}
		}
	}

	IEnumerator cooldown () {
		yield return new WaitForSecondsRealtime (0.5f);
		blockCooldown = false;
	}
}
