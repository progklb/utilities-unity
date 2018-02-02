using UnityEngine;

namespace Utilities.Cameras
{
	public class CameraController2D : MonoBehaviour 
	{

		#region VARIABLES
		/// The camera we are controlling
		public Camera m_Camera;
		/// The object we are following
		public Transform m_FollowBody;

		public bool m_LockZAxis = true;
		public float m_Smoothing;

		/// The target position of the camera 
		Vector3 m_Target;
		#endregion


		#region UNITY EVENTS
		void Awake () 
		{
			// Try to find a camera on this game object if a camera has not been assigned
			if (m_Camera == null)
			{
				m_Camera = GetComponent<Camera>();

				if (m_Camera == null)
				{
					Debug.LogError("Camera Controller: Cannot find camera. Please assign a camera in the inspector or attach one to the game object on which this controller lives.");
				}
			}
		}

		void Update () 
		{
			m_Target = Vector3.Lerp(m_Target, m_FollowBody.transform.position, Time.deltaTime * m_Smoothing);

			if (m_LockZAxis)
			{
				m_Target.z = m_Camera.transform.position.z;
			}

			m_Camera.transform.position = m_Target;
		}
		#endregion
	}
}