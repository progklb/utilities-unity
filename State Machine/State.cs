using UnityEngine;
using System.Collections;

namespace Utilities.StateMachine
{
	/// <summary>
	/// Defines a base for deriving states used by the <see cref="StateMachine"/>.
	/// </summary>
	public abstract class State : MonoBehaviour 
	{
		#region PROPERTIES
		public int id { get; set; }
		public StateMachine stateMachine { get; set; }
		#endregion


		#region VARIABLES
		public State m_NextState;
		#endregion


		#region UNITY EVENTS
		protected virtual void OnEnable() { }
		protected virtual void OnDisable() { }
		protected virtual void Update() { }
		#endregion


		#region HELPERS
		public void NextState()
		{
			stateMachine.SetState(m_NextState.id);
		}
		#endregion
	}
}

