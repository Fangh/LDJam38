using UnityEngine;
using System.Collections;

public class FixTransform : MonoBehaviour 
{
	Transform originalTransform;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector3 parentRotation = new Vector3 ( 0, -transform.parent.localEulerAngles.y, 0);
		transform.localRotation = Quaternion.Euler( parentRotation );	
	}
}
