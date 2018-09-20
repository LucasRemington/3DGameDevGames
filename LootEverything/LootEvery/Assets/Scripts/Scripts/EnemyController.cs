using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {

	public GameObject keySpawned;
	public bool spawnsKey;
	private bool goldOnce;

	public Animator anim;

	public float health = 100.0f;
	public Transform[] destinations;
	public int maxDestinations = 2;
	public int currentDestination = 0;
	private NavMeshAgent botAgent;
	public float idleTime = 2.0f;
	private bool isReverse = false;
	public bool inCombat = false;
	private bool isDead = false;
	private Enemy1FOV fovScript;
	private bool targetinRange;
	private float distance;
	public float attackRange = 5.0f;
	public GameObject player;
	public Transform attackSpawn;
	public GameObject attackHitbox;
	public float attackCooldown;
	public GameObject[] tempHealth;
	private int onHealth;
	private bool hurtCooldown;
	private bool knockedBack = false;
	Vector3 knockDirection;
	public float knockbackStrength = 2f;
	private bool attackDelay = true;

	void Start () 
	{
		//currentDestination = Random.Range (0, maxDestinations - 1);
		botAgent = GetComponent<NavMeshAgent> ();
		botAgent.destination = destinations [currentDestination].position;
		StartCoroutine (AILoop ());
		StartCoroutine (AttackLoop ());
		onHealth = -1;
		goldOnce = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (health <= 0.0f) 
		{
			isDead = true;
			Destroy (gameObject, 2);
			anim.SetTrigger("Die");
			if (goldOnce == false) {
				PlayerControl.Gold = PlayerControl.Gold + 15;
				if (spawnsKey == true) {
					Instantiate (keySpawned, transform.position, Quaternion.identity);
				}
				goldOnce = true;
			}

		}
		fovScript = gameObject.GetComponentInChildren <Enemy1FOV> ();
		if (fovScript.playerInSight == false) 
		{
			inCombat = false;
		}
		if (fovScript.player) 
		{
			distance = Vector3.Distance (fovScript.player.transform.position, transform.position);
			if (distance <= attackRange) 
			{
				targetinRange = true;
			} 
			else 
			{
				targetinRange = false;
			}
			player = fovScript.player;
		}

		if (knockedBack == true) 
		{
			botAgent.velocity = knockDirection * knockbackStrength;
		}
	}
	void FixedUpdate ()
	{
		

	}
	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "PlayerAttack" /*&& hurtCooldown == false*/) 
		{
			if (hurtCooldown == false) 
			{
				inCombat = true;
				health = health - 20.0f;
				anim.SetTrigger ("Hurt");
				onHealth++;
				tempHealth [onHealth].SetActive(false);

				StartCoroutine (hurtCool ());
			}
			knockDirection = other.transform.forward;
			StartCoroutine (Knockback ());
		}
		if (other.tag == "PlayerHeavyAttack" /*&& hurtCooldown == false*/) 
		{
			if (hurtCooldown == false) 
			{
				inCombat = true;
				health = health - 40.0f;
				anim.SetTrigger ("Hurt");
				onHealth++;
				tempHealth [onHealth].SetActive(false);

				StartCoroutine (hurtCool ());
			}
			knockDirection = other.transform.forward;
			StartCoroutine (Knockback ());
		}
	}
	private IEnumerator hurtCool(){
		hurtCooldown = true;
		yield return new WaitForSeconds(1.5f);
		hurtCooldown = false;
	}

	private IEnumerator AILoop () 
	{
		if (isDead == false) 
		{
			yield return StartCoroutine (CheckPath ());
			yield return StartCoroutine (SetPath ());
			StartCoroutine (AILoop ());
		}
			
	}
	private IEnumerator SetPath () 
	{

		if (inCombat == false && isDead == false) 
		{
			botAgent.destination = destinations [currentDestination].position;
		}
		if (inCombat == true && isDead == false && hurtCooldown == false) 
		{
			anim.SetTrigger ("Spot");
			botAgent.destination = gameObject.GetComponentInChildren <Enemy1FOV> ().player.transform.position;
		}
		yield return null;
	}
	private IEnumerator CheckPath () 
	{
		if (botAgent.remainingDistance <= 3) 
		{
			if (inCombat == false) 
			{
				if (currentDestination == maxDestinations - 1 && isReverse == false) 
				{
					isReverse = true;
				}
				if (currentDestination < maxDestinations - 1 && isReverse == false) 
				{
					currentDestination++;
				}
				if (currentDestination > 0 && isReverse == true) 
				{
					currentDestination = currentDestination - 1;
				}
				if (currentDestination == 0 && isReverse == true) 
				{
					isReverse = false;
				}

				yield return new WaitForSeconds (idleTime);
			} 
		} 
		else
		{
			yield return null;
		}
	}
	private IEnumerator AttackLoop ()
	{
		yield return StartCoroutine (AttackDelay ());
		yield return StartCoroutine (Attack());
		StartCoroutine (AttackLoop ());
	}
	private IEnumerator Attack ()
	{
		if (targetinRange == true && player && isDead == false) 
		{
			anim.SetTrigger ("Attack");
			transform.LookAt (player.transform);
			botAgent.isStopped = true;
			Instantiate (attackHitbox, attackSpawn.position, transform.rotation);
			botAgent.isStopped = false;
			attackDelay = false;
			yield return new WaitForSeconds (attackCooldown);
		} 
		else 
		{
			yield return null;
		}
	}

	IEnumerator Knockback () 
	{
		knockedBack = true;
		botAgent.speed = 10f;
		botAgent.angularSpeed = 0f;
		botAgent.acceleration = 20f;

		yield return new WaitForSeconds (0.5f);

		botAgent.destination = transform.position;
		knockedBack = false;
		botAgent.speed = 6f;
		botAgent.angularSpeed = 120f;
		botAgent.acceleration = 60f;

	}
	IEnumerator AttackDelay () 
	{
		if (attackDelay == true) {
			yield return new WaitForSeconds (2f);
		} else 
		{
			yield return null;
		}
	}
}
