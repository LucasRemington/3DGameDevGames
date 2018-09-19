using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scope : MonoBehaviour {

	public int scope = 1;
	public int noScope = 60;
	float lerp = 33;
	public GameObject reticule;
	Animator anim;
	bool Zoomed;
		
	void Start ()
	{
		anim = reticule.GetComponent<Animator> ();
	}


	void Update () 
		{
			if (Input.GetButtonDown ("Fire2")) 
				{
					GetComponentInParent<Camera>().fieldOfView = Mathf.Lerp (GetComponent<Camera> ().fieldOfView, scope, Time.deltaTime * lerp);
				anim.SetBool ("Zoomed", true);
			//GetComponentInParent<Camera> ().fieldOfView = scope;
				} 
			if (Input.GetButtonUp ("Fire2")) 
				{
					GetComponentInParent<Camera> ().fieldOfView = noScope;
				anim.SetBool ("Zoomed", false);
				}
		}
}
