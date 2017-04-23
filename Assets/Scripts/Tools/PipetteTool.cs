using UnityEngine;
using System.Collections;

public class PipetteTool : BaseTool 
{
	public Material bonusMat;
	public Material malusMat;
	public Material geneticMat;
	public Material diseaseMat;

	private bool disease = false;

	public override void Start()
	{
		base.Start();
	}

	public override void Init( int variant )
	{
		if (variant == 0)
			transform.FindChild("liquide").GetComponent<Renderer>().material = bonusMat;
		else if (variant == 1)
			transform.FindChild("liquide").GetComponent<Renderer>().material = malusMat;
		else if (variant == 2)
			transform.FindChild("liquide").GetComponent<Renderer>().material = geneticMat;
		else if (variant == 3)
		{
			transform.FindChild("liquide").GetComponent<Renderer>().material = diseaseMat;
			disease = true;
		}
	}

	public override void Action(  )
	{
		base.Action();
		GameObject o = Instantiate(PrefabToDrop, transform.position, Quaternion.identity) as GameObject;
		if (!disease)
			o.GetComponentInChildren<Renderer>().material = transform.FindChild("liquide").GetComponent<Renderer>().material;
	}
}
