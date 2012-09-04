namespace CheesyShakespeareCodeMonkeyCrossovers
{
    public class ASummersDay : System.IComparable
    {
        public ASummersDay()
        {
        }

        int System.IComparable.CompareTo(object obj)
        {
            return "A Summer's Day".CompareTo(obj.ToString());
        }
    }
}