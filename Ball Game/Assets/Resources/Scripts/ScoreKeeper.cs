using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class ScoreKeeper : MonoBehaviour {

	int score = 0;
	int maxScore;
	int lives = 3;

	GameObject heart1;
	GameObject heart2;
	GameObject heart3;

	Text scoreText;
	Text livesText;
	GameObject levelManager;

	// Use this for initialization
	void Start () {

		scoreText = GameObject.Find ("score_text").GetComponent<Text>();

		levelManager = GameObject.Find("LevelManager");

		score = levelManager.GetComponent<LevelManager> ().getScore ();
		maxScore = levelManager.GetComponent<LevelManager> ().getMaxScore ();
		lives = levelManager.GetComponent<LevelManager> ().getLives ();

		updateScoreText ("Score: " + score.ToString());
		updateLivesDisplay (lives);

	}

	void Update(){
		if (lives <= 0) {
			levelManager.GetComponent<LevelManager> ().openLevel ("GameOver");
			updateHighScore ();
		}
	}

	public void updateHighScore(){
		if (SceneManager.GetActiveScene ().name.Equals ("Arcade") && score > PlayerPrefs.GetInt ("ArcadeHighScore"))
			PlayerPrefs.SetInt ("ArcadeHighScore", score);
	}
				

	public void updateLivesDisplay(int lives){
		if(lives == 1){
			heart3 = GameObject.Instantiate (Resources.Load ("prefabs/heart")) as GameObject;
			heart3.transform.position = new Vector2 (.7f, 2.1f);
		}
		if (lives == 2) {
			heart2 = GameObject.Instantiate (Resources.Load ("prefabs/heart")) as GameObject;
			heart2.transform.position = new Vector2 (.55f, 2.1f);
			heart3 = GameObject.Instantiate (Resources.Load ("prefabs/heart")) as GameObject;
			heart3.transform.position = new Vector2 (.7f, 2.1f);
		}
		if (lives == 3) {
			heart1 = GameObject.Instantiate (Resources.Load ("prefabs/heart")) as GameObject;
			heart1.transform.position = new Vector2 (.4f, 2.1f);
			heart2 = GameObject.Instantiate (Resources.Load ("prefabs/heart")) as GameObject;
			heart2.transform.position = new Vector2 (.55f, 2.1f);
			heart3 = GameObject.Instantiate (Resources.Load ("prefabs/heart")) as GameObject;
			heart3.transform.position = new Vector2 (.7f, 2.1f);
		}
	}

	public void updateScore(int newPoints){
		score += newPoints;
		scoreText.text = "Score: " + score.ToString ();
	}

	public void updateScoreText(string newString){
		scoreText.text = newString;
	}

	public void incrementLives() { 
		lives++;
		updateLivesDisplay (lives);
	}
	public void decrementLives() { 
		lives--; 
		if (lives == 2)
			Destroy (heart1.gameObject);
		if (lives == 1)
			Destroy (heart2.gameObject);
	}
		
	public int getScore(){ return score; }
	public int getLives(){ return lives; }
	public void setMaxScore(int newScore){ maxScore = newScore; }
	public void updateMaxScore(int newScore){ maxScore += newScore; }
	public int getMaxScore(){ return maxScore; }
	public void setLives(int lives){ this.lives = lives; }
}