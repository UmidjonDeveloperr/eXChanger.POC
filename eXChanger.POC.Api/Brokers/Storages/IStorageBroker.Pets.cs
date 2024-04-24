using System.Linq;
using System.Threading.Tasks;
using eXChanger.POC.Models.Foundations.Pets;

namespace eXChanger.POC.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<Pet> AddPetAsync(Pet pet);
        IQueryable<Pet> SelectAllPets();
        ValueTask<Pet> UpdatePetAsync(Pet pet);
    }
}
