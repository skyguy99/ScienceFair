using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class spawnCubes : MonoBehaviour {

	public static bool touchedMaze = false;
	public static bool finalPosReached = false;

	public Vector3 position; 
	public GameObject cube;
	public Canvas cubeAlert;
	int numToSpawn;

	// Use this for initialization
	void Start () {

		spawnRandCube ();
	
	}
	void spawnRandCube() {

		touchedMaze = false;
		position = new Vector3(Random.Range(7.0F, 26.0F), (float) 0.59, Random.Range(-9.0F, 10.0F));
		Debug.Log (position);

			Instantiate(cube, position, Quaternion.identity);
			Debug.Log ("spawned it!");
		finalPosReached = true;

	}

	
	// Update is called once per frame
	void Update () {

		while (touchedMaze == true) {
			spawnRandCube ();
		}
	
	}
}
