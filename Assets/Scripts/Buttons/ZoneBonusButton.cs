using UnityEngine;
using System.Collections;

public class ZoneBonusButton : BaseBonusButton 
{
	public GameObject zonePrefab;

	public void LaunchSkill()
	{
		base.LaunchSkill();
		Instantiate(zonePrefab, Input.mousePosition, Quaternion.identity);
	}
}
