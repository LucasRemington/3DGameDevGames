using UnityEngine;

public class Enemy2 : MonoBehaviour {

	public int currentHP2 = 3;
	Animator anim;
	private bool onceDamaged;
	private bool twiceDamaged;

	void Start () {
		anim = GetComponent<Animator> ();
		onceDamaged = false;
		twiceDamaged = false;
	}

	void Update () {
		if (currentHP2 == 2 && onceDamaged == false) {
			anim.SetTrigger ("Damaged");
			onceDamaged = true;
		}
		if (currentHP2 == 1 && twiceDamaged == false) {
			anim.SetTrigger ("Damaged");
			twiceDamaged = true;
		}
	}

	public void TakeDamage2( int damageAmount)
	{
		currentHP2 -= damageAmount;
		if (currentHP2 <= 0)
		{
			GameController.killScore = GameController.killScore+3;
			anim.SetTrigger ("Damaged");
			Destroy(gameObject, 0.5f);
		}
	}
}
