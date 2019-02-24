using UnityEngine;

namespace Utilities.StateMachine.Conditions
{
	/// <summary>
	/// Base class for defining conditions for states.
	/// </summary>
	public abstract class ConditionBehaviour : MonoBehaviour, ICondition
	{
		#region PROPERTIES
		public bool isSatisfied { get; protected set; }
		#endregion


		#region INTERFACE IMPLEMENTATION - ICondition
		public virtual void OnBegin()
		{
		}

		public virtual void OnUpdate()
		{
		}

		public virtual void OnEnd()
		{
		}

		public virtual void ResetCondition()
		{
			isSatisfied = false;
		}
		#endregion
	}
}