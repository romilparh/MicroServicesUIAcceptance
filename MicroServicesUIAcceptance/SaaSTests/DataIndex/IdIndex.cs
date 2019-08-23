namespace MicroServicesUIAcceptance.SaaSTests.DataIndex
{
    public sealed class IdIndex
    {
        // Singleton Pattern Object
        private static readonly IdIndex instance = new IdIndex();

        // ID Variable Strings here

        // Singleton Class Definition
        static IdIndex()
        {
        }
        private IdIndex()
        {
        }
        public static IdIndex Instance
        {
            get
            {
                return instance;
            }
        }

        // IdIndex Return Methods
    }
}
