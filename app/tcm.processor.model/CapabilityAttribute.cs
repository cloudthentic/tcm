namespace tcm.processor.model
{
    public class CapabilityAttribute
    {
        public string Name { get; set; }

        public string Value { get; set; }

        public CapabilityAttribute(string name, string value)
        {
            Name = name;
            Value = value;
        }
    }
}