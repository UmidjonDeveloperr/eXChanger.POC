using System;
using System.Linq;
using System.Threading.Tasks;
using eXChanger.POC.Api.Brokers.Loggings;
using eXChanger.POC.Brokers.Storages;
using eXChanger.POC.Models.Foundations.Persons;

namespace eXChanger.POC.Services.Foundations.Persons
{
    public partial class PersonService : IPersonService
    {
        private readonly IStorageBroker storageBroker;
        private readonly ILoggingBroker loggingBroker;

		public PersonService(IStorageBroker storageBroker, ILoggingBroker loggingBroker)
		{
			this.storageBroker = storageBroker;
			this.loggingBroker = loggingBroker;
		}

		public ValueTask<Person> AddPersonAsync(Person Person) =>
			TryCatch(async () =>
			{
				ValidatePersonOnAdd(Person);

				return await this.storageBroker.AddPersonAsync(Person);
			});

		public IQueryable<Person> RetrieveAllPersons() =>
			TryCatch(() => this.storageBroker.SelectAllPersons());

		public IQueryable<Person> RetrieveAllPersonsWithPets() =>
            TryCatch(() => this.storageBroker.SelectAllPersonsWithPets());

		public ValueTask<Person> UpdatePersonAsync(Person Person) =>
			TryCatch(async () =>
			{
				ValidatePersonOnModify(Person);

				return await this.storageBroker.UpdatePersonAsync(Person);
			});
	}
}
