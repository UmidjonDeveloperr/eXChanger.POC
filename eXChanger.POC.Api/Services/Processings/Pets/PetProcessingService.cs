using System.Linq;
using System.Threading.Tasks;
using eXChanger.POC.Models.Foundations.Pets;
using eXChanger.POC.Services.Foundations.Pets;

namespace eXChanger.POC.Services.Processings.Pets
{
    public class PetProcessingService : IPetProcessingService
    {
        private readonly IPetService petService;

        public PetProcessingService(IPetService petService) =>
            this.petService = petService;

        public async ValueTask<Pet> UpsertPetAsync(Pet pet)
        {
            Pet maybePet = RetrievePet(pet);

            return maybePet switch
            {
                null => await this.petService.AddPetAsync(pet),
                _ => await this.petService.UpdatePetAsync(pet)
            };
        }

        private Pet RetrievePet(Pet pet)
        {
            IQueryable<Pet> retrievedPets =
                this.petService.RetrieveAllPets();

            return retrievedPets.FirstOrDefault(storagePet =>
                storagePet.Id == pet.Id);
        }
    }
}
