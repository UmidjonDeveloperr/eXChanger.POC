using System.Threading.Tasks;
using eXChanger.POC.Models.Foundations.Persons;

namespace eXChanger.POC.Services.Processings.Persons
{
    public interface IPersonProcessingService
    {
        ValueTask<Person> UpsertPersonAsync(Person person);
    }
}
