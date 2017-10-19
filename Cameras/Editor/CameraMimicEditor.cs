using UnityEditor;
using UnityEngine;
using System.IO;

namespace Utilities.Cameras
{
	[CustomEditor(typeof(CameraMimic))]
	public class CameraMimicEditor : Editor
	{
		public override void OnInspectorGUI()
		{
			bool lockPos = false;
			
			// Get the UnityController that this panel will edit.
			CameraMimic camMim = (CameraMimic)target;
			if (camMim == null)
			{
				return;
			}

			EditorGUILayout.BeginVertical();

			camMim.cameraToMimic = (Camera)EditorGUILayout.ObjectField("Camera To Mimic", camMim.cameraToMimic, typeof (Camera), true);

			EditorGUILayout.Separator();

			camMim.movementFactor = EditorGUILayout.FloatField("Movement factor", camMim.movementFactor);
			camMim.rotationFactor = EditorGUILayout.FloatField("Rotation factor", camMim.rotationFactor);
			camMim.zoomFactor = EditorGUILayout.FloatField("Zoom factor", camMim.zoomFactor);

			EditorGUILayout.Separator();
			EditorGUILayout.LabelField("Lock positional movement?");

			camMim.lockPositionXAxis = EditorGUILayout.Toggle("X", camMim.lockPositionXAxis);
			camMim.lockPositionYAxis = EditorGUILayout.Toggle("Y", camMim.lockPositionYAxis);
			camMim.lockPositionZAxis = EditorGUILayout.Toggle("Z", camMim.lockPositionZAxis);

			EditorGUILayout.Separator();
			EditorGUILayout.LabelField("Lock rotational movement?");

			camMim.lockRotationXAxis = EditorGUILayout.Toggle("X", camMim.lockRotationXAxis);
			camMim.lockRotationYAxis = EditorGUILayout.Toggle("Y", camMim.lockRotationYAxis);
			camMim.lockRotationZAxis = EditorGUILayout.Toggle("Z", camMim.lockRotationZAxis);

			EditorGUILayout.Separator();
			camMim.lockZoom = EditorGUILayout.Toggle("Lock zoom?", camMim.lockZoom);

			EditorGUILayout.EndVertical();

		}
	}
}