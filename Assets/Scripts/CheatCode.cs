using UnityEngine;
using System.Collections;

public class CheatCode : MonoBehaviour 
{
	public float timescale = 1;

	void Start () 
	{
		string name = "lol";
		Debug.Log (name);
	}

	// Update is called once per frame
	void Update () 
	{
		Time.timeScale = timescale;
	}
}
