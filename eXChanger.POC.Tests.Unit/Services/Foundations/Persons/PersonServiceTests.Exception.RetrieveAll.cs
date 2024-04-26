using eXChanger.POC.Models.Foundations.Persons.Exceptions;
using Microsoft.Data.SqlClient;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace eXChanger.POC.Tests.Unit.Services.Foundations.Persons
{
	public partial class PersonServiceTests
	{
		[Fact]
		public void ShouldThrowExceptionOnRetrieveAllIfSqlErrorOccured()
		{
			// given
			SqlException sqlException = GetSqlError();

			var failedPersonStorageException =
				new FailedPersonStorageException(sqlException);

			var PersonDependencyException =
				new PersonDependencyException(failedPersonStorageException);

			this.storageBrokerMock.Setup(broker =>
				broker.SelectAllPersons()).Throws(sqlException);

			// when 
			Action retrieveAllPersonsAction = () => this.personService.RetrieveAllPersons();

			// then
			Assert.Throws<PersonDependencyException>(retrieveAllPersonsAction);

			this.storageBrokerMock.Verify(broker =>
				broker.SelectAllPersons(), Times.Once());

			this.loggingBrokerMock.Verify(broker =>
				broker.LogCritical(It.Is(SameExceptionAs(PersonDependencyException))),
					Times.Once());

			this.loggingBrokerMock.VerifyNoOtherCalls();
			this.storageBrokerMock.VerifyNoOtherCalls();
		}

		[Fact]
		public void ShouldThowExceptionOnRetrieveAllIfServiceErrorOccured()
		{
			// given
			var exception = new Exception();

			var failedPersonServiceException =
				new FailedPersonServiceException(exception);

			PersonDependencyServiceException PersonDependencyServiceException =
				new PersonDependencyServiceException(failedPersonServiceException);

			this.storageBrokerMock.Setup(broker =>
				broker.SelectAllPersons()).Throws(exception);

			// when & then
			Assert.Throws<PersonDependencyServiceException>(() =>
				this.personService.RetrieveAllPersons());

			this.storageBrokerMock.Verify(broker =>
				broker.SelectAllPersons(), Times.Once());

			this.loggingBrokerMock.Verify(broker =>
				broker.LogCritical(It.Is(SameExceptionAs(PersonDependencyServiceException))),
					Times.Once());

			this.loggingBrokerMock.VerifyNoOtherCalls();
			this.storageBrokerMock.VerifyNoOtherCalls();
		}
	}
}
