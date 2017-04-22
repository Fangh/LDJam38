using UnityEngine;
using System.Collections;

public class ZoneBonus : MonoBehaviour 
{
	public float lifeTime = 5.0f;

	// Use this for initialization
	void Start () 
	{
		Destroy (gameObject, lifeTime);	
	}
}
