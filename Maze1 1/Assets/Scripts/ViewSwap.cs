using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class ViewSwap : MonoBehaviour {
	 
	public static bool enableOkBtn = false;

	public GameObject[] cardboardObjects;
	public GameObject[] monoObjects;
	public bool switched = false;

	public GameObject[] cardboardObjects2;
	public GameObject[] monoObjects2;
	public bool switched2 = false;

	public Text advance;
	//public Button okBtn;

	//For trail renderer
	public TrailRenderer trail;
	public Material trailColor;
	public Material trailInvisible;

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

		if (GameLogic.gameStarted) {
			ActivateVRMode (false);
			switched = true;
			Debug.Log (switched + "No VR!");

			trail.GetComponent<Renderer> ().material = trailColor;
		}
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
		trail.GetComponent<Renderer>().material = trailInvisible;

		//okBtn.gameObject.SetActive (false);
		//okBtn.GetComponentInChildren<CanvasRenderer>().SetAlpha(0);
		//okBtn.GetComponentInChildren<Text>().color = Color.clear;


			Debug.Log (switched+"VR!");
		//round1.enabled = false;
	}

	void Update () {

		/*if (GameLogic.gameStarted) {
			if (Input.GetKey (KeyCode.Escape) || Cardboard.SDK.Tilted) {
				//Debug.Log ("Show map");
				Switch ();
				Debug.Log ("help em");

			}

			Cardboard.SDK.OnBackButton += () => {
				Debug.Log ("WORKING"); 
				Switch ();
			};
		}*/

		if (switched == true) {
			//round1.enabled = false;
			StartCoroutine(Switchback ());
		}

		//Debug.Log ("updating");

	}

	void Start() {

		//round1.enabled = true;
		//round1.enabled = false;

		trail.GetComponent<Renderer>().material = trailInvisible;
		ActivateVRMode(true);
	}
}
