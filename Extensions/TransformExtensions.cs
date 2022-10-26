using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.Extensions
{
	/// <summary>
    /// Contains a number of useful transform extensions that make it easier to
    /// work with the <see cref="Transform"/> type.
    /// </summary>
	public static class TransformExtensions
	{
		public static void DestroyChildren(this Transform t)
        {
			for (int i = t.childCount - 1; i >= 0; i--)
            {
				GameObject.Destroy(t.GetChild(i).gameObject);
            }
        }
	}
}