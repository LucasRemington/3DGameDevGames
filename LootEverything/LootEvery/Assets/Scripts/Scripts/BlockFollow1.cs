using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BlockFollow1 : MonoBehaviour {

	public Vector3 offset;
	public GameObject player;
	public Transform blockHold;
	public bool dragging = false;
	public bool canDrag;
	public bool mouseDrag;
	private float lockY;
	public PlayerControl PC;
	//public TempInvisibleWall wallScript;
	public bool isObstacle = true;
	public float smootherTime = 0.3f;
	private Vector3 speed = Vector3.zero;
	public bool hasMoved = false;
	private Rigidbody rigBod;
	public Transform blockHolder;
	public float distance;
	public float dragRange = 1f;
	public bool snapped = false;


	// Use this for initialization
	void Start () {
		lockY = transform.position.y;
		rigBod = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () 

	{
		Vector3 playerPosition = new Vector3 (player.GetComponent<Transform> ().position.x, player.GetComponent<Transform> ().position.y, player.GetComponent<Transform> ().position.z );
		distance = Vector3.Distance (playerPosition, transform.position);
		if (distance <= dragRange) {
			canDrag = true;
		} else 
		{
			canDrag = false;
		}

		if (Input.GetKeyDown (KeyCode.Mouse0) && canDrag == true && mouseDrag == true)
			{

			if (dragging == false)
			{
				offset = transform.position - player.transform.position;
				dragging = true;
				hasMoved = true;
				transform.parent = null;
			} else 
			{
				dragging = false;
			}
	
		
			}

		/*if (Input.GetKeyUp (KeyCode.Mouse0))
		{
			dragging = false;
		}*/

		if (dragging == true) 
		{
			rigBod.isKinematic = true;
			//transform.position = player.transform.position + offset;
			transform.position = blockHolder.position;
			isObstacle = false;
			player.GetComponent<PlayerControl> ().anim.SetBool ("Telekinesis", true);
		} 
		else
		{
			rigBod.isKinematic = false;
			player.GetComponent<PlayerControl>().anim.SetBool ("Telekinesis", false);
		}
		if (snapped == true) 
		{
			isObstacle = false;
		}
		if (snapped == false && dragging == false) 
		{
			isObstacle = true;
		}
		if (isObstacle == false) 
		{
			GetComponent <NavMeshObstacle> ().enabled = false;
		}
		else 
		{
			GetComponent <NavMeshObstacle> ().enabled = true;
		}

		/*if (transform.position.y < lockY && dragging == false) {
			transform.position = new Vector3 (0f, lockY, 0f);
		}*/
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Player") {
			//canDrag = true;
			PC.nearBlock = true;
		}
		if (other.tag == "BlockHold") 
		{
			GetComponent <NavMeshObstacle> ().enabled = false;
		}
	}

	void OnMouseEnter () 
	{
		mouseDrag = true;
	}

	void OnMouseExit () {
		mouseDrag = false;
	}

	void OnTriggerStay (Collider other) {
		if (other.tag == "BlockHold" && dragging == false && hasMoved == true) 
		{
			/*wallScript = other.GetComponent<TempInvisibleWall>();
			if (wallScript.floorIsActive == false) 
			{
				GetComponent <NavMeshObstacle> ().enabled = false;
				blockHold = other.GetComponent<Transform> ();
				//transform.SetParent (blockHold);
				//transform.position = blockHold.transform.position;
				snapped = true;
				StartCoroutine (MoveCube ());
			}*/
		}
	}

	void OnTriggerExit (Collider other)
	{
		if (other.tag == "Player") 
		{
			//canDrag = false;
			PC.nearBlock = false;
		}
		if (other.tag == "BlockHold") 
		{
			//isObstacle = true;
			snapped = false;
		}
	}

	public IEnumerator MoveCube () 
	{
		//Vector3 originScale = transform.localScale;
		//transform.SetParent (blockHold);
		//transform.localScale = originScale;
		rigBod.isKinematic = true;
		transform.position = Vector3.SmoothDamp (transform.position, blockHold.position, ref speed, smootherTime);
		yield return new WaitForSeconds (2.0f);
		rigBod.isKinematic = false;
	}
}
