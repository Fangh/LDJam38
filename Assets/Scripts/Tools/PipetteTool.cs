using UnityEngine;
using System.Collections;

public class PipetteTool : BaseTool 
{
	public Material bonusMat;
	public Material malusMat;
	public Material geneticMat;

	public override void Init( int variant )
	{
		if (variant == 0)
			transform.FindChild("liquide").GetComponent<Renderer>().material = bonusMat;
		else if (variant == 1)
			transform.FindChild("liquide").GetComponent<Renderer>().material = malusMat;
		else if (variant == 2)
			transform.FindChild("liquide").GetComponent<Renderer>().material = geneticMat;
	}

	public override void Action(  )
	{
		base.Action();
		GameObject o = Instantiate(PrefabToDrop, transform.position, Quaternion.identity) as GameObject;
		o.GetComponentInChildren<Renderer>().material = transform.FindChild("liquide").GetComponent<Renderer>().material;
	}
}
