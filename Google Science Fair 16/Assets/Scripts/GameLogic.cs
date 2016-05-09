using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;


public class GameLogic : MonoBehaviour {

	//For rotation
	public GameObject world;
	private Vector3 headTemp = new Vector2(0.0f, 0.0f);
	//private Vector3 newPos = new Vector3 (1.25f,0.4f,-0.4f);

	private float headX;

	public static bool roundWasReset = false;

	//public Canvas cubeCanvas;
	public static bool gameStarted = false;
	public static bool gameOver = false;

	public Camera cam1;
	public Camera cam2;

	//On eye canvas
	public Canvas eyeCanvas;
	public Image cubeImage;
	public Sprite img2;
	public Text timeEyeText;
	public Text timesFound;

	public static int score = 0;
	public static int currentRound = 1;

	public static float timer;
	string minutes = Mathf.Floor(timer / 60).ToString("00");
	string seconds = (timer % 60).ToString("00");
	private bool timerOn = false;

	//For round over move
	public Camera mainCamera;
	private Vector3 temp = new Vector3(6.2f,1.7f,-3.9f);
	private Quaternion rotation = Quaternion.Euler(0, 0, 0);

	//For round over canvas
	public Rigidbody m_RigidBody;
	public Canvas roundOver;
	public Text roundText;
	public Text timerText;
	float ctx;
	float ctz;

	//For start canvas 
	public Canvas startCanvas;
	public Text countdown; 

	//For mission accomplish/game over canvas
	public Canvas endCanvas;
	public Text results;
	public Image completes;
	public Text resultsRounds;
	string body = "hi";
	string body2 = "hi";

	//round completes
	public static bool r1Complete = false;
	public static bool r2Complete = false;
	public static bool r3Complete = false;
	public static bool r4Complete = false;
	public static bool r5Complete = false;

	public Sprite r1Comp;
	public Sprite r1InComp;
	public Sprite r2Comp;
	public Sprite r2InComp;
	public Sprite r3Comp;
	public Sprite r3InComp;
	public Sprite r4Comp;
	public Sprite r4InComp;
	public Sprite r5Comp;
	public Sprite r5InComp;

	//For bottom canvases
	public Canvas giveUp;
	public Canvas menu; 

	//For giving up
	public Canvas terminate;
	public Text results2;
	public Text roundsTxt2;
	public Text giveUpResults;
	//public Text roundsTxt2;

	public CardboardHead head1;
	public GameObject main;
	Quaternion headRot;
	Quaternion test1 = new Quaternion(0.0f,0.0f,0.0f,0.0f);

	// Use this for initialization
	void Start () {

		//headRot = head1.transform.localRotation;


		gameOver = false;
		currentRound = 1;
		gameStarted = false;
		score = 0;

		terminate.enabled = false;
		giveUp.enabled = false;
		endCanvas.enabled = false;
		eyeCanvas.enabled = false;
		timesFound.text = score.ToString();
		roundText.text = "ROUND "+currentRound+" COMPLETED";
		timerText.text = minutes + ":" + seconds;
		timeEyeText.text = minutes + ":" + seconds;

		countdown.text = "3";

		//CardboardReticle.cubeFound = false;
		roundOver.enabled = false;
		eyeCanvas.enabled = false; 

		move.moveEnabled = false;
		StartCoroutine (startUp ());

		r1Complete = false;
		r2Complete = false;
		r3Complete = false;
		r4Complete = false;
		r5Complete = false;



	} //--------------------------------------------------------------------------------------------------------------------

	public void VRToggle()
	{
		Debug.Log ("Switch!!");
		if (!Cardboard.SDK.VRModeEnabled)
			Cardboard.SDK.VRModeEnabled = true;
		else
			Cardboard.SDK.VRModeEnabled = false;
	}

	IEnumerator startUp()
	{
/* countdown.text = "3";    
		yield return new WaitForSeconds(1);  

		countdown.text = "2";    
		yield return new WaitForSeconds(1);  

		countdown.text = "1";    
		yield return new WaitForSeconds(1);  
*/
		countdown.text = "GO";    
		yield return new WaitForSeconds(1);  

		startCanvas.enabled = false;
		eyeCanvas.enabled = true;
		move.moveEnabled = true;
		//eyeCanvas.enabled = true;
		timerOn = true;
		gameStarted = true;
		countdown.text = "";  
	}

