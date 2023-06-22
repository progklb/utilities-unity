using UnityEngine;

namespace Utilities.Audio
{
	/// <summary>
	/// An trigger component to play a specific <see cref="AudioBank"/> via
	/// an <see cref="AudioController"/> we come into contact with.
	/// </summary>
	[RequireComponent(typeof(Collider))]
	public class AudioKeyTrigger : MonoBehaviour
	{
		#region VARIABLES
		[SerializeField]
		private string m_AudioBankKey;
		#endregion


		#region UNITY EVENTS
		private void OnTriggerEnter(Collider otherCollider)
		{
			HandleCollision(otherCollider);
		}

		private void OnDrawGizmos()
		{
			Gizmos.color = Color.yellow;
			Gizmos.DrawWireSphere(transform.position, 1f);
		}
		#endregion


		#region HELPER FUNCTIONS
		protected virtual void HandleCollision(Collider otherCollider)
		{
			var controller = otherCollider.gameObject.GetComponent<AudioController>();
			if (controller != null)
			{
				Debug.Log($"{name} audio trigger collided with {controller.name}. Assigning bank: {m_AudioBankKey}.");
				controller.SetBank(m_AudioBankKey);
			}
		}
		#endregion
	}
}
