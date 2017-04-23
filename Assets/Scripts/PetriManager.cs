using UnityEngine;
using System.Collections;

public class PetriManager : MonoBehaviour 
{
	public int life = 10000;
	public static PetriManager Instance = null;

	private int currentLife = 0;

	// Use this for initialization
	void Start () 
	{
		Instance = this;
		currentLife = life;	
	}

	public void Hit()
	{
		currentLife--;
		if (currentLife <=0)
		{
			Debug.Log("GAME OVER");
		}
		else
		{
			Debug.Log("Current life of petri = " + currentLife);
		}
	}
}
