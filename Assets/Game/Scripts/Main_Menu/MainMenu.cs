using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	public void OnSinglePlayerClicked()
	{
		Debug.Log("Loading Single Player Mode...");
		SceneManager.LoadScene("Single_Player", LoadSceneMode.Single);
		
	}

	public void OnCoopClicked()
	{
		Debug.Log("Loading Cooperative Mode...");
		SceneManager.LoadScene("Co-Op_Mode", LoadSceneMode.Single);
	}
}
