using UnityEditor;

using System.Collections.Generic;

using Utilities.StateMachine.States;

namespace Utilities.StateMachine
{
	[CustomEditor(typeof(LinearState)), CanEditMultipleObjects]
	class LinearStateEditor : Editor
	{
		#region UNITY EVENTS
		public override void OnInspectorGUI()
		{
			var state = target as LinearState;

			EditorGUI.BeginDisabledGroup(true);
			EditorGUILayout.ObjectField("Script", MonoScript.FromMonoBehaviour(state), typeof(MonoScript), false);
			EditorGUI.EndDisabledGroup();

			var excludedProperties = new List<string> {
				"m_Script"
			};

			if (state.isTerminal)
			{
				excludedProperties.Add("m_NextState");
			}

			DrawPropertiesExcluding(serializedObject, excludedProperties.ToArray());
			serializedObject.ApplyModifiedProperties();
		}
		#endregion
	}
}