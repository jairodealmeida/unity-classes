using UnityEngine;
using System.Collections;
using System;

public class BossTargetMover : MonoBehaviour {

	//define the possible states through an enumeration
	public enum motionDirections{Spin,Horizontal, Vertical};

	//store the state
	public motionDirections motionState = motionDirections.Horizontal;

	public float spinSpeedy = 180.0f;
	public float motionMagnitude = 0.1f;
	//	public bool doSpin = true;
	//	public bool doMotion = false;
	// Update is called once per frame
	float timeLeft = 30;
	void Update () {
		//		if (doSpin) {
		//			//rotate around the up of axis of the game
		//			gameObject.transform.Rotate (Vector3.up * spinSpeedy * Time.deltaTime);
		//		}
		//		if (doMotion) {
		//			//Move up down overtime
		//			gameObject.transform.Translate (Vector3.up * Mathf.Cos (Time.timeSinceLevelLoad) * motionMagnitude);
		//		}
		timeLeft -= Time.deltaTime;
		if ( timeLeft < 0 )
		{
			
			System.Random random = new System.Random ();
			int enumIndex = random.Next(0,2);
			if (enumIndex == 0) {
				motionState = motionDirections.Horizontal;
			} else if (enumIndex == 1) {
				motionState = motionDirections.Spin;
			} else if (enumIndex == 2) {
				motionState = motionDirections.Vertical;
			}
			timeLeft = 30;
		}
		switch (motionState) {
		case motionDirections.Spin:
			//rotate around the up axis of the gameObject
			gameObject.transform.Rotate(Vector3.up*spinSpeedy*Time.deltaTime);
			break;
			//move up and down over time
		case motionDirections.Horizontal:
			gameObject.transform.Translate (Vector2.right*Mathf.Cos(Time.timeSinceLevelLoad)*motionMagnitude);
			break;
			//move up and down over time
		case motionDirections.Vertical:
			gameObject.transform.Translate (Vector3.up * Mathf.Cos (Time.timeSinceLevelLoad) * motionMagnitude);
			break;
		}
	}
}
