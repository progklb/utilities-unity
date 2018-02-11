using UnityEngine;

namespace Utilities
{
    /// <summary>
    /// Follows a target object at an offset determined at startup.
    /// </summary>
    public class OffsetCameraFollow : MonoBehaviour
    {
        #region VARIABLES
        public Transform m_Target;

        public float m_Smoothing = 100;
        public bool m_LockX;
        public bool m_LockY;
        public bool m_LockZ;

        private Transform m_Transform;
        private Vector3 m_InitialOffset;

        private Vector3 m_Position;
        #endregion


        #region UNITY EVENTS
        void Awake()
        {
            m_Transform = transform;
            m_InitialOffset = m_Transform.position - m_Target.position;
        }

        void FixedUpdate()
        {
            m_Position = m_Transform.position - m_InitialOffset;

            if (!m_LockX)
            {
                m_Position.x = Mathf.Lerp(m_Position.x, m_Target.position.x, m_Smoothing * Time.deltaTime);
            }
            if (!m_LockY)
            {
                m_Position.y = Mathf.Lerp(m_Position.y, m_Target.position.y, m_Smoothing * Time.deltaTime);
            }
            if (!m_LockZ)
            {
                m_Position.z = Mathf.Lerp(m_Position.z, m_Target.position.z, m_Smoothing * Time.deltaTime);
            }

            m_Transform.position = m_Position + m_InitialOffset;
        }
        #endregion
    }
}