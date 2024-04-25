using Xeptions;

	namespace eXChanger.POC.Api.Models.Foundations.Persons.Exceptions
	{
	public class PersonValidationException : Xeption
	{
		public PersonValidationException(Xeption innerException)
			: base(message: "Person validation error occured, fix the errors and try again",
				  innerException)
		{ }
	}
}
