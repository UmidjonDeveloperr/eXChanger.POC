using System.Linq;
using System.Threading.Tasks;
using eXChanger.POC.Models.Foundations.Persons;

namespace eXChanger.POC.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<Person> AddPersonAsync(Person person);
        IQueryable<Person> SelectAllPersons();
        IQueryable<Person> SelectAllPersonsWithPets();
        ValueTask<Person> UpdatePersonAsync(Person person);
    }
}