	public void givingUp()
	{
		Debug.Log ("Terminate");
		m_RigidBody.transform.position = temp;
		m_RigidBody.transform.rotation = rotation;
		m_RigidBody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ; //freeze rotation
		move.moveEnabled = false;
		terminate.enabled = true;
		eyeCanvas.enabled = false;

		roundsTxt2.text = body2;
		giveUpResults.text = body;

		results2.text = body;

	}

	//Resets round when clicked
	public void roundReset() {

		//trail.GetComponent<Renderer>().material = trailInvisible;

		roundWasReset = true;
		//Debug.Log ("head is:"+head1.transform.localRotation);
		//head1.transform.localRotation = Quaternion.Euler(90.0f,70.0f,80.0f);
		//world.transform.rotation = headRotChanging;


		if (!gameOver) {
			m_RigidBody.transform.position = temp;
			//m_RigidBody.transform.rotation = rotation;

			Debug.Log ("games not over!!!!!!");
			updateRoundComplete ();
			currentRound++;

			timerOn = true;
			stopTimer ();
			roundOver.enabled = false;
			eyeCanvas.enabled = true;
			move.moveEnabled = true;
			updateRoundText ();
			CardboardReticle.cubeFound = false;
			//Instantiate (trail);
		} else {
			//currentRound++;
			updateRoundComplete();
			CardboardReticle.cubeFound = false;
			stopTimer ();
			roundOver.enabled = false;
			missionOverFunc ();
			Debug.Log ("Mission over!");
		}


	}
	void updateRoundComplete()
	{
		if (currentRound == 1)
			r1Complete = true;
		else if (currentRound == 2)
			r2Complete = true;
		else if (currentRound == 3)
			r3Complete = true;
		else if (currentRound == 4)
			r4Complete = true;
		else if (currentRound == 5)
			r5Complete = true;


	}

	void setTime() {

		if (currentRound == 1)
			email.r1Time = minutes + ":" + seconds;
		else if (currentRound == 2)
			email.r2Time = minutes + ":" + seconds;
		else if (currentRound == 3)
			email.r3Time = minutes + ":" + seconds;
		else if (currentRound == 4)
			email.r4Time = minutes + ":" + seconds;
		else if (currentRound == 5)
			email.r5Time = minutes + ":" + seconds;

		//Debug.Log ("Set time called for"+currentRound);
	}

	//Updates canvas when cube found
	public void cubeFound()
	{
		if (CardboardReticle.cubeFound == true) {

			score++;
			timesFound.text = score.ToString();

			CardboardReticle.cubeFound = false;
		}
	}
	void updateRoundText()
	{
		roundText.text = "ROUND "+currentRound+" COMPLETED";
	}

	void updateEyeText()
	{
		timesFound.text = score.ToString();
		//cubeImage.sprite = img2; //change image

		minutes = Mathf.Floor (timer / 60).ToString ("00");
		seconds = (timer % 60).ToString("00");
		timeEyeText.text = minutes + ":" + seconds;

	}
	void stopTimer()
	{
		//setTime ();

		timer = 0;
		minutes = 0.ToString("00");
		seconds = 0.ToString("00");
		timeEyeText.text = minutes + ":" + seconds;
	}
	void roundOverFunc()
	{
		//ok.enabled = true;

		//stopTimer ();
		setTime();
		timerOn = false;
		move.moveEnabled = false;
		timerText.text = minutes + ":" + seconds;
		roundOver.enabled = true;
		eyeCanvas.enabled = false;
	}
	/*Sprite gameOverImages() {

		if (UIManagerScript.roundNum == 1) {
			completes.transform.localPosition = new Vector2(completes.transform.localPosition.x, (completes.transform.localPosition.y)-0.5f);
			if (r1Complete)
				return r1Comp;
			else
				return r1InComp;
		}
		else if (UIManagerScript.roundNum == 2) {
			completes.transform.localPosition = new Vector2(completes.transform.localPosition.x, (completes.transform.localPosition.y)-0.3f);
			if (r2Complete)
				return r2Comp;
			else
				return r2InComp;
		}
		else if (UIManagerScript.roundNum == 3) {
			completes.transform.localPosition = new Vector2(completes.transform.localPosition.x, (completes.transform.localPosition.y)-0.2f);
			if (r3Complete)
				return r3Comp;
			else
				return r3InComp;
		}
		else if (UIManagerScript.roundNum == 4) {
			completes.transform.localPosition = new Vector2(completes.transform.localPosition.x, (completes.transform.localPosition.y)-0.2f);
			if (r4Complete)
				return r4Comp;
			else
				return r4InComp;
		}
		else {
			if (r5Complete)
				return r5Comp;
			else
				return r5InComp;
		}
	}*/

