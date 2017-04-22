using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Bonus : MonoBehaviour 
{
	public void LaunchSkill()
	{
		Color randomHSV = Random.ColorHSV();
		GetComponent<Image>().color = randomHSV;
	}
}
