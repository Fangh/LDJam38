using UnityEngine;
using System.Collections;

public class Disease : MonoBehaviour 
{
	private NavMeshAgent	agent = null;
	public float			radiusOfMovement = 47.0f;
	public float 			targetPrecision = 1.0f;

	void Awake()
	{
		agent = GetComponent<NavMeshAgent>();
	}

	// Use this for initialization
	void Start () 
	{
		agent.enabled = false;
		agent.enabled = true;

		agent.SetDestination(RandomPointOnNavMesh (Vector2.zero, radiusOfMovement));
	}
	
	// Update is called once per frame
	void Update () 
	{
		float distance = Vector3.Distance (transform.position, agent.destination);
		//quand tu es arrivé a la target
		if (distance < targetPrecision) 
		{
			//prend un novueau point sur le navmesh
			agent.SetDestination( RandomPointOnNavMesh (Vector2.zero, radiusOfMovement ));			
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
}
