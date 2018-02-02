using UnityEngine;

namespace Utilities.Transforms
{
    public class DestroySelf : MonoBehaviour
    {
        #region TYPES
        public enum DestroyCondition
        {
            Start,
            OnEnable,
            OnDisable,
        }
        #endregion


        #region VARIABLES
        public DestroyCondition m_Condition;
        public float m_Delay;
        #endregion


        #region UNITY EVENTS
        void Start()
        {
            if (m_Condition == DestroyCondition.Start)
            {
                Destroy(gameObject, m_Delay);
            }
        }

        void OnEnable()
        {
            if (m_Condition == DestroyCondition.OnEnable)
            {
                Destroy(gameObject, m_Delay);
            }
        }

        void OnDisable()
        {
            if (m_Condition == DestroyCondition.OnDisable)
            {
                Destroy(gameObject, m_Delay);
            }
        }
        #endregion
    }
}