using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMarker : MonoBehaviour {

	public Transform parent;
	public GameObject player;
	public PlayerControl playerScript;
	private MeshRenderer selfMesh;

	// Use this for initialization
	void Start ()
	{
		
		selfMesh = GetComponent<MeshRenderer> ();
		player = GameObject.FindGameObjectWithTag ("Player");

	}
	
	// Update is called once per frame
	void Update () 
	{

			if (player.GetComponent<PlayerControl> ().combatTarget == parent)
			{
				selfMesh.enabled = true;
			} 
			else 
			{
				selfMesh.enabled = false;
			}
	}



}
