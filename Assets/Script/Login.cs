using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;

public class Player : IComparable<Player>{

	public string username;
	public string first;
	public string last;
	public int grade;
	public int level;
	public string password;
	public int totalPoints;
	public int jumps;
	public string dob;
    public uint highscore;

	public Player(string Username) {
		this.username = Username;
	}

	public int CompareTo(Player other) {
		if (other == null)
			return 1;
		return highscore.CompareTo (other.highscore);
	}
}

public class Login : MonoBehaviour {
	public GameObject username;
	public GameObject password;
	public GameObject errorText;

	static public Player ActivePlayer;
	static public List<Player> Players = new List<Player>();
    static public int currentScene = 1;

	public string user;
	private string passwordInput;

	static public string usernameInput;
	static public DatabaseReference reference;

	void Start() {

		string playerData;

		// Set up the Editor before calling into the realtime database.
		FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://homework-app-81383.firebaseio.com/");

		// Get the root reference location of the database.
		reference = FirebaseDatabase.DefaultInstance.RootReference;
		FirebaseDatabase.DefaultInstance.GetReference ("users").GetValueAsync ().ContinueWith (task => {

			// TODO Handle the error...
			if (task.IsFaulted) {
				
				Debug.Log("failed to get data ");
			} else if (task.IsCompleted) {
				DataSnapshot snapshot = task.Result;

				// Adds all users into list Players
				foreach(var child in snapshot.Children){
					// stores username 
					user = child.Key;
					Debug.Log("Getting information for user: " + user);
					Player newPlayer = new Player(user);

					if(Players.Contains(newPlayer)){}
					else {
						Players.Add(newPlayer);
					}

					// Stores data from each player to object
					playerData = child.GetRawJsonValue();
					JsonUtility.FromJsonOverwrite(playerData, newPlayer);
				}
			}
		});
	}


	public void LoginButton(){
		// check if the usernames match 
		Debug.Log("Attempting login");
		foreach(var play in Players) {
			if (play.username == usernameInput){
				Debug.Log("Match");
				if (play.password == passwordInput) {
					ActivePlayer = play;
					SceneManager.LoadScene ("Leaderboard");
					return;
				} else {
					errorText.GetComponent<Text> ().text = "Error: Invalid password";
					return;
				}
			}
		}
		errorText.GetComponent<Text>().text = "Error: No such username";
		Debug.Log("No Match");
	}

	// Update is called once per frame
	void Update () {
		
		if (Input.GetKeyDown (KeyCode.Tab)) {
			if (username.GetComponent<InputField>().isFocused) {
				password.GetComponent<InputField>().Select();
			}
		}

		if(Input.GetKeyDown(KeyCode.Return)) {
			if(usernameInput != "" && passwordInput != "") {
				LoginButton ();
			}
		}
		usernameInput = username.GetComponent<InputField>().text;
		passwordInput = password.GetComponent<InputField>().text; 
	}

	public void SignUp () {
		SceneManager.LoadScene ("SignUpScene");
	}
}
