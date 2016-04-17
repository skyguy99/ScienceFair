using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour {

	public Text timesFound;
	public Canvas cubeCanvas;
	public Image cubeImage;
	public Sprite img2;
	public static int score = 0;

	// Use this for initialization
	void Start () {

		timesFound.text = score+"x";
		cubeCanvas.enabled = false;
	
	}
	public void resetRound()
	{
		//cubeFound = false; - reset cube
		Debug.Log("transport player back to start");
	}

	public void cubeFound()
	{
		if (CardboardReticle.cubeFound == true) {

			score++;
			timesFound.text = score+"x";
			cubeCanvas.enabled = true;
			CardboardReticle.cubeFound = false;
		}
	}
	void updateText()
	{
		timesFound.text = score+"x";
		cubeImage.sprite = img2; //change image
		resetRound ();
		
	}
	
	// Update is called once per frame
	void Update () {

		//cubeFound ();
		updateText();

	
	}
}
