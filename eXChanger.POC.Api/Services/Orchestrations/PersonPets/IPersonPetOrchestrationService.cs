using System.Threading.Tasks;
using eXChanger.POC.Models.Orchestrations.PersonPets;

namespace eXChanger.POC.Services.Orchestrations.PersonPets
{
    public interface IPersonPetOrchestrationService
    {
        ValueTask<PersonPet> ProcessPersonWithPetsAsync(PersonPet personPet);
    }
}
