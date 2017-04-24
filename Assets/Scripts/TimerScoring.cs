using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class TimerScoring : MonoBehaviour 
{
	float time;
	public string formatedTime;
	public static TimerScoring Instance = null;

	void Start()
	{
		Instance = this;
	}

	// Update is called once per frame
	void Update () 
	{
		if (GameManager.Instance.currentLife <= 0)
			return;
			
		//Set Timer
		time = Time.time;

		TimeSpan timeSpan = TimeSpan.FromSeconds (time);
		formatedTime =  string.Format ("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
		string timeText = "Lifespan : " + formatedTime;
		GetComponent<Text> ().text = timeText;
		
	}
}