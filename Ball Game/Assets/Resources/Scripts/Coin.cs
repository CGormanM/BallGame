 using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Ball") {
			Camera.main.GetComponent<ScoreKeeper> ().updateScore (10);
			Destroy (this.gameObject);
		}
	}

	public void setPosition(float x, float y, float z){
		transform.position = new Vector3(x, y, z);
	}
}