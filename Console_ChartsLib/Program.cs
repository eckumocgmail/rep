using Highcharts.Models.HighchartLineBasic;

namespace ChartsLib
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(new HighchartLineBasic().ToJsonOnScreen());
        }
    }
}