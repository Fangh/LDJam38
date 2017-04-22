using UnityEngine;
using System.Collections;

public class CottonBudTool : BaseTool 
{

	void Action()
	{
		base.Action();
		GetComponent<Collider>().enabled = true;
	}
}
