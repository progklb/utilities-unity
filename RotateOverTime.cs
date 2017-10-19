using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
	/// <summary>
	/// Rotates the transform to which this script is attached to over time.
	/// </summary>
	public class RotateOverTime : MonoBehaviour 
	{
		#region PUBLIC VARIABLES
		public float m_Speed = 1f;
		public Vector3 m_Axis;
		#endregion


		#region PRIVATE VARIABLES
		private Transform m_Transform;
		#endregion


		#region UNITY EVENTS
		void Start()
		{
			// Cache for performance.
			m_Transform = transform;
		}

		void Update()
		{
			m_Transform.RotateAround(m_Transform.position, m_Axis, 360 * Time.deltaTime * m_Speed);
		}
		#endregion
	}
}
