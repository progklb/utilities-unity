using UnityEngine;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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
		public bool isPlaying { get; private set; }

		public AudioBank currentAudioBank { get; set; }

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
		#endregion


		#region PUBLIC API
		/// <summary>
		/// Sets the current audio bank without playing the audio.
		/// </summary>
		/// <param name="key">The key of the audio bank to load.</param>
		public void SetBank(string key)
		{
			SetBank(m_AudioBanks.First(x => x.m_Key == key));
		}

		/// <summary>
		/// Sets the current audio bank without playing the audio.
		/// </summary>
		/// <param name="key">The audio bank to load.</param>
		public void SetBank(AudioBank bank)
		{
			currentAudioBank = bank;
		}

		/// <summary>
		/// Plays the current audio bank.
		/// </summary>
		public void Play()
		{
			if (currentAudioBank != null)
			{
				Play(currentAudioBank);
			}
			else if (m_DefaultAudioBank != null)
			{
				Play(m_DefaultAudioBank);
			}
			else
			{
				Debug.LogError("Cannot play audio as there is no current audio bank.");
			}
		}

		/// <summary>
		/// Attempts to load and play the audio bank with the specified key.
		/// This will use the bank's properties to control playback, unless
		/// an optional override for specifying specific playback behaviour is provided.
		/// </summary>
		/// <param name="key">The key of the audio set to play.</param>
		/// <param name="properties">Optional overrides for playback behaviour.</param>
		public void Play(string key, AudioBankProperties properties = null)
		{
			var bank = m_AudioBanks.First(x => x.m_Key == key);
			Play(bank, properties);
		}

		/// <summary>
		/// Assigns and plays the provided audio bank.
		/// This will use the bank's properties to control playback, unless
		/// an optional override for specifying specific playback behaviour is provided.
		/// </summary>
		/// <param name="bank">The audio bank to play.</param>
		/// <param name="properties">Optional overrides for playback behaviour.</param>
		public void Play(AudioBank bank, AudioBankProperties properties = null)
		{
			if (playbackRoutine != null)
			{
				StopCoroutine(playbackRoutine);
			}

			currentAudioBank = bank;

			playbackRoutine = StartCoroutine(PlayRoutine(
				//currentAudioBank,
				properties?.loop ?? currentAudioBank.m_Looping,
				properties?.mode ?? currentAudioBank.m_PlaybackMode,
				properties?.speed ?? currentAudioBank.m_Speed));
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
		private IEnumerator PlayRoutine(/*AudioBank bank, */bool loop, AudioBank.PlaybackMode mode, float speed)
		{
			AudioClip clip = null;
			int idx = 0;

			do
			{
				switch (mode)
				{
					case AudioBank.PlaybackMode.Randomised:
						var rand = (int)(UnityEngine.Random.value * (currentAudioBank.m_Clips.Count - 1));
						clip = currentAudioBank.m_Clips[rand];
						break;

					case AudioBank.PlaybackMode.Iterative:
						idx = ++idx % currentAudioBank.m_Clips.Count;
						clip = currentAudioBank.m_Clips[idx];
						break;

					default:
						Debug.LogError("Unsupported playback mode.");
						yield break;
				}
				
				m_AudioSource.PlayOneShot(clip);
				isPlaying = true;

				yield return new WaitForSeconds(Mathf.Min(clip.length, speed));
			}
			while (loop && isPlaying);

			isPlaying = false;
		}
		#endregion
	}
}
