using UnityEngine;
using System.Collections;

namespace Utilities.Math
{
	/// <summary>
	/// Defines an upper and lower bounding value, and provides convenience operations with respect to these defined values.
	/// </summary>
	public class Range 
	{
		public float upperBound, lowerBound;

		public Range(float upperBound, float lowerBound)
		{
			this.upperBound = upperBound;
			this.lowerBound = lowerBound;
		}

		/// <summary>
		/// Returns the distance between the two bounding values.
		/// </summary>
		public float DistanceBetweenBounds()
		{
			return upperBound - lowerBound;
		}

		/// <summary>
		/// Returns the midpoint between the two bounding values
		/// </summary>
		public float MidPointBetweenBounds()
		{
			return (upperBound + lowerBound) / 2;
		}

		#region CONVENIENCE FUNCTIONS
		/// <summary>
		/// Returns true if the value is equal to or within the defined bounds.
		/// </summary>
		public bool LiesWithinBoundsInclusive(float value)
		{
			if (value >= lowerBound && value <= upperBound)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		
		/// <summary>
		/// Returns true if the value is within, but not equal to, the defined bounds.
		/// </summary>
		public bool LiesWithinBoundsExclusive(float value)
		{
			if (value > lowerBound && value < upperBound)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// Returns the bounding value closest to the provided value
		/// </summary>
		public float ReturnClosestBound(float value)
		{
			float midpoint =( upperBound + lowerBound )/ 2;
			if (value >= midpoint)
			{
				return upperBound;
			}
			else 
			{
				return lowerBound;
			}
		}

		/// <summary>
		/// Generates a random value inbetween equal to or between the two bounds
		/// </summary>
		public float RandomValue()
		{
			return Random.Range(lowerBound, upperBound);
		}
		#endregion
	}
}
