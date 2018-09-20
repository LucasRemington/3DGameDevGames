using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour {

	public NavMeshAgent navMeshAgent;
	public Transform target;
	public bool targetIsEnemy = false;
	public float attackRange = 5.0f;
	public float blockRate = 1.5f;
	public float blockDuration = 0.6f;
	private const float blockCool = 1.5f;
	public float blockTimer;
	private bool blockCooldown;
	public int keyCount = 0;
	public GameObject attackHitbox;
	public Transform attackSpawn;
	public float health = 100.0f;
	public bool isBlocking = false;
	public GameObject meleeHitbox;
	public GameObject powerAttackHitbox;
	private Lifetime lifetimeScript;
	private float distance;
	private Rigidbody rigBod;
	public Animator anim;

	public Text healthText;
	public Text goldText;
	public Image keyImage;
	public Text winText;
	public Text deathText;

	public Animator doorAnim;
	public Animator chestAnim;
	public Animator keyAnim;
	public BoxCollider chestBox;
	public BoxCollider keyBox;
	public GameObject keyHolder;
	public Transform doorPlayHold;
	public Transform chestPlayHold;
	public bool nearBlock;
	public bool canReadSign;
	public bool canOpenDoor;

	public static int Gold;
	public bool isDead = false;
	private bool stagger = false;
	public float staggerDuration = 1.0f;
	public Texture2D attackCursor;
	public Texture2D moveCursor;
	public Texture2D interactCursor;
	public CursorMode cursorMode = CursorMode.Auto;
	public Vector2 spot = Vector2.zero;
	public GameObject moveMarker;
	public GameObject marker;
	public Transform combatTarget;
	public Transform otherTarget;
	public bool knockedBack = false;
	Vector3 knockDirection;
	public float knockbackStrength = 2f;
	public float mana = 100f;
	public float manaRegen = 3f;
	private float manaCool;
	public float healCost = 50f;
	public float healStrength = 50f;
	public Text manaText;
	private bool cantMove = false;
	public const float healCooldown = 20f;
	private float healTimer;
	public float healRate = 20f;
	private float goldFloat;
	private float speedTimer;
	private float speedRate = 20f;
	public float speedBoost = 5f;
	public float speedCost = 30f;
	public float speedDuration = 5f;
	private float powerCooldown;
	private float powerRate = 20f;
	private float powerTimer;
	public float powerCost = 25f;
	public const float attackCool = 2.0f;
	private float attackTimer;

	public Slider attackIcon;
	public Slider defendIcon;
	public Slider healIcon;
	public Slider powerIcon;
	public Slider speedIcon;
	public Slider healthBar;
	public Slider manaBar;


	void Start () 
	{
		//anim = GetComponent <Animator> ();
		Cursor.SetCursor (moveCursor, spot, cursorMode);
		blockCooldown = false;
		navMeshAgent = GetComponent <NavMeshAgent> ();
		navMeshAgent.updateRotation = false;
		rigBod = GetComponent <Rigidbody> ();
		keyImage.enabled = false;
		Gold = 0;
		attackTimer = 0.0f;
	}
	void Update () 
	{
		goldFloat = (float)Gold;
		healRate = 20f - (goldFloat * 0.01f);
		speedRate = 20f - (goldFloat * 0.01f);
		powerRate = 20f - (goldFloat * 0.01f);
		//Ray directionRay = Camera.main.ScreenPointToRay (Input.mousePosition);
		//RaycastHit currentTarget;
		//Turn();
		attackTimer -= Time.deltaTime;
		if (attackTimer < 0.0f) 
		{
			attackTimer = 0.0f;
		}

		attackIcon.value = attackTimer;

		blockTimer -= Time.deltaTime;
		if (blockTimer < 0.0f) 
		{
			blockTimer = 0.0f;
		}

		defendIcon.value = blockTimer;

		healTimer -= Time.deltaTime;
		if (healTimer < 0.0f) 
		{
			healTimer = 0.0f;
		}

		healIcon.value = healTimer;

		speedTimer -= Time.deltaTime;
		if (speedTimer < 0.0f) 
		{
			speedTimer = 0.0f;
		}

		speedIcon.value = speedTimer;

		powerTimer -= Time.deltaTime;
		if (powerTimer < 0.0f) 
		{
			powerTimer = 0.0f;
		}

		powerIcon.value = powerTimer;
		

		if (cantMove)
		{
			navMeshAgent.velocity = Vector3.zero;
		}
		if (Time.time > manaCool && mana < 100f) 
		{
			manaCool = Time.time + manaRegen;
			mana++;
		} 

		if (Input.GetKeyDown (KeyCode.W) && healTimer <= 0.0f)
		{
			
			if (mana >= healCost) 
			{
				healTimer = healRate;
				mana = mana - healCost;
				health = health + healStrength;
				if (health > 100f) 
				{
					health = 100f;
				}
			}
	
		}
		if (Input.GetKeyDown (KeyCode.E) && speedTimer <= 0.0f)
		{

			if (mana >= speedCost) 
			{
				StartCoroutine (SpeedBoost ());
				speedTimer = speedRate;

			}
		}

		if (Input.GetKey(KeyCode.Q) && powerTimer <= 0.0f)
		{

			if (mana >= powerCost) 
			{
				mana = mana - powerCost;
				transform.LookAt (target);
				if (isDead == false) 
				{
					navMeshAgent.isStopped = true;
				}

				//if (Time.time > attackCooldown) 
				//	{
				anim.SetTrigger ("heavyAttack");
				//attackCooldown = Time.time + attackRate;
				Instantiate (powerAttackHitbox, attackSpawn.position, transform.rotation);
				powerTimer = powerRate;
			}
		}


		//healthText.text = "Health: " + health;
		healthBar.value = health;
		goldText.text = Gold.ToString ();
		//manaText.text = "Mana: " + mana;
		manaBar.value = mana;


		if (navMeshAgent.velocity != Vector3.zero) {
			anim.SetBool ("Idle", false);
		} else if (anim.GetBool("Idle") == false) {
					StartCoroutine (IdleCountCommon ());
					anim.SetBool ("Idle", true);
				}

		rigBod.angularVelocity = Vector3.zero;

		if (Input.GetKey (KeyCode.Space) && blockCooldown == false) 
		{

			if (blockTimer <= 0.0f) 
			{
				StartCoroutine (BlockCooldown ());
				blockTimer = blockCool;
			}

		} 

		/*if (combatTarget != null) 
		{
			navMeshAgent.destination = combatTarget.position;
		}*/
 
		Ray directionRay = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit currentTarget;

		if (combatTarget) 
		{
			//Vector3 lookPoint = new Vector3 (combatTarget.position.x, transform.position.y, combatTarget.position.z);
			//transform.LookAt (lookPoint);
		} 

		if (Physics.Raycast (directionRay, out currentTarget, 200) && isDead == false) 
		{
			target = currentTarget.transform;

			if (target.tag == "Hostile") 
			{
				Cursor.SetCursor (attackCursor, spot, cursorMode);
			}
			else if (target.tag == "Block" || target.tag == "Interact")
			{
				Cursor.SetCursor (interactCursor, spot, cursorMode);
			}
			else 
			{
				Cursor.SetCursor (moveCursor, spot, cursorMode);
			}

			if (Input.GetKeyDown (KeyCode.Mouse1) && target.tag != "Hostile") 
			{
				otherTarget = target;
				marker.GetComponent<MeshRenderer> ().enabled = false;
			}

			if (Input.GetKeyUp (KeyCode.Mouse1) && isDead == false)
			{


				if (target.tag == "Hostile") 
				{
					targetIsEnemy = true;
					combatTarget = target;
					otherTarget = null;
					marker.GetComponent<MeshRenderer> ().enabled = false;
				} 
				else
				{
					targetIsEnemy = false;
					combatTarget = null;
					otherTarget = target;
					Vector3 markerPoint = new Vector3 (currentTarget.point.x, currentTarget.point.y + 0.5f, currentTarget.point.z);
					marker.transform.position = markerPoint;
					marker.GetComponent<MeshRenderer> ().enabled = true;

				}

			}


			if (combatTarget && isBlocking == false && isDead == false && stagger == false) 
			{
				distance = Vector3.Distance (combatTarget.position, transform.position);
				if (distance <= attackRange) 
				{
					
					if (attackTimer <= 0.0f) 
					{
						//attackCooldown = Time.time + attackRate;
						Attack ();
					}

				}
			}

			if (Input.GetKey (KeyCode.Mouse1)) 
			{
				
				if (otherTarget)
				{
					//Vector3 lookPoint = new Vector3 (currentTarget.point.x, transform.position.y, currentTarget.point.z);
					//transform.LookAt (lookPoint);
					targetIsEnemy = false;
				}

				if (isBlocking) 
				{
					navMeshAgent.isStopped = true;
				} 
				else
				{
					navMeshAgent.destination = currentTarget.point;
					navMeshAgent.isStopped = false;
				
				}
			}
		}

		if (combatTarget == null) 
		{
			targetIsEnemy = false;
		}
			

		if (combatTarget) 
		{
			navMeshAgent.destination = combatTarget.position;
		}

		Vector3 lookPoint = new Vector3 (navMeshAgent.steeringTarget.x, transform.position.y, navMeshAgent.steeringTarget.z);
		transform.LookAt (lookPoint);
	}
	void FixedUpdate ()
	{
		if (knockedBack == true) 
		{
			navMeshAgent.velocity = knockDirection * knockbackStrength;
		}

		//transform.LookAt (navMeshAgent.steeringTarget);
	}
	void OnTriggerEnter (Collider other) 
	{
		if (other.tag == "Key") 
		{
			keyAnim = other.GetComponent<Animator>();
			keyAnim.enabled = true;
			keyBox = other.GetComponent<BoxCollider> ();
			other.transform.parent = keyHolder.transform;
			keyBox.enabled = false;
			keyAnim.SetTrigger ("Get");
			keyCount++;
			keyImage.enabled = true;
			Gold = Gold + 10;
		}
		if (other.tag == "Door") 
		{
			doorAnim = other.GetComponent<Animator> ();
		}
		if (other.tag == "NextLevelLoad") 
		{
			StartCoroutine (win ());
		}
		if (other.tag == "Chest") 
		{
			chestAnim = other.GetComponent<Animator>();
			chestBox = other.GetComponent<BoxCollider> ();
			chestBox.enabled = false;
			chestAnim.SetTrigger ("Open");
			Gold = Gold + 30;
			anim.SetTrigger ("OpenChest");
			StartCoroutine (AnimatorStop ());
		}
		if (other.tag == "KeyChest") 
		{
			chestAnim = other.GetComponent<Animator>();
			chestBox = other.GetComponent<BoxCollider> ();
			chestBox.enabled = false;
			chestAnim.SetTrigger ("Open");
			Gold = Gold + 10;
			keyCount++;
			keyImage.enabled = true;
			anim.SetTrigger ("OpenChest");
			StartCoroutine (AnimatorStop ());
		}
		/*if (other.tag == "ChestPlayerHolder") 
		{
			chestPlayHold = other.GetComponent<Transform> ();
			transform.SetParent(chestPlayHold);
			playerSnap ();
		}*/
		if (other.tag == "SmallEnemyAttack1" && isBlocking == false) 
		{
			health = health - 10.0f;
			if (health <= 10f) {
				anim.SetTrigger ("Death");
				isDead = true;
				navMeshAgent.velocity = Vector3.zero;
				StartCoroutine (die ());
			} else {
				knockDirection = other.transform.forward;
				StartCoroutine (Knockback ());
				anim.SetTrigger ("Knockback");
				StartCoroutine (Stagger ());
			}
		}
		if (other.tag == "SmallEnemyAttack1" && isBlocking == true) {
			StartCoroutine (Knockback ());
			//anim.SetTrigger ("Knockback");
			//StartCoroutine (Stagger ());
		}
		if (other.tag == "Hostile" && targetIsEnemy == true) 
		{
			navMeshAgent.isStopped = true;
		}

	}

	void OnTriggerStay (Collider other) {
		if (other.tag == "Sign") 
		{
			canReadSign = true;
		}

		if (other.tag == "Door") 
		{
			canOpenDoor = true;
		}
	}

	void OnTriggerExit (Collider other) {
		if (other.tag == "Sign") 
		{
			canReadSign = false;
		}

		if (other.tag == "Door") 
		{
			canOpenDoor = false;
		}
	}

	public void openDoor () {
			anim.SetTrigger ("OpenDoor");
			keyCount--;
		if (keyCount <= 0) {
			keyImage.enabled = false;
		}
			Gold = Gold + 10;
			StartCoroutine (AnimatorStop ());
	}


	void playerSnap() {
		transform.position = new Vector3 (0f, 0f, 0f);
		transform.rotation = Quaternion.Euler (0f, 0f, 180f);
		transform.parent = null;
	}

	IEnumerator BlockCooldown () {
		blockCooldown = true;
		anim.SetTrigger ("Block");
		isBlocking = true;
		//navMeshAgent.enabled = false;  <==== this causes occasional crashes, better to just stop it from moving w/ cantMove = true;
		cantMove = true;
		yield return new WaitForSeconds(blockDuration);
		//yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
		//navMeshAgent.enabled = true;

		cantMove = false;
		blockCooldown = false;
		isBlocking = false;
	}

	IEnumerator AnimatorStop () {
		//navMeshAgent.enabled = false;
		cantMove = true;
		yield return new WaitForSeconds(2);
		//navMeshAgent.enabled = true;
		cantMove = false;
	}

	IEnumerator win () {
		cantMove = true;
		winText.gameObject.SetActive (true);
		yield return new WaitForSeconds(2);
		SceneManager.LoadScene ("MainMenu");
		health = 100000;
	}

	IEnumerator die () {
		cantMove = true;
		deathText.gameObject.SetActive (true);
		yield return new WaitForSeconds(5);
		SceneManager.LoadScene ("MainMenu");
	}

	IEnumerator IdleCountCommon () {
		Debug.Log ("IdleNow");
		yield return new WaitForSeconds(8f);
		Debug.Log ("Idle5");
		if (navMeshAgent.velocity == Vector3.zero && combatTarget == null) {
			anim.SetTrigger ("IdleBreakout1");
			yield return new WaitForSeconds(8f);
			if (navMeshAgent.velocity == Vector3.zero) {
				anim.SetTrigger ("IdleBreakout2");
				StartCoroutine (IdleCountCommon ());
			}
		}
	}

	IEnumerator Stagger () 
	{
		stagger = true;
		yield return new WaitForSeconds (staggerDuration);
		stagger = false;
	}

	private void Attack ()
	{
		if (targetIsEnemy && isDead == false) 
		{
			navMeshAgent.destination = combatTarget.position;
			//if (navMeshAgent.remainingDistance >= attackRange) 
			//	{
			//	navMeshAgent.isStopped = false;
			//	}
			//distance = Vector3.Distance (target.position, transform.position);
			//if (distance <= attackRange) 
			//{
			//}
			transform.LookAt (combatTarget);
			if (isDead == false) 
			{
				navMeshAgent.isStopped = true;
			}

			//if (Time.time > attackCooldown) 
			//	{
			anim.SetTrigger ("Attack");
			//attackCooldown = Time.time + attackRate;
			Instantiate (attackHitbox, attackSpawn.position, transform.rotation);
			attackTimer = attackCool;
			//}
		}
	}

	IEnumerator Knockback () 
	{
		knockedBack = true;
		navMeshAgent.speed = 10f;
		navMeshAgent.angularSpeed = 0f;
		navMeshAgent.acceleration = 20f;

		yield return new WaitForSeconds (0.1f);

		knockedBack = false;
		navMeshAgent.speed = 6f;
		navMeshAgent.angularSpeed = 120f;
		navMeshAgent.acceleration = 60f;

	}
	IEnumerator SpeedBoost () 
	{
		mana = mana - speedCost;
		navMeshAgent.speed = navMeshAgent.speed + speedBoost;
		yield return new WaitForSeconds (speedDuration);
		navMeshAgent.speed = 6f;
	}
	IEnumerator PowerAttack ()
	{
		if (targetIsEnemy && isDead == false) 
		{
			navMeshAgent.destination = target.position;
			//if (navMeshAgent.remainingDistance >= attackRange) 
			//	{
			//	navMeshAgent.isStopped = false;
			//	}
			//distance = Vector3.Distance (target.position, transform.position);
			//if (distance <= attackRange) 
			//{
			//}
		
			yield return null;
			//}
		}
	}
}
