using System.Collections.Generic;

using Newtonsoft.Json;
using System;

namespace Highcharts.Models.HighchartComboDualAxes
{
	public class HighchartComboDualAxesChart: Highchart {
	
		public HighchartComboDualAxesChart():base(){
			ZoomType="xy";
	
		}
	
		[InputText("zoomType")]
		[JsonProperty("zoomType")]
		public string ZoomType{ get; set; }
	}
	
	
	public class HighchartComboDualAxesTitle: Highchart {
	
		public HighchartComboDualAxesTitle():base(){
			Text="Average Monthly Temperature and Rainfall in Tokyo";
	
		}
	
		[InputText("text")]
		[JsonProperty("text")]
		public string Text{ get; set; }
	}
	
	
	public class HighchartComboDualAxesSubtitle: Highchart {
	
		public HighchartComboDualAxesSubtitle():base(){
			Text="Source: WorldClimate.com";
	
		}
	
		[InputText("text")]
		[JsonProperty("text")]
		public string Text{ get; set; }
	}
	
	
	public class HighchartComboDualAxesXAxisItemCrosshair: Highchart {
	
		public HighchartComboDualAxesXAxisItemCrosshair():base(){
	
		}
	
	
	}
	
	
	public class HighchartComboDualAxesXAxisItem: Highchart {
	
		public HighchartComboDualAxesXAxisItem():base(){
			Categories=new List<string>{"Jan","Feb","Mar","Apr","May","Jun","Jul","Aug","Sep","Oct","Nov","Dec"};
			Crosshair=new HighchartComboDualAxesXAxisItemCrosshair();
	
		}
	
		[JsonProperty("categories")]
		public List<string> Categories{ get; set; }	[JsonProperty("crosshair")]
		public HighchartComboDualAxesXAxisItemCrosshair Crosshair{ get; set; }
	}
	
	
	public class HighchartComboDualAxesYAxisItemLabelsStyle: Highchart {
	
		public HighchartComboDualAxesYAxisItemLabelsStyle():base(){
			Color="#434348";
	
		}
	
		[InputText("color")]
		[JsonProperty("color")]
		public string Color{ get; set; }
	}
	
	
	public class HighchartComboDualAxesYAxisItemLabels: Highchart {
	
		public HighchartComboDualAxesYAxisItemLabels():base(){
			Format="{value}°C";
			Style=new HighchartComboDualAxesYAxisItemLabelsStyle();
	
		}
	
		[InputText("format")]
		[JsonProperty("format")]
		public string Format{ get; set; }	[JsonProperty("style")]
		public HighchartComboDualAxesYAxisItemLabelsStyle Style{ get; set; }
	}
	
	
	public class HighchartComboDualAxesYAxisItemTitleStyle: Highchart {
	
		public HighchartComboDualAxesYAxisItemTitleStyle():base(){
			Color="#434348";
	
		}
	
		[InputText("color")]
		[JsonProperty("color")]
		public string Color{ get; set; }
	}
	
	
	public class HighchartComboDualAxesYAxisItemTitle: Highchart {
	
		public HighchartComboDualAxesYAxisItemTitle():base(){
			Text="Temperature";
			Style=new HighchartComboDualAxesYAxisItemTitleStyle();
	
		}
	
		[InputText("text")]
		[JsonProperty("text")]
		public string Text{ get; set; }	[JsonProperty("style")]
		public HighchartComboDualAxesYAxisItemTitleStyle Style{ get; set; }
	}
	
	
	public class HighchartComboDualAxesYAxisItem: Highchart {
	
		public HighchartComboDualAxesYAxisItem():base(){
			Labels=new HighchartComboDualAxesYAxisItemLabels();
			Title=new HighchartComboDualAxesYAxisItemTitle();
	
		}
	
		[JsonProperty("labels")]
		public HighchartComboDualAxesYAxisItemLabels Labels{ get; set; }	[JsonProperty("title")]
		public HighchartComboDualAxesYAxisItemTitle Title{ get; set; }
	}
	
	
	public class HighchartComboDualAxesTooltipShared: Highchart {
	
		public HighchartComboDualAxesTooltipShared():base(){
	
		}
	
	
	}
	
	
	public class HighchartComboDualAxesTooltip: Highchart {
	
		public HighchartComboDualAxesTooltip():base(){
			Shared=new HighchartComboDualAxesTooltipShared();
	
		}
	
