using UnityEngine;

namespace Utilities
{
	/// <summary>
	/// A wrapper for <see cref="Debug"/> logging that helps with
	/// adds some additional functionality for convenience and nicety.
	/// </summary>
    public static class Log
    {
		#region PUBLIC API
		public static void Info(object context, string message)
			=> Info(context.GetType().Name, message);

		public static void Info(string context, string message)
			=> Debug.Log($"[{context}] {message}");

		public static void Warning(string context, string message)
			=> Warning(context, message);

		public static void Warning(object context, string message)
            => Debug.LogWarning($"[{context.GetType().Name}] {message}");

		public static void Error(string context, string message)
			=> Error(context, message);

		public static void Error(object context, string message)
            => Debug.LogError($"[{context.GetType().Name}] {message}");

		public static void Assertion(string context, string message)
			=> Assertion(context, message);

		public static void Assertion(object context, string message)
            => Debug.LogAssertion($"[{context.GetType().Name}] {message}");

		public static void Assert(string context, bool condition, string message)
			=> Assert(context, condition, message);

		public static void Assert(object context, bool condition, string message)
            => Debug.Assert(condition, $"[{context.GetType().Name}] {message}");
		#endregion
	}
}
