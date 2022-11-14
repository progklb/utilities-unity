using UnityEngine;

namespace Utilities.StateMachine.StateEvents
{
	public class OnStateSetAnimatorBool : OnStateBehaviour
	{
		#region VARIABLES
		[SerializeField]
		private Animator m_Animator;
		[SerializeField]
		private string m_AnimationKey;
		[SerializeField]
		private bool m_ValueOnBegin;
		[SerializeField]
		private bool m_ValueOnEnd;
		#endregion


		#region PUBLIC API
		public override void OnBegin(IState previousState, IState currentState)
		{
			if (m_Animator.GetBool(m_AnimationKey) != m_ValueOnBegin)
			{
				m_Animator.SetBool(m_AnimationKey, m_ValueOnBegin);
			}
		}

		public override void OnEnd(IState currentState, IState nextState)
		{
			if (m_Animator.GetBool(m_AnimationKey) != m_ValueOnEnd)
			{
				m_Animator.SetBool(m_AnimationKey, m_ValueOnEnd);
			}
		}
		#endregion
	}
}