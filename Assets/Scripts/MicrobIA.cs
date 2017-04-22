using UnityEngine;
using System.Collections;

public class MicrobIA : MonoBehaviour 
{
	public bool 		primordialMicrob = false;
	public GameObject 	microbPrefab;
	public float 		targetPrecision = 1.0f;
	public bool 		isInfertil = true;
	public float 		infertilDuration = 2f;
	public float		radiusOfMovement = 47.0f;

	private float infertilCurrentTime = 0f;
	private NavMeshAgent agent;


	void Awake()
	{
		agent = GetComponent<NavMeshAgent>();
	}


	// Use this for initialization
	void Start () 
	{
		//first microbs don't go on a random point but forward first
		if (primordialMicrob)
		{
			agent.SetDestination(Vector3.zero);
		}
		else
			agent.SetDestination(RandomPointOnNavMesh (Vector2.zero, radiusOfMovement));
	}
	
	// Update is called once per frame
	void Update () 
	{
		if ( isInfertil )
			Grow ();
		
		Move ();
	}

	void Move()
	{
		float distance = Vector3.Distance (transform.position, agent.destination);
		//quand tu es arrivé a la target
		if (distance < targetPrecision) 
		{
			//prend un novueau point sur le navmesh
			agent.SetDestination( RandomPointOnNavMesh (Vector2.zero, radiusOfMovement ));			
		}
	}

	void Grow()
	{
		if (infertilCurrentTime < infertilDuration)
			infertilCurrentTime += Time.deltaTime;
		else
			isInfertil = false;
	}

	void Procreate()
	{
		GameObject childGO = GameObject.Instantiate (microbPrefab, Vector3.zero, Quaternion.identity) as GameObject;
		MicrobIA childAgent = childGO.GetComponentInChildren<MicrobIA>();
		childAgent.transform.position = transform.position;
		childAgent.name = "Gérard";
		Debug.Log(agent.gameObject.name + " at pos " + transform.position + " has given birth to " + childAgent.name + " at " + childAgent.transform.position);
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
		//procreation if touch another
		if (other.tag == "Microb" && !isInfertil && !other.GetComponent<MicrobIA>().isInfertil )
		{
			isInfertil = true;
			other.GetComponent<MicrobIA>().isInfertil = true;
			Procreate();
		}

		//BONUS ZONE
		if (other.tag == "ZoneBonus")
		{
			isInfertil = true;
			Procreate();
		}
	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.blue;
		if (null != agent)
			Gizmos.DrawLine(transform.position, agent.destination);
	}

}