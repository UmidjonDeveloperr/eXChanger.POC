using System.Collections.Generic;
using System.Threading.Tasks;
using eXChanger.POC.Models.Foundations.ExternalPersons;

namespace eXChanger.POC.Services.Orchestrations.ExternalPersons
{
    public interface IExternalPersonOrchestrationService
    {
        ValueTask<List<ExternalPerson>> RetrieveFormattedExternalPersonsAsync();
    }
}