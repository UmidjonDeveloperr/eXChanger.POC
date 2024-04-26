using eXChanger.POC.Models.Foundations.Persons;
using eXChanger.POC.Models.Foundations.Persons.Exceptions;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Xeptions;

namespace eXChanger.POC.Services.Foundations.Persons
{
	public partial class PersonService
	{
		private delegate ValueTask<Person> ReturningPersonFunction();
		private delegate IQueryable<Person> ReturningPersonsFunction();

		private async ValueTask<Person> TryCatch(ReturningPersonFunction returningPersonFunction)
		{
			try
			{
				return await returningPersonFunction();
			}
			catch (NullPersonException nullPersonExcepsion)
			{
				throw CreateAndLogValidationException(nullPersonExcepsion);
			}
			catch (InvalidPersonException invalidPersonException)
			{
				throw CreateAndLogValidationException(invalidPersonException);
			}
			catch (SqlException sqlException)
			{
				var failedGuestStorageException =
					new FailedPersonStorageException(sqlException);

				throw CreateAndLogCriticalException(failedGuestStorageException);
			}
		}

		private IQueryable<Person> TryCatch(ReturningPersonsFunction returningPersonsFunction)
		{
			return returningPersonsFunction();
		}

		private PersonValidationException CreateAndLogValidationException(Xeption exception)
		{
			var PersonValidationException = new PersonValidationException(exception);
			this.loggingBroker.LogError(PersonValidationException);
			return PersonValidationException;
		}

		private PersonDependencyException CreateAndLogCriticalException(Xeption exception)
		{
			var PersonDependencyException = new PersonDependencyException(exception);
			this.loggingBroker.LogCritical(PersonDependencyException);
			return PersonDependencyException;
		}

		private PersonDependencyValidationException CreateAndLogDuplicateKeyException(Xeption exception)
		{
			var PersonDependencyValidationException = new PersonDependencyValidationException(exception);
			this.loggingBroker.LogError(PersonDependencyValidationException);
			return PersonDependencyValidationException;
		}

		private PersonDependencyServiceException CreateAndLogPersonDependencyServiceException(Xeption exception)
		{
			var PersonDependencyServiceException = new PersonDependencyServiceException(exception);
			this.loggingBroker.LogCritical(PersonDependencyServiceException);
			return PersonDependencyServiceException;
		}
	}
}
