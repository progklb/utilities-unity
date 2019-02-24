namespace Utilities.StateMachine.StateLogic
{
	public interface IStateLogic
	{
		#region PUBLIC API
		void Initialise();

		void OnUpdate(IState currentState);

		void OnReset();
		#endregion
	}
}