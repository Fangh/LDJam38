using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CottonBudTool : BaseTool 
{
	public AudioClip	SFX_crash;

	float currentLifeTime = 0f;

	void Start()
	{
		AnimatorReceiveEvent.OnReceiveEvent += ToggleCollider;
	}

	void OnDestroy()
	{
		AnimatorReceiveEvent.OnReceiveEvent -= ToggleCollider;
	}

	public override void Action()
	{
		GetComponent<AudioSource> ().PlayOneShot (SFX_crash);
		GetComponent<Animator>().SetTrigger("Action");
	}

	void ToggleCollider()
	{
		GetComponent<Collider>().enabled = !GetComponent<Collider>().enabled;		
	}

	public override void Update()
	{
		base.Update();		
	}

	void OnDrawGizmos()
	{
		if(GetComponent<Collider>().enabled)
			Gizmos.DrawSphere(transform.position, 1f);
	}
}
