using UnityEngine;
using System.Collections;

public class BaseTool : MonoBehaviour 
{
	public GameObject PrefabToDrop;
	public float lifeTime = 5.0f;

	bool isDropped = false;

	public virtual void Init( int variant = 0)
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!isDropped)
		{
			Vector3 mousePos = Input.mousePosition;
			mousePos.z = Camera.main.transform.position.y;
			transform.position =transform.position = Camera.main.ScreenToWorldPoint(mousePos);

			if (Input.GetMouseButtonDown(0))
				Action();
		}
	}

	public virtual void Action( )
	{
		GetComponent<Animator>().SetTrigger("Action");
		isDropped = true;
		if (lifeTime > 0)
			Destroy(gameObject, lifeTime);
	}
}