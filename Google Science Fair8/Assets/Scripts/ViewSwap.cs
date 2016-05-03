using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class ViewSwap : MonoBehaviour {

	public GameObject[] cardboardObjects;
	public GameObject[] monoObjects;
	public bool switched = false;
	public Text advance;
	public Button okBtn;

	// Turn on or off VR mode
	void ActivateVRMode(bool goToVR) {
		foreach (GameObject cardboardThing in cardboardObjects) {
			cardboardThing.SetActive(goToVR);
		}
		foreach (GameObject monoThing in monoObjects) {
			monoThing.SetActive(!goToVR);
		}
		Cardboard.SDK.VRModeEnabled = goToVR;

		// Tell the game over screen to redisplay itself if necessary
		//gameObject.GetComponent<GameController>().RefreshGameOver();
	}

	public void Switch() {
		ActivateVRMode(false);
		switched = true;
		Debug.Log (switched+"No VR!");
	}
	IEnumerator Switchback() 
	{
		//yield return new WaitForSeconds(3); 
		advance.text = "Advance to next round in 3...";    
		yield return new WaitForSeconds(1);  

		advance.text = "Advance to next round in 2...";    
		yield return new WaitForSeconds(1);  

		advance.text = "Advance to next round in 1...";
		yield return new WaitForSeconds(1);
			ActivateVRMode(true);
			switched = false;

		okBtn.gameObject.SetActive (false);
		//okBtn.GetComponentInChildren<CanvasRenderer>().SetAlpha(0);
		//okBtn.GetComponentInChildren<Text>().color = Color.clear;
			Debug.Log (switched+"VR!");
	}

	void Update () {

		Debug.Log ("ITS:"+GameLogic.roundWasReset);
		if(GameLogic.roundWasReset == true) 
		{
			okBtn.gameObject.SetActive (true);
			//GameLogic.roundWasReset = false;
		}

		if (switched == true) {
			StartCoroutine(Switchback ());
		}

		//Debug.Log ("updating");

	}

	void Start() {
		ActivateVRMode(true);
	}
}
