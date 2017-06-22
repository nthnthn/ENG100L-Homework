using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameStart : MonoBehaviour {
    public static bool practice;
    public GameObject errorText;

    public void startGame(bool isPractice)
    {
        practice = isPractice;
        if (!practice)
        {
            if (Login.ActivePlayer.jumps <= 0)
            {
                errorText.SetActive(true);
            }
            else
            {
                Login.ActivePlayer.jumps--;
                Login.reference.Child("users").Child(Login.ActivePlayer.username).SetRawJsonValueAsync(JsonUtility.ToJson(Login.ActivePlayer));
                SceneManager.LoadScene("GamePlay");
            }
        }
        else
        {
            SceneManager.LoadScene("GamePlay");
        }
    }

}
