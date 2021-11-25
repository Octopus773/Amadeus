using System;

namespace Amadeus.Server.Exceptions
{
	/// <summary>
	/// Exception through when a db fail to create a record due to an unexpected unique field.
	/// </summary>
	public class DuplicateField : Exception
	{
		/// <summary>
		/// ctor.
		/// </summary>
		/// <param name="message">The message explaining the error.</param>
		public DuplicateField(string message)
		: base(message)
		{
		}
	}
}
