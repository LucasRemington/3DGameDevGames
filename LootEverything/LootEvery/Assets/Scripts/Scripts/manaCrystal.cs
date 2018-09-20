using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class manaCrystal : MonoBehaviour {

	private bool mouseDrag;
	public GameObject player;
	public float manaRegen = 1f;
	private float manaCool;
	public float totalMana = 50f;
	private bool canDrain = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKey (KeyCode.Mouse0) && canDrain == true) 
		{
			if (Time.time > manaCool && player.GetComponent<PlayerControl>().mana < 97f && totalMana > 0f) 
			{
				manaCool = Time.time + manaRegen;
				player.GetComponent<PlayerControl> ().mana = player.GetComponent<PlayerControl> ().mana + 5f;
			}
		}
	}
	void OnMouseEnter () {
		mouseDrag = true;
	}

	void OnMouseExit () {
		mouseDrag = false;
	}

	void OnTriggerEnter (Collider other) 
	{
		if (other.tag == "Player") 
		{
			canDrain = true;
		}
	}

	void OnTriggeExit (Collider other)
	{
		if (other.tag == "Player") 
		{
			canDrain = false;
		}
	}
}
