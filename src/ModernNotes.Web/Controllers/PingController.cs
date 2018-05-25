using Microsoft.AspNetCore.Mvc;

namespace ModernNotes.Web.Controllers
{
	/// <summary>
	/// Ping
	/// </summary>
    [Produces("text/plain")]
    [Route("api/Ping")]
    public class PingController : Controller
	{

		/// <summary>
		/// Ping service to check connection and that the service is alive.
		/// </summary>
		/// <returns>"OK" as a plain text string</returns>
		[HttpGet]
		[ProducesResponseType(typeof(string), 200)]
		public string Ping()
		{
			return "OK";
		}
	}
}