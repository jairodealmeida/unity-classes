using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class GameManager : MonoBehaviour {

	// make game manager public static so can access this from other scripts
	public static GameManager gm;

	// public variables
	public int score=0;

	public bool canBeatLevel = false;
	public int beatLevelScore=0;

	public float startTime=5.0f;
	
	public Text mainScoreDisplay;
	public Text mainTimerDisplay;

	public GameObject gameOverScoreOutline;

	public AudioSource musicAudioSource;

	public bool gameIsOver = false;

	public GameObject playAgainButtons;

	public GameObject level2Labels;
	public GameObject level3Labels;
	public GameObject level4Labels;
	public GameObject youwin;

	public GameObject boss;
	public GameObject enemies;

	public Text playerLifeValue;

	public Text bossLife;
	public Text bossLifeValue;

	public string playAgainLevelToLoad;

	public GameObject nextLevelButtons;
	public string nextLevelToLoad;

	public GameObject player;

	private float currentTime;

	// setup the game
	void Start () {

		// set the current time to the startTime specified
		currentTime = startTime;

		// get a reference to the GameManager component for use by other scripts
		if (gm == null) 
			gm = this.gameObject.GetComponent<GameManager>();

		// init scoreboard to 0
		mainScoreDisplay.text = "0";

		// inactivate the gameOverScoreOutline gameObject, if it is set
		if (gameOverScoreOutline)
			gameOverScoreOutline.SetActive (false);

		// inactivate the playAgainButtons gameObject, if it is set
		if (playAgainButtons)
			playAgainButtons.SetActive (false);

		// inactivate the nextLevelButtons gameObject, if it is set
		if (nextLevelButtons)
			nextLevelButtons.SetActive (false);


		playerLifeValue.text = ""+player.GetComponent<Health> ().numberOfLives;
	}

	// this is the main game event loop
	void Update () {
		if (!gameIsOver) {
			if (canBeatLevel && (score >= beatLevelScore)) {  // check to see if beat game
				BeatLevel ();
			} else if (currentTime < 0) { // check to see if timer has run out
				EndGame ();
			} else { // game playing state, so update the timer
				currentTime -= Time.deltaTime;
				mainTimerDisplay.text = currentTime.ToString ("0.00");				
			}
		} else {
			if (nextLevelToLoad == "YOUWIN") {
				if (youwin)
					youwin.SetActive (true);
			}
		}
	}

	void EndGame() {
		// game is over
		gameIsOver = true;

		// repurpose the timer to display a message to the player
		mainTimerDisplay.text = "GAME OVER";

		// activate the gameOverScoreOutline gameObject, if it is set 
		if (gameOverScoreOutline)
			gameOverScoreOutline.SetActive (true);
	
		// activate the playAgainButtons gameObject, if it is set 
		if (playAgainButtons)
			playAgainButtons.SetActive (true);

		// reduce the pitch of the background music, if it is set 
		if (musicAudioSource)
			musicAudioSource.pitch = 0.5f; // slow down the music
	}
	
	void BeatLevel() {
		// game is over
		gameIsOver = true;

		// repurpose the timer to display a message to the player
		mainTimerDisplay.text = "LEVEL COMPLETE";

		// activate the gameOverScoreOutline gameObject, if it is set 
		if (gameOverScoreOutline)
			gameOverScoreOutline.SetActive (true);

		// activate the nextLevelButtons gameObject, if it is set 
		if (nextLevelButtons)
			nextLevelButtons.SetActive (true);
		
		// reduce the pitch of the background music, if it is set 
		if (musicAudioSource)
			musicAudioSource.pitch = 0.5f; // slow down the music
	}

	// public function that can be called to update the score or time
	public void targetHit (TargetBehavior tb, int scoreAmount, float timeAmount)
	{
		Shooter s = player.GetComponentInChildren<Shooter> ();
		// increase the score by the scoreAmount and update the text UI
		score += scoreAmount;
		if (score >= 0 && score <= 30) {
			s.setArmor (1);

			
			Debug.Log ("Armor 1");
		} else if (score > 30 && score <= 100) {
			s.setArmor (2);
			Debug.Log ("Armor 2");
			// set the current time to the startTime specified
			currentTime = startTime;
		
			this.nextLevelToLoad = "Level2";
			if (level2Labels)
				level2Labels.SetActive (true);
		} else if (score > 100 && score <= 200) {
			s.setArmor (3);
			Debug.Log ("Armor 3");
			// set the current time to the startTime specified
			currentTime = startTime;

			this.nextLevelToLoad ="Level3";
			if (level3Labels)
				level3Labels.SetActive (true);
		} else if (score > 200 && score <= 300) {
			s.setArmor (4);
			Debug.Log ("Armor 4");
			// set the current time to the startTime specified
			currentTime = startTime;

			this.nextLevelToLoad ="Level4";
			if (level4Labels)
				level4Labels.SetActive (true);
			if (enemies)
				enemies.SetActive (true);
			if (boss)
				boss.SetActive (true);

			if (bossLife)
				bossLife.enabled = true;
			if (bossLifeValue)
				bossLifeValue.enabled = true;

		} else if (score > 360){
			s.setArmor (5);
			this.nextLevelToLoad = "YOUWIN";
			Debug.Log ("You Win");
			gameIsOver = true;
			// set the current time to the startTime specified
			//currentTime = startTime;
		}
		mainScoreDisplay.text = score.ToString ();
		
		// increase the time by the timeAmount
		currentTime += timeAmount;
		
		// don't let it go negative
		if (currentTime < 0)
			currentTime = 0.0f;

		// update the text UI
		mainTimerDisplay.text = currentTime.ToString ("0.00");
	}

	// public function that can be called to restart the game
	public void RestartGame ()
	{
		// we are just loading a scene (or reloading this scene)
		// which is an easy way to restart the level
		Application.LoadLevel (playAgainLevelToLoad);
	}

	// public function that can be called to go to the next level of the game
	public void NextLevel ()
	{
		// we are just loading the specified next level (scene)
		Application.LoadLevel (nextLevelToLoad);
	}
	// public function that can be called to go to the next level of the game
	public void NextLevel (string nextLevelToLoad)
	{
		this.nextLevelToLoad = nextLevelToLoad;
		Application.LoadLevel (nextLevelToLoad);
	}
	public void Collect(int amount){
		score += amount;
		if (canBeatLevel) {
			mainScoreDisplay.text = score.ToString () + " of " + beatLevelScore.ToString ();

		} else {
			mainScoreDisplay.text = score.ToString ();
		}
	}

}
