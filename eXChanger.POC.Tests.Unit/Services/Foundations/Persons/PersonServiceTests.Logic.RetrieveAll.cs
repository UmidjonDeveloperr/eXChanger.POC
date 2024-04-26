using eXChanger.POC.Models.Foundations.Persons;
using FluentAssertions;
using Force.DeepCloner;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace eXChanger.POC.Tests.Unit.Services.Foundations.Persons
{
	public partial class PersonServiceTests
	{
		[Fact]
		public void ShouldRetrieveAllPersons()
		{
			// given
			IQueryable<Person> randomPersons = CreateRandomPersons();
			IQueryable<Person> storagePersons = CreateRandomPersons();
			IQueryable<Person> expectedPersons = storagePersons.DeepClone();

			this.storageBrokerMock.Setup(broker =>
				broker.SelectAllPersons()).Returns(storagePersons);

			// when
			IQueryable<Person> ActualPersons =
				this.personService.RetrieveAllPersons();

			// then 
			ActualPersons.Should().BeEquivalentTo(expectedPersons);

			this.storageBrokerMock.Verify(broker =>
				broker.SelectAllPersons(), Times.Once());

			this.loggingBrokerMock.VerifyNoOtherCalls();
			this.storageBrokerMock.VerifyNoOtherCalls();
		}
	}
}
