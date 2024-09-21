using System.Collections.Generic;

using Newtonsoft.Json;
using System;

namespace Highcharts.Models.HighchartAccessibleLine
{
	public class HighchartAccessibleLineChart: Highchart {
	
		public HighchartAccessibleLineChart():base(){
			Type="spline";
	
		}
	
		[InputText("type")]
		[JsonProperty("type")]
		public string Type{ get; set; }
	}
	
	
	public class HighchartAccessibleLineLegend: Highchart {
	
		public HighchartAccessibleLineLegend():base(){
			SymbolWidth=40f;
	
		}
	
		[InputNumber("symbolWidth")]
		[JsonProperty("symbolWidth")]
		public float SymbolWidth{ get; set; }
	}
	
	
	public class HighchartAccessibleLineTitle: Highchart {
	
		public HighchartAccessibleLineTitle():base(){
			Text="Most common desktop screen readers";
	
		}
	
		[InputText("text")]
		[JsonProperty("text")]
		public string Text{ get; set; }
	}
	
	
	public class HighchartAccessibleLineSubtitle: Highchart {
	
		public HighchartAccessibleLineSubtitle():base(){
			Text="Source: WebAIM. Click on points to visit official screen reader website";
	
		}
	
		[InputText("text")]
		[JsonProperty("text")]
		public string Text{ get; set; }
	}
	
	
	public class HighchartAccessibleLineYAxisTitle: Highchart {
	
		public HighchartAccessibleLineYAxisTitle():base(){
			Text="Percentage usage";
	
		}
	
		[InputText("text")]
		[JsonProperty("text")]
		public string Text{ get; set; }
	}
	
	
	public class HighchartAccessibleLineYAxis: Highchart {
	
		public HighchartAccessibleLineYAxis():base(){
			Title=new HighchartAccessibleLineYAxisTitle();
	
		}
	
		[JsonProperty("title")]
		public HighchartAccessibleLineYAxisTitle Title{ get; set; }
	}
	
	
	public class HighchartAccessibleLineXAxisTitle: Highchart {
	
		public HighchartAccessibleLineXAxisTitle():base(){
			Text="Time";
	
		}
	
		[InputText("text")]
		[JsonProperty("text")]
		public string Text{ get; set; }
	}
	
	
	public class HighchartAccessibleLineXAxisAccessibility: Highchart {
	
		public HighchartAccessibleLineXAxisAccessibility():base(){
			Description="Time from December 2010 to September 2019";
	
		}
	
		[InputText("description")]
		[JsonProperty("description")]
		public string Description{ get; set; }
	}
	
	
	public class HighchartAccessibleLineXAxis: Highchart {
	
		public HighchartAccessibleLineXAxis():base(){
			Title=new HighchartAccessibleLineXAxisTitle();
			Accessibility=new HighchartAccessibleLineXAxisAccessibility();
			Categories=new List<string>{"December 2010","May 2012","January 2014","July 2015","October 2017","September 2019"};
	
		}
	
		[JsonProperty("title")]
		public HighchartAccessibleLineXAxisTitle Title{ get; set; }	[JsonProperty("accessibility")]
		public HighchartAccessibleLineXAxisAccessibility Accessibility{ get; set; }	[JsonProperty("categories")]
		public List<string> Categories{ get; set; }
	}
	
	
	public class HighchartAccessibleLineTooltip: Highchart {
	
		public HighchartAccessibleLineTooltip():base(){
			ValueSuffix="%";
	
		}
	
		[InputText("valueSuffix")]
		[JsonProperty("valueSuffix")]
		public string ValueSuffix{ get; set; }
	}
	
	
	public class HighchartAccessibleLinePlotOptionsSeriesPointEvents: Highchart {
	
		public HighchartAccessibleLinePlotOptionsSeriesPointEvents():base(){
			Click=()=>{};
	
		}
	
		[JsonProperty("click")]
		[JsonIgnore()]
		public Action Click{ get; set; }
	}
	
	
	public class HighchartAccessibleLinePlotOptionsSeriesPoint: Highchart {
	
		public HighchartAccessibleLinePlotOptionsSeriesPoint():base(){
			Events=new HighchartAccessibleLinePlotOptionsSeriesPointEvents();
	
		}
	
		[JsonProperty("events")]
		public HighchartAccessibleLinePlotOptionsSeriesPointEvents Events{ get; set; }
	}
	
	
	public class HighchartAccessibleLinePlotOptionsSeries: Highchart {
	
