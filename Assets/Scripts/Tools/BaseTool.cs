using UnityEngine;
using System.Collections;

public class BaseTool : MonoBehaviour 
{
	public GameObject PrefabToDrop;
	public float lifeTime = 5.0f;

	public BaseBonusButton ButtonFrom = null;

	bool isDropped = false;

	public virtual void Start()
	{
		name = transform.parent.name;
		GameManager.Instance.currentTool = this;
	}

	public virtual void Init(int variant = 0)
	{
		
	}
	
	// Update is called once per frame
	public virtual void Update () 
	{
		if (!isDropped)
		{
			Vector3 mousePos = Input.mousePosition;
			mousePos.z = Camera.main.transform.position.y;
			transform.parent.position =transform.position = Camera.main.ScreenToWorldPoint(mousePos);

			if (Input.GetMouseButtonDown(0) && Input.mousePosition.x < Screen.width - Screen.width * 0.15)
				Action();
		}
	}

	public virtual void Action( )
	{
		ButtonFrom.UpdateCharges(-1);
		GetComponent<Animator>().SetTrigger("Action");
		isDropped = true;
		if (lifeTime > 0)
			Destroy(gameObject, lifeTime);
	}
}