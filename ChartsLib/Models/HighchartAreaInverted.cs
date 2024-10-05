using System.Collections.Generic;

using Newtonsoft.Json;
using System;

namespace Highcharts.Models.HighchartAreaInverted
{
	public class HighchartAreaInvertedChartInverted: Highchart {
	
		public HighchartAreaInvertedChartInverted():base(){
	
		}
	
	
	}
	
	
	public class HighchartAreaInvertedChart: Highchart {
	
		public HighchartAreaInvertedChart():base(){
			Type="area";
			Inverted=new HighchartAreaInvertedChartInverted();
	
		}
	
		[InputText("type")]
		[JsonProperty("type")]
		public string Type{ get; set; }	[JsonProperty("inverted")]
		public HighchartAreaInvertedChartInverted Inverted{ get; set; }
	}
	
	
	public class HighchartAreaInvertedTitle: Highchart {
	
		public HighchartAreaInvertedTitle():base(){
			Text="Average fruit consumption during one week";
	
		}
	
		[InputText("text")]
		[JsonProperty("text")]
		public string Text{ get; set; }
	}
	
	
	public class HighchartAreaInvertedAccessibilityKeyboardNavigationSeriesNavigation: Highchart {
	
		public HighchartAreaInvertedAccessibilityKeyboardNavigationSeriesNavigation():base(){
			Mode="serialize";
	
		}
	
		[InputText("mode")]
		[JsonProperty("mode")]
		public string Mode{ get; set; }
	}
	
	
	public class HighchartAreaInvertedAccessibilityKeyboardNavigation: Highchart {
	
		public HighchartAreaInvertedAccessibilityKeyboardNavigation():base(){
			SeriesNavigation=new HighchartAreaInvertedAccessibilityKeyboardNavigationSeriesNavigation();
	
		}
	
		[JsonProperty("seriesNavigation")]
		public HighchartAreaInvertedAccessibilityKeyboardNavigationSeriesNavigation SeriesNavigation{ get; set; }
	}
	
	
	public class HighchartAreaInvertedAccessibility: Highchart {
	
		public HighchartAreaInvertedAccessibility():base(){
			KeyboardNavigation=new HighchartAreaInvertedAccessibilityKeyboardNavigation();
	
		}
	
		[JsonProperty("keyboardNavigation")]
		public HighchartAreaInvertedAccessibilityKeyboardNavigation KeyboardNavigation{ get; set; }
	}
	
	
	public class HighchartAreaInvertedLegendFloating: Highchart {
	
		public HighchartAreaInvertedLegendFloating():base(){
	
		}
	
	
	}
	
	
	public class HighchartAreaInvertedLegend: Highchart {
	
		public HighchartAreaInvertedLegend():base(){
			Layout="vertical";
			Align="right";
			VerticalAlign="top";
			X=-150f;
			Y=100f;
			Floating=new HighchartAreaInvertedLegendFloating();
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
		public HighchartAreaInvertedLegendFloating Floating{ get; set; }	[InputNumber("borderWidth")]
		[JsonProperty("borderWidth")]
		public float BorderWidth{ get; set; }	[InputText("backgroundColor")]
		[JsonProperty("backgroundColor")]
		public string BackgroundColor{ get; set; }
	}
	
	
	public class HighchartAreaInvertedXAxis: Highchart {
	
		public HighchartAreaInvertedXAxis():base(){
			Categories=new List<string>{"Monday","Tuesday","Wednesday","Thursday","Friday","Saturday","Sunday"};
	
		}
	
		[JsonProperty("categories")]
		public List<string> Categories{ get; set; }
	}
	
	
	public class HighchartAreaInvertedYAxisTitle: Highchart {
	
		public HighchartAreaInvertedYAxisTitle():base(){
			Text="Number of units";
	
		}
	
		[InputText("text")]
		[JsonProperty("text")]
		public string Text{ get; set; }
	}
	
	
	public class HighchartAreaInvertedYAxis: Highchart {
	
		public HighchartAreaInvertedYAxis():base(){
			Title=new HighchartAreaInvertedYAxisTitle();
			AllowDecimals=null;
			Min=null;
	
		}
	
		[JsonProperty("title")]
		public HighchartAreaInvertedYAxisTitle Title{ get; set; }	[JsonProperty("allowDecimals")]
		public object AllowDecimals{ get; set; }	[JsonProperty("min")]
		public object Min{ get; set; }
	}
	
	
	public class HighchartAreaInvertedPlotOptionsArea: Highchart {
	
		public HighchartAreaInvertedPlotOptionsArea():base(){
			FillOpacity=0.5f;
	
		}
	
		[InputNumber("fillOpacity")]
		[JsonProperty("fillOpacity")]
		public float FillOpacity{ get; set; }
	}
	
	
	public class HighchartAreaInvertedPlotOptions: Highchart {
	
		public HighchartAreaInvertedPlotOptions():base(){
			Area=new HighchartAreaInvertedPlotOptionsArea();
	
		}
	
		[JsonProperty("area")]
		public HighchartAreaInvertedPlotOptionsArea Area{ get; set; }
	}
	
	
	public class HighchartAreaInvertedSeriesItem: Highchart {
	
		public HighchartAreaInvertedSeriesItem():base(){
			Name="John";
			Data=new List<Nullable<float>>{3f,4f,3f,5f,4f,10f,12f};
	
		}
	
		[InputText("name")]
		[JsonProperty("name")]
		public string Name{ get; set; }	[JsonProperty("data")]
		public List<Nullable<float>> Data{ get; set; }
	}
	
	
	public class HighchartAreaInverted: Highchart {
	
		public HighchartAreaInverted():base(){
			Chart=new HighchartAreaInvertedChart();
			Title=new HighchartAreaInvertedTitle();
			Accessibility=new HighchartAreaInvertedAccessibility();
			Legend=new HighchartAreaInvertedLegend();
			XAxis=new HighchartAreaInvertedXAxis();
			YAxis=new HighchartAreaInvertedYAxis();
			PlotOptions=new HighchartAreaInvertedPlotOptions();
			Series=new List<HighchartAreaInvertedSeriesItem>{new HighchartAreaInvertedSeriesItem(),new HighchartAreaInvertedSeriesItem()};
	
		}
	
		[JsonProperty("chart")]
		public HighchartAreaInvertedChart Chart{ get; set; }	[JsonProperty("title")]
		public HighchartAreaInvertedTitle Title{ get; set; }	[JsonProperty("accessibility")]
		public HighchartAreaInvertedAccessibility Accessibility{ get; set; }	[JsonProperty("legend")]
		public HighchartAreaInvertedLegend Legend{ get; set; }	[JsonProperty("xAxis")]
		public HighchartAreaInvertedXAxis XAxis{ get; set; }	[JsonProperty("yAxis")]
		public HighchartAreaInvertedYAxis YAxis{ get; set; }	[JsonProperty("plotOptions")]
		public HighchartAreaInvertedPlotOptions PlotOptions{ get; set; }	[JsonProperty("series")]
		public List<HighchartAreaInvertedSeriesItem> Series{ get; set; }
	}
	
	
}