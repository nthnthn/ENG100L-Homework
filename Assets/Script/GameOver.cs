using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {
	private static uint score = 0;
	public Text scoreToDisplay;

	// Use this for initialization
	void Start () {
        if (!GameStart.practice) {
            //check high score
            //float timer = GameScript.getTimer ();
            score = (uint)(GameScript.timer * 10.0f);
            if (score > Login.ActivePlayer.highscore) {
                Login.ActivePlayer.highscore = score;
                Login.reference.Child("users").Child(Login.ActivePlayer.username).SetRawJsonValueAsync(JsonUtility.ToJson(Login.ActivePlayer));

            } else {
                score = Login.ActivePlayer.highscore;
            }

            //display high score
            scoreToDisplay.text = "High Score\n" + score.ToString();
            Debug.Log(score);
        }
	}

	public void backToGame(){
		SceneManager.LoadScene ("Game");
	}

	// Update is called once per frame
	void Update () {
		
	}
}
