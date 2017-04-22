using UnityEngine;
using System.Collections;

public class PipetteTool : BaseTool 
{
	public Material bonusMat;
	public Material malusMat;

	public override void Init( int variant )
	{
		if (variant == 0)
			transform.FindChild("liquide").GetComponent<Renderer>().material = bonusMat;
		else
			transform.FindChild("liquide").GetComponent<Renderer>().material = malusMat;		
	}

	public override void Action(  )
	{
		base.Action();
		GameObject o = Instantiate(PrefabToDrop, transform.position, Quaternion.identity) as GameObject;
		o.GetComponentInChildren<Renderer>().material = transform.FindChild("liquide").GetComponent<Renderer>().material;
	}
}
