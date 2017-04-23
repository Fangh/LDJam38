using UnityEngine;
using System.Collections;

public class AnimatorSendEventAtFrame : StateMachineBehaviour 
{
	public enum ChooseState
	{
		Enter,
		Exit,
		SpecificFrame,
	}

	public ChooseState m_choosenState = ChooseState.Exit;
	public float m_frameToSend = 0;

	private bool m_hasSent = false;

//	 OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
	{
		if (m_choosenState == ChooseState.Enter)
		{
			SendEvent (animator);
		}	
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
	{
		if (m_choosenState == ChooseState.SpecificFrame && !m_hasSent)
		{
			// Check if state playing is our specific animation
			AnimatorClipInfo currentClipInfo = new AnimatorClipInfo();

			AnimatorClipInfo[] clipInfos;
			if (animator.IsInTransition (layerIndex))
				clipInfos = animator.GetNextAnimatorClipInfo (layerIndex);
			else
				clipInfos = animator.GetCurrentAnimatorClipInfo (layerIndex);

			if (clipInfos.Length > 0)
			{
				currentClipInfo = clipInfos[0];
			}
			else
				return;

			// Send uscrpt at goo frame
			float timeSpent = stateInfo.normalizedTime * currentClipInfo.clip.length;
			float frame = timeSpent * currentClipInfo.clip.frameRate;
			if (frame >= m_frameToSend)
			{
				m_hasSent = true;
				SendEvent (animator);
			}
		}	
	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
	{
		m_hasSent = false;
		if (m_choosenState == ChooseState.Exit)
		{
			SendEvent (animator);
		}
	}

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	private void SendEvent(Animator animator)
	{
		if (null != animator.GetComponent<AnimatorReceiveEvent>())
			animator.GetComponent<AnimatorReceiveEvent>().ReceiveEvent();
		else
			Debug.LogError("you need a Animator Receive Event on this game object", this);
	}
}
