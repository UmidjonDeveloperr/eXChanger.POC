using Xeptions;

namespace eXChanger.POC.Api.Models.Foundations.Persons.Exceptions
{
	public class NullPersonException : Xeption
	{
        public NullPersonException()
            : base(message: "Person is null")
        { }
    }
}
