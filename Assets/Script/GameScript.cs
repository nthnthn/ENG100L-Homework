using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameScript : MonoBehaviour {
    public GameObject player;
    public AudioSource deathSound;
    public AudioSource jumpSound;
    private Rigidbody2D playerrb;
    private Animator animator;

    public Text timeText;

    //Game
    public Transform spawnPoint;
    public bool practice = GameStart.practice;
    private bool gameStart = false;
    private bool grounded = true;
	public static float timer;
    private bool dead = false;
    private bool gameEnd = false;
    private float spawnDelay = 3.0f;
    private GameObject tallRock;
    private GameObject rockCreature;
    private GameObject triangleRock;
    private GameObject skull;

    // Use this for initialization
    void Start () {
        playerrb = player.GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        tallRock = Resources.Load("Tall Rock") as GameObject;
        rockCreature = Resources.Load("Rock Creature") as GameObject;
        triangleRock = Resources.Load("Triangle Rock") as GameObject;
        skull = Resources.Load("Skull") as GameObject;
    }
	
	// Update is called once per frame
	void Update () {
        if (gameStart && !gameEnd)
        {
            timer += Time.deltaTime; // timer
            timeText.text = (timer*10.0f).ToString("0"); // score text
            if (player.transform.position.y < 0.0f && playerrb.gravityScale != 0.0f)
            {
                //if player is touching floor disable gravity
                playerrb.gravityScale = 0.0f;
                player.transform.position = Vector3.zero;
                playerrb.velocity = Vector2.zero;
                grounded = true;
            }
            else if (player.transform.position.y > 0.0f && playerrb.gravityScale != 2.0f)
            {
                //enable gravity when jumping
                playerrb.gravityScale = 2.0f;
            }

            if (Input.GetMouseButtonDown(0) && grounded)
            {
                jump();
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                gameOver();
            }

            //Spawn enemies
            if (spawnDelay < timer)
            {
                spawnObstacle();
                spawnDelay = timer + (Random.Range(1.0f, 3.0f));
            }
        }
        else if (!gameStart && !gameEnd)
        {
            if (Input.GetMouseButtonDown(0))
            {
                startGame();
            }
        }

        if (dead)
        {
            player.transform.Rotate(Vector3.forward * 10.0f);
			if (Input.GetMouseButtonDown (0)) {
                if (!GameStart.practice) {
                    SceneManager.LoadScene("GameOver");
                }
                else
                {
                    SceneManager.LoadScene("PracticeOver");
                }
			}
        }
        
        if (GameStart.practice && timer > 30.0f)  
        {
            Debug.Log("game end");
            gameEnd = true;
            dead = true;
            timer = 0.0f;
            SceneManager.LoadScene("PracticeOver");
        }
	}

    private void jump()
    {     
        jumpSound.Play();  // play jumping sound effect
        animator.SetTrigger("Jump");  // set jumping animation

        playerrb.AddForce(Vector2.up * 500.0f);
        grounded = false;
    }

    private void startGame()
    {
        timer = 0.0f;
        gameStart = true;
    }

    private void gameOver()
    {
        gameEnd = true;
        dead = true;
        deathSound.Play();  // play death sound effect
        animator.SetBool("Dead", true);  // set death animation
        playerrb.gravityScale = 1.0f;
        playerrb.AddForce(Vector2.up * 500.0f);

    }

    private void spawnObstacle()
    {
        float resize = Random.Range(0.1f, 0.3f);
        Vector3 resizeVec = new Vector3(resize, resize, resize);
        GameObject obstacle;
        int select = Random.Range(0, 4);
        switch (select)
        {
            case 0:
                obstacle = Instantiate(tallRock, spawnPoint);
                obstacle.transform.localScale = resizeVec;
                obstacle.SetActive(true);
                break;
            case 1:
                obstacle = Instantiate(rockCreature, spawnPoint);
                obstacle.transform.localScale = resizeVec;
                obstacle.SetActive(true);
                break;
            case 2:
                obstacle = Instantiate(triangleRock, spawnPoint);
                obstacle.transform.localScale = resizeVec;
                obstacle.SetActive(true);
                break;
            case 3:
                obstacle = Instantiate(skull, spawnPoint);
                obstacle.transform.localScale = resizeVec;
                obstacle.SetActive(true);
                break;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Obstacle")
        {
            gameOver();
        }
    }

	public static float getTimer(){
		return timer;
	}
}
