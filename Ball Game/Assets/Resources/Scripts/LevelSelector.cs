using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour {

	GameObject levelManager;
	Button[] buttons;
	Button button2;
	Button button3;
	Button button4;
	Button button5;
	Button button6;
	Button button7;
	Button button8;
	Button button9;

	void Start(){
		levelManager = GameObject.Find ("LevelManager");

		button2 = GameObject.Find("level_button_2").GetComponent<Button>();
		button3 = GameObject.Find("level_button_3").GetComponent<Button>();
		button4 = GameObject.Find("level_button_4").GetComponent<Button>();
		button5 = GameObject.Find("level_button_5").GetComponent<Button>();
		button6 = GameObject.Find("level_button_6").GetComponent<Button>();
		button7 = GameObject.Find("level_button_7").GetComponent<Button>();
		button8 = GameObject.Find("level_button_8").GetComponent<Button>();
		button9 = GameObject.Find("level_button_9").GetComponent<Button>();

		if(PlayerPrefs.GetInt("CampaignHighLevel")/10 < 2)
			button2.interactable = false;
		if(PlayerPrefs.GetInt("CampaignHighLevel")/10 < 3)
			button3.interactable = false;
		if(PlayerPrefs.GetInt("CampaignHighLevel")/10 < 4)
			button4.interactable = false;
		if(PlayerPrefs.GetInt("CampaignHighLevel")/10 < 5)
			button5.interactable = false;
		if(PlayerPrefs.GetInt("CampaignHighLevel")/10 < 6)
			button6.interactable = false;
		if(PlayerPrefs.GetInt("CampaignHighLevel")/10 < 7)
			button7.interactable = false;
		if(PlayerPrefs.GetInt("CampaignHighLevel")/10 < 8)
			button8.interactable = false;
		if(PlayerPrefs.GetInt("CampaignHighLevel")/10 < 9)
			button9.interactable = false;

	}

	public void openLevel(string level){
		levelManager.GetComponent<LevelManager> ().openLevel (level);
	}

	public void openLevel(int level){
		levelManager.GetComponent<LevelManager> ().openCampaignLevel (level);
	}
}
