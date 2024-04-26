using eXChanger.POC.Api.Brokers.Loggings;
using eXChanger.POC.Brokers.Storages;
using eXChanger.POC.Services.Foundations.Persons;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Xeptions;

namespace eXChanger.POC.Tests.Unit.Services.Foundations.Persons
{
	public partial class PersonServiceTests
	{
		private readonly Mock<IStorageBroker> storageBrokerMock;
		private readonly Mock<ILoggingBroker> loggingBrokerMock;
		private readonly IPersonService personService;

		public PersonServiceTests(Mock<IStorageBroker> storageBrokerMock, Mock<ILoggingBroker> loggingBrokerMock, IPersonService personService)
		{
			this.storageBrokerMock = storageBrokerMock;
			this.loggingBrokerMock = loggingBrokerMock;
			this.personService = personService;
		}

		private Expression<Func<Xeption, bool>> SameExceptionAs(Xeption expectedException) =>
			actualException => actualException.SameExceptionAs(expectedException);
		
	}
}
