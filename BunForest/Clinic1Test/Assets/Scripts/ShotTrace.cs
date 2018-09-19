using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotTrace : MonoBehaviour {

    private Camera fpsCam
    ;
	
	void Start ()
    {
        fpsCam = GetComponentInParent<Camera>();
    }
	
	void Update ()
    {
        Vector3 traceOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
        Debug.DrawRay(traceOrigin, fpsCam.transform.forward * 100f, Color.yellow);
    }
}
