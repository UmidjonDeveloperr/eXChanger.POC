using Xeptions;

namespace eXChanger.POC.Models.Foundations.Persons.Exceptions
{
	public class PersonDependencyValidationException : Xeption
	{
		public PersonDependencyValidationException(Xeption innerException)
			: base(message: "Person dependency error occured. Fix errors and try again",
				  innerException)
		{ }
	}
}