	void roundOverFunc2()
	{
		//stopTimer ();
		gameOver = true;

		//completes.sprite = gameOverImages ();
		setTime();
		timerOn = false;
		move.moveEnabled = false;
		timerText.text = minutes + ":" + seconds;
		roundOver.enabled = true;
		eyeCanvas.enabled = false;
	}
	void missionOverFunc()
	{
		//roundOver.enabled = false;
		//currentRound = 1; //RESET ROUNDS

		Debug.Log ("Mission over!");
		move.moveEnabled = false;
		endCanvas.enabled = true;
		results.text = body;
		resultsRounds.text = body2;

		//Set give up too
		roundsTxt2.text = body2;
		giveUpResults.text = body;
	}
	void updateFinalText() {
		if (UIManagerScript.roundNum == 1)
			body = "\n"+email.r1Time;
		else if (UIManagerScript.roundNum == 2)
			body = "\n"+email.r1Time + "\n" + email.r2Time;
		else if (UIManagerScript.roundNum == 3)
			body = "\n" + email.r1Time + "\n " + email.r2Time + "\n " + email.r3Time;
		else if (UIManagerScript.roundNum == 4)
			body = "\n" + email.r1Time + "\n" + email.r2Time + "\n" + email.r3Time + "\n" + email.r4Time;
		else if (UIManagerScript.roundNum == 5)
			body = "\n " + email.r1Time + "\n" + email.r2Time + "\n" + email.r3Time + "\n" + email.r4Time + "\n" + email.r5Time;
	}
	void updateFinalTextRounds() {
		if (UIManagerScript.roundNum == 1)
			body2 = "\nRound 1: ";
		else if (UIManagerScript.roundNum == 2)
			body2 = "\nRound 1: "+ "\nRound 2: ";
		else if (UIManagerScript.roundNum == 3)
			body2 = "\nRound 1: " + "\nRound 2: "+"\nRound 3: ";
		else if (UIManagerScript.roundNum == 4)
			body2 = "\nRound 1: "+"\nRound 2: "+"\nRound 3: "+"\nRound 4: ";
		else if (UIManagerScript.roundNum == 5)
			body2 = "\nRound 1: "+"\nRound 2: "+ "\nRound 3: " +"\nRound 4: " + "\nRound 5: ";

	}
	void updateLineRender()
	{
	}

