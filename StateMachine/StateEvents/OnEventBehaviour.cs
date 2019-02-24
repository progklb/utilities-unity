using UnityEngine;

namespace Utilities.StateMachine.StateEvents
{
	/// <summary>
	/// Base class for defining state events.
	/// </summary>
	public abstract class OnEventBehaviour : MonoBehaviour, IStateEvent
	{
		#region PUBLIC API
		public virtual void Initialise() { }

		public virtual void OnBegin(IState previousState, IState currentState) { }

		public virtual void OnEnd(IState currentState, IState nextState) { }

		public virtual void OnReset() { }
		#endregion
	}
}