using UnityEngine;
using System.Collections;

namespace Utilities.StateMachine
{
	/// <summary>
	/// Waits in this state for a set amount of time before moving to next state.
	/// </summary>
	public class TimedState : State 
	{
		#region VARIABLES
		public float m_Timeout;
		private float m_TimeAcc;
		#endregion


		#region OVERRIDES
		protected override void OnEnable()
		{
			m_TimeAcc = 0f;
		}

		protected override void Update()
		{
			m_TimeAcc += Time.deltaTime;

			if (m_TimeAcc >= m_Timeout)
			{
				NextState();
			}
		}
		#endregion
	}
}