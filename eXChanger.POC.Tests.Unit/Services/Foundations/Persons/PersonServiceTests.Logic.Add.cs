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
		public async Task ShouldAddPersonAsync()
		{
			// given
			Person randomPerson = CreateRandomPerson();
			Person inputPerson = randomPerson;
			Person storagePerson = inputPerson;
			Person expectedPerson = storagePerson.DeepClone();

			this.storageBrokerMock.Setup(broker =>
			  broker.AddPersonAsync(inputPerson))
				.ReturnsAsync(expectedPerson);

			// when 
			Person actualPerson =
				await this.personService.AddPersonAsync(inputPerson);

			// then
			actualPerson.Should().BeEquivalentTo(expectedPerson);
			this.storageBrokerMock.Verify(broker =>
			  broker.AddPersonAsync(It.IsAny<Person>()), Times.Once());

			this.storageBrokerMock.VerifyNoOtherCalls();
			this.loggingBrokerMock.VerifyNoOtherCalls();
		}
	}
}
