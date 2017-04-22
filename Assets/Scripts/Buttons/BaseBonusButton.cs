using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BaseBonusButton : MonoBehaviour 
{
	public GameObject toolPrefab;
	public GameObject bonusPrefab;

	public void LaunchSkill()
	{
		GameObject o = Instantiate(toolPrefab, Input.mousePosition, Quaternion.identity) as GameObject;
		o.GetComponentInChildren<Tool>().PrefabToDrop = bonusPrefab;
	}
}
