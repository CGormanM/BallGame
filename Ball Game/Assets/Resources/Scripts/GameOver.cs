using UnityEngine;
using System.Collections;
using UnityEngine.UI;

using UnityEngine.SceneManagement;


public class GameOver : MonoBehaviour {

	GameObject levelManager;
	Text scoreText;
	Text highScoreText;

	int stars = 0;
	GameObject star1;
	GameObject star2;
	GameObject star3;

	// Use this for initialization
	void Start () {
		levelManager = GameObject.Find ("LevelManager");
		scoreText = GameObject.Find ("finalscore_text").GetComponent<Text>();

		if (levelManager.GetComponent<LevelManager> ().getLastLevel () == "Arcade"){
			highScoreText = GameObject.Find ("highscore_text").GetComponent<Text>();
			highScoreText.text = "High Score: " + PlayerPrefs.GetInt ("ArcadeHighScore");
			scoreText.text = "Score: " + levelManager.GetComponent<LevelManager> ().getScore ().ToString ();
		} else {
			scoreText.text = "Score: " + levelManager.GetComponent<LevelManager> ().getScore ().ToString () + " / " + levelManager.GetComponent<LevelManager> ().getMaxScore().ToString ();
			calculateStars ();
			updateStarDisplay ();
		}
	}

	void calculateStars(){
		if (SceneManager.GetActiveScene ().name != "GameOver") {
			if (levelManager.GetComponent<LevelManager> ().getScore () == 500)
				stars = 1;
			if (levelManager.GetComponent<LevelManager> ().getScore () > 500)
				stars = 2;
			if (levelManager.GetComponent<LevelManager> ().getScore () == levelManager.GetComponent<LevelManager> ().getMaxScore ())
				stars = 3;
		}
	}

	void updateStarDisplay(){
		if (stars == 1) {
			star1 = GameObject.Instantiate (Resources.Load ("prefabs/star")) as GameObject;
			star1.transform.position = new Vector2 (-.3f, .5f);
		}
		if (stars == 2) {
			star1 = GameObject.Instantiate (Resources.Load ("prefabs/star")) as GameObject;
			star1.transform.position = new Vector2 (-.3f, .5f);
			star2 = GameObject.Instantiate (Resources.Load ("prefabs/star")) as GameObject;
			star2.transform.position = new Vector2 (0f, .5f);
		}
		if (stars == 3) {
			star1 = GameObject.Instantiate (Resources.Load ("prefabs/star")) as GameObject;
			star1.transform.position = new Vector2 (-.3f, .5f);
			star2 = GameObject.Instantiate (Resources.Load ("prefabs/star")) as GameObject;
			star2.transform.position = new Vector2 (0f, .5f);
			star3 = GameObject.Instantiate (Resources.Load ("prefabs/star")) as GameObject;
			star3.transform.position = new Vector2 (.3f, .5f);
		}
	}
	
	public void openLevel(string level){
		levelManager.GetComponent<LevelManager> ().resetStats ();
		levelManager.GetComponent<LevelManager> ().openLevel (level);
	}

	public void openNextLevel(){
		levelManager.GetComponent<LevelManager> ().resetStats ();
		levelManager.GetComponent<LevelManager> ().openCampaignLevel (levelManager.GetComponent<LevelManager> ().getCampaignLevel ());
	}

	public void playAgain(){
		if (levelManager.GetComponent<LevelManager> ().getLastLevel () == "Arcade") {
			levelManager.GetComponent<LevelManager> ().resetStats ();
			levelManager.GetComponent<LevelManager> ().openLevel ("Arcade");
		} else {
			levelManager.GetComponent<LevelManager> ().resetStats ();
			levelManager.GetComponent<LevelManager> ().restartCampaignRound ();
			levelManager.GetComponent<LevelManager> ().openCampaignLevel (levelManager.GetComponent<LevelManager> ().getCampaignLevel ());
		}
	}
}
