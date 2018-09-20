using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour {

    public Rigidbody rb;
    private float speed = 10.0f;
    public PlayerMovement pm;
    public GameObject player;

	void Start () {
        player = GameObject.FindWithTag("Player");
        rb = GetComponent<Rigidbody>();
        pm = player.GetComponent<PlayerMovement>();
		if (Input.GetButtonDown("Fire1"))
		{
			rb.velocity = (Vector3.forward * speed);
		}
		if (Input.GetButtonDown("Fire2"))
		{
			rb.velocity = (Vector3.back * speed);
		}

	}
	
	void Update () {
        Object.Destroy(gameObject,5.0f);
	}
}
