using Console_StatisticsModel.BusinessAnaliticsServices;

using Microsoft.EntityFrameworkCore;

namespace Console_StatisticsModel
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (var db = new BusinessDataModel())
            {
                var initializer = new BusinessInitiallizer(db);
                initializer.InitData(db);                
            }
                
        }
    }
}