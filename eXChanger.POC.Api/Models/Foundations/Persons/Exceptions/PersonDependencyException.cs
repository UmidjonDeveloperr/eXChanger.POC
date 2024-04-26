using Xeptions;

namespace eXChanger.POC.Models.Foundations.Persons.Exceptions
{
	public class PersonDependencyException : Xeption
	{
		public PersonDependencyException(Xeption innerException)
			: base(message: "Person dependency error occured. Contact support",
				  innerException)
		{ }
	}
}
