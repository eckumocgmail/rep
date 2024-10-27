using System.Collections.Generic;
using System.Linq;
using Console_AuthModel;
using Console_AuthModel.BusinessAnaliticsServices;

namespace Console_AuthModel.BusinessAnaliticsServices
{
    public class BusinessUnit : TestingElement
    {
        public override void OnTest()
        {
            BusinessIndicator p = null;
            using (var db = new BusinessDataModel())
            {
                var fasade = new EntityFasade<BusinessIndicator>(null);
                int id = fasade.Create(p = new BusinessIndicator()
                {
                    Name = "A"
                });
                IEnumerable<BusinessIndicator> indicators = fasade.Get(p.Id);
                indicators.ToJsonOnScreen().WriteToConsole();
                var indicator = indicators.ToList().First();
                indicator.Description = "test";
                fasade.Update(indicator);
                fasade.Remove(id);
                fasade.Search(" A ");

            }
        }
    }
}