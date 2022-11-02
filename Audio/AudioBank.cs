using UnityEngine;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Utilities.Audio
{
	/// <summary>
	/// Represents a set of related audio clips with playback properties.
	/// </summary>
	[CreateAssetMenu(fileName = "AudioBank", menuName = "Utilities/Audio/AudioBank", order = 0)]
	public class AudioBank : ScriptableObject
	{
		#region TYPES
		public enum PlaybackMode
		{
			Randomised,
			Iterative
		}
		#endregion


		public string m_Key;
		public List<AudioClip> m_Clips;

		[Tooltip("Whether playback will automatically continue after each clip ends.")]
		public bool m_Looping;
		[Tooltip("How the next clip is chosen after the current ends playback.")]
		public PlaybackMode m_PlaybackMode;
		[Tooltip("The length of time between each playback.")]
		public float m_Speed;
	}
}
