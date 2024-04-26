using EFxceptions.Models.Exceptions;
using Microsoft.Data.SqlClient;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;
using Xunit;
using eXChanger.POC.Models.Foundations.Persons;
using eXChanger.POC.Models.Foundations.Persons.Exceptions;

namespace eXChanger.POC.Tests.Unit.Services.Foundations.Persons
{
	public partial class PersonServiceTests
	{
		[Fact]
		public async Task ShouldThrowCriticalDependencyExceptionOnAddIfSqlErrorOccursAndLogItAsync()
		{
			// given
			Person somePerson = CreateRandomPerson();
			SqlException sqlException = GetSqlError();
			FailedPersonStorageException failedPersonStorageException = new(sqlException);

			PersonDependencyException expectedPersonDependencyException =
				new(failedPersonStorageException);

			this.storageBrokerMock.Setup(broker =>
				broker.AddPersonAsync(somePerson))
				.ThrowsAsync(sqlException);

			// when
			ValueTask<Person> AddPersonTask =
				this.personService.AddPersonAsync(somePerson);

			// then
			await Assert.ThrowsAsync<PersonDependencyException>(() => AddPersonTask.AsTask());

			this.storageBrokerMock.Verify(broker =>
				broker.AddPersonAsync(somePerson),
				Times.Once());

			this.loggingBrokerMock.Verify(broker =>
				broker.LogCritical(It.Is(SameExceptionAs(expectedPersonDependencyException))),
				Times.Once());

			this.storageBrokerMock.VerifyNoOtherCalls();
			this.loggingBrokerMock.VerifyNoOtherCalls();
		}
		[Fact]
		public async Task ShouldThrowExceptiononAddIfDuplicateKeyErrorOccurs()
		{
			// given
			Person somePerson = CreateRandomPerson();
			string someString = GetRandomString();

			var duplicateKeyException = new DuplicateKeyException(someString);

			var alreadyExistPersonException =
				new AlreadyExistPersonException(duplicateKeyException);

			var PersonDependencyValidationException =
				new PersonDependencyValidationException(alreadyExistPersonException);

			this.storageBrokerMock.Setup(broker =>
				broker.AddPersonAsync(somePerson))
				.ThrowsAsync(duplicateKeyException);

			// when 
			ValueTask<Person> AddPersonTask =
				this.personService.AddPersonAsync(somePerson);

			// then
			await Assert.ThrowsAnyAsync<PersonDependencyValidationException>(() =>
			   AddPersonTask.AsTask());

			this.storageBrokerMock.Verify(broker =>
				broker.AddPersonAsync(somePerson),
				Times.Once());

			this.loggingBrokerMock.Verify(broker =>
			   broker.LogError(It.Is(SameExceptionAs(PersonDependencyValidationException))),
			   Times.Once());

			this.loggingBrokerMock.VerifyNoOtherCalls();
			this.storageBrokerMock.VerifyNoOtherCalls();
		}
		[Fact]
		public async Task ShouldThrowExceptionOnAddIfServiceErrorOccurs()
		{
			// given
			Person somePerson = CreateRandomPerson();
			var exception = new Exception();

			var failedPersonServiceException =
				new FailedPersonServiceException(exception);

			var PersonDependencyServiceException =
				new PersonDependencyServiceException(failedPersonServiceException);

			this.storageBrokerMock.Setup(broker =>
				broker.AddPersonAsync(somePerson))
				.ThrowsAsync(exception);

			// when
			ValueTask<Person> AddPersonTask =
				this.personService.AddPersonAsync(somePerson);

			// then
			await Assert.ThrowsAsync<PersonDependencyServiceException>(() =>
				AddPersonTask.AsTask());

			this.storageBrokerMock.Verify(broker =>
				broker.AddPersonAsync(somePerson),
				Times.Once());

			this.loggingBrokerMock.Verify(broker =>
				broker.LogCritical(It.Is(SameExceptionAs(PersonDependencyServiceException))),
				Times.Once());

			this.storageBrokerMock.VerifyNoOtherCalls();
			this.loggingBrokerMock.VerifyNoOtherCalls();
		}
	}
}
