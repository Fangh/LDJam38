using UnityEngine;
using System.Collections;

public class MicrobIA : MonoBehaviour 
{
	public GameObject microbPrefab;
	public float timeBeforeIsAdult = 2.0f;
	public GameObject target;
	public float targetPrecision = 1.0f;
	public float pregnancyTime = 0.5f;

	[HideInInspector] 
	public bool isAdult = false;
	public bool isPregnant = false;
	private float age = 0f;
	private float birthTime = 0f;
	private NavMeshAgent agent;


	void Awake()
	{
		agent = GetComponent<NavMeshAgent>();		
	}


	// Use this for initialization
	void Start () 
	{
		if (target.transform.localPosition == Vector3.zero)
			target.transform.position = RandomPointOnNavMesh (Vector2.zero, 47.0f);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if ( !isAdult )
			Grow ();
		if ( isPregnant )
			Procreate();
		
		Move ();
	}

	void Move()
	{
		float distance = Vector3.Distance (transform.position, target.transform.position);
		if (distance < targetPrecision) 
		{
			target.transform.position = RandomPointOnNavMesh (Vector2.zero, 47.0f);			
		} 
		else 
		{
			agent.destination = target.transform.position;
		}
	}

	void Grow()
	{
		if (age < timeBeforeIsAdult)
			age += Time.deltaTime;
		else
			isAdult = true;
	}

	void Procreate()
	{
		if ( birthTime < pregnancyTime )
			birthTime += Time.deltaTime;
		else
		{
			isPregnant = false;
			birthTime = 0f;
			GameObject childGO = GameObject.Instantiate (microbPrefab, Vector3.zero, Quaternion.identity) as GameObject;
			MicrobIA childAgent = childGO.GetComponentInChildren<MicrobIA>();
			childAgent.transform.position = transform.position;
			childAgent.name = "Gérard";
			Debug.Log(agent.gameObject.name + " at pos " + transform.position + " has given birth to " + childAgent.name + " at " + childAgent.transform.position);
		}
	}

	Vector3 RandomPointOnNavMesh(Vector2 center, float range)
	{
		Vector2 randomPoint2D = center + Random.insideUnitCircle * range;
		Vector3 randomPoint = new Vector3(randomPoint2D.x, 0, randomPoint2D.y);
		NavMeshHit hit;
		if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
			return hit.position;
		else
			return Vector3.zero;
	}

	void OnTriggerEnter( Collider other )
	{
		if (other.tag == "Microb" 
			&& isAdult 
			&& !isPregnant
			&& other.GetComponent<MicrobIA>().isAdult 
			&& !other.GetComponent<MicrobIA>().isPregnant
			) 
		{
			isPregnant = true;
		}
	}
}