using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class TimerScoring : MonoBehaviour 
{
	float time;

	// Update is called once per frame
	void Update () {
			
		//Set Timer
		time = Time.time;

		TimeSpan timeSpan = TimeSpan.FromSeconds (time);
		string timeText = string.Format ("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
		GetComponent<Text> ().text = timeText;
		
	}
}