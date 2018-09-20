using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class protagopendoor : MonoBehaviour {

	public PlayerControl PC;


	public void doorSync () {
		PC.doorAnim.SetTrigger ("Open");
	}
}
