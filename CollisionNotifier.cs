using System;

using UnityEngine;

namespace Utilities
{
	/// <summary>
	/// Reports Unity collision and trigger callbacks as standard events that can be subscribed to.
	/// This is a convenience component that allows you to listen for events on other objects, without
	/// requiring the Unity callback handlers to be defined on the object with the <see cref="Rigidbody"/>.
	/// </summary>
	public class CollisionNotifier : MonoBehaviour
	{
		#region EVENTS
		public event Action<Collision> onCollisionStay = delegate { };
		public event Action<Collider> onTriggerStay = delegate { };
		#endregion


		#region UNITY EVENTS
		void OnCollisionStay(Collision col)
		{
			onCollisionStay(col);
		}

		void OnTriggerStay(Collider col)
		{
			onTriggerStay(col);
		}
		#endregion
	}
}
