using System.Collections.Generic;

using Newtonsoft.Json;
using System;

namespace Highcharts.Models.HighchartAccessiblePie
{
	public class HighchartAccessiblePieChart: Highchart {
	
		public HighchartAccessiblePieChart():base(){
			Type="pie";
	
		}
	
		[InputText("type")]
		[JsonProperty("type")]
		public string Type{ get; set; }
	}
	
	
	public class HighchartAccessiblePieTitle: Highchart {
	
		public HighchartAccessiblePieTitle():base(){
			Text="Primary desktop/laptop screen readers";
	
		}
	
		[InputText("text")]
		[JsonProperty("text")]
		public string Text{ get; set; }
	}
	
	
	public class HighchartAccessiblePieSubtitle: Highchart {
	
		public HighchartAccessiblePieSubtitle():base(){
			Text="Source: WebAIM. Click on point to visit official website";
	
		}
	
		[InputText("text")]
		[JsonProperty("text")]
		public string Text{ get; set; }
	}
	
	
	public class HighchartAccessiblePieTooltip: Highchart {
	
		public HighchartAccessiblePieTooltip():base(){
			ValueSuffix="%";
			BorderColor="#8ae";
	
		}
	
		[InputText("valueSuffix")]
		[JsonProperty("valueSuffix")]
		public string ValueSuffix{ get; set; }	[InputText("borderColor")]
		[JsonProperty("borderColor")]
		public string BorderColor{ get; set; }
	}
	
	
	public class HighchartAccessiblePiePlotOptionsSeriesDataLabelsEnabled: Highchart {
	
		public HighchartAccessiblePiePlotOptionsSeriesDataLabelsEnabled():base(){
	
		}
	
	
	}
	
	
	public class HighchartAccessiblePiePlotOptionsSeriesDataLabels: Highchart {
	
		public HighchartAccessiblePiePlotOptionsSeriesDataLabels():base(){
			Enabled=new HighchartAccessiblePiePlotOptionsSeriesDataLabelsEnabled();
			ConnectorColor="#777";
			Format="<b>{point.name}</b>: {point.percentage:.1f} %";
	
		}
	
		[JsonProperty("enabled")]
		public HighchartAccessiblePiePlotOptionsSeriesDataLabelsEnabled Enabled{ get; set; }	[InputText("connectorColor")]
		[JsonProperty("connectorColor")]
		public string ConnectorColor{ get; set; }	[InputText("format")]
		[JsonProperty("format")]
		public string Format{ get; set; }
	}
	
	
	public class HighchartAccessiblePiePlotOptionsSeriesPointEvents: Highchart {
	
		public HighchartAccessiblePiePlotOptionsSeriesPointEvents():base(){
			Click=()=>{};
	
		}
	
		[JsonProperty("click")]
		[JsonIgnore()]
		public Action Click{ get; set; }
	}
	
	
	public class HighchartAccessiblePiePlotOptionsSeriesPoint: Highchart {
	
		public HighchartAccessiblePiePlotOptionsSeriesPoint():base(){
			Events=new HighchartAccessiblePiePlotOptionsSeriesPointEvents();
	
		}
	
		[JsonProperty("events")]
		public HighchartAccessiblePiePlotOptionsSeriesPointEvents Events{ get; set; }
	}
	
	
	public class HighchartAccessiblePiePlotOptionsSeries: Highchart {
	
		public HighchartAccessiblePiePlotOptionsSeries():base(){
			DataLabels=new HighchartAccessiblePiePlotOptionsSeriesDataLabels();
			Point=new HighchartAccessiblePiePlotOptionsSeriesPoint();
			Cursor="pointer";
			BorderWidth=3f;
	
		}
	
		[JsonProperty("dataLabels")]
		public HighchartAccessiblePiePlotOptionsSeriesDataLabels DataLabels{ get; set; }	[JsonProperty("point")]
		public HighchartAccessiblePiePlotOptionsSeriesPoint Point{ get; set; }	[InputText("cursor")]
		[JsonProperty("cursor")]
		public string Cursor{ get; set; }	[InputNumber("borderWidth")]
		[JsonProperty("borderWidth")]
		public float BorderWidth{ get; set; }
	}
	
	
	public class HighchartAccessiblePiePlotOptions: Highchart {
	
