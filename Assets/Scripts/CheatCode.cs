using UnityEngine;
using System.Collections;

public class CheatCode : MonoBehaviour 
{
	public float timescale = 1;
	
	// Update is called once per frame
	void Update () 
	{
		Time.timeScale = timescale;
	}
}
