using UnityEngine;

namespace Utilities.Audio
{
	/// <summary>
	/// A base class for defining components that trigger audio actions.
	/// </summary>
	public abstract class AudioTrigger : MonoBehaviour
	{
		#region HELPER FUNCTIONS
		protected bool TryGetControllerFromCollider(Collider otherCollider, out AudioController controller)
		{
			controller = otherCollider.gameObject.GetComponent<AudioController>();
			return controller != null;
		}
		#endregion
	}
}
