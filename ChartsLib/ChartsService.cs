using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChartsLib
{
    public class ChartsService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Highcharts.Models.HighchartLineBasic.HighchartLineBasic CreateLineChart()
        {
            var model = new Highcharts.Models.HighchartLineBasic.HighchartLineBasic();
            return model;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Highcharts.Models.HighchartAreaStackedPercent.HighchartAreaStackedPercent CreateAreaChart()
        {
            var model = new Highcharts.Models.HighchartAreaStackedPercent.HighchartAreaStackedPercent();
            return model;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Highcharts.Models.HighchartBarBasic.HighchartBarBasic CreateBarChart()
        {
            var model = new Highcharts.Models.HighchartBarBasic.HighchartBarBasic();
            return model;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Highcharts.Models.HighchartPieDrilldown.HighchartPieDrilldown CreatePieChart()
        {
            var model = new Highcharts.Models.HighchartPieDrilldown.HighchartPieDrilldown();
            return model;
        }
    }
}
