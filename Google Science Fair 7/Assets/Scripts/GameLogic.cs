using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;


public class GameLogic : MonoBehaviour {

	//For line rendering
	List<Vector2> playerPos = new List<Vector2>();
	public TrailRenderer trail;
	public Button okBtn2;

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

	//For start canvas 
	public Canvas startCanvas;
	public Text countdown; 

	//For mission accomplish canvas
	public Canvas endCanvas;
	public Text results;
	public Text resultsRounds;
	string body = "hi";
	string body2 = "hi";

	//For bottom canvases
	public Canvas giveUp;
	public Canvas menu; 

	//For giving up
	public Canvas terminate;
	public Text results2;

	public CardboardHead head1;

	// Use this for initialization
	void Start () {


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


	
	} //--------------------------------------------------------------------------------------------------------------------

	public void disableVR()
	{
		Cardboard.SDK.enabled = false;
	}
	public void enableVR()
	{
		Cardboard.SDK.enabled = true;
	}

	IEnumerator startUp()
	{
		countdown.text = "3";    
		yield return new WaitForSeconds(1);  

		countdown.text = "2";    
		yield return new WaitForSeconds(1);  

		countdown.text = "1";    
		yield return new WaitForSeconds(1);  

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
		results2.text = body;

	}

	//Resets round when clicked
	public void roundReset() {

		Debug.Log ("head is:"+head1.transform.localRotation);
		head1.transform.localRotation = Quaternion.Euler(90.0f,70.0f,80.0f);
		//head1.transform.rotation = new Quaternion(0.0f,0.0f,0.0f,0.0f);
		//head1.transform.localRotation = new Quaternion(0.0f,90.0f,70.0f,80.0f);
		//head1.transform.rotation = Quaternion.Slerp(0.0f,70.0f,0.0f,0);
		//m_RigidBody.transform.rotation = Quaternion.Euler(0, 80, 0);
		//Camera.main.transform.rotation = Quaternion.Euler(0.0f,0.0f,0.0f);
		//CardboardHead.transform.rotation = Quaternion.Euler(0.0f,70.0f,0.0f);

		//Color col = new Color ();
		okBtn2.gameObject.SetActive (true);
		trail.material.SetColor("_TintColor",new Color(0.0f,0.5f,0.6f,0.0f));
		//Destroy(trail);
		//each new round instantiate

		if (!gameOver) {
			m_RigidBody.transform.position = temp;
			//m_RigidBody.transform.rotation = rotation;

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
			CardboardReticle.cubeFound = false;
			stopTimer ();
			roundOver.enabled = false;
			missionOverFunc ();
			Debug.Log ("Mission over!");
		}


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
		//stopTimer ();
		setTime();
		timerOn = false;
		move.moveEnabled = false;
		timerText.text = minutes + ":" + seconds;
		roundOver.enabled = true;
		eyeCanvas.enabled = false;
	}
	void roundOverFunc2()
	{
		//stopTimer ();
		gameOver = true;
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
		Debug.Log ("Mission over!");
		move.moveEnabled = false;
		endCanvas.enabled = true;
		results.text = body;
		resultsRounds.text = body2;
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
		
	
	// Update is called once per frame --------------------------------------------------------------------------------------
	void Update () {

		//trail.material.SetColor("_TintColor", new Color(1, 160, 1, 1));
		//Cardboard.SDK.VRModeEnabled = false;

		/*mazeLine.SetVertexCount(5);
		 * 

		for (int i = 0; i < 4; i++ )
		{
			//Change the postion of the lines
			mazeLine.SetPosition(i, new Vector3(0.0f, 5.0f, 8.0f));
		}*/

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