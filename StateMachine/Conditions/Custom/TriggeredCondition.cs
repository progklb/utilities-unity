namespace Utilities.StateMachine.Conditions
{
	/// <summary>
	/// A condition that waits to be satisfied by an external source.
	/// </summary>
	public class TriggeredCondition : ConditionBehaviour
	{
		#region PUBLIC API
		public void Trigger()
		{
			isSatisfied = true;
		}
		#endregion
	}
}