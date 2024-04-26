using System;
using Xeptions;

namespace eXChanger.POC.Models.Foundations.Persons.Exceptions
{
	public class FailedPersonServiceException : Xeption
	{
		public FailedPersonServiceException(Exception innerException)
			: base(message: "Unexpected error of person service occured",
				  innerException)
		{ }
	}
}
