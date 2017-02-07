using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PurpleBall : MonoBehaviour {

	GameObject levelManager;
	float timeLeft = .1f;
	bool targetHit;
	bool generatedBalls;
	bool arcadeMode;

	// Use this for initialization
	void Start () {
		levelManager = GameObject.Find ("LevelManager");
		if (SceneManager.GetActiveScene().name == "Arcade") {
			arcadeMode = true;
			timeLeft = 0.1f;
		}
	}
	
	// Update is called once per frame
	void Update () {
		timeLeft -= Time.deltaTime;
		if ( timeLeft < 0 )
		{
			if (arcadeMode) {
				CreateObject ("prefabs/blue_ball");
				CreateObject ("prefabs/red_ball");
				generatedBalls = true;
				if (!targetHit)
					Camera.main.GetComponent<ScoreKeeper> ().decrementLives ();
				Destroy (this.gameObject);
			} else {
				
				CreateObject ("prefabs/blue_ball");
				CreateObject ("prefabs/red_ball");
				generatedBalls = true;
				if (!targetHit)
					Camera.main.GetComponent<ScoreKeeper> ().decrementLives ();
				Destroy (this.gameObject);
			}

		}
		if (Input.touchCount > 0)
			timeLeft = 0;
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.name == "arcade_target") {
			Camera.main.GetComponent<ScoreKeeper> ().updateScore (100);
			targetHit = true;
			timeLeft = .1f;
		}
		if (other.gameObject.name == "campaign_target") {
			Camera.main.GetComponent<ScoreKeeper> ().updateScore (100);
			levelManager.GetComponent<LevelManager> ().incrementCampaignLevel ();

		}
	}

	public void setPosition(float x, float y, float z){
		transform.position = new Vector3(x, y, z);
	}

	public void CreateObject(string prefabName) {
		if (!generatedBalls) {
			if (prefabName == ("prefabs/blue_ball")) {
				GameObject blue_ball = GameObject.Instantiate (Resources.Load (prefabName)) as GameObject;
			}
			if (prefabName == ("prefabs/red_ball")) {
				GameObject red_ball = GameObject.Instantiate (Resources.Load (prefabName)) as GameObject;
			}
		}
	}

}
