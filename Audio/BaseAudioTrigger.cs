using UnityEngine;

namespace Utilities.Audio
{
	/// <summary>
	/// A base class for defining components that trigger audio actions.
	/// </summary>
	public abstract class BaseAudioTrigger : MonoBehaviour
	{
		#region UNITY EVENTS
		private void OnTriggerEnter(Collider otherCollider)
		{
			HandleCollisionEnter(otherCollider);
		}

		private void OnTriggerExit(Collider otherCollider)
		{
			HandleCollisionExit(otherCollider);
		}
		#endregion


		#region HELPER FUNCTIONS
		protected bool TryGetControllerFromCollider(Collider otherCollider, out AudioController controller)
		{
			controller = otherCollider.gameObject.GetComponent<AudioController>();
			return controller != null;
		}

		protected abstract void HandleCollisionEnter(Collider otherCollider);

		protected abstract void HandleCollisionExit(Collider otherCollider);
		#endregion
	}
}
