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
		birthCount = MicrobCount.Instance.NbMicrobBirth;
		UpdateCharges();
	}

	public virtual void LaunchSkill( int variant = 0 )
	{
		if (NbCharge == 0)
			return;
		GameObject o = Instantiate(toolPrefab, Input.mousePosition, Quaternion.identity) as GameObject;
		o.GetComponentInChildren<BaseTool>().PrefabToDrop = bonusPrefab;
		o.GetComponentInChildren<BaseTool>().Init(variant);
		NbCharge--;
		UpdateCharges();
	}

	public void Update()
	{
		int birthSinceLastUnlock = MicrobCount.Instance.NbMicrobBirth - birthCount;

		if (birthSinceLastUnlock < priceToUnlock )
			unlockText.text = "Unlock in "+ (priceToUnlock - birthSinceLastUnlock).ToString();
		else
		{
			unlockText.text = "Unlock!";
			birthCount = MicrobCount.Instance.NbMicrobBirth;
			NbCharge++;
			UpdateCharges();
		}
	}
		
	public void UpdateCharges()
	{
		if(NbCharge > 0)
			chargeText.text = "Charges : "+NbCharge;
		else if(NbCharge == -1)
			chargeText.text = "Unlimited";
		else if(NbCharge == 0)
			chargeText.text = "No Charge Left";		
	}
}
