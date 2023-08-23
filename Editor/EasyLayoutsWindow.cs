using UnityEngine;
using UnityEditor;

namespace Utilities.EditorScripts
{
	/// <summary>
	/// Allows for easy row/grid positioning of a set of selected objects.
	/// </summary>
	public class EasyLayoutsWindow : EditorWindow
	{
		#region TYPES
		enum Mode
		{
			RowCentric,
			ColCentric
		}
		#endregion


		#region VARIABLES
		private Mode m_Mode;
		private int m_LimitPerLine;
		private float m_Offset;

		private bool m_CustomStartGroupEnabled;
		private Vector3 m_CustomRootPos;
		#endregion


		#region API
		[MenuItem("ApricotJams/Easy Layouts")]
		public static void ShowWindow()
		{
			//Show existing window instance. If one doesn't exist, make one.
			GetWindow(typeof(EasyLayoutsWindow));
		}

		void OnGUI()
		{
			EditorGUILayout.BeginVertical();

			GUILayout.Label("Layout Options", EditorStyles.boldLabel);
			GUILayout.Space(10);

			// General Params

			m_Mode = (Mode)EditorGUILayout.EnumPopup("Mode", m_Mode);
			m_LimitPerLine = EditorGUILayout.IntField("Objects Limit Per Line", m_LimitPerLine < 1 ? 1 : m_LimitPerLine);
			m_Offset = EditorGUILayout.FloatField("Offset Between Objects", m_Offset);

			// Explicit Overrides

			GUILayout.Space(10);

			m_CustomStartGroupEnabled = EditorGUILayout.BeginToggleGroup("Explicit Start Position", m_CustomStartGroupEnabled);
			m_CustomRootPos = EditorGUILayout.Vector3Field("Start Position", m_CustomRootPos);
			EditorGUILayout.EndToggleGroup();


			if (GUILayout.Button("Apply Layout"))
			{
				Layout();
			}

			EditorGUILayout.EndVertical();
		}

		void Layout()
		{
			if (Selection.objects == null || Selection.objects.Length == 0)
			{
				Log.Error(this, $"No objects are selected. Please select at least 1 object in the scene hierarchy.");
				return;
			}

			Vector3 rootPos;

			// If we are specifying a custom root position.
			if (m_CustomStartGroupEnabled)
			{
				rootPos = m_CustomRootPos;
			}
			// Otherwise use the values of the first selected object.
			else
			{
				var rootObj = CastToGameObject(Selection.objects[0]);
				rootPos = rootObj.transform.position;
			}

			int primaryIdx = 0;
			int secondaryIdx = 0;

			for (int i = 0; i < Selection.objects.Length; i++)
			{
				var currObj = CastToGameObject(Selection.objects[i]);

				if (currObj != null)
				{
					// Apply position
					var pos = new Vector3(
						rootPos.x + ((m_Mode == Mode.RowCentric ? primaryIdx : secondaryIdx) * m_Offset),
						rootPos.y,
						rootPos.z + ((m_Mode == Mode.ColCentric ? primaryIdx : secondaryIdx) * m_Offset * -1f));

					currObj.transform.position = pos;

					// Increment our offset, and wrap when we reach the limit.
					primaryIdx++;
					if ((i+1) % m_LimitPerLine == 0)
					{
						primaryIdx = 0;
						secondaryIdx++;
					}
				}
				else
				{
					Log.Error(this, $"Selected object {i} is not a game object!");
				}
			}
		}

		GameObject CastToGameObject(Object obj)
		{
			return obj as GameObject;
		}
		#endregion
	}
}