using UnityEngine;

using System;

namespace Utilities.StateMachine.States
{
	/// <summary>
	/// Base class for creating states.
	/// </summary>
	[DisallowMultipleComponent]
	public abstract class State : MonoBehaviour, IState
	{
		#region PROPERTIES
		public Guid id { get; protected set; }
		public StateMachine stateMachine { get; protected set; }

		public bool isInitialised { get; protected set; }
		#endregion


		#region INTERFACE IMPLEMENTATION - IState
		public virtual void Initialise(StateMachine stateMachine, Guid id)
		{
			this.stateMachine = stateMachine;
			this.id = id;

			isInitialised = true;
		}

		public abstract void OnBegin(IState previousState);

		public abstract void OnUpdate();

		public abstract void OnEnd(IState nextState);

		public abstract void OnEvaluate();

		public abstract void OnReset();
		#endregion
	}
}