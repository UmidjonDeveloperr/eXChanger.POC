using Xeptions;

namespace eXChanger.POC.Models.Foundations.Persons.Exceptions
{
	public class PersonDependencyServiceException : Xeption
	{
		public PersonDependencyServiceException(Xeption innerExpection)
			: base(message: "Unexpected service error occured. Contact support",
				  innerExpection)
		{ }
	}
}
