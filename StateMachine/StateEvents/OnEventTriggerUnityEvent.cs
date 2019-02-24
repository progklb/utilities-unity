using UnityEngine;
using UnityEngine.Events;

namespace Utilities.StateMachine.StateEvents
{
	/// <summary>
	/// Provides a generic means of triggering logic through the inspector.
	/// </summary>
	[AddComponentMenu("Utilities/State Machine/State Events/OnEvent Trigger Unity Event")]
	public class OnEventTriggerUnityEvent : OnEventBehaviour
	{
		#region PROPERTIES
		private UnityEvent beginEvent { get => m_BeginEvent; set => m_BeginEvent = value; }
		private UnityEvent endEvent { get => m_EndEvent; set => m_EndEvent = value; }
		#endregion


		#region VARIABLES
		[SerializeField]
		private UnityEvent m_BeginEvent;
		[SerializeField]
		private UnityEvent m_EndEvent;
		#endregion


		#region PUBLIC API
		public override void OnBegin(IState previousState, IState currentState)
		{
			beginEvent.Invoke();
		}

		public override void OnEnd(IState currentState, IState nextState)
		{
			endEvent.Invoke();
		}
		#endregion
	}
}