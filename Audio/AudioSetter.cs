using UnityEngine;

namespace Utilities.Audio
{
	/// <summary>
	/// An trigger component to play a specific <see cref="AudioBank"/> via
	/// an <see cref="AudioController"/> we come into contact with.
	/// </summary>
	[RequireComponent(typeof(Collider))]
	public class AudioSetter : BaseAudioTrigger
	{
		#region VARIABLES
		[SerializeField]
		private AudioBank m_AudioBank;
		[SerializeField]
		private string m_AudioBankKey;
		#endregion


		#region UNITY EVENTS
		private void Start()
		{
			Debug.Assert(
				!(m_AudioBank == null && string.IsNullOrWhiteSpace(m_AudioBankKey)),
				$"A bank or key must be assigned to this {nameof(AudioSetter)} instance.");
		}

		private void OnDrawGizmos()
		{
			Gizmos.color = Color.yellow;
			Gizmos.DrawWireSphere(transform.position, 1f);
			Gizmos.DrawSphere(transform.position, 0.35f);
		}
		#endregion


		#region HELPER FUNCTIONS
		protected override void HandleCollisionEnter(Collider otherCollider)
		{
			if (TryGetControllerFromCollider(otherCollider, out var controller))
			{
				Log($"{name} audio trigger collided with {controller.name}. Assigning bank: {m_AudioBank?.m_Key ?? m_AudioBankKey}.");
				if (m_AudioBank != null)
				{
					controller.SetBank(m_AudioBank);
				}
				else
				{
					controller.SetBank(m_AudioBankKey);
				}
			}
		}

		protected override void HandleCollisionExit(Collider otherCollider)
		{
			if (TryGetControllerFromCollider(otherCollider, out var controller))
			{
				Log($"{name} audio trigger collided with {controller.name}. Unassigning bank: {m_AudioBank?.m_Key ?? m_AudioBankKey}.");
				if (m_AudioBank != null)
				{
					controller.UnsetBank(m_AudioBank);
				}
				else
				{
					controller.UnsetBank(m_AudioBankKey);
				}
			}
		}

		void Log(string message)
		{
			if (AudioController.enableLogging)
			{
				Utilities.Log.Info(this, message);
			}
		}
		#endregion
	}
}
