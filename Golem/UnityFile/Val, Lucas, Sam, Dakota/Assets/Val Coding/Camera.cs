using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour {

    private Transform look;
    private Vector3 offset;
    private Vector3 moveV;

	void Start () {
        look = GameObject.FindGameObjectWithTag("Player").transform;
        offset = transform.position - look.position;
	}
	
	// Update is called once per frame
	void Update () {
        moveV = look.position + offset;
        moveV.x = 0.0f;
        moveV.y = Mathf.Clamp(moveV.y, 3, 5);

        transform.position = moveV;
	}
}
