using System;
using System.Collections.Generic;

using UnityEngine;

namespace Utilities.Timers
{
	/// <summary>
	/// A simple timer script that contains the internal logic to update its time and execute assigned callbacks when the timer reachs its call.
	/// </summary>
	public class CallbackTimer
	{
		#region VARIABLES
		///Accumulated delta time
		public float accumulatedTime { get; set; }
		///Target time
		public float goalTime { get; set; }

		///Callbacks for when we reach goal time.
		List<Action> callbacks;
		#endregion


		#region CONSTRUCTOR
		public CallbackTimer(float goalTime)
		{
			this.goalTime = goalTime;
			this.callbacks = new List<Action>();
		}
		#endregion


		#region PUBLIC-FACING API
		/// <summary>
		/// Updates the timer with the latest delta time. Will return true if timer exceeds its goal.
		/// </summary>
		public bool Update()
		{
			bool goalReached = (accumulatedTime += Time.deltaTime) > goalTime;

			if (goalReached)
			{
				FireCallbacks();
			}

			return goalReached;
		}

		/// <summary>
		/// Resets the accumulated time to 0.
		/// </summary>
		public void Reset()
		{
			accumulatedTime = 0;
		}
		#endregion


		#region PUBLIC-FACING CALLBACKS
		/// <summary>
		/// Adds a callback to this object that will fire every time the timer reaches its goal.
		/// </summary>
		public void AddCallback(Action callback)
		{
			callbacks.Add(callback);
		}

		/// <summary>
		/// Removes a callback of the matching parameter
		/// </summary>
		public void RemoveCallback(Action callback)
		{
			callbacks.Remove(callback);
		}

		/// <summary>
		/// Clears the list of assigned callbacks.
		/// </summary>
		public void ClearCallbacks()
		{
			callbacks.Clear();
		}
		#endregion


		#region PUBLIC-FACING GETTERS AND SETTERS
		/// <summary>
		/// Returns progress to goal time as a value between 0 and 1.
		/// </summary>
		public float GetProgress()
		{
			return (accumulatedTime / goalTime);
		}
		#endregion


		#region INTERNAL FUNCTIONALITY
		/// <summary>
		/// Iterates through all assigned callbacks and executes them.
		/// </summary>
		void FireCallbacks()
		{
			foreach (var callback in callbacks)
			{
				callback();
			}
		}
		#endregion
	}
}