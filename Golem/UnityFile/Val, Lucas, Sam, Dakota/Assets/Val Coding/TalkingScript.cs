using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalkingScript : MonoBehaviour {

	public Text talk;
	public Image textbox;
	public int collectablecount = 0;
	public PlayerMovement pm;
	public int totemnum;
	public int totem1Click;
	public int totem2Click;
	public int totem3Click;

	void Start () {
		textbox.enabled = false;
		totem1Click = 0;
		totem2Click = 0;
		totem3Click = 0;
	}

	void Update () {
		if (collectablecount < pm.collected) {
			collectablecount = pm.collected;
			Collectable ();
		}
	}

	public void Chat(){
		textbox.enabled = true;
		if (totemnum == 0) {
			if (totem1Click == 0) {
				talk.text = "Oh, neat, you finally woke up. You really take your sweet time, huh?";
				totem1Click++;
			}
			 else if (totem1Click == 1) {
				talk.text = "That's okay, I forgive you. Most of us have left, though.";
				totem1Click++;
			}
			else if (totem1Click == 2) {
				talk.text = "Go ahead and wander around. Maybe there's something good left.";
				totem1Click++;
			}
			else if (totem1Click == 3) {
				talk.text = "I don't have anything else to say, so... I'm going to repeat what I've already said.";
				totem1Click = 0;
				Invoke ("DoneTalking", 5);
				totemnum++;
			} 
			return;
		} 
		else if (totemnum ==1) {
				if (totem2Click == 0) {
					talk.text = "Hey, see that big glowy blue cylinder thing?";
					totem2Click++;
				}
				else if (totem2Click == 1) {
					talk.text = "If you try to talk to it, it'll send you into the background. Pretty wild.";
					totem2Click++;
				}
				else if (totem2Click == 2) {
					talk.text = "Also, there are spider ghosts trying to kill you.";
					totem2Click++;
				}
				else if (totem2Click == 3) {
					talk.text = "I probably should've mentioned that. Just shoot them, they'll go away.";
					totem2Click = 0;
					Invoke ("DoneTalking", 5);
				totemnum++;
				}
			return;
			} 
		else if (totemnum == 2) {
			if (totem2Click == 0) {
				talk.text = "Well, you made it. Sorry there wasn't much.";
				totem2Click++;
			}
			else if (totem2Click == 1) {
				talk.text = "We had everything all planned out. But we kinda had to rush everything.";
				totem2Click++;
			}
			else if (totem2Click == 2) {
				talk.text = "Anyways. Go ahead and walk out that door.";
				totem2Click++;
			}
			else if (totem2Click == 3) {
				talk.text = "It was nice seeing you, I guess. Even though you stole "+ collectablecount.ToString()+ "/7 things..";
				totem2Click = 0;
				Invoke ("DoneTalking", 5);
			}
			return;
		}
		Invoke ("DoneTalking", 50);
	}
	void DoneTalking(){
		textbox.enabled = false;
		talk.text = "";

	}
	public void Collectable(){
		textbox.enabled = true;
		if (collectablecount == 1) {
			talk.text = "You think this is some sort of gross foot?";
			Invoke ("DoneTalking", 5);
		} 
		else if (collectablecount == 2) {
			talk.text = "One of the spiders left this behind. You don't know why. It smells.";
			Invoke ("DoneTalking", 5);
		}
		else if (collectablecount == 3) {
			talk.text = "A gem of some sort. It's made entirely of plastic.";
			Invoke ("DoneTalking", 5);
		}
		else if (collectablecount == 4) {
			talk.text = "You thought you saw something, but actually, there was nothing.";
			Invoke ("DoneTalking", 5);
		}
		else if (collectablecount == 5) {
			talk.text = "You find a very, very small pillar. (Like so small, bro)";
			Invoke ("DoneTalking", 5);
		}
		else if (collectablecount == 6) {
			talk.text = "I mean, I'm still not over that pillar. You find some stuff but like, wow.";
			Invoke ("DoneTalking", 5);
		}
		else if (collectablecount == 7) {
			talk.text = "You really hope this isn't poop. But it is. It is poop.";
			Invoke ("DoneTalking", 5);
		}
		else if (collectablecount == 8) {
			talk.text = "A sort of shiny rock. You figure it's probably cool enough to take.";
			Invoke ("DoneTalking", 5);
		}
		Invoke ("DoneTalking", 4);
	}
}
