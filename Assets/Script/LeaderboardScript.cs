using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LeaderboardScript : MonoBehaviour {

	public GameObject nameBox;
	public GameObject pointBox;
    public GameObject pageBox;

	private int count = 10;
	private int i = 0;

	private Text words;
	private Text points;
    private Text page;

	// Use this for initialization
	void Start () {

		Login.Players.Sort();
        Login.Players.Reverse();
		words = nameBox.GetComponent<Text> ();
		points = pointBox.GetComponent<Text> ();
        page = pageBox.GetComponent<Text>();
        newboard();
        
	}

    public void newboard()
    {
        page.text = (count / 10).ToString();
        words.text = "Name\n\n";
        points.text = "High Score\n\n";
        for (; i < count; i++)
        {

            //Debug.Log(Login.Players[i].totalPoints);
            if (i >= Login.Players.Count)
            {
                continue;
            }

            words.text += Login.Players[i].first + " " + Login.Players[i].last + "\n";
            points.text += Login.Players[i].highscore + "\n";
        }
        Debug.Log("Count: " + count + "  i: " + i);
    }

	public void next(){

		count += 10;
        newboard();
	}

	public void back(){
        if(count == 10)
        {
            return;
        }
		i -= 20;
		count -= 10;
        newboard();
	}

    public void okay()
    {
        SceneManager.LoadScene("Game");
    }
}
