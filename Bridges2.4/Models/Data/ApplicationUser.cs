using Microsoft.AspNetCore.Identity;

namespace Bridges2._4.Models.Data
{
	public class ApplicationUser:IdentityUser
	{
		public string LName { get; set; }
	}
}
