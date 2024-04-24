using System.Collections.Generic;
using System.Threading.Tasks;
using eXChanger.POC.Models.Foundations.ExternalPersons;

namespace eXChanger.POC.Brokers.Sheets
{
    public partial interface ISheetBroker
    {
        ValueTask<List<ExternalPerson>> ReadAllExternalPersonsAsync();
    }
}
