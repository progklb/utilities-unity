using UnityEngine;
using System.Collections;
using System;

namespace Utilities.Cameras
{
	// TODO Clean up this class.
	
	/// <summary>
	/// Using a camera as a reference, this class updates the camera of the gameobject to which it is attached to, according to the parameters provided.
	/// </summary>
	public class CameraMimic: MonoBehaviour
	{
		#region PUBLIC VARIABLES
		public Camera cameraToMimic;

		public float movementFactor = 1f;
		public float zoomFactor = 1f;
		public float rotationFactor = 1f;

		// Prevent mimicking along specific axes
		public bool lockPositionXAxis, lockPositionYAxis, lockPositionZAxis;
		public bool lockRotationXAxis, lockRotationYAxis, lockRotationZAxis;
		public bool lockZoom;
		#endregion


		#region PRIVATE VARIABLES
		private Camera m_Camera;

		private Vector3 m_CameraIntialPos;
		private Vector3 m_CameraToMimicInitialPos;

		private Quaternion m_CameraInitialRot;
		private Quaternion m_CameraToMimicInitialRot;

		private float m_CameraInitialZoom;
		private float m_CameraToMimicInitialZoom;
		#endregion


		void Start()
		{
			// Get camera on the same game object as this script
			m_Camera = (Camera) GetComponent(typeof(Camera));

			// Set initial positions
			m_CameraIntialPos = new Vector3(
				m_Camera.transform.position.x, 
				m_Camera.transform.position.y, 
			    m_Camera.transform.position.z
				);
			m_CameraToMimicInitialPos = new Vector3(
				cameraToMimic.transform.position.x, 
			    cameraToMimic.transform.position.y, 
			    cameraToMimic.transform.position.z
				);

			// Set intial rotations
			m_CameraInitialRot = m_Camera.transform.rotation;
			m_CameraToMimicInitialRot = cameraToMimic.transform.rotation;

			// Set initial zooms
			if (m_Camera.orthographic)
				m_CameraInitialZoom = m_Camera.orthographicSize;
			else
				m_CameraInitialZoom = m_Camera.fieldOfView;

			if (cameraToMimic.orthographic)
				m_CameraToMimicInitialZoom = cameraToMimic.orthographicSize;
			else
				m_CameraToMimicInitialZoom = cameraToMimic.fieldOfView;
		}
		
		// Update is called once per frame
		void FixedUpdate () 
		{
			UpdatePosition();
			UpdateRotation();
			UpdateZoom();
		}

		void UpdatePosition()
		{
			// Calulate offset of primary camera compared to its intial position
			Vector3 offset = cameraToMimic.transform.position - m_CameraToMimicInitialPos;
			
			// Apply factored translation to primary camera, considering axis locking.
			float x = 0 , y = 0 , z = 0 ;
			
			if (!lockPositionXAxis)
				x = offset.x * movementFactor;
			if (!lockPositionYAxis)
				y = offset.y * movementFactor;
			if (!lockPositionZAxis)
				z = offset.z * movementFactor;
			
			Vector3 mimickedOffset = new Vector3 (x, y ,z);
			
			// Reset secondary camera, and apply relative translation
			m_Camera.transform.position = m_CameraIntialPos + mimickedOffset;
		}

		void UpdateRotation()
		{
			// NOTE 
			// Passing the master camera through 360 degrees will cause the slave camera to jump back to the initial rotation if the scaling is not 1:1.
			// It is safe to use this function so long as either camera does not go through 360 degrees of rotation about any axis with a rotation factor != 1.
			// This cannot be avoided, because if the slave camera does not reset when the master completes a full revolution, the cameras will be out of sync.

			Vector3 rotation = cameraToMimic.transform.eulerAngles - m_CameraToMimicInitialRot.eulerAngles;

			float x = 0 , y = 0 , z = 0 ;

			if (!lockRotationXAxis)
				x = rotation.x * rotationFactor;
			if (!lockRotationYAxis)
				y = rotation.y * rotationFactor;
			if (!lockRotationZAxis)
				z = rotation.z * rotationFactor;

			Vector3 mimickedRotation = new Vector3 (x, y, z);

			// Reset and apply
			m_Camera.transform.rotation = m_CameraInitialRot;
			m_Camera.transform.Rotate (mimickedRotation);
		}

