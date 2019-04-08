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

        /// <summary>
        /// 
        /// </summary>
        /// <todo>
        /// 
        /// </todo>
        /// <param name="path"></param>
        /// <returns></returns>
        public IList<ProductAggregate> ProcessAll(string path)
        {
            try
            {
                using (var telemetryOperation = telemetry.StartOperation<RequestTelemetry>("Processor.ProcessAll"))
                {

                    // read all files from the file system and parse  product descriptions
                    var fsa = new adapter.fileSystem.FileSystemReaderAdapter();
                    var productAggregateList = fsa.ParseFileSystemWithProductHiearchy(path);

                    // persist product specifications to the table storage
                    var tsa = new adapter.tableStorage.TableStorageWritterAdapter(storageConnection);
                    tsa.WriteProductAggregateListToTableStorage(productAggregateList);

                    // extract capability definitions from the product hiearchy
                    var capabilitiesToCapabilityDefinitionService = new model.Services.ProductToCapabilityDefinitionAggregateService();
                    var productsToCapabilitiesAggregateService = new model.Services.ProductToCapabilityAggregateService();
                    var capabilityAggregateList = productsToCapabilitiesAggregateService.ConvertProductToCapabilityAggregate(productAggregateList);
                    var capabilityDefinitionAggregateList = capabilitiesToCapabilityDefinitionService.CapabilityAggregateListToCapabilityDefinitionAggregateService(capabilityAggregateList);

                    // persist capabilit definition aggragates to table storage
                    tsa.WriteCapabilityDefinitionAggregateListToTableStorage(capabilityDefinitionAggregateList);

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
