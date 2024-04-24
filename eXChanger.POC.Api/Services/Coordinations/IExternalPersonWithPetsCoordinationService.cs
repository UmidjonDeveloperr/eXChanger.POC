using System.Collections.Generic;
using System.Threading.Tasks;
using eXChanger.POC.Models.Orchestrations.PersonPets;

namespace eXChanger.POC.Services.Coordinations
{
    public interface IExternalPersonWithPetsCoordinationService
    {
        ValueTask<List<PersonPet>> CoordinateExternalPersons();
		string SerializeToXml(List<PersonPet> personPets);
    }
}
