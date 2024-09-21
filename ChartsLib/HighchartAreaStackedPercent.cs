using System.Collections.Generic;


using Newtonsoft.Json;
using System;

namespace Highcharts.Models.HighchartAreaStackedPercent
{
	public class HighchartAreaStackedPercentChart: Highchart {
	
		public HighchartAreaStackedPercentChart():base(){
			Type="area";
	
		}
	
		[InputText("type")]
		[JsonProperty("type")]
		public string Type{ get; set; }
	}
	
	
	public class HighchartAreaStackedPercentTitle: Highchart {
	
		public HighchartAreaStackedPercentTitle():base(){
			Text="Historic and Estimated Worldwide Population Distribution by Region";
	
		}
	
		[InputText("text")]
		[JsonProperty("text")]
		public string Text{ get; set; }
	}
	
	
	public class HighchartAreaStackedPercentSubtitle: Highchart {
	
		public HighchartAreaStackedPercentSubtitle():base(){
			Text="Source: Wikipedia.org";
	
		}
	
		[InputText("text")]
		[JsonProperty("text")]
		public string Text{ get; set; }
	}
	
	
	public class HighchartAreaStackedPercentXAxisTitle: Highchart {
	
		public HighchartAreaStackedPercentXAxisTitle():base(){
			Enabled=null;
	
		}
	
		[JsonProperty("enabled")]
		public object Enabled{ get; set; }
	}
	
	
	public class HighchartAreaStackedPercentXAxis: Highchart {
	
		public HighchartAreaStackedPercentXAxis():base(){
			Categories=new List<string>{"1750","1800","1850","1900","1950","1999","2050"};
			TickmarkPlacement="on";
			Title=new HighchartAreaStackedPercentXAxisTitle();
	
		}
	
		[JsonProperty("categories")]
		public List<string> Categories{ get; set; }	[InputText("tickmarkPlacement")]
		[JsonProperty("tickmarkPlacement")]
		public string TickmarkPlacement{ get; set; }	[JsonProperty("title")]
		public HighchartAreaStackedPercentXAxisTitle Title{ get; set; }
	}
	
	
	public class HighchartAreaStackedPercentYAxisLabels: Highchart {
	
		public HighchartAreaStackedPercentYAxisLabels():base(){
			Format="{value}%";
	
		}
	
		[InputText("format")]
		[JsonProperty("format")]
		public string Format{ get; set; }
	}
	
	
	public class HighchartAreaStackedPercentYAxisTitle: Highchart {
	
		public HighchartAreaStackedPercentYAxisTitle():base(){
			Enabled=null;
	
		}
	
		[JsonProperty("enabled")]
		public object Enabled{ get; set; }
	}
	
	
	public class HighchartAreaStackedPercentYAxis: Highchart {
	
		public HighchartAreaStackedPercentYAxis():base(){
			Labels=new HighchartAreaStackedPercentYAxisLabels();
			Title=new HighchartAreaStackedPercentYAxisTitle();
	
		}
	
		[JsonProperty("labels")]
		public HighchartAreaStackedPercentYAxisLabels Labels{ get; set; }	[JsonProperty("title")]
		public HighchartAreaStackedPercentYAxisTitle Title{ get; set; }
	}
	
	
	public class HighchartAreaStackedPercentTooltipSplit: Highchart {
	
		public HighchartAreaStackedPercentTooltipSplit():base(){
	
		}
	
	
	}
	
	
	public class HighchartAreaStackedPercentTooltip: Highchart {
	
		public HighchartAreaStackedPercentTooltip():base(){
			PointFormat="<span style='color:{series.color}'>{series.name}</span>: <b>{point.percentage:.1f}%</b> ({point.y:,.0f} millions)<br/>";
			Split=new HighchartAreaStackedPercentTooltipSplit();
	
		}
	
		[InputText("pointFormat")]
		[JsonProperty("pointFormat")]
		public string PointFormat{ get; set; }	[JsonProperty("split")]
		public HighchartAreaStackedPercentTooltipSplit Split{ get; set; }
	}
	
	
	public class HighchartAreaStackedPercentPlotOptionsAreaMarker: Highchart {
	
		public HighchartAreaStackedPercentPlotOptionsAreaMarker():base(){
			LineWidth=1f;
			LineColor="#ffffff";
	
		}
	