	void alterCanvasDirection()
	{
		if (headRot.y >= -0.4f && headRot.y < 0.3f) {
			Debug.Log ("NORTH");
			//roundOver.transform.position = new Vector3(roundOver.transform.position.x, roundOver.transform.position.y, roundOver.transform.position.z);
			roundOver.transform.rotation = Quaternion.Euler(Vector3.up * 1);
			ctx = main.transform.localPosition.x + 0.0f;
			ctz = main.transform.localPosition.z + 1.0f;
			roundOver.transform.position = new Vector3(ctx, 2.0f ,ctz);

			//roundOver.transform.position = new Vector3(6.2f,2.0f,-3.0f);
			endCanvas.transform.rotation = Quaternion.Euler(Vector3.up * 1);
			endCanvas.transform.position = new Vector3(ctx, 2.0f ,ctz);

			terminate.transform.rotation = Quaternion.Euler(Vector3.up * 1);
			terminate.transform.position = new Vector3(ctx, 2.0f ,ctz);

		}
		else if(headRot.y >= 0.3f && headRot.y < 0.8f) {
			Debug.Log ("EAST");
			//roundOver.transform.position = new Vector3(roundOver.transform.position.x, roundOver.transform.position.y, roundOver.transform.position.z);
			roundOver.transform.rotation = Quaternion.Euler(Vector3.up * 90);
			ctx = main.transform.localPosition.x + 1.0f;
			ctz = main.transform.localPosition.z + 0.0f;
			roundOver.transform.position = new Vector3(ctx, 2.0f ,ctz);

			endCanvas.transform.rotation = Quaternion.Euler(Vector3.up * 90);
			endCanvas.transform.position = new Vector3(ctx, 2.0f ,ctz);

			terminate.transform.rotation = Quaternion.Euler(Vector3.up * 90);
			terminate.transform.position = new Vector3(ctx, 2.0f ,ctz);
		}
		else if(headRot.y >= 0.8f && headRot.y < 1.0f || headRot.y >= -0.9f && headRot.y < -1.0f) 
		
		{
			Debug.Log ("SOUTH");
			//roundOver.transform.position = new Vector3(roundOver.transform.position.x, roundOver.transform.position.y, roundOver.transform.position.z);
			roundOver.transform.rotation = Quaternion.Euler(Vector3.up * 180);
			ctx = main.transform.localPosition.x + 0.0f;
			ctz = main.transform.localPosition.z + -1.0f;
			roundOver.transform.position = new Vector3(ctx, 2.0f ,ctz);

			endCanvas.transform.rotation = Quaternion.Euler(Vector3.up * 180);
			endCanvas.transform.position = new Vector3(ctx, 2.0f ,ctz);

			terminate.transform.rotation = Quaternion.Euler(Vector3.up * 180);
			terminate.transform.position = new Vector3(ctx, 2.0f ,ctz);
		}
		else if(headRot.y >= -0.9f && headRot.y < -0.4f) {
			Debug.Log ("WEST");
			//roundOver.transform.position = new Vector3(roundOver.transform.position.x, roundOver.transform.position.y, roundOver.transform.position.z);
			roundOver.transform.rotation = Quaternion.Euler(Vector3.up * 270);
			ctx = main.transform.localPosition.x + -1.0f;
			ctz = main.transform.localPosition.z + 0.0f;
			roundOver.transform.position = new Vector3(ctx, 2.0f ,ctz);

			endCanvas.transform.rotation = Quaternion.Euler(Vector3.up * 270);
			endCanvas.transform.position = new Vector3(ctx, 2.0f ,ctz);

			terminate.transform.rotation = Quaternion.Euler(Vector3.up * 270);
			terminate.transform.position = new Vector3(ctx, 2.0f ,ctz);
		}
	}


	// Update is called once per frame --------------------------------------------------------------------------------------
	void Update () {

		/*if (CardboardReticle.cubeFound == false) {
			headRotChanging = head1.transform.localRotation;
			//headX = head1.transform.rotation.eulerAngles.x; //get x rot of head
		}*/
		headRot = head1.transform.localRotation;

		//Debug.Log ("headTemp is"+headTemp);
		Debug.Log ("Head lateral pos is "+headX);
		Debug.Log (headTemp);
		Debug.Log ("Position x is"+main.transform.localPosition.x);
		//Debug.Log ("Position y is"+main.transform.localPosition.y);
		Debug.Log ("Position z is"+main.transform.localPosition.z);
		//Debug.Log ((float)0.87);
		//world.transform.position = new Vector3 ();
		//headTemp.x = headX;
		//test1.w = headX;
		//roundOver.transform.rotation = Quaternion.Euler(Vector3.up * 90);
		//roundOver.transform.position = new Vector3(roundOver.transform.position.x+5, roundOver.transform.position.y, roundOver.transform.position.z);
		//roundOver.transform.position = new Vector3(1.0f, roundOver.transform.position.y, roundOver.transform.position.z);
		alterCanvasDirection();

		//world.transform.rotation = new Quaternion(0.0f, )
		Debug.Log ("head is:"+head1.transform.localRotation);
		//headRot = head1.transform.localRotation;
		//Debug.Log ("HeadRot"+headRot);
		//roundOver.transform.rotation = headRot;

		Debug.Log ("Canvas is"+roundOver.transform.position);
		//Debug.Log ("world is"+world.transform.rotation);

		//-------------------------------------------------

		if (!move.moveBegun) {
			giveUp.enabled = false;
			menu.enabled = true;
		} else {
			giveUp.enabled = true;
			menu.enabled = false;
		}

		if(timerOn)
			timer += Time.deltaTime;

		//Debug.Log (timer);
		updateFinalText();
		updateFinalTextRounds ();
		updateEyeText ();

		if (CardboardReticle.cubeFound == true) {
			//Reset round if still rounds to go
			if (currentRound < UIManagerScript.roundNum) {
				//Debug.Log ("Round over!");
				roundOverFunc ();
			} else {
				Debug.Log ("Rounds over! Game over");

				roundOverFunc2();
			}
			//move and cardboard to false
		}

	}
}