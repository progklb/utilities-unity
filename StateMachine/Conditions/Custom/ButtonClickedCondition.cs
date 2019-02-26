using UnityEngine;
using UnityEngine.UI;

namespace Utilities.StateMachine.Conditions
{
	/// <summary>
	/// A condition that listens for a button click, or can be triggered by a button click.
	/// </summary>
	[AddComponentMenu("Utilities/State Machine/Conditions/Button Clicked Condition")]
	public class ButtonClickedCondition : TriggeredCondition
	{
		#region PROPERTIES
		public Button targetButton { get => m_TargetButton; set => m_TargetButton = value; }
		/// Whether this condition should subscribed to the target button in order to listen for events.
		public bool selfRegister { get => m_SelfRegister; set => m_SelfRegister = value; }
		#endregion


		#region VARIABLES
		[SerializeField]
		private Button m_TargetButton;
		[SerializeField]
		private bool m_SelfRegister;
		#endregion


		#region INTERFACE IMPLEMENTATION - ICondition
		public override void OnBegin()
		{
			if (selfRegister)
			{
				targetButton.onClick.AddListener(Trigger);
			}
		}

		public override void OnEnd()
		{
			if (selfRegister)
			{
				targetButton.onClick.RemoveListener(Trigger);
			}
		}
		#endregion
	}
}