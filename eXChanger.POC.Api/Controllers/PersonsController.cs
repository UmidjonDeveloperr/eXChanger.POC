using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;
using eXChanger.POC.Models.Orchestrations.PersonPets;
using eXChanger.POC.Services.Coordinations;
using eXChanger.POC.Services.Foundations.Persons;

namespace eXChanger.POC.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class PersonsController : RESTFulController
    {
        private readonly IExternalPersonWithPetsCoordinationService externalPersonWithPetsCoordinationService;
        private readonly IPersonService personService;

        public PersonsController(
            IExternalPersonWithPetsCoordinationService externalPersonWithPetsCoordinationService,
            IPersonService personService)
        {
            this.externalPersonWithPetsCoordinationService = externalPersonWithPetsCoordinationService;
            this.personService = personService;
        }

        [HttpGet]
        public async ValueTask<ActionResult<List<PersonPet>>> GetStoredPersons()
        {
            var personPets = await externalPersonWithPetsCoordinationService
                .CoordinateExternalPersons();

            string xmlString = externalPersonWithPetsCoordinationService.SerializeToXml(personPets);

            return File(Encoding.UTF8.GetBytes(xmlString), "application/xml", "personpets/xml");
        }

		[HttpGet]
        public ActionResult<IQueryable<PersonPet>> GetAllPersonsWithPets() =>
            Ok(this.personService.RetrieveAllPersonsWithPets());
    }
}
