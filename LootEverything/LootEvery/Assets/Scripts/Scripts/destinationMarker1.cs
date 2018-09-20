using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destinationMarker1 : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Player") 
		{
			GetComponent<MeshRenderer> ().enabled = false;
		}
	}
	void OnTriggerStay (Collider other)
	{
		if (other.tag == "Player") 
		{
			GetComponent<MeshRenderer> ().enabled = false;
		}
	}
}
