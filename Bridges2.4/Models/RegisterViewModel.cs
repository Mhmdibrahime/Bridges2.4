using System.ComponentModel.DataAnnotations;

namespace Bridges2._4.Models
{
	public class RegisterViewModel
	{
		public string FName { get; set; }
		public string LName { get; set; }

		[DataType(DataType.EmailAddress)]
		public string EMail { get; set; }

		
		public string PhoneNumber { get; set; }

		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Compare("Password")]
		public string ComparePassword { get; set; }
	}
}
