using UnityEngine;
using System.Collections;

public class MicrobIA : MonoBehaviour 
{
	public GameObject 	microbPrefab;
	public GameObject	secondEye;
	public float 		targetPrecision = 1.0f;
	public bool 		isInfertil = true;
	public float 		infertilDuration = 2f;
	public float		radiusOfMovement = 47.0f;
	public float		timeBeforeReproduceMin = 8f;
	public float		timeBeforeReproduceMax = 13f;

	private float infertilCurrentTime = 0f;
	private NavMeshAgent agent;


	void Awake()
	{
		agent = GetComponent<NavMeshAgent>();
	}


	// Use this for initialization
	void Start () 
	{
		secondEye.transform.localScale = Vector3.zero;
		agent.enabled = false;
		agent.enabled = true;
		agent.SetDestination(RandomPointOnNavMesh (Vector2.zero, radiusOfMovement));
		infertilDuration = Random.Range(timeBeforeReproduceMin, timeBeforeReproduceMax);
		MicrobCount.NBMicrob++;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if ( isInfertil )
			Grow ();
		else
			Multiply();
		
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
		{
			infertilCurrentTime += Time.deltaTime;
			float size = infertilCurrentTime / infertilDuration;
			secondEye.transform.localScale = new Vector3 (size, size, size);
		}
		else
		{
			infertilCurrentTime = 0f;
			isInfertil = false;
		}
	}

	void Multiply()
	{
		secondEye.transform.localScale = Vector3.one;
		isInfertil = true;
		infertilDuration = Random.Range(timeBeforeReproduceMin, timeBeforeReproduceMax);

		GameObject childGO = GameObject.Instantiate (microbPrefab, Vector3.zero, Quaternion.identity) as GameObject;
		MicrobIA childAgent = childGO.GetComponentInChildren<MicrobIA>();
		childAgent.transform.position = new Vector3 (transform.position.x, 0, transform.position.z);
		childAgent.name = "Gérard";

		//Debug.Log(agent.gameObject.name + " at pos " + transform.position + " has given birth to " + childAgent.name + " at " + childAgent.transform.position);
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
		//BONUS ZONE
		if (other.tag == "MultiplyZone")
		{
			Multiply();
		}

		if (other.tag == "KillZone")
		{
			Destroy(gameObject);
		}
	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.blue;
		if (null != agent)
			Gizmos.DrawLine(transform.position, agent.destination);
	}

}