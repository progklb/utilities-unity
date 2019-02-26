using UnityEngine;

namespace Utilities.StateMachine.StateLogic
{
	public abstract class OnUpdateBehaviour : MonoBehaviour, IStateLogic
	{
		#region PUBLIC API
		public virtual void Initialise() { }

		public abstract void OnUpdate(IState currentState);

		public virtual void OnReset() { }
		#endregion
	}
}