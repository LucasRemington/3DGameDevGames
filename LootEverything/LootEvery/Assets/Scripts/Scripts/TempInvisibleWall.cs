using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempInvisibleWall : MonoBehaviour {

	public bool floorIsActive = true;
	public GameObject walkable;
	public GameObject deadBlock;

	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
		if (floorIsActive == false) 
		{
			
			walkable.SetActive (false);
		} 
		else 
		{
			
			walkable.SetActive (true);
		}

	}

	void OnTriggerStay (Collider other) 
	{
		if (other.tag == "Block") 
		{
			floorIsActive = false;
		} 
		else 
		{
			floorIsActive = true;
		}
	}

	/*void OnTriggerExit (Collider other)
	{
		if (other.tag == "Block") 
		{
			floorIsActive = true;
		}
	}
*/
}
