using UnityEngine;

using Utilities.StateMachine.Conditions;
using Utilities.StateMachine.StateEvents;
using Utilities.StateMachine.StateLogic;

namespace Utilities.StateMachine.States
{
	/// <summary>
	/// A state with a single next state.
	/// </summary>
	[RequireComponent(typeof(ConditionalBehaviour))]
	[AddComponentMenu("Utilities/State Machine/States/Linear State")]
	public class LinearState : State
	{
		#region PROPERTIES
		/// The state to adavnce to when this state is complete.
		public State nextState { get => m_NextState; set => m_NextState = value; }
		/// Whether this state should shut down the state machine when complete.
		public bool isTerminal { get => m_IsTerminal; set => m_IsTerminal = value; }

		public IConditional conditional { get; private set; }

		public IStateEvent[] stateEvents { get; private set; }
		public IStateLogic[] stateLogics { get; private set; }
		#endregion


		#region VARIABLES
		[SerializeField]
		private State m_NextState;
		[SerializeField]
		private bool m_IsTerminal;
		#endregion


		#region PUBLIC API
		public override void Initialise(StateMachine stateMachine)
		{
			// Cache and initialise all required components.

			conditional = GetComponent<IConditional>();
			conditional.Initialise();

			stateEvents = GetComponents<IStateEvent>();
			foreach (var stateEvent in stateEvents)
			{
				stateEvent.Initialise();
			}

			stateLogics = GetComponents<IStateLogic>();
			foreach (var stateLogic in stateLogics)
			{
				stateLogic.Initialise();
			}

			base.Initialise(stateMachine);
		}

		public override void OnBegin(IState previousState)
		{
			foreach (var stateEvent in stateEvents)
			{
				stateEvent.OnBegin(previousState, this);
			}

			conditional.OnBegin();
		}

		public override void OnUpdate()
		{
			foreach (var stateLogic in stateLogics)
			{
				stateLogic.OnUpdate(this);
			}

			OnEvaluate();
		}

		public override void OnEnd(IState nextState)
		{
			foreach (var stateEvent in stateEvents)
			{
				stateEvent.OnEnd(this, nextState);
			}

			conditional.OnEnd();
		}

		public override void OnEvaluate()
		{
			conditional.UpdateConditions();

			if (conditional.isSatisfied)
			{
				if (isTerminal)
				{
					stateMachine.ShutDown();
				}
				else
				{
					stateMachine.SetState(nextState);
				}
			}
		}

		public override void OnReset()
		{
			conditional.ResetConditions();

			foreach (var stateEvent in stateEvents)
			{
				stateEvent.OnReset();
			}
		}
		#endregion
	}
}