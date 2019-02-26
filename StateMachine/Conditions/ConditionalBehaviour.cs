using UnityEngine;

namespace Utilities.StateMachine.Conditions
{
	/// <summary>
	/// A basic conditional implementation that will monitor multiple <see cref="ICondition"/>s for satisfaction.
	/// </summary>
	[DisallowMultipleComponent]
	[AddComponentMenu("Utilities/State Machine/Conditions/Conditional")]
	public class ConditionalBehaviour : MonoBehaviour, IConditional
	{
		#region TYPES
		public enum SatisfiedCondition
		{
			Any,
			All
		}
		#endregion


		#region PROPERTIES
		public bool isSatisfied { get; private set; }

		public ICondition[] conditions { get; private set; }

		/// The mode of satisfaction that will be used during evaluation.
		public SatisfiedCondition satisfiedCondition { get => m_SatisfiedCondition; set => m_SatisfiedCondition = value; }
		#endregion


		#region VARILABLES
		[SerializeField]
		private SatisfiedCondition m_SatisfiedCondition;
		#endregion


		#region INTERFACE IMPLEMENTATION - ICondition
		public void Initialise()
		{
			conditions = GetComponents<ICondition>();
		}

		public void OnBegin()
		{
			foreach (var condition in conditions)
			{
				condition.OnBegin();
			}
		}

		public void OnEnd()
		{
			foreach (var condition in conditions)
			{
				condition.OnEnd();
			}
		}

		public void UpdateConditions()
		{
			if (conditions.Length == 0)
			{
				isSatisfied = true;
				return;
			}

			isSatisfied = satisfiedCondition == SatisfiedCondition.Any ? false : true;

			foreach (var condition in conditions)
			{
				condition.OnUpdate();

				if (satisfiedCondition == SatisfiedCondition.Any)
				{
					isSatisfied |= condition.isSatisfied;
				}
				else
				{
					isSatisfied &= condition.isSatisfied;
				}
			}
		}

		public void ResetConditions()
		{
			isSatisfied = false;

			foreach (var condition in conditions)
			{
				condition.ResetCondition();
			}
		}
		#endregion
	}
}