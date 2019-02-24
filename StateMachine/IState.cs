using System;

namespace Utilities.StateMachine
{
	/// <summary>
	/// Interface for defining states for the <see cref="StateMachine"/>.
	/// </summary>
	public interface IState
	{
		#region PROPERTIES
		Guid id { get; }
		StateMachine stateMachine { get; }

		bool isInitialised { get; }
		#endregion


		#region PUBLIC API
		void Initialise(StateMachine stateMachine, Guid id);

		void OnBegin(IState previousState);
		void OnUpdate();
		void OnEnd(IState nextState);

		void OnEvaluate();
		void OnReset();
		#endregion
	}
}