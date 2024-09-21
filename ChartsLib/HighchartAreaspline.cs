using System.Collections.Generic;

using Newtonsoft.Json;
using System;

namespace Highcharts.Models.HighchartAreaspline
{
	public class HighchartAreasplineChart: Highchart {
	
		public HighchartAreasplineChart():base(){
			Type="areaspline";
	
		}
	
		[InputText("type")]
		[JsonProperty("type")]
		public string Type{ get; set; }
	}
	
	
	public class HighchartAreasplineTitle: Highchart {
	
		public HighchartAreasplineTitle():base(){
			Text="Average fruit consumption during one week";
	
		}
	
		[InputText("text")]
		[JsonProperty("text")]
		public string Text{ get; set; }
	}
	
	
	public class HighchartAreasplineLegendFloating: Highchart {
	
		public HighchartAreasplineLegendFloating():base(){
	
		}
	
	
	}
	
	
	public class HighchartAreasplineLegend: Highchart {
	
		public HighchartAreasplineLegend():base(){
			Layout="vertical";
			Align="left";
			VerticalAlign="top";
			X=150f;
			Y=100f;
			Floating=new HighchartAreasplineLegendFloating();
			BorderWidth=1f;
			BackgroundColor="#FFFFFF";
	
		}
	
		[InputText("layout")]
		[JsonProperty("layout")]
		public string Layout{ get; set; }	[InputText("align")]
		[JsonProperty("align")]
		public string Align{ get; set; }	[InputText("verticalAlign")]
		[JsonProperty("verticalAlign")]
		public string VerticalAlign{ get; set; }	[InputNumber("x")]
		[JsonProperty("x")]
		public float X{ get; set; }	[InputNumber("y")]
		[JsonProperty("y")]
		public float Y{ get; set; }	[JsonProperty("floating")]
		public HighchartAreasplineLegendFloating Floating{ get; set; }	[InputNumber("borderWidth")]
		[JsonProperty("borderWidth")]
		public float BorderWidth{ get; set; }	[InputText("backgroundColor")]
		[JsonProperty("backgroundColor")]
		public string BackgroundColor{ get; set; }
	}
	
	
	public class HighchartAreasplineXAxisPlotBandsItem: Highchart {
	
		public HighchartAreasplineXAxisPlotBandsItem():base(){
			From=4.5f;
			To=6.5f;
			Color="rgba(68, 170, 213, .2)";
	
		}
	
		[InputNumber("from")]
		[JsonProperty("from")]
		public float From{ get; set; }	[InputNumber("to")]
		[JsonProperty("to")]
		public float To{ get; set; }	[InputText("color")]
		[JsonProperty("color")]
		public string Color{ get; set; }
	}
	
	
	public class HighchartAreasplineXAxis: Highchart {
	
		public HighchartAreasplineXAxis():base(){
			Categories=new List<string>{"Monday","Tuesday","Wednesday","Thursday","Friday","Saturday","Sunday"};
			PlotBands=new List<HighchartAreasplineXAxisPlotBandsItem>{new HighchartAreasplineXAxisPlotBandsItem()};
	
		}
	
		[JsonProperty("categories")]
		public List<string> Categories{ get; set; }	[JsonProperty("plotBands")]
		public List<HighchartAreasplineXAxisPlotBandsItem> PlotBands{ get; set; }
	}
	
	
	public class HighchartAreasplineYAxisTitle: Highchart {
	
		public HighchartAreasplineYAxisTitle():base(){
			Text="Fruit units";
	
		}
	
		[InputText("text")]
		[JsonProperty("text")]
		public string Text{ get; set; }
	}
	
	
	public class HighchartAreasplineYAxis: Highchart {
	
		public HighchartAreasplineYAxis():base(){
			Title=new HighchartAreasplineYAxisTitle();
	
		}
	
		[JsonProperty("title")]
		public HighchartAreasplineYAxisTitle Title{ get; set; }
	}
	
	
	public class HighchartAreasplineTooltipShared: Highchart {
	
		public HighchartAreasplineTooltipShared():base(){
	
		}
	
	
	}
	
	
	public class HighchartAreasplineTooltip: Highchart {
	
		public HighchartAreasplineTooltip():base(){
			Shared=new HighchartAreasplineTooltipShared();
			ValueSuffix=" units";
	
		}
	
		[JsonProperty("shared")]
		public HighchartAreasplineTooltipShared Shared{ get; set; }	[InputText("valueSuffix")]
		[JsonProperty("valueSuffix")]
		public string ValueSuffix{ get; set; }
	}
	
	
	public class HighchartAreasplineCredits: Highchart {
	
		public HighchartAreasplineCredits():base(){
			Enabled=null;
	
		}
	
		[JsonProperty("enabled")]
		public object Enabled{ get; set; }
	}
	
	
	public class HighchartAreasplinePlotOptionsAreaspline: Highchart {
	
		public HighchartAreasplinePlotOptionsAreaspline():base(){
			FillOpacity=0.5f;
	
		}
	
		[InputNumber("fillOpacity")]
		[JsonProperty("fillOpacity")]
		public float FillOpacity{ get; set; }
	}
	
	
	public class HighchartAreasplinePlotOptions: Highchart {
	
		public HighchartAreasplinePlotOptions():base(){
			Areaspline=new HighchartAreasplinePlotOptionsAreaspline();
	
		}
	
		[JsonProperty("areaspline")]
		public HighchartAreasplinePlotOptionsAreaspline Areaspline{ get; set; }
	}
	
	
	public class HighchartAreasplineSeriesItem: Highchart {
	
		public HighchartAreasplineSeriesItem():base(){
			Name="John";
			Data=new List<Nullable<float>>{3f,4f,3f,5f,4f,10f,12f};
	
		}
	
		[InputText("name")]
		[JsonProperty("name")]
		public string Name{ get; set; }	[JsonProperty("data")]
		public List<Nullable<float>> Data{ get; set; }
	}
	
	
	public class HighchartAreaspline: Highchart {
	
		public HighchartAreaspline():base(){
			Chart=new HighchartAreasplineChart();
			Title=new HighchartAreasplineTitle();
			Legend=new HighchartAreasplineLegend();
			XAxis=new HighchartAreasplineXAxis();
			YAxis=new HighchartAreasplineYAxis();
			Tooltip=new HighchartAreasplineTooltip();
			Credits=new HighchartAreasplineCredits();
			PlotOptions=new HighchartAreasplinePlotOptions();
			Series=new List<HighchartAreasplineSeriesItem>{new HighchartAreasplineSeriesItem(),new HighchartAreasplineSeriesItem()};
	
		}
	
		[JsonProperty("chart")]
		public HighchartAreasplineChart Chart{ get; set; }	[JsonProperty("title")]
		public HighchartAreasplineTitle Title{ get; set; }	[JsonProperty("legend")]
		public HighchartAreasplineLegend Legend{ get; set; }	[JsonProperty("xAxis")]
		public HighchartAreasplineXAxis XAxis{ get; set; }	[JsonProperty("yAxis")]
		public HighchartAreasplineYAxis YAxis{ get; set; }	[JsonProperty("tooltip")]
		public HighchartAreasplineTooltip Tooltip{ get; set; }	[JsonProperty("credits")]
		public HighchartAreasplineCredits Credits{ get; set; }	[JsonProperty("plotOptions")]
		public HighchartAreasplinePlotOptions PlotOptions{ get; set; }	[JsonProperty("series")]
		public List<HighchartAreasplineSeriesItem> Series{ get; set; }
	}
	
	
}