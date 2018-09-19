using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

	public GameObject target1;
	public GameObject target2;
	public GameObject target3;
	public GameObject target4;
	public GameObject target5;
	public GameObject target6;
	public GameObject target7;
	public GameObject target8;
	public GameObject target9;
	public GameObject target10;
	public Transform trans1;
	public Transform trans2;
	public Transform trans3;
	public Transform trans4;
	public Transform trans5;
	public Transform trans6;
	public Transform trans7;
	public Transform trans8;
	public Transform trans9;
	public Transform trans10;

	Transform target;

	public int pinTarget;
	public float speed;

	public int finalLoopNumber;
	private int loopNumber;
	public bool loopPinPath;

	public bool spawnOne = true;

	void Start () {
		SetTargets ();
	}

	void Awake () {
		
		SetTargets ();
	}

	void OnTriggerEnter(Collider other) {
		
		if(other.gameObject.tag == "Pin1" && pinTarget == 1) {
			pinTarget = pinTarget + 1;
			Debug.Log("ChangedPinPath");
		} else if(other.gameObject.tag == "Pin2" && pinTarget == 2) {
			pinTarget = pinTarget + 1;
			Debug.Log("ChangedPinPath");
		} else if(other.gameObject.tag == "Pin3" && pinTarget == 3) {
			pinTarget = pinTarget + 1;
			Debug.Log("ChangedPinPath");
		} else if(other.gameObject.tag == "Pin4" && pinTarget == 4) {
			pinTarget = pinTarget + 1;
			Debug.Log("ChangedPinPath");
		} else if(other.gameObject.tag == "Pin5" && pinTarget == 5) {
			pinTarget = pinTarget + 1;
			Debug.Log("ChangedPinPath");
		} else if(other.gameObject.tag == "Pin6" && pinTarget == 6) {
			pinTarget = pinTarget + 1;
			Debug.Log("ChangedPinPath");
		} 


		if(other.gameObject.tag == "Goal") {
			Destroy(gameObject);
			GameController.gateHP--;
			Debug.Log("Damage");
		}
			
	}

	void OnTriggerStay(Collider other) {

		if(other.gameObject.tag == "Spawn1") {
			spawnOne = true;
		}

		if(other.gameObject.tag == "Spawn2") {
			spawnOne = false;
		}
	}


	void FixedUpdate () {
		float step = speed * Time.deltaTime;
		if (spawnOne == true) {
			if (pinTarget == 1) {
				transform.position = Vector3.MoveTowards (transform.position, trans1.position, step);
			} else if (pinTarget == 2) {
				transform.position = Vector3.MoveTowards (transform.position, trans2.position, step);
			} else if (pinTarget == 3) {
				transform.position = Vector3.MoveTowards (transform.position, trans3.position, step);
			} else if (pinTarget == 4) {
				transform.position = Vector3.MoveTowards (transform.position, trans4.position, step);
			} else if (pinTarget == 5) {
				transform.position = Vector3.MoveTowards (transform.position, trans5.position, step);
			} else if (pinTarget == 6) {
				transform.position = Vector3.MoveTowards (transform.position, trans6.position, step);
			}
		} else 
			if (pinTarget == 1) {
				transform.position = Vector3.MoveTowards (transform.position, trans7.position, step);
			} else if (pinTarget == 2) {
				transform.position = Vector3.MoveTowards (transform.position, trans8.position, step);
			} else if (pinTarget == 3) {
				transform.position = Vector3.MoveTowards (transform.position, trans9.position, step);
			} else if (pinTarget == 4) {
				transform.position = Vector3.MoveTowards (transform.position, trans10.position, step);
			} else if (pinTarget == 5) {
				transform.position = Vector3.MoveTowards (transform.position, trans5.position, step);
			} else if (pinTarget == 6) {
				transform.position = Vector3.MoveTowards (transform.position, trans6.position, step);
			}

		if (loopPinPath == true && pinTarget > 7) {
			pinTarget = 1;
			if (loopNumber < finalLoopNumber) {
				loopNumber++;
			} else {
				loopPinPath = false;
			}
		}
	}

		void SetTargets () {
		pinTarget = 1;
		loopNumber = 0;
		Debug.Log("targets" +
			"woo");

		target1 = GameObject.Find("Pin1");
		trans1 = target1.transform;

		target2 = GameObject.Find("Pin2");
		trans2 = target2.transform;

		target3 = GameObject.Find("Pin3");
		trans3 = target3.transform;

		target4 = GameObject.Find("Pin4");
		trans4 = target4.transform;

		target5 = GameObject.Find("Pin5");
		trans5 = target5.transform;

		target6 = GameObject.Find("Pin6");
		trans6 = target6.transform;

		target7 = GameObject.Find("Pin7");
		trans7 = target7.transform;

		target8 = GameObject.Find("Pin8");
		trans8 = target8.transform;

		target9 = GameObject.Find("Pin9");
		trans9 = target9.transform;

		target10 = GameObject.Find("Pin10");
		trans10 = target10.transform;

	}
}