		[JsonProperty("shared")]
		public HighchartComboDualAxesTooltipShared Shared{ get; set; }
	}
	
	
	public class HighchartComboDualAxesLegendFloating: Highchart {
	
		public HighchartComboDualAxesLegendFloating():base(){
	
		}
	
	
	}
	
	
	public class HighchartComboDualAxesLegend: Highchart {
	
		public HighchartComboDualAxesLegend():base(){
			Layout="vertical";
			Align="left";
			X=120f;
			VerticalAlign="top";
			Y=100f;
			Floating=new HighchartComboDualAxesLegendFloating();
			BackgroundColor="rgba(255,255,255,0.25)";
	
		}
	
		[InputText("layout")]
		[JsonProperty("layout")]
		public string Layout{ get; set; }	[InputText("align")]
		[JsonProperty("align")]
		public string Align{ get; set; }	[InputNumber("x")]
		[JsonProperty("x")]
		public float X{ get; set; }	[InputText("verticalAlign")]
		[JsonProperty("verticalAlign")]
		public string VerticalAlign{ get; set; }	[InputNumber("y")]
		[JsonProperty("y")]
		public float Y{ get; set; }	[JsonProperty("floating")]
		public HighchartComboDualAxesLegendFloating Floating{ get; set; }	[InputText("backgroundColor")]
		[JsonProperty("backgroundColor")]
		public string BackgroundColor{ get; set; }
	}
	
	
	public class HighchartComboDualAxesSeriesItemTooltip: Highchart {
	
		public HighchartComboDualAxesSeriesItemTooltip():base(){
			ValueSuffix=" mm";
	
		}
	
		[InputText("valueSuffix")]
		[JsonProperty("valueSuffix")]
		public string ValueSuffix{ get; set; }
	}
	
	
	public class HighchartComboDualAxesSeriesItem: Highchart {
	
		public HighchartComboDualAxesSeriesItem():base(){
			Name="Rainfall";
			Type="column";
			YAxis=1f;
			Data=new List<Nullable<float>>{49.9f,71.5f,106.4f,129.2f,144f,176f,135.6f,148.5f,216.4f,194.1f,95.6f,54.4f};
			Tooltip=new HighchartComboDualAxesSeriesItemTooltip();
	
		}
	
		[InputText("name")]
		[JsonProperty("name")]
		public string Name{ get; set; }	[InputText("type")]
		[JsonProperty("type")]
		public string Type{ get; set; }	[InputNumber("yAxis")]
		[JsonProperty("yAxis")]
		public float YAxis{ get; set; }	[JsonProperty("data")]
		public List<Nullable<float>> Data{ get; set; }	[JsonProperty("tooltip")]
		public HighchartComboDualAxesSeriesItemTooltip Tooltip{ get; set; }
	}
	
	
	public class HighchartComboDualAxes: Highchart {
	
		public HighchartComboDualAxes():base(){
			Chart=new HighchartComboDualAxesChart();
			Title=new HighchartComboDualAxesTitle();
			Subtitle=new HighchartComboDualAxesSubtitle();
			XAxis=new List<HighchartComboDualAxesXAxisItem>{new HighchartComboDualAxesXAxisItem()};
			YAxis=new List<HighchartComboDualAxesYAxisItem>{new HighchartComboDualAxesYAxisItem(),new HighchartComboDualAxesYAxisItem()};
			Tooltip=new HighchartComboDualAxesTooltip();
			Legend=new HighchartComboDualAxesLegend();
			Series=new List<HighchartComboDualAxesSeriesItem>{new HighchartComboDualAxesSeriesItem(),new HighchartComboDualAxesSeriesItem()};
	
		}
	
		[JsonProperty("chart")]
		public HighchartComboDualAxesChart Chart{ get; set; }	[JsonProperty("title")]
		public HighchartComboDualAxesTitle Title{ get; set; }	[JsonProperty("subtitle")]
		public HighchartComboDualAxesSubtitle Subtitle{ get; set; }	[JsonProperty("xAxis")]
		public List<HighchartComboDualAxesXAxisItem> XAxis{ get; set; }	[JsonProperty("yAxis")]
		public List<HighchartComboDualAxesYAxisItem> YAxis{ get; set; }	[JsonProperty("tooltip")]
		public HighchartComboDualAxesTooltip Tooltip{ get; set; }	[JsonProperty("legend")]
		public HighchartComboDualAxesLegend Legend{ get; set; }	[JsonProperty("series")]
		public List<HighchartComboDualAxesSeriesItem> Series{ get; set; }
	}
	
	
}