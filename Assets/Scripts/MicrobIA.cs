﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;


public class MicrobIA : MonoBehaviour 
{
	public GameObject 	microbPrefab = null;
	public GameObject	secondEye = null;
	public GameObject	FX_Hit = null;
	public float 		targetPrecision = 1.0f;
	public bool 		isInfertil = true;
	public float 		infertilDuration = 2f;
	public float		radiusOfMovement = 47.0f;
	public float		timeBeforeReproduceMin = 8f;
	public float		timeBeforeReproduceMax = 13f;
	public float		newBornDuration = 2.0f;
	public bool			isDying = false;
	public bool			isAffectedByGeneticAlteration = false;
	public bool			hadAChild = false;
	public AudioClip	SFX_death;
	public AudioClip	SFX_birth;

	private bool isNewBorn = true;
	private float newBornCurrentTime = 0f;
	private float infertilCurrentTime = 0f;
	private NavMeshAgent agent = null;


	void Awake()
	{
		agent = GetComponent<NavMeshAgent>();
	}

	// Use this for initialization
	void Start () 
	{
		microbPrefab = Resources.Load("Microb") as GameObject;
		HistoryManager.Instance.AddEntry(gameObject.name + " is born\n");
		secondEye.transform.localScale = Vector3.zero;

		agent.enabled = false;
		agent.enabled = true;

		agent.SetDestination(RandomPointOnNavMesh (Vector2.zero, radiusOfMovement));
		infertilDuration = Random.Range(timeBeforeReproduceMin, timeBeforeReproduceMax);
		GameManager.Instance.AddMicrob(this);
	}

	// Update is called once per frame
	void Update () 
	{
		if (isDying)
			return;
		
		if (isNewBorn)
		{
			if (newBornCurrentTime < newBornDuration)
				newBornCurrentTime += Time.deltaTime;
			else
			{
				newBornCurrentTime = 0f;
				isNewBorn = false;
			}
		}
		if ( isInfertil && !isAffectedByGeneticAlteration )
			Grow ();
		else if (!isNewBorn && !isInfertil)
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
		transform.parent.GetComponent<AudioSource> ().PlayOneShot(SFX_birth);

		if (!hadAChild)
			hadAChild = true;
		
		secondEye.transform.localScale = Vector3.one;
		isInfertil = true;
		infertilDuration = Random.Range(timeBeforeReproduceMin, timeBeforeReproduceMax);

		GameObject childGO = GameObject.Instantiate (microbPrefab, Vector3.zero, Quaternion.identity) as GameObject;
		MicrobIA childAgent = childGO.GetComponentInChildren<MicrobIA>();
		childAgent.transform.position = new Vector3 (transform.position.x, 0, transform.position.z);
		childAgent.name = GameManager.Instance.GetRandomName();
		childAgent.transform.localScale = Vector3.zero;
		childAgent.transform.DOScale(Vector3.one, 1f).SetEase(Ease.OutElastic); 

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
		if (other.tag == "MultiplyZone" && !isNewBorn)
		{
			isNewBorn = true;
			Multiply();
		}

		if (other.tag == "KillZone")
		{
			if (other.name.Contains("Disease"))
			{
				other.GetComponent<Disease>().AnimKill();
			}
			Destroy(gameObject);
		}
	}

	void OnTriggerExit( Collider other)
	{
		if (other.tag == "Petri")
		{
			GameObject fx = GameObject.Instantiate(FX_Hit, transform.position, Quaternion.identity) as GameObject;
			fx.transform.LookAt(Vector3.zero);
			fx.GetComponent<ParticleSystem>().collision.SetPlane(0,GameManager.Instance.collisionPlane.transform);
			Destroy(fx, 2f);
			GameManager.Instance.Hit();
		}
	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.blue;
		if (null != agent)
			Gizmos.DrawLine(transform.position, agent.destination);
	}

	void OnDestroy()
	{
		Destroy(transform.parent.gameObject, 2f);
		if (null != GameManager.Instance)
		{
			transform.parent.GetComponent<AudioSource>().PlayOneShot(SFX_death);
			GameManager.Instance.RemoveMicrob(this);
		}
			
		if (null != HistoryManager.Instance)
		{
			string text = string.Format("<color=red>{0} is Dead</color>\n", gameObject.name);
			HistoryManager.Instance.AddEntry(text);
		}
	}

}