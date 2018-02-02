using UnityEngine;

namespace Utilities
{
    public static class LogContext
    {
        public static void LogFormat(object context, string message)
        {
            Debug.LogFormat("[{0}] {1}", context.GetType().Name, message);
        }

		public static void LogWarningFormat(object context, string message)
		{
			Debug.LogWarningFormat("[{0}] {1}", context.GetType().Name, message);
		}

		public static void LogErrorFormat(object context, string message)
        {
            Debug.LogErrorFormat("[{0}] {1}", context.GetType().Name, message);
        }
	}
}
