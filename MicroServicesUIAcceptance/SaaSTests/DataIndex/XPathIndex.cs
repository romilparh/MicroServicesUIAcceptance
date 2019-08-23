namespace MicroServicesUIAcceptance.SaaSTests.DataIndex
{
    public sealed class XPathIndex
    {
        // Singleton Pattern Object
        private static readonly XPathIndex instance = new XPathIndex();

        // XPath Variable Strings here
        private static readonly string demoXPath = "";

        // Singleton Class Definition
        static XPathIndex()
        {
        }
        private XPathIndex()
        {
        }
        public static XPathIndex Instance
        {
            get
            {
                return instance;
            }
        }

        // XPath Return Methods
        public string Return_demoXPath(){
            return this.demoXPath;
        }
    }
}
