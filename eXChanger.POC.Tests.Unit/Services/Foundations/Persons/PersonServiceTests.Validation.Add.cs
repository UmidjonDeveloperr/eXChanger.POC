using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;
using Xunit;
using eXChanger.POC.Models.Foundations.Persons;
using eXChanger.POC.Api.Models.Foundations.Persons.Exceptions;

namespace eXChanger.POC.Tests.Unit.Services.Foundations.Persons
{
	public partial class PersonServiceTests
	{
		[Fact]
		public async Task ShouldThrowValidationExceptionOnAddIfPersonIsNullAndLogItAsync()
		{
			// given
			Person nullPerson = null;
			NullPersonException nullPersonException = new();

			PersonValidationException expectedPersonValidationException =
				new(nullPersonException);

			// when
			ValueTask<Person> addPersonTask =
				this.personService.AddPersonAsync(nullPerson);

			// then
			await Assert.ThrowsAsync<PersonValidationException>(() =>
			  addPersonTask.AsTask());

			this.loggingBrokerMock.Verify(broker =>
				broker.LogError(It.Is(SameExceptionAs(expectedPersonValidationException))),
				Times.Once());
			this.storageBrokerMock.Verify(broker =>
			 broker.AddPersonAsync(It.IsAny<Person>()), Times.Never);

			this.loggingBrokerMock.VerifyNoOtherCalls();
			this.storageBrokerMock.VerifyNoOtherCalls();
		}
	}
}
