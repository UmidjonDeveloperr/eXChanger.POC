using System;
using Xeptions;

namespace eXChanger.POC.Models.Foundations.Persons.Exceptions
{
	public class FailedPersonStorageException : Xeption
	{
		public FailedPersonStorageException(Exception innerException)
			: base(message: "Failed person storage error occured. Contact support",
				  innerException)
		{ }
	}
}
