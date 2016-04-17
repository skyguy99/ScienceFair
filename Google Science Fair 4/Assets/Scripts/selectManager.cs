using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class selectManager : MonoBehaviour {

	public static bool hasClicked = false;
	Text txt;

	// Use this for initialization
	void Start () {
		txt = gameObject.GetComponent<Text>(); 
		txt.text="Select a maze";
	}

	// Update is called once per frame
	void Update () {
		if (!hasClicked)
			txt.text = "Select a maze";
		else {
			if (UIManagerScript.sceneNum == 0)
				txt.text = "Maze 1";
			else if (UIManagerScript.sceneNum == 1)
				txt.text = "Double Grid";
			else if (UIManagerScript.sceneNum == 2)
				txt.text = "Labyrinth";
		}
  
	}
}
