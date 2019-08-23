namespace MicroServicesUIAcceptance.SaaSTests.DataIndex
{
    public sealed class TextIndex
    {
        // Singleton Pattern Object
        private static readonly TextIndex instance = new TextIndex();

        // Text Variable Strings here

        // Singleton Class Definition
        static TextIndex()
        {
        }
        private TextIndex()
        {
        }
        public static TextIndex Instance
        {
            get
            {
                return instance;
            }
        }

        // TextIndex Return Methods
    }
}
