using System.Collections.Generic;
using System.Threading.Tasks;
using eXChanger.POC.Models.Foundations.ExternalPersons;

namespace eXChanger.POC.Services.Foundations.ExternalPersons
{
    public interface IExternalPersonService
    {
        ValueTask<List<ExternalPerson>> RetrieveAllExternalPersonsAsync();
    }
}
