using System;

namespace Utilities.Exceptions
{
	public class NotImplementedException : Exception
	{
		public NotImplementedException() : base("This has not been implemented!") { }

		public NotImplementedException(string message) : base(message) { }
	}
}