		void UpdateZoom()
		{
			// NOTE
			// Whatever you do, do not switch from perspective to orthographic projections during runtime. 
			// This just may break the universe. It may not, but don't take chances.

			if (!lockZoom)
			{
				//if both cameras are othographic
				if (m_Camera.orthographic && cameraToMimic.orthographic)
				{
					float zoomDifference = cameraToMimic.orthographicSize - m_CameraToMimicInitialZoom;
					zoomDifference *= zoomFactor;
					m_Camera.orthographicSize = m_CameraInitialZoom;
					m_Camera.orthographicSize += zoomDifference;
				}

				//if both cameras are perspective
				else if ( !m_Camera.orthographic && !cameraToMimic.orthographic)
				{
					float zoomDifference = cameraToMimic.fieldOfView - m_CameraToMimicInitialZoom ;
					zoomDifference *= zoomFactor;
					m_Camera.fieldOfView = m_CameraInitialZoom;
					m_Camera.fieldOfView += zoomDifference;
				}

				else
				{
					//if master cam is ortho and slave cam is perspective
					if (cameraToMimic.orthographic)
					{
						/*float zoomDifference =  cameraToMimic.orthographicSize - m_CameraToMimicInitialZoom;
						zoomDifference *= zoomFactor;
						m_Camera.orthographicSize = m_CameraInitialZoom;
						m_Camera.orthographicSize += zoomDifference;*/

						//TODO Clamp the zoom within acceptable bounds once assigned
						Debug.LogWarning ("CameraMimic: A combination of master=orthographic and slave=perspective cameras is not yet supported.");
					}

					//if master is perspective and slave is ortho
					else 
					{
						/*float zoomDifference = cameraToMimic.orthographicSize - m_CameraToMimicInitialZoom;
						zoomDifference *= zoomFactor;
						m_Camera.orthographicSize = m_CameraInitialZoom;
						m_Camera.orthographicSize += zoomDifference;*/

						Debug.LogWarning ("CameraMimic: A combination of master=perspective and slave=orthographic cameras is not yet supported.");
					}

					//throw new Exception ("CameraMimic: The camera you are trying to mimic and the camera you are applying the mimic to must both be orthographic or both be perspective.");
					//camera fov range = 1 -> 179
				}
			}
			/*  
			 	If perspective
					? Accept a maximum orthographic size as minimum FOV, and vice versa.
					? Calculate the position of the FOV range as the equivalent position of the ortho range

					Work out a constant value that pretty much maps 1:1 zooming wise.
					Apply zoom update as cam.fov = (otherCam.otho * constant) * zoomFactor; ?
			*/

				
		}

		#region HELPER FUNCTIONS
		/// <summary>
		/// Returns the camera that is our master for the slave camera attached to this game object. 
		/// </summary>
		public Camera GetCameraToMimic()
		{
			return cameraToMimic;
		}

		/// <summary>
		/// Returns the slave camera that is copying the movements of the camera to mimic.
		/// </summary>
		public Camera GetCameraThatIsMimicking()
		{
			return m_Camera;
		}

		/// <summary>
		/// The initial position of the camera we are mimicking the movements of.
		/// </summary>
		public Vector3 GetCamerToMimicInitialPosition()
		{
			return m_CameraToMimicInitialPos;
		}

		/// <summary>
		/// The inital position of the slave camera that is mimicking our master camera.
		/// </summary>
		public Vector3 GetCameraThatIsMimickingInitialPosition()
		{
			return m_CameraIntialPos;
		}

		/// <summary>
		/// Returns the factors used in mimicing the master camera, packaged as a Vector3 for convenience. Each factor represents a scaling value that is applied to the master camera's values, in order to produce the value which we mimic by.
		/// x = movement factor. y = rotational factor. z = zoom factor.
		/// </summary>
		public Vector3 GetCameraMimicFactors()
		{
			return new Vector3(movementFactor, rotationFactor, zoomFactor);
		}
		#endregion
	}
}