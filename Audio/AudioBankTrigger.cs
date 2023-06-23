using UnityEngine;

namespace Utilities.Audio
{
	/// <summary>
	/// An trigger component to play a specific <see cref="AudioBank"/> via
	/// an <see cref="AudioController"/> we come into contact with, using .
	/// </summary>
	[RequireComponent(typeof(Collider))]
	public class AudioBankTrigger : AudioTrigger
	{
		#region VARIABLES
		[SerializeField]
		private AudioBank m_AudioBank;
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
				Debug.Log($"{name} audio trigger collided with {controller.name}. Assigning bank: {m_AudioBank.m_Key}.");
				controller.SetBank(m_AudioBank);
			}
		}

		protected virtual void HandleCollisionExit(Collider otherCollider)
		{
			if (TryGetControllerFromCollider(otherCollider, out var controller))
			{
				Debug.Log($"{name} audio trigger collided with {controller.name}. Unassigning bank: {m_AudioBank.m_Key}.");
				controller.UnsetBank(m_AudioBank);
			}
		}
		#endregion
	}
}
