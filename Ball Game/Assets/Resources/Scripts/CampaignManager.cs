using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CampaignManager : MonoBehaviour {

	GameObject levelManager;
	GameObject target;
	GameObject coin;

	float firstAngle;
	float secondAngle;

	float speed = 2;
	Vector2 destination;
	bool isMoving;

	int maxScore = 100;


	void Start () {
		target = GameObject.Find ("campaign_target");
		levelManager = GameObject.Find ("LevelManager");
		checkLevel ();
	}

	void Update(){
		if (isMoving) 
			target.transform.position = Vector2.MoveTowards (target.transform.position, destination, speed * Time.deltaTime);
		if (Vector2.Distance(target.transform.position, destination) == 0)
			destination = randomPosition();
	}
		
	void checkLevel(){
		switch (levelManager.GetComponent<LevelManager> ().getCampaignLevel () / 10) {

		case 1:
			target.transform.position = new Vector3 (0f, randomYPos(), 0f);
			calculateMaxScore ();
			break;

		case 2:
			target.transform.position = new Vector3 (randomXPos(), 0f, 0f);
			calculateMaxScore ();
			break;

		case 3:
			target.transform.position = new Vector3 (randomXPos(), randomYPos(), 0f);
			calculateMaxScore ();
			break;

		case 4:
			firstAngle = randomAngle ();
			secondAngle = -(180f - firstAngle);
			generateCoins (firstAngle);
			generateCoins (secondAngle);
			calculateMaxScore ();
			break;

		case 5:
			target.transform.position = new Vector3 (randomXPos (), randomYPos (), 0f);

			firstAngle = randomAngle ();
			secondAngle = -(180f - firstAngle);
			RaycastHit2D firstHit = Physics2D.Raycast (target.transform.position, Vector2FromAngle (firstAngle));
			RaycastHit2D secondHit = Physics2D.Raycast (target.transform.position, Vector2FromAngle (secondAngle));

			while (firstHit.collider == null || secondHit.collider == null) {
				firstAngle = randomAngle ();
				secondAngle = -(180f - firstAngle);
				firstHit = Physics2D.Raycast (target.transform.position, Vector2FromAngle (firstAngle));
				secondHit = Physics2D.Raycast (target.transform.position, Vector2FromAngle (secondAngle));
			}
			generateCoins (firstAngle);
			generateCoins (secondAngle);

			calculateMaxScore ();
			break;

		case 6:
			destination = randomPosition ();
			isMoving = true;
			calculateMaxScore ();
			break;

		case 7:
			destination = randomPosition ();
			speed = 3;
			isMoving = true;
			calculateMaxScore ();
			break;

		case 8:
			coin = GameObject.Instantiate (Resources.Load ("prefabs/coin")) as GameObject;
			coin.GetComponent<Coin> ().setPosition (randomXPos(), randomYPos(), 0f);
			destination = randomPosition ();
			speed = 3;
			isMoving = true;

			calculateMaxScore ();
			break;

		case 9:
			coin = GameObject.Instantiate (Resources.Load ("prefabs/coin")) as GameObject;
			coin.GetComponent<Coin> ().setPosition (randomXPos(), randomYPos(), 0f);
			destination = randomPosition ();
			speed = 3;
			isMoving = true;

			calculateMaxScore ();
			break;

		case 10:
			levelManager.GetComponent<LevelManager> ().openLevel ("Menu");
			calculateMaxScore ();
			break;

		default:
			break;
		}
	}

	void generateCoins(float angle){
		float distance = .23f;
		RaycastHit2D hit = Physics2D.Raycast (target.transform.position, Vector2FromAngle (angle));

		for (int i = 0; i <= ((hit.distance - .26f) / .1f); i++) {
			GameObject coin = GameObject.Instantiate (Resources.Load ("prefabs/coin")) as GameObject;
			Vector2 Pos = new Vector2 (target.transform.position.x, target.transform.position.y) + Vector2FromAngle (angle) * distance;
			coin.GetComponent<Coin> ().setPosition (Pos.x, Pos.y, 0f);
			distance += .1f;
		}
	}

	public Vector2 Vector2FromAngle(float a){	
		Vector2 angledVector = Quaternion.AngleAxis (a, Vector3.forward) * Vector2.up;
		return angledVector;
	}
		
	Vector2 randomPosition(){
		Vector2 randomPosition = new Vector2 (randomXPos (), randomYPos ());
		return randomPosition;
	}

	void calculateMaxScore(){
		GameObject[] coinList;
		coinList = GameObject.FindGameObjectsWithTag("Coin");
		maxScore += 10 * coinList.Length;
		Camera.main.GetComponent<ScoreKeeper> ().updateMaxScore (maxScore);
	}

	float randomYPos(){ return Random.Range (-2f, 2f); }
	float randomXPos(){ return Random.Range (-.6f, .6f); }
	float randomAngle(){ return Random.Range (25f, 155f); }
}


