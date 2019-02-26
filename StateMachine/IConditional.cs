namespace Utilities.StateMachine
{
	/// <summary>
	/// Interface for defining conditionals that determine whether or not a state should transition.
	/// </summary>
	public interface IConditional
	{
		#region PROPERTIES
		bool isSatisfied { get; }

		ICondition[] conditions { get; }
		#endregion


		#region PUBLIC API
		void Initialise();

		void OnBegin();
		void OnEnd();

		void UpdateConditions();
		void ResetConditions();
		#endregion
	}
}