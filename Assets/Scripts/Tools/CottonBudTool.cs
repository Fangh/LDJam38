using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CottonBudTool : BaseTool 
{
	public AudioClip	SFX_crash;

	public override void Start()
	{
		base.Start();
		AnimatorReceiveEvent.OnReceiveEvent += ToggleCollider;
	}

	void OnDestroy()
	{
		AnimatorReceiveEvent.OnReceiveEvent -= ToggleCollider;
	}

	public override void Action()
	{
		GetComponent<Animator>().SetTrigger("Action");
	}

	void ToggleCollider()
	{
		GetComponent<Collider>().enabled = !GetComponent<Collider>().enabled;

		if (GetComponent<Collider>().enabled)
			GetComponent<AudioSource>().PlayOneShot(SFX_crash);
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
