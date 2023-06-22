using System;
using System.Runtime.CompilerServices;

using UnityEngine;

namespace Utilities
{
	/// <summary>
	/// A wrapper for <see cref="Debug"/> logging that helps with
	/// adds some additional functionality for convenience and nicety.
	/// </summary>
	public static class Log
	{
		#region PROPERTIES
		private static LogTopics topics { get; set; }
		#endregion


		#region PUBLIC API
		public static void Info(Enum topic, string message, [CallerMemberName] string memberName = "")
		{
			if (FilterTopic(topic))
			{
				Info($"{topic}::{memberName}", message);
			}
		}

		public static void Info(object context, string message)
			=> Info(context.GetType().Name, message);

		public static void Info(string context, string message)
			=> Debug.Log($"I [{context}] {message}");


		public static void Warning(Enum topic, string message, [CallerMemberName] string memberName = "")
		{
			if (FilterTopic(topic))
			{
				Warning($"{topic}::{memberName}", message);
			}
		}

		public static void Warning(object context, string message)
			=> Warning(context.GetType().Name, message);

		public static void Warning(string context, string message)
			=> Debug.LogWarning($"W [{context}] {message}");


		public static void Error(Enum topic, string message, [CallerMemberName] string memberName = "")
			=> Error($"{topic}::{memberName}", message);

		public static void Error(object context, string message)
			=> Error(context.GetType().Name, message);

		public static void Error(string context, string message)
			=> Debug.LogError($"E [{context}] {message}");


		public static void Assertion(object context, string message)
			=> Assertion(context.GetType().Name, message);

		public static void Assertion(string context, string message)
			=> Debug.LogAssertion($"[{context}] {message}");


		public static void Assert(Enum topic, bool condition, string message, [CallerMemberName] string memberName = "")
			=> Assert($"{topic}::{memberName}", condition, message);

		public static void Assert(object context, bool condition, string message)
			=> Assert(context.GetType().Name, condition, message);

		public static void Assert(string context, bool condition, string message)
			=> Debug.Assert(condition, $"[{context}] {message}");
		#endregion


		#region HELPER FUNCTIONS
		private static bool FilterTopic(Enum topic)
		{
			// For performance/security, we ignore all non-critical logs for release builds.
#if DEVELOPMENT_BUILD || UNITY_EDITOR
			if (topics == null)
			{
				var path = "Log/LogTopics";
				topics = Resources.Load<LogTopics>(path);
				if (topics == null)
				{
					Warning(nameof(Log), $"A log topics asset must be created in your project at Resource/{path}.asset.");
					return true;
				}
			}

			return topics.HasEnabled(topic);
#else
			return false;
#endif
		}
		#endregion
	}
}
