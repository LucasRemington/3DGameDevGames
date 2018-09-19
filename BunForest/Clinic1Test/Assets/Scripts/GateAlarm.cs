using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateAlarm : MonoBehaviour {

	private AudioSource source;
	public AudioClip alarm;

	public void Alarm () {
		source.PlayOneShot(alarm);
	}
}
