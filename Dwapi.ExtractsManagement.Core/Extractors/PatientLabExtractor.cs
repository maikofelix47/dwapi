﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using Dwapi.Domain;
using Dwapi.ExtractsManagement.Core.Commands;
using Dwapi.ExtractsManagement.Core.Services;
using Dwapi.SharedKernel.Model;
using NPoco;

namespace Dwapi.ExtractsManagement.Core.Extractors
{
    public class PatientLabExtractor : IExtractor
    {
        private readonly IExtractUnitOfWork _unitOfWork;
        private Func<IDatabase> _databaseFactory;

        public PatientLabExtractor(IExtractUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task ExtractAsync(DwhExtract extract, DbProtocol dbProtocol)
        {
            try
            {
                _databaseFactory = ExtractorHelper.NPocoEmrDataFactory(dbProtocol);

                IList<TempPatientLaboratoryExtract> tempPatientLabExtracts;
                using (var database = _databaseFactory())
                    tempPatientLabExtracts = await database.FetchAsync<TempPatientLaboratoryExtract>(extract.SqlQuery);
                await _unitOfWork.Repository<TempPatientLaboratoryExtract>().AddRangeAsync(tempPatientLabExtracts);
                await _unitOfWork.SaveAsync();
            }

            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
