
using UnityEngine;

public class Enemy1 : MonoBehaviour {

    public int currentHP1 = 1;
	Animator anim;

	void Start () {
		anim = GetComponent<Animator> ();
	}

    public void TakeDamage( int damageAmount)


    {
        currentHP1 -= damageAmount;
        if (currentHP1 <= 0)
        {
			GameController.killScore++;
			anim.SetTrigger ("death");
			Destroy(gameObject, 0.5f);
        }
    }

}