		public HighchartAccessiblePiePlotOptions():base(){
			Series=new HighchartAccessiblePiePlotOptionsSeries();
	
		}
	
		[JsonProperty("series")]
		public HighchartAccessiblePiePlotOptionsSeries Series{ get; set; }
	}
	
	
	public class HighchartAccessiblePieSeriesItemDataItemColorPattern: Highchart {
	
		public HighchartAccessiblePieSeriesItemDataItemColorPattern():base(){
			Path="M 0 0 L 5 5 M 4.5 -0.5 L 5.5 0.5 M -0.5 4.5 L 0.5 5.5";
			Color="#49a65e";
			Width=5f;
			Height=5f;
	
		}
	
		[InputText("path")]
		[JsonProperty("path")]
		public string Path{ get; set; }	[InputText("color")]
		[JsonProperty("color")]
		public string Color{ get; set; }	[InputNumber("width")]
		[JsonProperty("width")]
		public float Width{ get; set; }	[InputNumber("height")]
		[JsonProperty("height")]
		public float Height{ get; set; }
	}
	
	
	public class HighchartAccessiblePieSeriesItemDataItemColor: Highchart {
	
		public HighchartAccessiblePieSeriesItemDataItemColor():base(){
			Pattern=new HighchartAccessiblePieSeriesItemDataItemColorPattern();
	
		}
	
		[JsonProperty("pattern")]
		public HighchartAccessiblePieSeriesItemDataItemColorPattern Pattern{ get; set; }
	}
	
	
	public class HighchartAccessiblePieSeriesItemDataItemAccessibility: Highchart {
	
		public HighchartAccessiblePieSeriesItemDataItemAccessibility():base(){
			Description="This is the most used desktop screen reader";
	
		}
	
		[InputText("description")]
		[JsonProperty("description")]
		public string Description{ get; set; }
	}
	
	
	public class HighchartAccessiblePieSeriesItemDataItem: Highchart {
	
		public HighchartAccessiblePieSeriesItemDataItem():base(){
			Name="NVDA";
			Y=40.6f;
			Color=new HighchartAccessiblePieSeriesItemDataItemColor();
			Website=" ";
			Accessibility=new HighchartAccessiblePieSeriesItemDataItemAccessibility();
	
		}
	
		[InputText("name")]
		[JsonProperty("name")]
		public string Name{ get; set; }	[InputNumber("y")]
		[JsonProperty("y")]
		public float Y{ get; set; }	[JsonProperty("color")]
		public HighchartAccessiblePieSeriesItemDataItemColor Color{ get; set; }	[InputText("website")]
		[JsonProperty("website")]
		public string Website{ get; set; }	[JsonProperty("accessibility")]
		public HighchartAccessiblePieSeriesItemDataItemAccessibility Accessibility{ get; set; }
	}
	
	
	public class HighchartAccessiblePieSeriesItem: Highchart {
	
		public HighchartAccessiblePieSeriesItem():base(){
			Name="Screen reader usage";
			Data=new List<HighchartAccessiblePieSeriesItemDataItem>{new HighchartAccessiblePieSeriesItemDataItem(),new HighchartAccessiblePieSeriesItemDataItem(),new HighchartAccessiblePieSeriesItemDataItem(),new HighchartAccessiblePieSeriesItemDataItem(),new HighchartAccessiblePieSeriesItemDataItem()};
	
		}
	
		[InputText("name")]
		[JsonProperty("name")]
		public string Name{ get; set; }	[JsonProperty("data")]
		public List<HighchartAccessiblePieSeriesItemDataItem> Data{ get; set; }
	}
	
	
	public class HighchartAccessiblePieResponsiveRulesItemCondition: Highchart {
	
		public HighchartAccessiblePieResponsiveRulesItemCondition():base(){
			MaxWidth=500f;
	
		}
	
		[InputNumber("maxWidth")]
		[JsonProperty("maxWidth")]
		public float MaxWidth{ get; set; }
	}
	
	
	public class HighchartAccessiblePieResponsiveRulesItemChartOptionsPlotOptionsSeriesDataLabels: Highchart {
	
		public HighchartAccessiblePieResponsiveRulesItemChartOptionsPlotOptionsSeriesDataLabels():base(){
			Format="<b>{point.name}</b>";
	
		}
	
