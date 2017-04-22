using UnityEngine;
using System.Collections;

public class Tool : MonoBehaviour 
{
	public GameObject PrefabToDrop;
	public float lifeTime = 5.0f;

	bool isDropped = false;

	// Use this for initialization
	void Start () 
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
				Drop();
		}
	}

	void Drop()
	{
		GetComponent<Animator>().SetTrigger("DropLiquide");
		GameObject o = Instantiate(PrefabToDrop, transform.position, Quaternion.identity) as GameObject;
		isDropped = true;
		Destroy(gameObject, 1.0f);
	}
}