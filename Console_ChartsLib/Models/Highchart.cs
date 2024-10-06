
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


[Icon("insert_chart")]
public class Highchart
{

    public static string[] Types = GetTypes();
    private static string[] GetTypes()
    {
        var allTypes = new List<string>() { "Highchart3dColumnInteractive", "Highchart3dColumnNullValues", "Highchart3dColumnStackingGrouping", "Highchart3dPie", "Highchart3dPieDonut", "Highchart3dScatterDraggable", "HighchartAccessibleLine", "HighchartAccessiblePie", "HighchartAnnotations", "HighchartAreaBasic", "HighchartAreaInverted", "HighchartAreaMissing", "HighchartAreaNegative", "HighchartArearange", "HighchartArearangeLine", "HighchartAreaspline", "HighchartAreaStacked", "HighchartAreaStackedPercent", "HighchartBarBasic", "HighchartBarNegativeStack", "HighchartBarStacked", "HighchartBellcurve", "HighchartBoxPlot", "HighchartBubble", "HighchartBubble3d", "HighchartBulletGraph", "HighchartChartUpdate", "HighchartColumnBasic", "HighchartColumnComparison", "HighchartColumnDrilldown", "HighchartColumnNegative", "HighchartColumnParsed", "HighchartColumnPyramid", "HighchartColumnrange", "HighchartColumnRotatedLabels", "HighchartColumnStacked", "HighchartColumnStackedAndGrouped", "HighchartColumnStackedPercent", "HighchartCombo", "HighchartComboDualAxes", "HighchartComboMeteogram", "HighchartComboMultiAxes", "HighchartComboRegression", "HighchartController", "HighchartCylinder", "HighchartDependencyWheel", "HighchartDumbbell", "HighchartDynamicClickToAdd", "HighchartDynamicUpdate", "HighchartErrorBar", "HighchartEulerDiagram", "HighchartFlame", "HighchartFunnel", "HighchartFunnel3d", "HighchartGaugeActivity", "HighchartGaugeClock", "HighchartGaugeDual", "HighchartGaugeSolid", "HighchartGaugeSpeedometer", "HighchartGaugeVuMeter", "HighchartHeatmap", "HighchartHistogram", "HighchartHoneycombUsa", "HighchartLineAjax", "HighchartLineBasic", "HighchartLineLabels", "HighchartLineLogAxis", "HighchartLiveData", "HighchartLollipop", "HighchartNetworkGraph", "HighchartOrganizationChart", "HighchartPackedBubble", "HighchartPackedBubbleSplit", "HighchartParallelCoordinates", "HighchartPareto", "HighchartParliamentChart", "HighchartPieBasic", "HighchartPieDonut", "HighchartPieDrilldown", "HighchartPieGradient", "HighchartPieLegend", "HighchartPieMonochrome", "HighchartPieSemiCircle", "HighchartPolar", "HighchartPolarRadialBar", "HighchartPolarSpider", "HighchartPolarWindRose", "HighchartPolygon", "HighchartPyramid", "HighchartPyramid3d", "HighchartRenderer", "HighchartSonification", "HighchartSparkline", "HighchartViewComponent" };
        return allTypes.ToArray();
        //return (from p in AssemblyReader.GetTypeNamesFromNamespace("Highcharts.Models") where allTypes.Contains(p) select p).ToArray();
    }

        
    public string HighchartType { get; set; }
    
    public Highchart()
    {
        //sShowProperties(true);
    }
    
}