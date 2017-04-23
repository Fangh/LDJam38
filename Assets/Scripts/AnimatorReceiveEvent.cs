using UnityEngine;
using System.Collections;

public class AnimatorReceiveEvent : MonoBehaviour 
{
	public delegate void DoAction();
	public static event DoAction OnReceiveEvent;

	public void ReceiveEvent()
	{
		OnReceiveEvent();
	}
}
