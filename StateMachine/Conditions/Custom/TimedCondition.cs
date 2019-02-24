using UnityEngine;

namespace Utilities.StateMachine.Conditions
{
	/// <summary>
	/// A timed condition that becomes satisfied after a timeout.
	/// </summary>
	public class TimedCondition : ConditionBehaviour
	{
		#region PROPERTIES
		private float timeout { get; set; }
		#endregion


		#region VARIABLES
		[SerializeField]
		private float m_Timeout;
		#endregion


		#region PUBLIC API
		public override void OnUpdate()
		{
			timeout -= Time.deltaTime;

			if (timeout <= 0f)
			{
				isSatisfied = true;
			}
		}

		public override void ResetCondition()
		{
			base.ResetCondition();

			timeout = m_Timeout;
		}
		#endregion
	}
}