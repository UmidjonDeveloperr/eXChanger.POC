using eXChanger.POC.Models.Foundations.Persons;
using eXChanger.POC.Models.Foundations.Persons.Exceptions;
using Moq;
using Xunit;
using Xunit.Abstractions;

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

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData(" ")]
		public async Task ShouldThrowValidationExceptionOnAddIfPersonIsInvalidDataAndLogItAsync(string invalidData)
		{
			// given
			var invalidPerson = new Person()
			{
				Name = invalidData
			};

			InvalidPersonException invalidPersonException = new();

			invalidPersonException.AddData(key: nameof(Person.Id),
				values: "Id is required");

			invalidPersonException.AddData(key: nameof(Person.Name),
				values: "Text is invalid");

			invalidPersonException.AddData(key: nameof(Person.Age),
				values: "Age is required");

			var expectedPersonValidationExpected =
				new PersonValidationException(invalidPersonException);

			// when
			ValueTask<Person> addPersonTask =
			   this.personService.AddPersonAsync(invalidPerson);

			// then
			await Assert.ThrowsAsync<PersonValidationException>(() => addPersonTask.AsTask());

			this.loggingBrokerMock.Verify(broker =>
			  broker.LogError(It.Is(SameExceptionAs(expectedPersonValidationExpected))),
			  Times.Once());

			this.storageBrokerMock.Verify(broker =>
			  broker.AddPersonAsync(It.IsAny<Person>()),
			  Times.Never);

			this.loggingBrokerMock.VerifyNoOtherCalls();
			this.storageBrokerMock.VerifyNoOtherCalls();
		}

	}
}
