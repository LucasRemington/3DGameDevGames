using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

	public TalkingScript tak;
	public DeathMenu dm;
    public float speed;
    public GameObject player;
	public GameObject spellhand1;
	public GameObject spellhand2;
    public GameObject fire;
    private float gravity = 8.0f;
    private CharacterController controller;
    private float vertVelocity;
    public int fireval;
    public float jumpForce = 1.5f;
    private float Haxis;
    private Vector3 moveV;
	Animator anim;
	private float xLock;
	public float score = 0;
	public Transform[] depths;
	private int cdepth;
	private bool tp;
	public bool talk;
	public AudioSource jump;
	public AudioSource pickup;
	public AudioSource firesound;
	public AudioSource move;
	public AudioSource switchsound;
	public AudioSource talksound;
	public int collected;

    void Start()
    {
		tp = false;
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
		xLock = transform.position.x;
		score += Time.deltaTime;
		cdepth = 0;
		move.volume = 0.05f;
		talk = true;
    }

    void Update()
    {
		transform.position = new Vector3 (xLock,transform.position.y, transform.position.z);

		Quaternion shooting = Quaternion.Euler(new Vector3(fire.transform.rotation.x, player.transform.rotation.y, 0.0f));
        if (controller.isGrounded)
        {
            vertVelocity = -0.5f;
            if (Input.GetButtonDown("Jump"))
            {
                vertVelocity = jumpForce;
                anim.SetBool("jumping", true);
				if (jump.isPlaying == false) {
					jump.Play ();
				}
                Invoke("stopJumping", 0.1f);
            }
        }

        else if (!controller.isGrounded) {
            vertVelocity -= gravity * Time.deltaTime;
        }          
        Haxis = Input.GetAxis("Horizontal");
        moveV = new Vector3(0.0f, vertVelocity, Haxis);
		if (moveV.z != 0) {
			anim.SetBool ("walking", true);
			move.volume = 0.1f;
		} else {
			anim.SetBool ("walking", false);
			move.volume = 0.05f;
		}
        controller.Move(moveV * speed);
        Vector3 movement = new Vector3(0.0f, 0.0f, Haxis);
        if (Haxis != 0) {
            transform.rotation = Quaternion.LookRotation(-movement);
            transform.Translate(movement * speed * Time.deltaTime, Space.World);
        }


        if(Haxis < 0)
        {
            fireval = 1;
        }
        if (Haxis > 0)
        {
            fireval = 0;
        }

        if (Input.GetButtonDown("Fire1"))
        {
				firesound.Play ();
			if (fireval == 1) {
				Instantiate (fire, spellhand2.transform.position, shooting);

			}
			if (fireval == 0) {
				Instantiate (fire, spellhand1.transform.position, shooting);

			}	
        }
		if (Input.GetButtonDown ("Cancel")) {
			Application.Quit ();
		}

		if (Input.GetButtonDown("Fire2"))
		{
				firesound.Play ();
			if (fireval == 1) {
				Instantiate (fire, spellhand1.transform.position, shooting);

			}
			if (fireval == 0) {
				Instantiate (fire, spellhand2.transform.position, shooting);

			}	
		}

		if (tp) {
			if (Input.GetButtonDown ("Fire3")) {
				if (cdepth == 0) {
					if (switchsound.isPlaying == false) {
					switchsound.Play ();
					}
					xLock = depths [1].position.x;
					cdepth = 1;
				} else {
					if (switchsound.isPlaying == false) {
						switchsound.Play ();
					}
					xLock = depths [0].position.x;
					cdepth = 0;
				}
					
			}
		}

		if (talk == true) {
			if (Input.GetButtonDown ("Fire3")) {
				tak.Chat ();
				if (talksound.isPlaying == false) {
					talksound.Play ();
				}
			} else {
				return;
			}
		}

	//void FireAnim () {
		//if (fireCooldown == false) {
			//anim.SetTrigger ("fire");
			//fireCooldown = true;
			//StartCoroutine (FireCooldown());
		//}
	}
		

    void stopJumping()
    {
        anim.SetBool("jumping", false);
    }
	public void OnTriggerEnter(Collider other){
		if (other.CompareTag ("Tele")) {
			tp = true;
		}
		if (other.CompareTag ("talk")) {
			talk = true;
		}
		if (other.CompareTag("Collect"))
		{
			Destroy(other.gameObject);
			pickup.Play ();
			collected++;
		}
		if (other.CompareTag ("Slurp")) {
			dm.ToggleEndMenu ();
		}
	}

	public void OnTriggerExit(Collider other){
		if (other.CompareTag ("Tele")) {
			tp = false;
		}
		if (other.CompareTag ("talk")) {
			talk = false;
		}
	}
		
}
