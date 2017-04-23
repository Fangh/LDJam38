using UnityEngine;
using System.Collections;

public class CottonBudTool : BaseTool 
{
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
