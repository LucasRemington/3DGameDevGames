using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour {


    public BoxCollider view;
    public EnemyMove em;
    public PlayerMovement pm;
    public Transform target;
    public int combat;

    void Start () {
        view = GetComponent<BoxCollider>();

	}
	
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
            combat = 1;
            transform.LookAt(pm.player.transform);
            transform.Translate(0, 0, 1);
           

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            combat = 0;
        }

    }
}
