using UnityEngine;
using System.Collections;

public class CottonBudTool : BaseTool 
{

	public override void Action()
	{
		base.Action();
		GetComponent<Collider>().enabled = true;
	}
}
