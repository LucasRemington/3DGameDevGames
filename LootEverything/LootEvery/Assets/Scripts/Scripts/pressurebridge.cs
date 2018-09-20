using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pressurebridge : MonoBehaviour {

	public Animator bridgeAnim;
	private bool bridgeCooldown;
	private bool bridgeDownCooldown;
	private Animator anim;

	void Start () {
		anim = GetComponent<Animator>();
	}

	void OnTriggerEnter (Collider other)
	{
		if ((other.tag == "Block") && bridgeCooldown == false) {
			StartCoroutine (BridgeCooldown ());
		}
	}

	void OnTriggerStay (Collider other)
	{
		if ((other.tag == "Block") && bridgeCooldown == false) {
			StartCoroutine (BridgeCooldown ());
		}
	}

	void OnTriggerExit (Collider other)
	{
		if ((other.tag == "Block") && bridgeDownCooldown == false) {
			StartCoroutine (BridgeDownCooldown ());
		}
	}

	IEnumerator BridgeCooldown () {
		bridgeCooldown = true;
		//anim.SetTrigger ("Press");
		bridgeAnim.SetTrigger ("Bridge");
		yield return new WaitForSeconds(0.1f);
		bridgeCooldown = false;
	}

	IEnumerator BridgeDownCooldown () {
		bridgeDownCooldown = true;
		//anim.SetTrigger ("Press");
		bridgeAnim.SetTrigger ("BridgeDown");
		yield return new WaitForSeconds(0.1f);
		bridgeDownCooldown = false;
	}
}
