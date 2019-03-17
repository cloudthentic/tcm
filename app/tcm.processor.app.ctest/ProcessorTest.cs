using System;
using Xunit;

namespace tcm.processor.app.ctest
{
    public class ProcessorTest
    {
        [Theory]
        [InlineData("../../../../../products/azure")]
        public void ProcessorAppTest(object path)
        {
            var processor = new processor.app.Processor();
            processor.ProcessAll(path as string);
        }
    }
}
