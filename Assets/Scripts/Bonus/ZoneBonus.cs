using UnityEngine;
using System.Collections;

public class ZoneBonus : MonoBehaviour 
{
	public float lifeTime = 5.0f;

	bool isDropped = false;

	// Use this for initialization
	void Start () 
	{
		GetComponent<Collider>().enabled = false;	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!isDropped)
		{
			Vector3 mousePos = Input.mousePosition;
			mousePos.z = 100;
			transform.position =transform.position = Camera.main.ScreenToWorldPoint(mousePos);

			if (Input.GetMouseButtonDown(0))
				Drop();
		}
	}

	void Drop()
	{
		isDropped =true;
		GetComponent<Collider>().enabled = true;
		Destroy(gameObject, lifeTime);
	}
}