		public HighchartAccessibleLinePlotOptionsSeries():base(){
			Point=new HighchartAccessibleLinePlotOptionsSeriesPoint();
			Cursor="pointer";
	
		}
	
		[JsonProperty("point")]
		public HighchartAccessibleLinePlotOptionsSeriesPoint Point{ get; set; }	[InputText("cursor")]
		[JsonProperty("cursor")]
		public string Cursor{ get; set; }
	}
	
	
	public class HighchartAccessibleLinePlotOptions: Highchart {
	
		public HighchartAccessibleLinePlotOptions():base(){
			Series=new HighchartAccessibleLinePlotOptionsSeries();
	
		}
	
		[JsonProperty("series")]
		public HighchartAccessibleLinePlotOptionsSeries Series{ get; set; }
	}
	
	
	public class HighchartAccessibleLineSeriesItemAccessibility: Highchart {
	
		public HighchartAccessibleLineSeriesItemAccessibility():base(){
			Description="This is the most used screen reader in 2019";
	
		}
	
		[InputText("description")]
		[JsonProperty("description")]
		public string Description{ get; set; }
	}
	
	
	public class HighchartAccessibleLineSeriesItem: Highchart {
	
		public HighchartAccessibleLineSeriesItem():base(){
			Name="NVDA";
			Data=new List<Nullable<float>>{34.8f,43f,51.2f,41.4f,64.9f,72.4f};
			Website="";
			Color="#49a65e";
			Accessibility=new HighchartAccessibleLineSeriesItemAccessibility();
	
		}
	
		[InputText("name")]
		[JsonProperty("name")]
		public string Name{ get; set; }	[JsonProperty("data")]
		public List<Nullable<float>> Data{ get; set; }	[InputText("website")]
		[JsonProperty("website")]
		public string Website{ get; set; }	[InputText("color")]
		[JsonProperty("color")]
		public string Color{ get; set; }	[JsonProperty("accessibility")]
		public HighchartAccessibleLineSeriesItemAccessibility Accessibility{ get; set; }
	}
	
	
	public class HighchartAccessibleLineResponsiveRulesItemCondition: Highchart {
	
		public HighchartAccessibleLineResponsiveRulesItemCondition():base(){
			MaxWidth=550f;
	
		}
	
		[InputNumber("maxWidth")]
		[JsonProperty("maxWidth")]
		public float MaxWidth{ get; set; }
	}
	
	
	public class HighchartAccessibleLineResponsiveRulesItemChartOptionsLegend: Highchart {
	
		public HighchartAccessibleLineResponsiveRulesItemChartOptionsLegend():base(){
			ItemWidth=150f;
	
		}
	
		[InputNumber("itemWidth")]
		[JsonProperty("itemWidth")]
		public float ItemWidth{ get; set; }
	}
	
	
	public class HighchartAccessibleLineResponsiveRulesItemChartOptionsXAxis: Highchart {
	
		public HighchartAccessibleLineResponsiveRulesItemChartOptionsXAxis():base(){
			Categories=new List<string>{"Dec. 2010","May 2012","Jan. 2014","July 2015","Oct. 2017","Sep. 2019"};
	
		}
	
		[JsonProperty("categories")]
		public List<string> Categories{ get; set; }
	}
	
	
	public class HighchartAccessibleLineResponsiveRulesItemChartOptionsYAxisTitle: Highchart {
	
		public HighchartAccessibleLineResponsiveRulesItemChartOptionsYAxisTitle():base(){
			Enabled=null;
	
		}
	
		[JsonProperty("enabled")]
		public object Enabled{ get; set; }
	}
	
	
	public class HighchartAccessibleLineResponsiveRulesItemChartOptionsYAxisLabels: Highchart {
	
		public HighchartAccessibleLineResponsiveRulesItemChartOptionsYAxisLabels():base(){
			Format="{value}%";
	
		}
	
		[InputText("format")]
		[JsonProperty("format")]
		public string Format{ get; set; }
	}
	
	
	public class HighchartAccessibleLineResponsiveRulesItemChartOptionsYAxis: Highchart {
	
		public HighchartAccessibleLineResponsiveRulesItemChartOptionsYAxis():base(){
			Title=new HighchartAccessibleLineResponsiveRulesItemChartOptionsYAxisTitle();
			Labels=new HighchartAccessibleLineResponsiveRulesItemChartOptionsYAxisLabels();
	
		}
	
