using UnityEngine;
using System.Collections;

public class PipetteTool : BaseTool 
{

	void Action()
	{
		base.Action();
		Instantiate(PrefabToDrop, transform.position, Quaternion.identity);
	}
}
