using UnityEngine;

namespace Utilities
{
	/// <summary>
	/// A base class for creating singleton objects.
	/// </summary>
	/// <typeparam name="T">The concrete type.</typeparam>
	public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
	{
		#region PROPERTIES
		public static T instance { get; private set; }
		#endregion


		#region UNITY EVENTS
		protected virtual void Awake()
		{
			if (instance == null)
			{
				instance = (T)this;
			}
			else
			{
				Debug.LogError($"An instance of manager {instance.GetType().Name} already exists in scene.");
			}
		}

		protected virtual void OnDestroy()
		{
			if (instance == this)
			{
				instance = null;
			}
		}
		#endregion
	}
}