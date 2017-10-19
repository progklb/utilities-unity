using UnityEngine;
using System.Collections;

namespace Utilities
{
	/// <summary>
	/// Holds maximum movement contraints along the X and Y axis, and provided convenience functions to ensure
	/// that provided positions lie within these constraints.
	/// </summary>
	public class PositionConstraints
	{
		#region VARIABLES
		public float minx { get; set; }
		public float maxx { get; set; }
		public float miny { get; set; }
		public float maxy { get; set; }

		private float lockingLimit;
		private float smoothing;
		#endregion


		#region CONSTRUCTOR
		/// <summary>
		/// Initializes a new instance of the <see cref="PositionConstraints"/> class.
		/// </summary>
		/// <param name="initialValue">The initial value of all constraints.</param>
		/// <param name="lockingLimit">The maximum distance beyond the current min/max value that will be allowed before aggressively locking movement.</param>
		/// <param name="clampSmoothing">The smoothing applied to lerped clamping. Larger values are snappier.</param>
		public PositionConstraints(float initialValue = 0f, float lockingLimit = float.MaxValue, float clampSmoothing = 10)
		{
			SetAllConstraints(initialValue);

			this.lockingLimit = lockingLimit;
			this.smoothing = clampSmoothing;
		}
		#endregion


		#region FUNCTIONALITY
		/// <summary>
		/// Sets all min and max constraints to the provided value.
		/// </summary>
		public void SetAllConstraints(float value)
		{
			minx = miny = maxx = maxy = value;
		}

		/// <summary>
		/// Returns true if the provided value does not exceed the current constraints.
		/// </summary>
		public bool IsWithinConstraints(Vector3 position)
		{
			if (position.x < minx || position.y < miny)
			{
				return false;
			}
			else if (position.x > maxx || position.y > maxy)
			{
				return false;
			}

			return true;
		}

		/// <summary>
		/// Aggressively constrain movement. This just locks (clamps) the position at the boundary.
		/// </summary>
		public Vector3 ClampMovementAggressive(Vector3 position)
		{
			if (position.x < minx)
			{
				position.x = Mathf.Clamp (position.x,position.x, maxx);
			}
			if (position.x > maxx)
			{
				position.x = Mathf.Clamp (position.x, minx, position.x);
			}
			if (position.y < miny)
			{
				position.y = Mathf.Clamp (position.y, position.y, maxy);
			}
			if (position.y > maxy)
			{
				position.y = Mathf.Clamp (position.y, miny, position.y);
			}

			return position;
		}

		/// <summary>
		/// Calculates a lerp back toward the boundary, but also enforces a clamp limit beyond the boundary to prevent straying too far.
		/// </summary>
		public Vector3 ClampMovementLerp(Vector3 position)
		{
			// Set a locking limit beyond the constraint boundary
			float minLimitX = minx - lockingLimit;
			float maxLimitX = maxx + lockingLimit;
			float minLimitY = miny - lockingLimit;
			float maxLimitY = maxy + lockingLimit;

			// Apply limiting: Because we are constraining via a lerp, increasing position values will allow the position 
			// to keep moving away from the min/max. We prevent this by limiting the distance the cam can be from the border. 
			if (position.x < minLimitX || position.x > maxLimitX)
			{
				position.x = Mathf.Clamp (position.x, minLimitX, maxLimitX);
			}
			if (position.y < minLimitY || position.y > maxLimitY)
			{
				position.y = Mathf.Clamp (position.y, minLimitY, maxLimitY);
			}

			//Smoothly constrain movement. Lerps back to boundary.
			if (position.x < minx)
			{
				position.x = Mathf.Lerp(position.x, minx, Time.deltaTime * smoothing);
			}
			else if (position.x > maxx)
			{
				position.x = Mathf.Lerp(position.x, maxx, Time.deltaTime * smoothing);
			}
			if (position.y < miny)
			{
				position.y = Mathf.Lerp(position.y, miny, Time.deltaTime * smoothing);
			}
			else if (position.y > maxy)
			{
				position.y = Mathf.Lerp(position.y, maxy, Time.deltaTime * smoothing);
			}

			return position;
		}
		#endregion
	}
}