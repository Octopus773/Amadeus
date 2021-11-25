using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Amadeus.Server.Views.Secure
{
	[ApiController]
	[Route("/secure")]
	[Authorize]
	public class SecureView : ControllerBase
	{
		[HttpGet]
		public string idk()
		{
			return "Hello world!";
		}
	}
}
