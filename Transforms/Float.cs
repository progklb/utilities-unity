using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.Transforms
{
	/// <summary>
	/// Causes the object to which this component is attached to float randomly in place.
	/// </summary>
	public class Float : MonoBehaviour 
	{
		#region VARIABLES
		public Transform m_ReferencePosition;
		
		[Header("PHASE")]
		public Vector3 m_FloatMagnitude;
		public Vector3 m_RandomPhaseOffset;

		[Header("SPEED")]
		public Vector3 m_Speed = Vector3.one;
		public Vector3 m_RandomSpeedOffset;

		private Transform m_Transform;
		private Vector3 m_OriginalPosition;
		private Vector3 m_PhaseOffset;
		private Vector3 m_SpeedOffset;

		private float m_OriginalScale;
		#endregion


		#region UNITY EVENTS
		void Start() 
		{
			m_Transform = this.transform;
			m_OriginalPosition = m_Transform.position;
			m_OriginalScale = ScaleManager.instance.globalScale;

			m_PhaseOffset = new Vector3 (
				(Random.value - 0.5f) * m_RandomPhaseOffset.x,
				(Random.value - 0.5f) * m_RandomPhaseOffset.y,
				(Random.value - 0.5f) * m_RandomPhaseOffset.z
			);

			m_Speed = new Vector3(
				m_Speed.x + ((Random.value - 0.5f) * m_SpeedOffset.x),
				m_Speed.y + ((Random.value - 0.5f) * m_SpeedOffset.y),
				m_Speed.z + ((Random.value - 0.5f) * m_SpeedOffset.z)
			);
		}

		void Update() 
		{
			m_Transform.position = m_ReferencePosition.position + m_OriginalPosition + new Vector3(
				Mathf.Sin(Time.time * m_Speed.x + m_PhaseOffset.x) * m_FloatMagnitude.x,
				Mathf.Sin(Time.time * m_Speed.y + m_PhaseOffset.y) * m_FloatMagnitude.y,
				Mathf.Sin(Time.time * m_Speed.z + m_PhaseOffset.z) * m_FloatMagnitude.z 
			);
		}
		#endregion
	}
}
