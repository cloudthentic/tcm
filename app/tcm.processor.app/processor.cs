using System;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;

namespace tcm.processor.app
{
    public class Processor
    {
        TelemetryClient telemetry;

        public Processor()
        {
            TelemetryConfiguration config = new TelemetryConfiguration("362767d6-2f0d-48cc-b789-ae478063e59f");
            telemetry = new TelemetryClient(config);
        }

        public void ProcessAll(string path)
        {
            try
            {
                using (var telemetryOperation = telemetry.StartOperation<RequestTelemetry>("Processor.ProcessAll"))
                {

                    var fsa = new adapter.fileSystem.FileSystemReaderAdapter();

                    var productAggregateList = fsa.ParseFileSystemWithProductHiearchy(path);
                    var tsa = new adapter.tableStorage.TableStorageWritterAdapter();
                    tsa.WriteProductAggregateListToTableStorage(productAggregateList);
                    telemetry.TrackEvent("Processor.ProcessAll Completed");
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
