using UnityEngine;

using System;
using System.Collections.Generic;

using UObject = UnityEngine.Object;

namespace Utilities.StateMachine
{
	///<summary>
	/// A modular state machine.
	///</summary>
	[AddComponentMenu("Utilities/State Machine/State Machine")]
	public class StateMachine : MonoBehaviour
	{
		#region PROPERTIES
		/// The currently active state.
		public IState currentState { get; private set; }
		/// A cache of all states known by this state machine.
		public Dictionary<Guid, IState> states { get; private set; }

		/// The startup state for this state machine.
		public IState initialState { get => m_InitialState as IState; set => m_InitialState = value as UObject; }
		/// Whether this state machine should start automatically.
		public bool autoStart { get => m_AutoStart; set => m_AutoStart = value; }
		/// Whether game objects are toggled on/off as states are enabled/disabled.
		public bool toggleActive { get => m_ToggleActive; set => m_ToggleActive = value; }

		/// Whether this state machine has initialised.
		public bool isInitialised { get; private set; }
		/// Whether this state machine is currently running.
		public bool isRunning { get; private set; }
		/// Whether this state machine was instructed to shutdown.
		public bool hasShutdown { get; private set; }
		#endregion


		#region VARIABLES
		[SerializeField]
		private UObject m_InitialState;

		[SerializeField]
		private bool m_AutoStart;
		[SerializeField]
		private bool m_ToggleActive = true;
		#endregion


		#region UNITY EVENTS
		void Start()
		{
			if (!isInitialised)
			{
				Initialise();
			}

			if (autoStart)
			{
				if (m_InitialState != null)
				{
					StartUp();
				}
				else
				{
					Debug.LogError("Cannot start state machine as no initial state has been set.");
				}
			}
		}

		void Update()
		{
			if (isRunning)
			{
				currentState.OnUpdate();
			}
		}

		void OnDisable()
		{
			ShutDown();
		}
		#endregion


		#region PUBLIC API
		public void StartUp()
		{
			if (!isInitialised)
			{
				Initialise();
			}

			if (!isRunning)
			{
				SetState(initialState.id);
				isRunning = true;
			}
			else
			{
				Debug.LogWarning("State machine is already running.");
			}
		}

		public void ShutDown()
		{
			if (currentState != null)
			{
				currentState.OnEnd(null);

				if (toggleActive)
				{
					SetActive(currentState, false);
				}

				currentState = null;

				isRunning = false;
				hasShutdown = true;
			}
		}

		public void SetState(IState state)
		{
			if (state != null)
			{
				SetState(state.id);
			}
			else
			{
				Debug.LogError($"Provided state is null and cannot be set as the active state.");
			}
		}

		public void SetState(Guid id)
		{
			if (states.ContainsKey(id))
			{
				var nextState = states[id];

				if (currentState != null && currentState.id != id)
				{
					currentState.OnEnd(nextState);
					currentState.OnReset();

					SetActive(currentState, false);
				}

				var prevState = currentState;
				currentState = nextState;

				SetActive(currentState, true);

				currentState.OnBegin(prevState);
			}
			else
			{
				Debug.LogError($"Provided state ({id}) is not part of this state machine's states.");
			}
		}

		public void AddState(IState state)
		{
			if (!states.ContainsKey(state.id))
			{
				if (!state.isInitialised)
				{
					state.Initialise(this, Guid.NewGuid());
				}

				states.Add(state.id, state);
			}
			else
			{
				Debug.LogError($"Cannot remove provided state with id {state.id} as it is not part of this state machine's states.");
			}
		}

		public void RemoveState(IState state)
		{
			if (states.ContainsKey(state.id))
			{
				states.Remove(state.id);
			}
			else
			{
				Debug.LogError($"Cannot remove provided state with id {state.id} as it is not part of this state machine's states.");
			}
		}
		#endregion


		#region HELPERS
		/// <summary>
		/// Cache and intialise all states that will be referenced by this state machine.
		/// </summary>
		void Initialise()
		{
			states = new Dictionary<Guid, IState>();

			foreach (var state in GetComponentsInChildren<IState>())
			{
				AddState(state);
				SetActive(state, false);
			}

			isInitialised = true;
		}

		/// <summary>
		/// Sets the active state of the game object of the provided state to the value specified by <paramref name="isActive"/>.
		/// </summary>
		/// <param name="state">The state whose <see cref="GameObject"/> should be toggled.</param>
		/// <param name="isActive">The active state to set the <see cref="GameObject"/> to.</param>
		void SetActive(IState state, bool isActive)
		{
			if (toggleActive && state is MonoBehaviour)
			{
				((MonoBehaviour)state).gameObject.SetActive(isActive);
			}
		}
		#endregion

		// TODO
		// Use manual recursive search and stop if sub-statemachine is found.
		// Branching states
	}
}