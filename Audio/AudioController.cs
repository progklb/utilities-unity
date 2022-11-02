using UnityEngine;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Utilities.Audio
{
	public class AudioController : MonoBehaviour
	{
		#region PROPERTIES
		public bool isPlaying { get; private set; }

		private Coroutine playbackRoutine { get; set; }
		#endregion


		#region VARIABLES
		[SerializeField]
		private List<AudioBank> m_AudioBanks;

		[SerializeField]
		private AudioSource m_AudioSource;
		#endregion


		#region PUBLIC API
		/// <summary>
		/// Plays the audio set with the specified key.
		/// Uses the <see cref="AudioBank"/>'s properties to control playback.
		/// </summary>
		/// <param name="key">The key of the audio set to play.</param>
		public void Play(string key)
		{
			if (playbackRoutine != null)
			{
				StopCoroutine(playbackRoutine);
			}

			var bank = m_AudioBanks.First(x => x.m_Key == key);
			playbackRoutine = StartCoroutine(PlayRoutine(bank, bank.m_Looping, bank.m_PlaybackMode, bank.m_Speed));
		}

		public void Play(AudioBank bank)
		{
			if (playbackRoutine != null)
			{
				StopCoroutine(playbackRoutine);
			}

			playbackRoutine = StartCoroutine(PlayRoutine(bank, bank.m_Looping, bank.m_PlaybackMode, bank.m_Speed));
		}

		/// <summary>
		/// Plays the audio set with the specified key.
		/// Uses the provided parameters to control playback.
		/// </summary>
		/// <param name="key">The key of the audio set to play.</param>
		public void Play(string key, bool loop, AudioBank.PlaybackMode mode, float speed)
		{
			if (playbackRoutine != null)
			{
				StopCoroutine(playbackRoutine);
			}

			var bank = m_AudioBanks.First(x => x.m_Key == key);
			playbackRoutine = StartCoroutine(PlayRoutine(bank, loop, bank.m_PlaybackMode, bank.m_Speed));
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
		private IEnumerator PlayRoutine(AudioBank bank, bool loop, AudioBank.PlaybackMode mode, float speed)
		{
			AudioClip clip = null;
			int idx = 0;

			do
			{
				switch (mode)
				{
					case AudioBank.PlaybackMode.Randomised:
						var rand = (int)(UnityEngine.Random.value * (bank.m_Clips.Count - 1));
						clip = bank.m_Clips[rand];
						break;

					case AudioBank.PlaybackMode.Iterative:
						idx = ++idx % bank.m_Clips.Count;
						clip = bank.m_Clips[idx];
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
