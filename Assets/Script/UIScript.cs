using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIScript : MonoBehaviour {

	public GameObject text;

	private Text words;

	void Start(){

		words = text.GetComponent<Text> ();
		words.text = "Plays left: " + Login.ActivePlayer.jumps.ToString(); 
	}

	public void questionButton(){

		SceneManager.LoadScene ("Play");
	}

	public void trophyButton(){

		SceneManager.LoadScene ("Leaderboard");
	}
}
//		Login.reference.Child ("users").Child (Login.ActivePlayer.username).SetRawJsonValueAsync(JsonUtility.ToJson(Login.ActivePlayer));
