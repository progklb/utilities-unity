using UnityEngine;
using UnityEditor;

namespace Utilities.EditorScripts
{
	/// <summary>
	/// Allows for adjusting of the game time scale.
	/// </summary>
	public class TimeScalerWindow : EditorWindow
	{
		#region VARIABLES
		private float m_TimeScale;
		#endregion


		#region API
		[MenuItem("ApricotJams/Time Scaler")]
		public static void ShowWindow()
		{
			//Show existing window instance. If one doesn't exist, make one.
			var win = GetWindow(typeof(TimeScalerWindow));
			(win as TimeScalerWindow).m_TimeScale = Time.timeScale;
		}

		void OnGUI()
		{
			EditorGUILayout.BeginVertical();

			// General Params

			EditorGUILayout.LabelField($"Current time scale: {Time.timeScale}");
			m_TimeScale = EditorGUILayout.Slider("Timescale", m_TimeScale, 0.1f, 10f);

			GUILayout.Space(10);

			if (GUILayout.Button("Apply"))
			{
				Time.timeScale = m_TimeScale;
			}

			if (GUILayout.Button("Pause"))
			{
				Time.timeScale = 0f;
			}

			if (GUILayout.Button("Reset"))
			{
				Time.timeScale = m_TimeScale = 1f;
			}

			EditorGUILayout.EndVertical();
		}
		#endregion
	}
}