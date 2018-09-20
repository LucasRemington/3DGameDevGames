using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour {

    public Image bg;
    private bool isUp = false;
    private float transition = 0.0f;

	void Start () {
        gameObject.SetActive(false);
	}

	void Update () {
		if (!isUp)
        {
            return;
        }
        transition += Time.deltaTime;
        bg.color = Color.Lerp(new Color(0, 0, 0, 0), Color.black,transition);
	}

    public void ToggleEndMenu()
    {
        gameObject.SetActive(true);
        isUp = true;
    }


    public void ToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
