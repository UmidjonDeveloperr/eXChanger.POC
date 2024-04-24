using System.Threading.Tasks;
using eXChanger.POC.Models.Foundations.Pets;

namespace eXChanger.POC.Services.Processings.Pets
{
    public interface IPetProcessingService
    {
        ValueTask<Pet> UpsertPetAsync(Pet pet);
    }
}