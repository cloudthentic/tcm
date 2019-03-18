using System;
using System.Collections.Generic;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;
using tcm.processor.model;

namespace tcm.processor.app
{
    public class Processor
    {
        TelemetryClient telemetry;
        string storageConnection;

        public Processor(string connection)
        {
            TelemetryConfiguration config = new TelemetryConfiguration("362767d6-2f0d-48cc-b789-ae478063e59f");
            telemetry = new TelemetryClient(config);
            storageConnection = connection;
        }

        public IList<ProductAggregate> ProcessAll(string path)
        {
            try
            {
                using (var telemetryOperation = telemetry.StartOperation<RequestTelemetry>("Processor.ProcessAll"))
                {

                    var fsa = new adapter.fileSystem.FileSystemReaderAdapter();

                    var productAggregateList = fsa.ParseFileSystemWithProductHiearchy(path);
                    var tsa = new adapter.tableStorage.TableStorageWritterAdapter(storageConnection);
                    tsa.WriteProductAggregateListToTableStorage(productAggregateList);
                    telemetry.TrackEvent("Processor.ProcessAll Completed");
                    return productAggregateList;
                }
            }
            catch (Exception ex)
            {
                telemetry.TrackException(ex);
                throw ex;
            }
            finally
            {
                telemetry.Flush();
            }
        }
    }
}
