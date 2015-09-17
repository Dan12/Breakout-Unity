using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public int lives = 3;
	public float resetDelay = 1f;

	public Text livesText;
	public GameObject paddle;
	public GameObject youWin;
	public GameObject gameOver;
	public GameObject deathParticles;
	public GameObject bricksPrefab;
	public static GameManager instance = null;

	public GameObject[] brickLevels;

	private GameObject clonePaddle;
	private int levels = 0;
	private int curLevel = 0;
	private GameObject cloneBricks;
	private int bricks = 0;
	private bool gameOverBool = false;

	// Use this for initialization
	void Awake () {
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);

		levels = brickLevels.Length;

		Setup ();
	}

	void Setup(){
		clonePaddle = Instantiate(paddle, transform.position, Quaternion.identity) as GameObject;
		cloneBricks = Instantiate (brickLevels[curLevel], transform.position+(new Vector3(-11f,7.67f,0f)), Quaternion.identity) as GameObject;
		bricks = cloneBricks.transform.childCount;
	}

	void CheckGameOver(){
		if (bricks < 1) {
			youWin.SetActive(true);
			Time.timeScale = 0.25f;
			curLevel++;
			gameOverBool = true;
			if(curLevel >= levels)
				curLevel = levels;
			Invoke("Reset", resetDelay);
		}
		if (lives < 1) {
			gameOver.SetActive(true);
			Time.timeScale = 0.25f;
			curLevel = 0;
			gameOverBool = true;
			Invoke("Reset", resetDelay);
			lives = 3;
			livesText.text = "Lives: " + lives;
		}
	}

	void Reset(){
		Time.timeScale = 1f;
		gameOverBool = false;
		Destroy (clonePaddle);
		Destroy (cloneBricks);
		Setup ();
	}

	public void loseLife(){
		lives--;
		livesText.text = "Lives: " + lives;
		Instantiate (deathParticles, clonePaddle.transform.position, Quaternion.identity);
		Destroy (clonePaddle);
		gameOverBool = true;
		Invoke ("SetupPaddle", resetDelay);
		CheckGameOver ();
	}

	void SetupPaddle(){
		gameOverBool = false;
		clonePaddle = Instantiate (paddle, transform.position, Quaternion.identity) as GameObject;
	}

	public void destroyBrick(){
		bricks--;
		CheckGameOver ();
	}

	public void hideText(){
		youWin.SetActive (false);
		gameOver.SetActive (false);
	}

	public bool getGameOver(){
		return gameOverBool;
	}
}
