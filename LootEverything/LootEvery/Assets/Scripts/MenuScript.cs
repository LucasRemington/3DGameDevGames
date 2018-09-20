using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

[RequireComponent(typeof(AudioSource))]

public class MenuScript : MonoBehaviour {

	public Canvas instrMenu;
	public Canvas quitMenu;
	public Button quitText;
	public Button startText;
	public Button exitText;
	AudioSource audio;

	// Use this for initialization
	void Start () {
		audio = GetComponent<AudioSource> ();
		quitMenu = quitMenu.GetComponent<Canvas> ();
		startText = startText.GetComponent<Button> ();
		exitText = exitText.GetComponent<Button> ();
		instrMenu = instrMenu.GetComponent<Canvas> ();
		quitMenu.enabled = false;

	}


	public void ExitPress()
	{
		quitMenu.enabled = true;
		startText.enabled = false;
	}

	public void NoPress()
	{
		quitMenu.enabled = false;
		startText.enabled = true;
		exitText.enabled = true;
	}

	public void Instr()
	{
		instrMenu.enabled = true;
	}
		

	public void StartLevel()
	{
		SceneManager.LoadScene ("lvl1_tutorial");
	}

	public void ExitGame()
	{
		Application.Quit ();
	}


	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown (KeyCode.Space))
		{
			instrMenu.enabled = false;
		}
	}
}
