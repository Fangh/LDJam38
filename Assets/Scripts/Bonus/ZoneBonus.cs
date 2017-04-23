using UnityEngine;
using System.Collections;

public class ZoneBonus : MonoBehaviour 
{
	public float lifeTime = 5.0f;

	// Use this for initialization
	void Start () 
	{
		transform.position = new Vector3( transform.position.x, -0.02f, transform.position.z);
		Destroy (gameObject, lifeTime);	
	}
}
