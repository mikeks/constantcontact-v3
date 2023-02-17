using CTCT.Util;
using System;

namespace CTCT.Exceptions
{
    /// <summary>
    /// General exception.
    /// </summary>
    public class CtctException : Exception
    {
		/// <summary>
		/// Response
		/// </summary>
        public CUrlResponse Response { get; }

		/// <summary>
		/// Class constructor.
		/// </summary>
		/// <param name="response">REST API response</param>
		protected CtctException(CUrlResponse response) : base(response.GetErrorMessage()) {
            Response = response;
		}

		/// <summary>
		/// Factory method
		/// </summary>
		/// <param name="response">REST API Response</param>
		/// <returns>Exception</returns>
		public static CtctException Create(CUrlResponse response)
		{
			return response.IsAuthError ? new CtctAuthException(response) : new CtctException(response);
		}

    }

	/// <summary>
	/// Authentication exception
	/// </summary>
	public class CtctAuthException : CtctException
	{
		internal CtctAuthException(CUrlResponse response) : base(response)
		{
		}
	}

}
