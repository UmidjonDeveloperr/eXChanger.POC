using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using eXChanger.POC.Models.Foundations.Pets;

namespace eXChanger.POC.Models.Foundations.Persons
{
    public class Person
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

        [JsonIgnore]
        public List<Pet> Pets { get; set; }
    }
}
