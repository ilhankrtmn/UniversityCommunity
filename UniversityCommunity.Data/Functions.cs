namespace UniversityCommunity.Data
{
    public class Functions
    {
        public static double RandomNumber()
        {
            Random random = new Random();
            return random.NextDouble();
        }
    }
}