namespace Utilities.StateMachine.StateEvents
{
	public interface IStateEvent
	{
		#region PUBLIC API
		void Initialise();

		void OnBegin(IState previousState, IState currentState);
		void OnEnd(IState currentState, IState nextState);

		void OnReset();
		#endregion
	}
}