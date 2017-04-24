using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GeneticAlteration : MonoBehaviour 
{
	// Use this for initialization
	void Start () 
	{
		AnimatorReceiveEvent.OnReceiveEvent += DestroyWhenAnimIsFinished;
		transform.position = new Vector3( 0, -0.02f, 0);

		foreach (MicrobIA m in GameManager.Instance.microbsList)
		{
			if (!m.hadAChild)
				continue;
			
			m.isAffectedByGeneticAlteration = true;
			m.isInfertil = true;
			m.transform.GetChild(0).GetChild(0).GetComponent<Renderer>().material.color = GetComponent<Renderer>().material.color;
			m.transform.GetChild(0).GetChild(3).GetComponent<Renderer>().material.color = GetComponent<Renderer>().material.color;
			GameManager.Instance.nbSterilized++;
		}

	}

	void DestroyWhenAnimIsFinished()
	{
		AnimatorReceiveEvent.OnReceiveEvent -= DestroyWhenAnimIsFinished;
		Destroy (gameObject);
	}
}
