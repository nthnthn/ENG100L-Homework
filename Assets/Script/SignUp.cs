using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;

public class SignUp : MonoBehaviour {
	public GameObject username;
	public GameObject lastName;
	public GameObject firstName;
	public GameObject password;
	public GameObject confPassword;
	public GameObject grade;
	public GameObject date;
	public GameObject month; 
	public GameObject year; 
	public GameObject errorText;

	private string Username;
	private string LastName;
	private string FirstName;
	private string Password;
	private string ConfPassword;
	private string Grade;
	private string Date;
	private string Month;
	private string Year;

	public void SignUpButton() {

		if (Username != "" && FirstName != "" && Password != "" && ConfPassword != "" &&
			LastName != "" && Grade != "" && Date != "" && Month != "" && Year != "") {

			if (Grade == "Choose your grade") {
				errorText.GetComponent<Text>().text = "Error: Please choose your grade level.";
			}
			else if (Date == "Date") {
				errorText.GetComponent<Text>().text = "Error: Please choose your birth date.";
			}
			else if (Month == "Month") {
				errorText.GetComponent<Text>().text = "Error: Please choose your birth month.";
			}
			else if (Year.Length != 4) {
				errorText.GetComponent<Text>().text = "Error: Please enter a valid birth year.";
			}
			else if (Password == ConfPassword) {

				for (int i = 0; i < Login.Players.Count; i++){

					if (Login.Players[i].username == Username) {

						errorText.GetComponent<Text>().text = "Error: Username already taken";
						return;
					}

				}

				createUser (Username, Password, LastName, FirstName, Grade, Date, Month, Year);
				errorText.GetComponent<Text>().text = "Sign up successfully.";
				Thread.Sleep(2000);
				SceneManager.LoadScene ("LoginScene");

			} else {
				errorText.GetComponent<Text>().text = "Error: Two passwords don't match.";
			}
		} else {
			errorText.GetComponent<Text>().text = "Error: There are some blanks.";
		}
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Tab)) {
			if (username.GetComponent<InputField>().isFocused) {
				lastName.GetComponent<InputField>().Select();
			}
			else if (lastName.GetComponent<InputField>().isFocused) {
				firstName.GetComponent<InputField>().Select();
			}
			else if (firstName.GetComponent<InputField>().isFocused) {
				password.GetComponent<InputField>().Select();
			}
			else if (password.GetComponent<InputField>().isFocused) {
				confPassword.GetComponent<InputField>().Select();
			}
		}

		if(Input.GetKeyDown(KeyCode.Return)) {
			SignUpButton ();
		}

		Username = username.GetComponent<InputField>().text;
		LastName = lastName.GetComponent<InputField> ().text;
		FirstName = firstName.GetComponent<InputField> ().text;
		Password = password.GetComponent<InputField>().text; 
		ConfPassword = confPassword.GetComponent<InputField> ().text;
		Grade = grade.GetComponent<Dropdown>().captionText.text;
		Date = date.GetComponent<Dropdown>().captionText.text;
		Month = month.GetComponent<Dropdown>().captionText.text;
		Year = year.GetComponent<InputField>().text;
	}

	void createUser(string username, string password, string lastName, 
		string firstName, string grade, string date, string month, string year) {

		Player newPlayer = new Player (username);
		newPlayer.password = password;
		newPlayer.last = lastName;
		newPlayer.first = firstName;
		newPlayer.grade = Convert.ToInt32(grade);
		newPlayer.dob = date + "-" + month + "-" + year;
		newPlayer.jumps = 0;
		newPlayer.totalPoints = 0;
		newPlayer.level = 0;

		Login.reference.Child ("users").Child (username).SetRawJsonValueAsync(JsonUtility.ToJson(newPlayer));
	}

	public void goToLogin(){

		SceneManager.LoadScene ("LoginScene");
	}

}


