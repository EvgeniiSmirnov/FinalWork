namespace FinalWork.Models.UI;

public class Suite(string suiteName)
{
    public string SuiteName { get; } = suiteName;

    public class SuiteBuilder
    {
        public string SuiteName { get; set; }

        public SuiteBuilder SetSuiteName(string suiteName)
        {
            SuiteName = suiteName;
            return this;
        }

        public Suite Build()
        {
            if (string.IsNullOrWhiteSpace(SuiteName))
                throw new InvalidOperationException("SuiteName can not be null");

            return new Suite(SuiteName);
        }
    }
}