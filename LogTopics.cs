using UnityEngine;

using System;
using System.Linq;

namespace Utilities
{
	/// <summary>
	/// Base class for defining a set of logging topics.
	/// You should inherit from this class and extend it with public booleans that represent your topics
	/// that you provide when logging. You boolean names should exactly match the enum topic names.
	/// </summary>
	public abstract class LogTopics : ScriptableObject
	{
		public virtual bool HasEnabled(Enum topic)
		{
			var fieldInfo = GetType().GetFields().FirstOrDefault(f => f.Name == topic.ToString());
			if (fieldInfo != null)
			{
				return (bool)fieldInfo.GetValue(this);
			}
			else
			{
				Log.Error(this, $"Requested undefined topic: {topic}");
				return false;
			}
		}
	}
}