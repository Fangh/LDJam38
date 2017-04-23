using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BaseBonusButton : MonoBehaviour 
{
	public Text unlockText;
	public int priceToUnlock;

	public Text chargeText;
	public int NbCharge = 0;

	public GameObject toolPrefab;
	public GameObject bonusPrefab;

	private int birthCount = 0;

	public virtual void Start()
	{
		birthCount = GameManager.Instance.NbMicrobBirth;
		UpdateCharges();
	}

	public virtual void LaunchSkill( int variant = 0 )
	{
		if (NbCharge == 0)
			return;

		BaseTool currentTool = GameManager.Instance.currentTool;
		if (null != currentTool)
		{
			currentTool.ButtonFrom.NbCharge++;
			Destroy(GameManager.Instance.currentTool.gameObject);
			if (currentTool.name.Contains(toolPrefab.name))
				return;
		}

		GameObject o = Instantiate(toolPrefab, Vector3.zero, Quaternion.identity) as GameObject;
		o.GetComponentInChildren<BaseTool>().PrefabToDrop = bonusPrefab;
		o.GetComponentInChildren<BaseTool>().Init(variant);
		o.GetComponentInChildren<BaseTool>().ButtonFrom = this;
		NbCharge--;
		UpdateCharges();
	}

	public void Update()
	{
		if (priceToUnlock <= 0)
			return;
		
		int birthSinceLastUnlock = GameManager.Instance.NbMicrobBirth - birthCount;

		if (birthSinceLastUnlock < priceToUnlock )
			unlockText.text = "Unlock in "+ (priceToUnlock - birthSinceLastUnlock).ToString();
		else
		{
			unlockText.text = "Unlock!";
			birthCount = GameManager.Instance.NbMicrobBirth;
			NbCharge++;
			UpdateCharges();
		}
	}
		
	public void UpdateCharges()
	{
		if(NbCharge > 0)
			chargeText.text = NbCharge.ToString();
		else if(NbCharge == -1)
			chargeText.text = "Unlimited";
		else if(NbCharge == 0)
			chargeText.text = "0";		
	}
}
