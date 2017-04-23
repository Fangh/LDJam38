using UnityEngine;
using System.Collections;

public class MicrobCount : MonoBehaviour
{	
	public static MicrobCount Instance = null;
	public int NBMicrob = 0;
	public int NbMicrobBirth = 0;

	public void Birth()
	{
		NBMicrob++;
		NbMicrobBirth++;
	}

	public void Death()
	{
		NBMicrob--;		
	}
}
