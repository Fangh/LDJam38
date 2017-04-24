using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class BaseBonusButton : MonoBehaviour 
{
	public Text unlockText;
	public int priceToUnlock;
	public GameObject helpPanel = null;

	public Text chargeText;
	public int NbCharge = 0;

	public GameObject toolPrefab;
	public GameObject bonusPrefab;
	public AudioClip	SFX_button;

	private int birthCount = 0;

	public virtual void Start()
	{
		helpPanel.SetActive(false);
		birthCount = GameManager.Instance.NbMicrobBirth;
		UpdateCharges(0);
	}

	public virtual void LaunchSkill( int variant = 0 )
	{
		BaseTool currentTool = GameManager.Instance.currentTool;
		GetComponent<AudioSource> ().PlayOneShot (SFX_button);

		if (NbCharge == 0)
			return;

		if (null != currentTool)
		{
			Destroy(GameManager.Instance.currentTool.gameObject);
			if (currentTool.name.Contains(toolPrefab.name))
				return;
		}

		GameObject o = Instantiate(toolPrefab, Vector3.zero, Quaternion.identity) as GameObject;
		o.GetComponentInChildren<BaseTool>().PrefabToDrop = bonusPrefab;
		o.GetComponentInChildren<BaseTool>().Init(variant);
		o.GetComponentInChildren<BaseTool>().ButtonFrom = this;
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
			UpdateCharges(1);
		}
	}
		
	public void UpdateCharges(int howMany)
	{
		NbCharge += howMany;

		GetComponent<Button>().interactable = true;
		if (NbCharge > 0)
			chargeText.text = NbCharge.ToString();
		else if(NbCharge == -1)
			chargeText.text = "∞";
		else if(NbCharge == 0)
		{
			chargeText.text = "0";
			GetComponent<Button>().interactable = false;
		}
	}

	public void ToggleHelp(bool active)
	{
		helpPanel.SetActive(active);
	}
}
