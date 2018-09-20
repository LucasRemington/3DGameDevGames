using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beanopendoor : MonoBehaviour {

	public bool canOpen = false;
	public bool isOpen;
	public BoxCollider box;
	public PlayerControl PC;
	public Animator doorAnim;

	void Start () {
		box = GetComponent <BoxCollider> ();
		isOpen = false;
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.Mouse0) && canOpen == true && PC.canOpenDoor == true && PC.keyCount > 0 && isOpen == false)
		{
			PC.openDoor ();
			//doorAnim.SetTrigger ("Open");
			Destroy (box);
			isOpen = false;
		}
	}

	void OnTriggerEnter (Collider other) {
		if (other.tag == "Player") {
			PC = other.GetComponent<PlayerControl>();
		}
	}

	void OnMouseEnter () {
		canOpen = true;
	}

	void OnMouseExit () {
		canOpen = false;
	}

}
	