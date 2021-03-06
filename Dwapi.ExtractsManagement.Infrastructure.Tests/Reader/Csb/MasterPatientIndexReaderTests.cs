﻿using System;
using System.Data.SqlClient;
using System.Linq;
using Dwapi.ExtractsManagement.Core.Interfaces.Reader.Cbs;
using Dwapi.ExtractsManagement.Core.Model.Source.Cbs;
using Dwapi.SettingsManagement.Infrastructure;
using Dwapi.SharedKernel.Model;
using Dwapi.SharedKernel.Utility;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MySql.Data.MySqlClient;
using NUnit.Framework;

namespace Dwapi.ExtractsManagement.Infrastructure.Tests.Reader.Csb
{
    [TestFixture]
    [Category("Cbs")]
    public class MasterPatientIndexReaderTests
    {
        private SettingsContext _settingsContext;
        private SettingsContext _settingsContextMysql;
        private DbProtocol _iQtoolsDb, _kenyaEmrDb;
        
        private IMasterPatientIndexReader _reader;

        [OneTimeSetUp]
        public void Init()
        {
            _settingsContext = TestInitializer.ServiceProvider.GetService<SettingsContext>();
            _settingsContextMysql = TestInitializer.ServiceProviderMysql.GetService<SettingsContext>();
            _iQtoolsDb = TestInitializer.IQtoolsDbProtocol;
            _kenyaEmrDb = TestInitializer.KenyaEmrDbProtocol;
        }


        [Test]
        public void should_Execute_Reader_MsSql()
        {
            var extract = TestInitializer.Iqtools.Extracts.First(x => x.DocketId.IsSameAs("CBS"));

            _reader = TestInitializer.ServiceProvider.GetService<IMasterPatientIndexReader>();
            var reader = _reader.ExecuteReader(_iQtoolsDb, extract).Result as SqlDataReader;
            Assert.NotNull(reader);
            Assert.True(reader.HasRows);
            reader.Close();
        }

        [Test]
        public void should_Execute_Reader_MySql()
        {
            var extract = TestInitializer.KenyaEmr.Extracts.First(x => x.DocketId.IsSameAs("CBS"));

            _reader = TestInitializer.ServiceProviderMysql.GetService<IMasterPatientIndexReader>();
            var reader = _reader.ExecuteReader(_kenyaEmrDb, extract).Result as MySqlDataReader;
            Assert.NotNull(reader);
            Assert.True(reader.HasRows);
            reader.Close();
        }   
    }
}