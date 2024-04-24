using System.Linq;
using System.Threading.Tasks;
using eXChanger.POC.Models.Foundations.Pets;

namespace eXChanger.POC.Services.Foundations.Pets
{
    public interface IPetService
    {
        ValueTask<Pet> AddPetAsync(Pet pet);
        IQueryable<Pet> RetrieveAllPets();
        ValueTask<Pet> UpdatePetAsync(Pet pet);
    }
}