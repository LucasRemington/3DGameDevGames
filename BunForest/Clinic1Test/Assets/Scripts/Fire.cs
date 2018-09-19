using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour {

    public float fireRate = 1;
    public int DMG = 1;
    public Transform bezzel;

    private Camera fpsCam;
    private WaitForSeconds lineDuration = new WaitForSeconds(1.3f);
    private AudioSource rifleShot;
    private LineRenderer laserLine;
    private float nextFire;


	void Start ()
	    {
	        laserLine = GetComponent<LineRenderer>();
	        rifleShot = GetComponent<AudioSource>();
	        fpsCam = GetComponentInParent<Camera>();
		}
	
	void Update ()
	    {
			if (Input.GetButtonDown ("Fire1") && Time.time > nextFire)
	        {
	            nextFire = Time.time + fireRate;
	            StartCoroutine(ShotTraceFX());

	            Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f,0));
	            RaycastHit hit;
	            laserLine.SetPosition(0, bezzel.position);

	            if (Physics.Raycast(rayOrigin, fpsCam.transform.forward, out hit))
		            {
		                laserLine.SetPosition(1, hit.point);

		                Enemy1 target1 = hit.transform.GetComponent<Enemy1>();
							if (target1 != null) 
							{
								target1.TakeDamage(DMG);
							}

						Enemy2 target2 = hit.transform.GetComponent<Enemy2>();
							if (target2 != null) 
							{
								target2.TakeDamage2(DMG);
							}

						Enemy3 target3 = hit.transform.GetComponent<Enemy3>();
							if (target3 != null) 
							{
								target3.TakeDamage3(DMG);
							}
							if (hit.rigidbody != null)
							{
								hit.rigidbody.AddForce (-hit.normal * 1555);
							}
		            }
	            else
		            {
		                laserLine.SetPosition(1, rayOrigin + (fpsCam.transform.forward * 100));
		            }
	        }

		}

    private IEnumerator ShotTraceFX()
	    {
	        rifleShot.Play();

	        laserLine.enabled = true;
	        yield return lineDuration;
            laserLine.enabled = false;
	    }
}
