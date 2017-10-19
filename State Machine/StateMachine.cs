using UnityEngine;
using System.Collections.Generic;

namespace Utilities.StateMachine
{
	/// <summary>
	/// A simple state machine that relies on the states themselves to indicate when to move to the next state.
	/// </summary>
	public class StateMachine : MonoBehaviour 
	{
		#region VARIABLES
		public State m_InitialState;

		private State m_CurrentState;
		private Dictionary<int, State> m_AllStates;
		#endregion


		#region UNITY EVENTS
		void Start()
		{
			FindAllStates();
			SetState(m_InitialState.id);
		}
		#endregion


		#region PUBLIC-API
		public bool SetState(int stateID)
		{
			if (m_AllStates.ContainsKey(stateID))
			{
				if (m_CurrentState != null && stateID != m_CurrentState.id)
				{
					m_CurrentState.gameObject.SetActive(false);
				}

				m_CurrentState = m_AllStates[stateID];
				m_CurrentState.gameObject.SetActive(true);

				return true;
			}

			return false;
		}
		#endregion
	

		#region HELPERS
		/// <summary>
		/// Finds all states under this machine and stores a reference to them. Disables all states found.
		/// </summary>
		void FindAllStates()
		{
			m_AllStates = new Dictionary<int, State>();

			int id = 0;
			foreach (var state in GetComponentsInChildren<State>())
			{
				state.id = id++;
				state.stateMachine = this;
				m_AllStates.Add(state.id, state);

				state.gameObject.SetActive(false);
			}
		}
		#endregion
	}
}