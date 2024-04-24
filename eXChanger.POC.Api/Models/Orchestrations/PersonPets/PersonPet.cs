using System.Collections.Generic;
using eXChanger.POC.Models.Foundations.Persons;
using eXChanger.POC.Models.Foundations.Pets;

namespace eXChanger.POC.Models.Orchestrations.PersonPets
{
    public class PersonPet
    {
        public Person Person { get; set; }
        public List<Pet> Pets { get; set; }
    }
}
