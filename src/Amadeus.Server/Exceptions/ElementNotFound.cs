using System;

namespace Amadeus.Server.Exceptions
{
	/// <summary>
	/// Exception through when an element isn't found in a IRepository.
	/// </summary>
	public class ElementNotFound : Exception
	{
		/// <summary>
		/// ctor.
		/// </summary>
		/// <param name="message">The message explaining the error.</param>
		public ElementNotFound(string message)
			: base(message)
		{
		}
	}
}
