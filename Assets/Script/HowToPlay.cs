using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HowToPlay : MonoBehaviour {

	public void nextButton(){

		SceneManager.LoadScene ("Points");
	}

	public void okButton(){

		SceneManager.LoadScene ("Game");
	}

	public void backButton(){
	
		SceneManager.LoadScene ("Play");
	}

	public void howButton(){

		SceneManager.LoadScene ("Play");
	}

	public void introOkButton(){

		SceneManager.LoadScene ("Leaderboard");
	}
		
}
