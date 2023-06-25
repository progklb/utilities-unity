using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

namespace Utilities.Audio
{
	public class AudioController : MonoBehaviour
	{
		#region TYPES
		public class AudioBankProperties
		{
			public bool? loop { get; set; }
			public AudioBank.PlaybackMode? mode { get; set; }
			public float? speed { get; set; }
		}
		#endregion


		#region PROPERTIES
		public static bool enableLogging { get; set; }

		public bool isPlaying { get; private set; }

		public List<AudioBank> currentAudioBanks { get; set; } = new(4);

		private Coroutine playbackRoutine { get; set; }
		#endregion


		#region VARIABLES
		[SerializeField]
		private List<AudioBank> m_AudioBanks;
		[SerializeField, Tooltip(
			"Optional. Will be used if the controller is requested to play audio " +
			"when there is no current audio bank")]
		private AudioBank m_DefaultAudioBank;

		[SerializeField]
		private AudioSource m_AudioSource;

		[SerializeField]
		[Tooltip("If true, we stack audio banks as we come into contact with their volume, " +
			"and pop them when leaving their volume. If false, we replace the current bank.")]
		private bool m_IsStacking;
		#endregion


		#region UNITY EVENTS
		void OnValidate()
		{
			// Ensure that we don't have any duplicate entries.
			if (!m_AudioBanks.TrueForAll(x => m_AudioBanks.Where(y => x == y).Count() == 1))
			{
				LogError("Duplicate audio bank(s) found! Ensure that all banks are unique.");
			}
		}
		#endregion


		#region PUBLIC API
		/// <summary>
		/// Sets the current audio bank without playing the audio.
		/// This will load the audio bank with the specified key, and requires
		/// that this controller has this bank assigned in <see cref="m_AudioBanks"/>.
		/// </summary>
		/// <param name="key">The key of the audio bank to load.</param>
		public void SetBank(string key)
		{
			SetBank(m_AudioBanks.FirstOrDefault(x => x.m_Key == key));
		}

		/// <summary>
		/// Sets the current audio bank without playing the audio.
		/// This will set the provided audio bank as the current bank,
		/// and does not require that the bank is assigned in <see cref="m_AudioBanks"/>.
		/// </summary>
		/// <param name="key">The audio bank to load.</param>
		public void SetBank(AudioBank bank)
		{
			if (bank == null)
			{
				LogError("Cannot set audio bank. Provided bank is null.");
				return;
			}

			Log($"Setting audio bank ({(m_IsStacking ? "stacking" : "non-stacking")}): {bank.m_Key} ({bank.name})");

			if (!m_IsStacking)
			{
				currentAudioBanks.Clear();
			}

			currentAudioBanks.Add(bank);
		}

		/// <summary>
		/// Sets the current audio bank without playing the audio.
		/// This will load the audio bank with the specified key, and requires
		/// that this controller has this bank assigned in <see cref="m_AudioBanks"/>.
		/// </summary>
		/// <param name="key">The key of the audio bank to load.</param>
		public void UnsetBank(string key)
		{
			UnsetBank(m_AudioBanks.FirstOrDefault(x => x.m_Key == key));
		}

		/// <summary>
		/// Sets the current audio bank without playing the audio.
		/// This will set the provided audio bank as the current bank,
		/// and does not require that the bank is assigned in <see cref="m_AudioBanks"/>.
		/// </summary>
		/// <param name="key">The audio bank to load.</param>
		public void UnsetBank(AudioBank bank)
		{
			if (bank == null)
			{
				LogError("Cannot unset audio bank. Provided bank is null.");
				return;
			}

			Log($"Unsetting audio bank: {bank.m_Key} ({bank.name})");

			var idx = currentAudioBanks.IndexOf(bank);
			if (idx >= 0)
			{
				currentAudioBanks.RemoveAt(idx);
			}
		}

		/// <summary>
		/// Plays the current audio bank.
		/// </summary>
		public void Play(AudioBankProperties properties = null)
		{
			if (currentAudioBanks.Count > 0)
			{
				Play(currentAudioBanks.Last(), properties);
			}
			else if (m_DefaultAudioBank != null)
			{
				Play(m_DefaultAudioBank, properties);
			}
			else
			{
				LogError("Cannot play audio as there is no current audio bank.");
			}
		}

		/// <summary>
		/// Stops any current playback.
		/// </summary>
		/// <param name="stopImmediately">Whether playback is allowed to end naturally, or should forcibly stopped.</param>
		public void Stop(bool stopImmediately = false)
		{
			isPlaying = false;

			if (stopImmediately)
			{
				m_AudioSource.Stop();
			}
		}
		#endregion


		#region HELPER FUNCTIONS
		/// <summary>
		/// Assigns and plays the provided audio bank.
		/// This will use the bank's properties to control playback, unless
		/// an optional override for specifying specific playback behaviour is provided.
		/// </summary>
		/// <param name="bank">The audio bank to play.</param>
		/// <param name="properties">Optional overrides for playback behaviour.</param>
		private void Play(AudioBank bank, AudioBankProperties properties = null)
		{
			if (playbackRoutine != null)
			{
				StopCoroutine(playbackRoutine);
			}

			playbackRoutine = StartCoroutine(PlayRoutine(
				bank,
				properties?.loop ?? bank.m_Looping,
				properties?.mode ?? bank.m_PlaybackMode,
				properties?.speed ?? bank.m_Speed));
		}

		private IEnumerator PlayRoutine(AudioBank bank, bool loop, AudioBank.PlaybackMode mode, float speed)
		{
			AudioClip clip = null;
			int idx = 0;

			do
			{
				switch (mode)
				{
					case AudioBank.PlaybackMode.Randomised:
						var rand = (int)(Random.value * (bank.m_Clips.Count - 1));
						clip = bank.m_Clips[rand];
						break;

					case AudioBank.PlaybackMode.Iterative:
						idx = ++idx % bank.m_Clips.Count;
						clip = bank.m_Clips[idx];
						break;

					default:
						LogError("Unsupported playback mode.");
						yield break;
				}

				m_AudioSource.PlayOneShot(clip);
				isPlaying = true;

				yield return new WaitForSeconds(Mathf.Min(clip.length, speed));
			}
			while (loop && isPlaying);

			isPlaying = false;
		}

		private void Log(string message)
		{
			if (enableLogging)
			{
				Utilities.Log.Info(this, message);
			}
		}

		private void LogError(string message)
		{
			Utilities.Log.Error(this, message);
		}
		#endregion
	}
}
