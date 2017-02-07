using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	int lives = 3;
	int score = 0;
	int maxScore;
	int campaignLevel = 0;
	string lastLevel;


	void Start () {
		DontDestroyOnLoad (this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			openLevel ("Menu");
			resetStats ();
		}
		if (campaignLevel % 10 == 5) {
			openLevel("LevelComplete");
			increaseCampaignLevel (5);
		}
	}

	public void openLevel(string name){
		if (name.Equals ("GameOver") || name.Equals("LevelComplete")) {
			lastLevel = SceneManager.GetActiveScene ().name;
			score = Camera.main.GetComponent<ScoreKeeper> ().getScore ();
			maxScore = Camera.main.GetComponent<ScoreKeeper> ().getMaxScore ();
		}
		SceneManager.LoadScene (name);
	}

	public void openCampaignLevel(int level){
		campaignLevel = level;
		SceneManager.LoadScene ("Default");
	}

	public void incrementCampaignLevel(){
		if (campaignLevel % 10 == 5)
			increaseCampaignLevel (5);
		else
			increaseCampaignLevel (1);
		score = Camera.main.GetComponent<ScoreKeeper> ().getScore ();
		lives = Camera.main.GetComponent<ScoreKeeper> ().getLives ();
		maxScore = Camera.main.GetComponent<ScoreKeeper> ().getMaxScore ();
		SceneManager.LoadScene ("Default");
	}

	public void resetStats(){
		resetScore ();
		resetLastLevel ();
		resetLives ();
		resetMaxScore ();
	}

	public void increaseCampaignLevel(int increment){ 
		campaignLevel += increment;
		if(campaignLevel > PlayerPrefs.GetInt("CampaignHighLevel"))
			PlayerPrefs.SetInt ("CampaignHighLevel", campaignLevel);
	}

	public int getCampaignLevel(){ return campaignLevel; }
	public int getScore(){ return score; }
	public void setMaxScore(int newPoints){ maxScore += newPoints; }
	public int getMaxScore(){ return maxScore; }
	public void resetMaxScore(){ maxScore = 0; }
	public int getLives(){ return lives; }
	public void resetLives(){ lives = 3; }
	public void resetScore(){ score = 0; }
	public void resetLastLevel(){ lastLevel = ""; }
	public void restartCampaignRound(){ campaignLevel -= campaignLevel % 10; }
	public string getLastLevel() { return lastLevel; }

	public void reset(){ PlayerPrefs.SetInt ("CampaignHighLevel", 0); }

}