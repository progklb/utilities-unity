namespace Utilities.StateMachine
{
	/// <summary>
	/// Interface for defining conditions for a state.
	/// </summary>
	public interface ICondition
	{
		#region PROPERTIES
		bool isSatisfied { get; }
		#endregion


		#region PUBLIC API
		void ResetCondition();

		void OnBegin();
		void OnUpdate();
		void OnEnd();
		#endregion
	}
}