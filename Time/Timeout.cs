using UnityEngine;
using System.Collections;

namespace Utilities.Time
{
	/// <summary>
	/// An object that contains a specified timeout value and an accumulation value.
	/// It provides convenience methods for dealing with timers.
	/// </summary>
	[System.Serializable]
	public class Timeout
	{
		#region VARIABLES
		/// The currently accumulating time
		public float time { get; set; }
		/// The maximum accumulated time
		public float timeout { get; set; }
		#endregion


		#region CONSTRUCTOR
		public Timeout(float timeoutLength)
		{
			time = 0f;
			timeout = timeoutLength;
		}
		#endregion


		#region FUNCTIONALITY
		public bool Accumulate(float deltaTime)
		{
			time += deltaTime;
			return time >= timeout;
		}

		public void SetTime(float value)
		{
			time = value;
		}
		#endregion
	}
}