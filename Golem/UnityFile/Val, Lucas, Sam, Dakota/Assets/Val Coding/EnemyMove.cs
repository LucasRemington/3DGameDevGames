using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour {


    public int hp;
    public Transform[] waypoints;
    public int pos = 0;
    private NavMeshAgent agent;
    public PlayerMovement pm;
    public DeathMenu dm;
    public LookAt la;
    private bool dead = false;
    public Transform myTransform;
	public AudioSource death;
	public AudioSource slowDeath;
	public AudioSource playerdeath;

    void Start () {
        //animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = true;
        Setup();
        myTransform = transform;
    }

    void Setup() {
        if (waypoints.Length == 0)
        {
            return;
        }
        if (la.combat == 1)
        {
            return;
        }

        agent.destination = waypoints[pos].position;
        pos = (pos + 1) % waypoints.Length;
    }
    void Update()
    {
        if (dead)
        {
            return;
        }
        if (la.combat == 1)
        {
            transform.position = Vector3.MoveTowards(transform.position, pm.player.transform.position, 3 * Time.deltaTime);
            waypoints[0] = pm.player.transform;
            waypoints[1] = pm.player.transform;
        }

        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            Setup();
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
			dm.ToggleEndMenu();
			playerdeath.Play ();
            
        }
        if (other.tag == "Fire")
        {
            Destroy(other.gameObject);
            Die();
        }
    }

    void Die()
    {
		death.Play ();
        dead = true;
		StartCoroutine (slowerDeath());
        Destroy(gameObject);

    }


	IEnumerator slowerDeath() {
		yield return new WaitForSeconds (1.9f);
		slowDeath.Play();
	}


}