		[InputNumber("lineWidth")]
		[JsonProperty("lineWidth")]
		public float LineWidth{ get; set; }	[InputText("lineColor")]
		[JsonProperty("lineColor")]
		public string LineColor{ get; set; }
	}
	
	
	public class HighchartAreaStackedPercentPlotOptionsAreaAccessibility: Highchart {
	
		public HighchartAreaStackedPercentPlotOptionsAreaAccessibility():base(){
			PointDescriptionFormatter=()=>{};
	
		}
	
		[JsonProperty("pointDescriptionFormatter")]
		[JsonIgnore()]
		public Action PointDescriptionFormatter{ get; set; }
	}
	
	
	public class HighchartAreaStackedPercentPlotOptionsArea: Highchart {
	
		public HighchartAreaStackedPercentPlotOptionsArea():base(){
			Stacking="percent";
			LineColor="#ffffff";
			LineWidth=1f;
			Marker=new HighchartAreaStackedPercentPlotOptionsAreaMarker();
			Accessibility=new HighchartAreaStackedPercentPlotOptionsAreaAccessibility();
	
		}
	
		[InputText("stacking")]
		[JsonProperty("stacking")]
		public string Stacking{ get; set; }	[InputText("lineColor")]
		[JsonProperty("lineColor")]
		public string LineColor{ get; set; }	[InputNumber("lineWidth")]
		[JsonProperty("lineWidth")]
		public float LineWidth{ get; set; }	[JsonProperty("marker")]
		public HighchartAreaStackedPercentPlotOptionsAreaMarker Marker{ get; set; }	[JsonProperty("accessibility")]
		public HighchartAreaStackedPercentPlotOptionsAreaAccessibility Accessibility{ get; set; }
	}
	
	
	public class HighchartAreaStackedPercentPlotOptions: Highchart {
	
		public HighchartAreaStackedPercentPlotOptions():base(){
			Area=new HighchartAreaStackedPercentPlotOptionsArea();
	
		}
	
		[JsonProperty("area")]
		public HighchartAreaStackedPercentPlotOptionsArea Area{ get; set; }
	}
	
	
	public class HighchartAreaStackedPercentSeriesItem: Highchart {
	
		public HighchartAreaStackedPercentSeriesItem():base(){
			Name="Asia";
			Data=new List<Nullable<float>>{502f,635f,809f,947f,1402f,3634f,5268f};
	
		}
	
		[InputText("name")]
		[JsonProperty("name")]
		public string Name{ get; set; }	[JsonProperty("data")]
		public List<Nullable<float>> Data{ get; set; }
	}
	
	
	public class HighchartAreaStackedPercent: Highchart {
	
		public HighchartAreaStackedPercent():base(){
			Chart=new HighchartAreaStackedPercentChart();
			Title=new HighchartAreaStackedPercentTitle();
			Subtitle=new HighchartAreaStackedPercentSubtitle();
			XAxis=new HighchartAreaStackedPercentXAxis();
			YAxis=new HighchartAreaStackedPercentYAxis();
			Tooltip=new HighchartAreaStackedPercentTooltip();
			PlotOptions=new HighchartAreaStackedPercentPlotOptions();
			Series=new List<HighchartAreaStackedPercentSeriesItem>{new HighchartAreaStackedPercentSeriesItem(),new HighchartAreaStackedPercentSeriesItem(),new HighchartAreaStackedPercentSeriesItem(),new HighchartAreaStackedPercentSeriesItem(),new HighchartAreaStackedPercentSeriesItem()};
	
		}
	
		[JsonProperty("chart")]
		public HighchartAreaStackedPercentChart Chart{ get; set; }	[JsonProperty("title")]
		public HighchartAreaStackedPercentTitle Title{ get; set; }	[JsonProperty("subtitle")]
		public HighchartAreaStackedPercentSubtitle Subtitle{ get; set; }	[JsonProperty("xAxis")]
		public HighchartAreaStackedPercentXAxis XAxis{ get; set; }	[JsonProperty("yAxis")]
		public HighchartAreaStackedPercentYAxis YAxis{ get; set; }	[JsonProperty("tooltip")]
		public HighchartAreaStackedPercentTooltip Tooltip{ get; set; }	[JsonProperty("plotOptions")]
		public HighchartAreaStackedPercentPlotOptions PlotOptions{ get; set; }	[JsonProperty("series")]
		public List<HighchartAreaStackedPercentSeriesItem> Series{ get; set; }
	}
	
	
}