using UnityEngine;
using System.Collections;

public class MicrobIA : MonoBehaviour 
{
	public GameObject microbPrefab;
	public float timeBeforeIsAdult = 2.0f;
	public GameObject target;
	public float targetPrecision = 1.0f;

	private bool isAdult = false;
	private float age = 0f;
	private NavMeshAgent agent;


	// Use this for initialization
	void Start () 
	{
		if (target.transform.localPosition == Vector3.zero)
			target.transform.position = FindRandomDestination ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if ( !isAdult )
			Grow ();
		Move ();
	}

	void Move()
	{
		if (Vector3.Distance (transform.position, target.transform.position) < targetPrecision) 
		{
			target.transform.position = FindRandomDestination ();			
		}
	}

	void Grow()
	{
		if (age < timeBeforeIsAdult)
			age += Time.deltaTime;
		else
			isAdult = true;
	}

	Vector3 FindRandomDestination()
	{
		float x = Random.Range (-50.0f, 50.0f);
		float z = Random.Range (-50.0f, 50.0f);
		return new Vector3 (x, 0, z);
	}

	void OnTriggerEnter( Collider other )
	{
		if (other.tag == "Microb" && isAdult) 
		{
			GameObject.Instantiate (microbPrefab, transform.position, Quaternion.identity);
		}
	}
}