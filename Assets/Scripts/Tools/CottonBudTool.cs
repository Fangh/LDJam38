using UnityEngine;
using System.Collections;

public class CottonBudTool : BaseTool 
{
	float currentLifeTime = 0f;

	public override void Action()
	{
		GetComponent<Animator>().SetTrigger("Action");
		GetComponent<Collider>().enabled = true;
		currentLifeTime = lifeTime;	
	}

	public override void Update()
	{
		base.Update();
		if (!GetComponent<Collider>().enabled)
			return;
		
		if (currentLifeTime > lifeTime)
		{
			currentLifeTime = 0f;
			GetComponent<Collider>().enabled = false;			
		}
		else
			currentLifeTime += Time.deltaTime;
		
	}
}
