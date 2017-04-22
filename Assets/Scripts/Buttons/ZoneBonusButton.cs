using UnityEngine;
using System.Collections;

public class ZoneBonusButton : BaseBonusButton 
{
	public void LaunchSkill()
	{
		base.LaunchSkill();
		GameObject o = Instantiate(toolPrefab, Input.mousePosition, Quaternion.identity) as GameObject;
		o.GetComponentInChildren<Tool>().PrefabToDrop = bonusPrefab;
	}
}
