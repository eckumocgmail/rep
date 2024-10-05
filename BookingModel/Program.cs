namespace BookingModel
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BookingInitializer.InitData();
            BookingUnit.Test();
        }
    }
}