using UnityEngine;
using System.Collections;

public class MicrobCount : MonoBehaviour
{	
	public static MicrobCount Instance = null;
	public int NBMicrob = 0;
	public int NbMicrobBirth = -2;

	void Awake()
	{
		Instance = this;
	}

	public void AddMicrob()
	{
		NBMicrob++;
		NbMicrobBirth++;
	}

	public void RemoveMicrob()
	{
		NBMicrob--;		
	}
}
