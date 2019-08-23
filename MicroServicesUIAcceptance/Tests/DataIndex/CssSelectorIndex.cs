namespace MicroServicesUIAcceptance.Tests.DataIndex
{
    public sealed class CssSelectorIndex
    {
        // Singleton Pattern Object
        private static readonly CssSelectorIndex instance = new CssSelectorIndex();

        // Selector Variable Strings here

        // Singleton Class Definition
        static CssSelectorIndex()
        {
        }
        private CssSelectorIndex()
        {
        }
        public static CssSelectorIndex Instance
        {
            get
            {
                return instance;
            }
        }

        // CssSelector Return Methods
    }
}
