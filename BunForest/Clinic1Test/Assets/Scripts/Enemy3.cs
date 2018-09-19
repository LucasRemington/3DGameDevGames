using UnityEngine;

public class Enemy3 : MonoBehaviour {

	public int currentHP3 = 10;
	Animator anim;

	void Start () {
		anim = GetComponent<Animator> ();

	}

	public void TakeDamage3( int damageAmount)
	{
		currentHP3 -= damageAmount;
		anim.SetTrigger ("Hit");
		if (currentHP3 <= 0)
		{
			GameController.killScore = GameController.killScore+10;
			anim.SetTrigger ("Die");
			Destroy(gameObject, 1f);
		}
	}

	private void Update()
	{
		
	}
}