		[InputText("format")]
		[JsonProperty("format")]
		public string Format{ get; set; }
	}
	
	
	public class HighchartAccessiblePieResponsiveRulesItemChartOptionsPlotOptionsSeries: Highchart {
	
		public HighchartAccessiblePieResponsiveRulesItemChartOptionsPlotOptionsSeries():base(){
			DataLabels=new HighchartAccessiblePieResponsiveRulesItemChartOptionsPlotOptionsSeriesDataLabels();
	
		}
	
		[JsonProperty("dataLabels")]
		public HighchartAccessiblePieResponsiveRulesItemChartOptionsPlotOptionsSeriesDataLabels DataLabels{ get; set; }
	}
	
	
	public class HighchartAccessiblePieResponsiveRulesItemChartOptionsPlotOptions: Highchart {
	
		public HighchartAccessiblePieResponsiveRulesItemChartOptionsPlotOptions():base(){
			Series=new HighchartAccessiblePieResponsiveRulesItemChartOptionsPlotOptionsSeries();
	
		}
	
		[JsonProperty("series")]
		public HighchartAccessiblePieResponsiveRulesItemChartOptionsPlotOptionsSeries Series{ get; set; }
	}
	
	
	public class HighchartAccessiblePieResponsiveRulesItemChartOptions: Highchart {
	
		public HighchartAccessiblePieResponsiveRulesItemChartOptions():base(){
			PlotOptions=new HighchartAccessiblePieResponsiveRulesItemChartOptionsPlotOptions();
	
		}
	
		[JsonProperty("plotOptions")]
		public HighchartAccessiblePieResponsiveRulesItemChartOptionsPlotOptions PlotOptions{ get; set; }
	}
	
	
	public class HighchartAccessiblePieResponsiveRulesItem: Highchart {
	
		public HighchartAccessiblePieResponsiveRulesItem():base(){
			Condition=new HighchartAccessiblePieResponsiveRulesItemCondition();
			ChartOptions=new HighchartAccessiblePieResponsiveRulesItemChartOptions();
	
		}
	
		[JsonProperty("condition")]
		public HighchartAccessiblePieResponsiveRulesItemCondition Condition{ get; set; }	[JsonProperty("chartOptions")]
		public HighchartAccessiblePieResponsiveRulesItemChartOptions ChartOptions{ get; set; }
	}
	
	
	public class HighchartAccessiblePieResponsive: Highchart {
	
		public HighchartAccessiblePieResponsive():base(){
			Rules=new List<HighchartAccessiblePieResponsiveRulesItem>{new HighchartAccessiblePieResponsiveRulesItem()};
	
		}
	
		[JsonProperty("rules")]
		public List<HighchartAccessiblePieResponsiveRulesItem> Rules{ get; set; }
	}
	
	
	public class HighchartAccessiblePie: Highchart {
	
		public HighchartAccessiblePie():base(){
			Chart=new HighchartAccessiblePieChart();
			Title=new HighchartAccessiblePieTitle();
			Subtitle=new HighchartAccessiblePieSubtitle();
			Tooltip=new HighchartAccessiblePieTooltip();
			PlotOptions=new HighchartAccessiblePiePlotOptions();
			Series=new List<HighchartAccessiblePieSeriesItem>{new HighchartAccessiblePieSeriesItem()};
			Responsive=new HighchartAccessiblePieResponsive();
	
		}
	
		[JsonProperty("chart")]
		public HighchartAccessiblePieChart Chart{ get; set; }	[JsonProperty("title")]
		public HighchartAccessiblePieTitle Title{ get; set; }	[JsonProperty("subtitle")]
		public HighchartAccessiblePieSubtitle Subtitle{ get; set; }	[JsonProperty("tooltip")]
		public HighchartAccessiblePieTooltip Tooltip{ get; set; }	[JsonProperty("plotOptions")]
		public HighchartAccessiblePiePlotOptions PlotOptions{ get; set; }	[JsonProperty("series")]
		public List<HighchartAccessiblePieSeriesItem> Series{ get; set; }	[JsonProperty("responsive")]
		public HighchartAccessiblePieResponsive Responsive{ get; set; }
	}
	
	
}