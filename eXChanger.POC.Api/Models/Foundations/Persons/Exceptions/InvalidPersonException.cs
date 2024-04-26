using Xeptions;

namespace eXChanger.POC.Models.Foundations.Persons.Exceptions
{
	public class InvalidPersonException : Xeption
	{
		public InvalidPersonException()
			: base(message: "Person is invalid")
		{ }
	}
}
