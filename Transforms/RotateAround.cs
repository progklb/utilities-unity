using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.Transforms
{
	/// <summary>
	/// Rotates the object to which this component is attached around a specified axis.
	/// </summary>
	public class RotateAround : MonoBehaviour 
	{
		#region VARIABLES
		public Transform m_Parent;
		public Vector3 m_Axis;
		public float m_Speed = 1f;

		private Transform m_Transform;
		#endregion


		#region UNITY EVENTS
		void Start()
		{
			m_Transform = this.transform;

			if (m_Parent == null) 
			{
				m_Parent = m_Transform.parent;
			}

			if (m_Axis == Vector3.zero) 
			{
				m_Axis = m_Transform.up;
			}
		}

		void Update() 
		{
			m_Transform.RotateAround(m_Parent.position, m_Axis, m_Speed);
		}
		#endregion
	}
}