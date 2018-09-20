using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {


	void Start () {
		
	}
    
    public void ToGame()
    {
        SceneManager.LoadScene("level1");
    } 
	public void EndGame()
	{
		Application.Quit ();
	}
}
