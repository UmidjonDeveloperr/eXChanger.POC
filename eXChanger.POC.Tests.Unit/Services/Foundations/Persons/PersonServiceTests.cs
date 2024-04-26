﻿using eXChanger.POC.Brokers.Loggings;
using eXChanger.POC.Brokers.Storages;
using eXChanger.POC.Models.Foundations.Persons;
using eXChanger.POC.Services.Foundations.Persons;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Tynamix.ObjectFiller;
using Xeptions;
using Xunit.Abstractions;

namespace eXChanger.POC.Tests.Unit.Services.Foundations.Persons
{
	public partial class PersonServiceTests
	{
		private readonly Mock<IStorageBroker> storageBrokerMock;
		private readonly Mock<ILoggingBroker> loggingBrokerMock;
		private readonly IPersonService personService;

		public PersonServiceTests()
		{
			this.storageBrokerMock = new Mock<IStorageBroker>();
			this.loggingBrokerMock = new Mock<ILoggingBroker>();

			this.personService =
				new PersonService(storageBroker: this.storageBrokerMock.Object,
				loggingBroker: this.loggingBrokerMock.Object);
		}

		private Expression<Func<Xeption, bool>> SameExceptionAs(Xeption expectedException) =>
			actualException => actualException.SameExceptionAs(expectedException);

		private static Person CreateRandomPerson() =>
			CreatePersonFiller(date: GetRandomDateTimeOffset).Create();

		private static DateTimeOffset GetRandomDateTimeOffset =>
			new DateTimeRange(earliestDate: new DateTime()).GetValue();

		private static Filler<Person> CreatePersonFiller(DateTimeOffset date)
		{
			var filler = new Filler<Person>();
			filler.Setup()
				.OnType<DateTimeOffset>().Use(date);

			return filler;
		}

	}
}
