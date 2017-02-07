using UnityEngine;
using System.Collections;

public class ArcadeTarget : MonoBehaviour {

	GameObject blueBall;
	GameObject redBall;
	public GameObject outOfBoundstarget;
	float speed = 1;
	bool gameStarted;

	// Use this for initialization
	void Start () {
		blueBall = GameObject.Find ("blue_ball");
		redBall = GameObject.Find ("red_ball");
	}
	
	// Update is called once per frame
	void Update () {
		
		if(gameStarted)
			transform.position = Vector2.MoveTowards (transform.position, outOfBoundstarget.transform.position, speed * Time.deltaTime);
		
		if (blueBall.GetComponent<PlayBall> ().getGameStarted () || redBall.GetComponent<PlayBall> ().getGameStarted ()) 
			gameStarted = true;
	}

	void OnTriggerEnter2D(Collider2D other) {
		
		if (other.gameObject.name == "OutOfBoundsBox") {
			transform.position = new Vector3(0f, 2.7f, 0f);
			Camera.main.GetComponent<ScoreKeeper> ().decrementLives ();
		}
		if (other.gameObject.tag == "purpleBall") {
			transform.position = new Vector3(0f, 2.7f, 0f);
			speed = speed + 0.5f;
		}
	}
}
