using System.Collections.Generic;
using System.Threading.Tasks;
using eXChanger.POC.Models.Foundations.ExternalPersons;

namespace eXChanger.POC.Services.Processings.ExternalPersons
{
    public interface IExternalPersonProcessingService
    {
        ValueTask<List<ExternalPerson>> RetrieveFormattedExternalPersonsAsync();
    }
}