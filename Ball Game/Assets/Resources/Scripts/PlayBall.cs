using UnityEngine;
using System.Collections;

public class PlayBall : MonoBehaviour {

	GameObject otherBall;
	GameObject purpleBall;
	public float speed = 15;
	bool isReleased;
	bool moving;
	bool gameStarted;
	Vector3 touchPosWorld;

	void Start(){
		if(this.gameObject.name == "blue_ball(Clone)")
			gameObject.name = "blue_ball";
		if(this.gameObject.name == "red_ball(Clone)")
			gameObject.name = "red_ball";
	
	}

	void Update () {
		if (this.gameObject.name == "blue_ball")
			otherBall = GameObject.Find ("red_ball");
		if(this.gameObject.name == "red_ball")
			otherBall = GameObject.Find ("blue_ball");
		enableReleased ();
		detectPanelTouch ();
		if (isReleased && gameStarted) {
			transform.position = Vector2.MoveTowards (transform.position, otherBall.transform.position, speed * Time.deltaTime);
			enableMoving ();
		}
	}

	void OnCollisionEnter2D (Collision2D collision) {
		
		if(collision.gameObject.name == "blue_ball"){
				CreateObject("prefabs/purple_ball");
				purpleBall.GetComponent<PurpleBall>().setPosition(this.transform.position.x, this.transform.position.y, this.transform.position.z);
				Destroy (this.gameObject);
				Destroy (otherBall.gameObject);
			}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "forbidden") {
			enableReleased ();
			enableMoving ();
		}
	}

	public void MoveBall(float x, float y){
			Vector3 pos = new Vector3 (x, y, transform.position.z - Camera.main.transform.position.z);
			transform.position = Camera.main.ScreenToWorldPoint (pos);
	}

	public void detectPanelTouch(){
		if (Input.touchCount > 0) {
			
			for (int i = 0; i < Input.touchCount; i++) {

				touchPosWorld = Camera.main.ScreenToWorldPoint (Input.GetTouch (i).position);
				Vector2 touchPosWorld2D = new Vector2 (touchPosWorld.x, touchPosWorld.y);
				RaycastHit2D hitInformation = Physics2D.Raycast (touchPosWorld2D, Camera.main.transform.forward);

				if (hitInformation.collider != null) {
					GameObject touchedObject = hitInformation.transform.gameObject;

					if (!moving && touchedObject.transform.name == "left_panel" && this.gameObject.name == "blue_ball" || !moving && touchedObject.transform.name == "blue_ball" && this.gameObject.name == "blue_ball") {
						
						gameStarted = true;
						disableReleased ();
						MoveBall (Input.GetTouch (i).position.x, Input.GetTouch (i).position.y);
					}

					if (!moving && touchedObject.transform.name == "right_panel" && this.gameObject.name == "red_ball" || !moving && touchedObject.transform.name == "red_ball" && this.gameObject.name == "red_ball") {
						gameStarted = true;
						disableReleased ();
						MoveBall (Input.GetTouch (i).position.x, Input.GetTouch (i).position.y);
					}
				}
			}
		}
	}

	public void CreateObject(string prefabName) {
		purpleBall = GameObject.Instantiate(Resources.Load(prefabName)) as GameObject;
	}

	public void enableMoving(){ moving = true; }
	public void disableMoving(){ moving = false; }
	public void enableReleased(){ isReleased = true; }
	public void disableReleased(){ isReleased = false; }
	public bool getGameStarted() {return gameStarted;}

}
	