		[JsonProperty("title")]
		public HighchartAccessibleLineResponsiveRulesItemChartOptionsYAxisTitle Title{ get; set; }	[JsonProperty("labels")]
		public HighchartAccessibleLineResponsiveRulesItemChartOptionsYAxisLabels Labels{ get; set; }
	}
	
	
	public class HighchartAccessibleLineResponsiveRulesItemChartOptions: Highchart {
	
		public HighchartAccessibleLineResponsiveRulesItemChartOptions():base(){
			Legend=new HighchartAccessibleLineResponsiveRulesItemChartOptionsLegend();
			XAxis=new HighchartAccessibleLineResponsiveRulesItemChartOptionsXAxis();
			YAxis=new HighchartAccessibleLineResponsiveRulesItemChartOptionsYAxis();
	
		}
	
		[JsonProperty("legend")]
		public HighchartAccessibleLineResponsiveRulesItemChartOptionsLegend Legend{ get; set; }	[JsonProperty("xAxis")]
		public HighchartAccessibleLineResponsiveRulesItemChartOptionsXAxis XAxis{ get; set; }	[JsonProperty("yAxis")]
		public HighchartAccessibleLineResponsiveRulesItemChartOptionsYAxis YAxis{ get; set; }
	}
	
	
	public class HighchartAccessibleLineResponsiveRulesItem: Highchart {
	
		public HighchartAccessibleLineResponsiveRulesItem():base(){
			Condition=new HighchartAccessibleLineResponsiveRulesItemCondition();
			ChartOptions=new HighchartAccessibleLineResponsiveRulesItemChartOptions();
	
		}
	
		[JsonProperty("condition")]
		public HighchartAccessibleLineResponsiveRulesItemCondition Condition{ get; set; }	[JsonProperty("chartOptions")]
		public HighchartAccessibleLineResponsiveRulesItemChartOptions ChartOptions{ get; set; }
	}
	
	
	public class HighchartAccessibleLineResponsive: Highchart {
	
		public HighchartAccessibleLineResponsive():base(){
			Rules=new List<HighchartAccessibleLineResponsiveRulesItem>{new HighchartAccessibleLineResponsiveRulesItem()};
	
		}
	
		[JsonProperty("rules")]
		public List<HighchartAccessibleLineResponsiveRulesItem> Rules{ get; set; }
	}
	
	
	public class HighchartAccessibleLine: Highchart {
	
		public HighchartAccessibleLine():base(){
			Chart=new HighchartAccessibleLineChart();
			Legend=new HighchartAccessibleLineLegend();
			Title=new HighchartAccessibleLineTitle();
			Subtitle=new HighchartAccessibleLineSubtitle();
			YAxis=new HighchartAccessibleLineYAxis();
			XAxis=new HighchartAccessibleLineXAxis();
			Tooltip=new HighchartAccessibleLineTooltip();
			PlotOptions=new HighchartAccessibleLinePlotOptions();
			Series=new List<HighchartAccessibleLineSeriesItem>{new HighchartAccessibleLineSeriesItem(),new HighchartAccessibleLineSeriesItem(),new HighchartAccessibleLineSeriesItem(),new HighchartAccessibleLineSeriesItem(),new HighchartAccessibleLineSeriesItem(),new HighchartAccessibleLineSeriesItem()};
			Responsive=new HighchartAccessibleLineResponsive();
	
		}
	
		[JsonProperty("chart")]
		public HighchartAccessibleLineChart Chart{ get; set; }	[JsonProperty("legend")]
		public HighchartAccessibleLineLegend Legend{ get; set; }	[JsonProperty("title")]
		public HighchartAccessibleLineTitle Title{ get; set; }	[JsonProperty("subtitle")]
		public HighchartAccessibleLineSubtitle Subtitle{ get; set; }	[JsonProperty("yAxis")]
		public HighchartAccessibleLineYAxis YAxis{ get; set; }	[JsonProperty("xAxis")]
		public HighchartAccessibleLineXAxis XAxis{ get; set; }	[JsonProperty("tooltip")]
		public HighchartAccessibleLineTooltip Tooltip{ get; set; }	[JsonProperty("plotOptions")]
		public HighchartAccessibleLinePlotOptions PlotOptions{ get; set; }	[JsonProperty("series")]
		public List<HighchartAccessibleLineSeriesItem> Series{ get; set; }	[JsonProperty("responsive")]
		public HighchartAccessibleLineResponsive Responsive{ get; set; }
	}
	
	
}