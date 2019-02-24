namespace Utilities.StateMachine
{
	/// <summary>
	/// Interface for defining conditionals.
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