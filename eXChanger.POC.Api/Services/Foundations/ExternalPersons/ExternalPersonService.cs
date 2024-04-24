using System.Collections.Generic;
using System.Threading.Tasks;
using eXChanger.POC.Brokers.Sheets;
using eXChanger.POC.Models.Foundations.ExternalPersons;

namespace eXChanger.POC.Services.Foundations.ExternalPersons
{
    public class ExternalPersonService : IExternalPersonService
    {
        private readonly ISheetBroker sheetBroker;

        public ExternalPersonService(ISheetBroker sheetBroker) =>
            this.sheetBroker = sheetBroker;

        public async ValueTask<List<ExternalPerson>> RetrieveAllExternalPersonsAsync() =>
            await this.sheetBroker.ReadAllExternalPersonsAsync();
    }
}
