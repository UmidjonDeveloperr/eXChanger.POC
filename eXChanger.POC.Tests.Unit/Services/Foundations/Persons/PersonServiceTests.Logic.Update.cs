using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;
using Xunit;
using eXChanger.POC.Models.Foundations.Persons;
using Force.DeepCloner;
using FluentAssertions;

namespace eXChanger.POC.Tests.Unit.Services.Foundations.Persons
{
	public partial class PersonServiceTests
	{
		[Fact]
		public async Task ShouldModifyPersonAsync()
		{
			// given
			Person randomPerson = CreateRandomPerson();
			Person inputPerson = randomPerson;
			Person updatedPerson = inputPerson.DeepClone();
			Person expectedPerson = inputPerson.DeepClone();

			this.storageBrokerMock.Setup(broker =>
				broker.UpdatePersonAsync(inputPerson))
				.ReturnsAsync(updatedPerson);

			// when
			Person actualPerson =
				await this.personService.UpdatePersonAsync(inputPerson);

			// then
			actualPerson.Should().BeEquivalentTo(expectedPerson);
			this.storageBrokerMock.Verify(broker =>
				broker.UpdatePersonAsync(It.IsAny<Person>()), Times.Once());

			this.storageBrokerMock.VerifyNoOtherCalls();
			this.loggingBrokerMock.VerifyNoOtherCalls();
		}
	}
}
