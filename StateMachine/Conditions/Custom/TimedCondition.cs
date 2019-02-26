using UnityEngine;

namespace Utilities.StateMachine.Conditions
{
	/// <summary>
	/// A timed condition that becomes satisfied after a timeout.
	/// </summary>
	[AddComponentMenu("Utilities/State Machine/Conditions/Timed Condition")]
	public class TimedCondition : ConditionBehaviour
	{
		#region PROPERTIES
		public float timeout { get => m_Timeout; private set => m_Timeout = value; }

		private float timeoutCounter { get; set; }
		#endregion


		#region VARIABLES
		[SerializeField]
		private float m_Timeout;
		#endregion


		#region PUBLIC API
		public override void OnUpdate()
		{
			timeoutCounter -= Time.deltaTime;

			if (timeoutCounter <= 0f)
			{
				isSatisfied = true;
			}
		}

		public override void ResetCondition()
		{
			base.ResetCondition();

			timeoutCounter = m_Timeout;
		}
		#endregion
	}
}