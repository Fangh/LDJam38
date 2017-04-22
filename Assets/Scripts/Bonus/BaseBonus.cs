using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BaseBonus : MonoBehaviour 
{
	public void LaunchSkill()
	{
		Color randomHSV = Random.ColorHSV();
		GetComponent<Image>().color = randomHSV;
	}
}
