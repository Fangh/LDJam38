using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BaseBonusButton : MonoBehaviour 
{
	public GameObject toolPrefab;
	public GameObject bonusPrefab;

	public virtual void LaunchSkill( int variant = 0 )
	{
		GameObject o = Instantiate(toolPrefab, Input.mousePosition, Quaternion.identity) as GameObject;
		o.GetComponentInChildren<BaseTool>().PrefabToDrop = bonusPrefab;
		o.GetComponentInChildren<BaseTool>().Init(variant);
	}
}
