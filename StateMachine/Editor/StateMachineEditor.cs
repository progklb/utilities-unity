using UnityEngine;
using UnityEditor;

using System.Collections.Generic;
using System.Reflection;

namespace Utilities.StateMachine
{
	[CustomEditor(typeof(StateMachine)), CanEditMultipleObjects]
	class StateMachineEditor : Editor
	{
		#region UNITY EVENTS
		public override void OnInspectorGUI()
		{
			var stateMachine = target as StateMachine;

			EditorGUI.BeginDisabledGroup(true);
			EditorGUILayout.ObjectField("Script", MonoScript.FromMonoBehaviour(stateMachine), typeof(MonoScript), false);
			EditorGUI.EndDisabledGroup();

			var excludedProperties = new List<string> {
				"m_Script"
			};

			CheckInitialStateType(stateMachine);

			DrawPropertiesExcluding(serializedObject, excludedProperties.ToArray());
			serializedObject.ApplyModifiedProperties();
		}
		#endregion


		#region HELPER FUNCTIONS
		/// <summary>
		/// This method checks that we have an IState value assigned to the <see cref="StateMachine"/>'s private inspector field.
		/// We use reflection to obtain the field value and perform type checks.
		/// </summary>
		/// <param name="stateMachine">State machine instance to check for assigned initial state.</param>
		void CheckInitialStateType(StateMachine stateMachine)
		{
			var initialStateField = stateMachine.GetType().GetField("m_InitialState", BindingFlags.Instance | BindingFlags.NonPublic);
			var initialStateValue = initialStateField.GetValue(stateMachine);

			// If this field as assigned, check that the correct type is assigned.
			if (initialStateValue?.GetType() != null)
			{
				// If an IState component is not assigned,
				if (!(stateMachine.initialState is IState))
				{
					// Check if it is a game object and assign the first available IState component if possible.
					if (initialStateValue is GameObject)
					{
						var states = ((GameObject)initialStateValue).GetComponents<IState>();
						stateMachine.initialState = states.Length > 0 ? states[0] : null;
					}
					// Otherwise an unsupported type is assigned and we unassign it.
					else
					{
						stateMachine.initialState = null;
					}
				}
			}
		}
		#endregion
	}
}