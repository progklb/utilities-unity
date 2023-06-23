using UnityEngine;

namespace Utilities.Audio
{
	/// <summary>
	/// An trigger component to play a specific <see cref="AudioBank"/> via
	/// an <see cref="AudioController"/> we come into contact with.
	/// </summary>
	[RequireComponent(typeof(Collider))]
	public class AudioKeyTrigger : AudioTrigger
	{
		#region VARIABLES
		[SerializeField]
		private string m_AudioBankKey;
		#endregion


		#region UNITY EVENTS
		private void OnTriggerEnter(Collider otherCollider)
		{
			HandleCollisionEnter(otherCollider);
		}

		private void OnTriggerExit(Collider otherCollider)
		{
			HandleCollisionExit(otherCollider);
		}

		private void OnDrawGizmos()
		{
			Gizmos.color = Color.yellow;
			Gizmos.DrawWireSphere(transform.position, 1f);
			Gizmos.DrawSphere(transform.position, 0.35f);
		}
		#endregion


		#region HELPER FUNCTIONS
		protected virtual void HandleCollisionEnter(Collider otherCollider)
		{
			if (TryGetControllerFromCollider(otherCollider, out var controller))
			{
				Debug.Log($"{name} audio trigger collided with {controller.name}. Assigning bank: {m_AudioBankKey}.");
				controller.SetBank(m_AudioBankKey);
			}
		}

		protected virtual void HandleCollisionExit(Collider otherCollider)
		{
			if (TryGetControllerFromCollider(otherCollider, out var controller))
			{
				Debug.Log($"{name} audio trigger collided with {controller.name}. Unassigning bank: {m_AudioBankKey}.");
				controller.UnsetBank(m_AudioBankKey);
			}
		}
		#endregion
	}
}
