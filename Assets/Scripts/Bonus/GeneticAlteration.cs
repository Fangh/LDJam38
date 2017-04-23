using UnityEngine;
using System.Collections;

public class GeneticAlteration : MonoBehaviour 
{
	// Use this for initialization
	void Start () 
	{
		AnimatorReceiveEvent.OnReceiveEvent += DestroyWhenAnimIsFinished;
		transform.position = new Vector3( 0, -0.02f, 0);

		foreach (MicrobIA m in GameManager.Instance.microbsList)
		{
			m.isAffectedByGeneticAlteration = true;
			m.isInfertil = true;
		}

	}

	void DestroyWhenAnimIsFinished()
	{
		Debug.Log("DESTROY");
		AnimatorReceiveEvent.OnReceiveEvent -= DestroyWhenAnimIsFinished;
		Destroy (gameObject);
	}
}
