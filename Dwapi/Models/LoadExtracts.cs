using Dwapi.ExtractsManagement.Core.Commands;
using Dwapi.ExtractsManagement.Core.Commands.Cbs;
using Dwapi.ExtractsManagement.Core.Commands.Dwh;

namespace Dwapi.Models
{
    public class LoadExtracts
    {
        public LoadFromEmrCommand LoadFromEmrCommand { get; set; }
        public ExtractMasterPatientIndex ExtractMpi { get; set; }
        public bool LoadMpi { get; set; }
    }
}
