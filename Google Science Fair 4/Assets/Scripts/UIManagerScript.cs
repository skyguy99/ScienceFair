using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManagerScript : MonoBehaviour {

	public static int sceneNum = 2;

	void Start() {

	}

	public void StartGame() {
		
		//Application.LoadLevel("SF7");
		if (selectManager.hasClicked == true) {
			if (sceneNum == 0) {
				SceneManager.LoadScene ("MazeScene1");
			} else if (sceneNum == 1) {
				SceneManager.LoadScene ("MazeScene2");
			} else if (sceneNum == 2) {
				SceneManager.LoadScene ("MazeScene3");
			}
		}
	}

	public void rightClick() {
		selectManager.hasClicked = true;
		if (sceneNum == 0) {
			sceneNum = 1;
			Debug.Log (sceneNum);

		} else if (sceneNum == 1) {
			sceneNum = 2;
			Debug.Log (sceneNum);
		}
		else if (sceneNum == 2) {
			sceneNum = 0;
			Debug.Log (sceneNum);
		}
	}

	public void leftClick() {
		selectManager.hasClicked = true;
		if (sceneNum == 0) {
			sceneNum = 2;
			Debug.Log (sceneNum);
		} else if (sceneNum == 1) {
			sceneNum = 0;
			Debug.Log (sceneNum);
		}
		else if (sceneNum == 2) {
			sceneNum = 1;
			Debug.Log (sceneNum);
		}
	}
}
