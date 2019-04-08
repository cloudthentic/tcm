using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Azure.KeyVault;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using System;
using System.Collections.Generic;
using System.Text;

namespace tcm.processor.adapter.tableStorage
{
    public class TableStorageAdapterBase
    {
        protected CloudStorageAccount storageAccount;
        protected TelemetryClient telemetry;
        protected string tableName = "tcmapp";

        public TableStorageAdapterBase(string connection)
        {
            storageAccount = CloudStorageAccount.Parse(connection);
            TelemetryConfiguration config = new TelemetryConfiguration("362767d6-2f0d-48cc-b789-ae478063e59f");
            telemetry = new TelemetryClient(config);
        }


    }
}
