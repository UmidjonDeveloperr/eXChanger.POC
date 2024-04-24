using System.Collections.Generic;
using System.Threading.Tasks;
using eXChanger.POC.Models.Foundations.ExternalPersons;
using eXChanger.POC.Services.Processings.ExternalPersons;

namespace eXChanger.POC.Services.Orchestrations.ExternalPersons
{
    public class ExternalPersonOrchestrationService : IExternalPersonOrchestrationService
    {
        private readonly IExternalPersonProcessingService externalPersonProcessingService;

        public ExternalPersonOrchestrationService(IExternalPersonProcessingService externalPersonProcessingService)
        {
            this.externalPersonProcessingService = externalPersonProcessingService;
        }

        public ValueTask<List<ExternalPerson>> RetrieveFormattedExternalPersonsAsync() =>
            this.externalPersonProcessingService.RetrieveFormattedExternalPersonsAsync();
    }
}
