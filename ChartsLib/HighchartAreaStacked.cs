using System.Collections.Generic;

using Newtonsoft.Json;
using System;

namespace Highcharts.Models.HighchartAreaStacked
{
	public class HighchartAreaStackedChart: Highchart {
	
		public HighchartAreaStackedChart():base(){
			Type="area";
	
		}
	
		[InputText("type")]
		[JsonProperty("type")]
		public string Type{ get; set; }
	}
	
	
	public class HighchartAreaStackedTitle: Highchart {
	
		public HighchartAreaStackedTitle():base(){
			Text="Historic and Estimated Worldwide Population Growth by Region";
	
		}
	
		[InputText("text")]
		[JsonProperty("text")]
		public string Text{ get; set; }
	}
	
	
	public class HighchartAreaStackedSubtitle: Highchart {
	
		public HighchartAreaStackedSubtitle():base(){
			Text="Source: Wikipedia.org";
	
		}
	
		[InputText("text")]
		[JsonProperty("text")]
		public string Text{ get; set; }
	}
	
	
	public class HighchartAreaStackedXAxisTitle: Highchart {
	
		public HighchartAreaStackedXAxisTitle():base(){
			Enabled=null;
	
		}
	
		[JsonProperty("enabled")]
		public object Enabled{ get; set; }
	}
	
	
	public class HighchartAreaStackedXAxis: Highchart {
	
		public HighchartAreaStackedXAxis():base(){
			Categories=new List<string>{"1750","1800","1850","1900","1950","1999","2050"};
			TickmarkPlacement="on";
			Title=new HighchartAreaStackedXAxisTitle();
	
		}
	
		[JsonProperty("categories")]
		public List<string> Categories{ get; set; }	[InputText("tickmarkPlacement")]
		[JsonProperty("tickmarkPlacement")]
		public string TickmarkPlacement{ get; set; }	[JsonProperty("title")]
		public HighchartAreaStackedXAxisTitle Title{ get; set; }
	}
	
	
	public class HighchartAreaStackedYAxisTitle: Highchart {
	
		public HighchartAreaStackedYAxisTitle():base(){
			Text="Billions";
	
		}
	
		[InputText("text")]
		[JsonProperty("text")]
		public string Text{ get; set; }
	}
	
	
	public class HighchartAreaStackedYAxisLabels: Highchart {
	
		public HighchartAreaStackedYAxisLabels():base(){
			Formatter=()=>{};
	
		}
	
		[JsonProperty("formatter")]
		[JsonIgnore()]
		public Action Formatter{ get; set; }
	}
	
	
	public class HighchartAreaStackedYAxis: Highchart {
	
		public HighchartAreaStackedYAxis():base(){
			Title=new HighchartAreaStackedYAxisTitle();
			Labels=new HighchartAreaStackedYAxisLabels();
	
		}
	
		[JsonProperty("title")]
		public HighchartAreaStackedYAxisTitle Title{ get; set; }	[JsonProperty("labels")]
		public HighchartAreaStackedYAxisLabels Labels{ get; set; }
	}
	
	
	public class HighchartAreaStackedTooltipSplit: Highchart {
	
		public HighchartAreaStackedTooltipSplit():base(){
	
		}
	
	
	}
	
	
	public class HighchartAreaStackedTooltip: Highchart {
	
		public HighchartAreaStackedTooltip():base(){
			Split=new HighchartAreaStackedTooltipSplit();
			ValueSuffix=" millions";
	
		}
	
		[JsonProperty("split")]
		public HighchartAreaStackedTooltipSplit Split{ get; set; }	[InputText("valueSuffix")]
		[JsonProperty("valueSuffix")]
		public string ValueSuffix{ get; set; }
	}
	
	
	public class HighchartAreaStackedPlotOptionsAreaMarker: Highchart {
	
		public HighchartAreaStackedPlotOptionsAreaMarker():base(){
			LineWidth=1f;
			LineColor="#666666";
	
		}
	
		[InputNumber("lineWidth")]
		[JsonProperty("lineWidth")]
		public float LineWidth{ get; set; }	[InputText("lineColor")]
		[JsonProperty("lineColor")]
		public string LineColor{ get; set; }
	}
	
	
	public class HighchartAreaStackedPlotOptionsArea: Highchart {
	
		public HighchartAreaStackedPlotOptionsArea():base(){
			Stacking="normal";
			LineColor="#666666";
			LineWidth=1f;
			Marker=new HighchartAreaStackedPlotOptionsAreaMarker();
	
		}
	
		[InputText("stacking")]
		[JsonProperty("stacking")]
		public string Stacking{ get; set; }	[InputText("lineColor")]
		[JsonProperty("lineColor")]
		public string LineColor{ get; set; }	[InputNumber("lineWidth")]
		[JsonProperty("lineWidth")]
		public float LineWidth{ get; set; }	[JsonProperty("marker")]
		public HighchartAreaStackedPlotOptionsAreaMarker Marker{ get; set; }
	}
	
	
	public class HighchartAreaStackedPlotOptions: Highchart {
	
		public HighchartAreaStackedPlotOptions():base(){
			Area=new HighchartAreaStackedPlotOptionsArea();
	
		}
	
		[JsonProperty("area")]
		public HighchartAreaStackedPlotOptionsArea Area{ get; set; }
	}
	
	
	public class HighchartAreaStackedSeriesItem: Highchart {
	
		public HighchartAreaStackedSeriesItem():base(){
			Name="Asia";
			Data=new List<Nullable<float>>{502f,635f,809f,947f,1402f,3634f,5268f};
	
		}
	
		[InputText("name")]
		[JsonProperty("name")]
		public string Name{ get; set; }	[JsonProperty("data")]
		public List<Nullable<float>> Data{ get; set; }
	}
	
	
	public class HighchartAreaStacked: Highchart {
	
		public HighchartAreaStacked():base(){
			Chart=new HighchartAreaStackedChart();
			Title=new HighchartAreaStackedTitle();
			Subtitle=new HighchartAreaStackedSubtitle();
			XAxis=new HighchartAreaStackedXAxis();
			YAxis=new HighchartAreaStackedYAxis();
			Tooltip=new HighchartAreaStackedTooltip();
			PlotOptions=new HighchartAreaStackedPlotOptions();
			Series=new List<HighchartAreaStackedSeriesItem>{new HighchartAreaStackedSeriesItem(),new HighchartAreaStackedSeriesItem(),new HighchartAreaStackedSeriesItem(),new HighchartAreaStackedSeriesItem(),new HighchartAreaStackedSeriesItem()};
	
		}
	
		[JsonProperty("chart")]
		public HighchartAreaStackedChart Chart{ get; set; }	[JsonProperty("title")]
		public HighchartAreaStackedTitle Title{ get; set; }	[JsonProperty("subtitle")]
		public HighchartAreaStackedSubtitle Subtitle{ get; set; }	[JsonProperty("xAxis")]
		public HighchartAreaStackedXAxis XAxis{ get; set; }	[JsonProperty("yAxis")]
		public HighchartAreaStackedYAxis YAxis{ get; set; }	[JsonProperty("tooltip")]
		public HighchartAreaStackedTooltip Tooltip{ get; set; }	[JsonProperty("plotOptions")]
		public HighchartAreaStackedPlotOptions PlotOptions{ get; set; }	[JsonProperty("series")]
		public List<HighchartAreaStackedSeriesItem> Series{ get; set; }
	}
	
	
}