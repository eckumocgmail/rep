using System.Collections.Generic;

using Newtonsoft.Json;
using System;

namespace Highcharts.Models.HighchartComboMultiAxes
{
	public class HighchartComboMultiAxesChart: Highchart {
	
		public HighchartComboMultiAxesChart():base(){
			ZoomType="xy";
	
		}
	
		[InputText("zoomType")]
		[JsonProperty("zoomType")]
		public string ZoomType{ get; set; }
	}
	
	
	public class HighchartComboMultiAxesTitle: Highchart {
	
		public HighchartComboMultiAxesTitle():base(){
			Text="Average Monthly Weather Data for Tokyo";
			Align="left";
	
		}
	
		[InputText("text")]
		[JsonProperty("text")]
		public string Text{ get; set; }	[InputText("align")]
		[JsonProperty("align")]
		public string Align{ get; set; }
	}
	
	
	public class HighchartComboMultiAxesSubtitle: Highchart {
	
		public HighchartComboMultiAxesSubtitle():base(){
			Text="Source: WorldClimate.com";
			Align="left";
	
		}
	
		[InputText("text")]
		[JsonProperty("text")]
		public string Text{ get; set; }	[InputText("align")]
		[JsonProperty("align")]
		public string Align{ get; set; }
	}
	
	
	public class HighchartComboMultiAxesXAxisItemCrosshair: Highchart {
	
		public HighchartComboMultiAxesXAxisItemCrosshair():base(){
	
		}
	
	
	}
	
	
	public class HighchartComboMultiAxesXAxisItem: Highchart {
	
		public HighchartComboMultiAxesXAxisItem():base(){
			Categories=new List<string>{"Jan","Feb","Mar","Apr","May","Jun","Jul","Aug","Sep","Oct","Nov","Dec"};
			Crosshair=new HighchartComboMultiAxesXAxisItemCrosshair();
	
		}
	
		[JsonProperty("categories")]
		public List<string> Categories{ get; set; }	[JsonProperty("crosshair")]
		public HighchartComboMultiAxesXAxisItemCrosshair Crosshair{ get; set; }
	}
	
	
	public class HighchartComboMultiAxesYAxisItemLabelsStyle: Highchart {
	
		public HighchartComboMultiAxesYAxisItemLabelsStyle():base(){
			Color="#90ed7d";
	
		}
	
		[InputText("color")]
		[JsonProperty("color")]
		public string Color{ get; set; }
	}
	
	
	public class HighchartComboMultiAxesYAxisItemLabels: Highchart {
	
		public HighchartComboMultiAxesYAxisItemLabels():base(){
			Format="{value}°C";
			Style=new HighchartComboMultiAxesYAxisItemLabelsStyle();
	
		}
	
		[InputText("format")]
		[JsonProperty("format")]
		public string Format{ get; set; }	[JsonProperty("style")]
		public HighchartComboMultiAxesYAxisItemLabelsStyle Style{ get; set; }
	}
	
	
	public class HighchartComboMultiAxesYAxisItemTitleStyle: Highchart {
	
		public HighchartComboMultiAxesYAxisItemTitleStyle():base(){
			Color="#90ed7d";
	
		}
	
		[InputText("color")]
		[JsonProperty("color")]
		public string Color{ get; set; }
	}
	
	
	public class HighchartComboMultiAxesYAxisItemTitle: Highchart {
	
		public HighchartComboMultiAxesYAxisItemTitle():base(){
			Text="Temperature";
			Style=new HighchartComboMultiAxesYAxisItemTitleStyle();
	
		}
	
		[InputText("text")]
		[JsonProperty("text")]
		public string Text{ get; set; }	[JsonProperty("style")]
		public HighchartComboMultiAxesYAxisItemTitleStyle Style{ get; set; }
	}
	
	
	public class HighchartComboMultiAxesYAxisItemOpposite: Highchart {
	
		public HighchartComboMultiAxesYAxisItemOpposite():base(){
	
		}
	
	
	}
	
	
	public class HighchartComboMultiAxesYAxisItem: Highchart {
	
		public HighchartComboMultiAxesYAxisItem():base(){
			Labels=new HighchartComboMultiAxesYAxisItemLabels();
			Title=new HighchartComboMultiAxesYAxisItemTitle();
			Opposite=new HighchartComboMultiAxesYAxisItemOpposite();
	
		}
	
		[JsonProperty("labels")]
		public HighchartComboMultiAxesYAxisItemLabels Labels{ get; set; }	[JsonProperty("title")]
		public HighchartComboMultiAxesYAxisItemTitle Title{ get; set; }	[JsonProperty("opposite")]
		public HighchartComboMultiAxesYAxisItemOpposite Opposite{ get; set; }
	}
	
	
	public class HighchartComboMultiAxesTooltipShared: Highchart {
	
		public HighchartComboMultiAxesTooltipShared():base(){
	
		}
	
	
	}
	
	
	public class HighchartComboMultiAxesTooltip: Highchart {
	
		public HighchartComboMultiAxesTooltip():base(){
			Shared=new HighchartComboMultiAxesTooltipShared();
	
		}
	
		[JsonProperty("shared")]
		public HighchartComboMultiAxesTooltipShared Shared{ get; set; }
	}
	
	
	public class HighchartComboMultiAxesLegendFloating: Highchart {
	
		public HighchartComboMultiAxesLegendFloating():base(){
	
		}
	
	
	}
	
	
	public class HighchartComboMultiAxesLegend: Highchart {
	
		public HighchartComboMultiAxesLegend():base(){
			Layout="vertical";
			Align="left";
			X=80f;
			VerticalAlign="top";
			Y=55f;
			Floating=new HighchartComboMultiAxesLegendFloating();
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
		public HighchartComboMultiAxesLegendFloating Floating{ get; set; }	[InputText("backgroundColor")]
		[JsonProperty("backgroundColor")]
		public string BackgroundColor{ get; set; }
	}
	
	
	public class HighchartComboMultiAxesSeriesItemTooltip: Highchart {
	
		public HighchartComboMultiAxesSeriesItemTooltip():base(){
			ValueSuffix=" mm";
	
		}
	
		[InputText("valueSuffix")]
		[JsonProperty("valueSuffix")]
		public string ValueSuffix{ get; set; }
	}
	
	
	public class HighchartComboMultiAxesSeriesItem: Highchart {
	
		public HighchartComboMultiAxesSeriesItem():base(){
			Name="Rainfall";
			Type="column";
			YAxis=1f;
			Data=new List<Nullable<float>>{49.9f,71.5f,106.4f,129.2f,144f,176f,135.6f,148.5f,216.4f,194.1f,95.6f,54.4f};
			Tooltip=new HighchartComboMultiAxesSeriesItemTooltip();
	
		}
	
		[InputText("name")]
		[JsonProperty("name")]
		public string Name{ get; set; }	[InputText("type")]
		[JsonProperty("type")]
		public string Type{ get; set; }	[InputNumber("yAxis")]
		[JsonProperty("yAxis")]
		public float YAxis{ get; set; }	[JsonProperty("data")]
		public List<Nullable<float>> Data{ get; set; }	[JsonProperty("tooltip")]
		public HighchartComboMultiAxesSeriesItemTooltip Tooltip{ get; set; }
	}
	
	
	public class HighchartComboMultiAxesResponsiveRulesItemCondition: Highchart {
	
		public HighchartComboMultiAxesResponsiveRulesItemCondition():base(){
			MaxWidth=500f;
	
		}
	
		[InputNumber("maxWidth")]
		[JsonProperty("maxWidth")]
		public float MaxWidth{ get; set; }
	}
	
	
	public class HighchartComboMultiAxesResponsiveRulesItemChartOptionsLegend: Highchart {
	
		public HighchartComboMultiAxesResponsiveRulesItemChartOptionsLegend():base(){
			Floating=null;
			Layout="horizontal";
			Align="center";
			VerticalAlign="bottom";
			X=null;
			Y=null;
	
		}
	
		[JsonProperty("floating")]
		public object Floating{ get; set; }	[InputText("layout")]
		[JsonProperty("layout")]
		public string Layout{ get; set; }	[InputText("align")]
		[JsonProperty("align")]
		public string Align{ get; set; }	[InputText("verticalAlign")]
		[JsonProperty("verticalAlign")]
		public string VerticalAlign{ get; set; }	[JsonProperty("x")]
		public object X{ get; set; }	[JsonProperty("y")]
		public object Y{ get; set; }
	}
	
	
	public class HighchartComboMultiAxesResponsiveRulesItemChartOptionsYAxisItemLabels: Highchart {
	
		public HighchartComboMultiAxesResponsiveRulesItemChartOptionsYAxisItemLabels():base(){
			Align="right";
			X=null;
			Y=-6f;
	
		}
	
		[InputText("align")]
		[JsonProperty("align")]
		public string Align{ get; set; }	[JsonProperty("x")]
		public object X{ get; set; }	[InputNumber("y")]
		[JsonProperty("y")]
		public float Y{ get; set; }
	}
	
	
	public class HighchartComboMultiAxesResponsiveRulesItemChartOptionsYAxisItem: Highchart {
	
		public HighchartComboMultiAxesResponsiveRulesItemChartOptionsYAxisItem():base(){
			Labels=new HighchartComboMultiAxesResponsiveRulesItemChartOptionsYAxisItemLabels();
			ShowLastLabel=null;
	
		}
	
		[JsonProperty("labels")]
		public HighchartComboMultiAxesResponsiveRulesItemChartOptionsYAxisItemLabels Labels{ get; set; }	[JsonProperty("showLastLabel")]
		public object ShowLastLabel{ get; set; }
	}
	
	
	public class HighchartComboMultiAxesResponsiveRulesItemChartOptions: Highchart {
	
		public HighchartComboMultiAxesResponsiveRulesItemChartOptions():base(){
			Legend=new HighchartComboMultiAxesResponsiveRulesItemChartOptionsLegend();
			YAxis=new List<HighchartComboMultiAxesResponsiveRulesItemChartOptionsYAxisItem>{new HighchartComboMultiAxesResponsiveRulesItemChartOptionsYAxisItem(),new HighchartComboMultiAxesResponsiveRulesItemChartOptionsYAxisItem(),new HighchartComboMultiAxesResponsiveRulesItemChartOptionsYAxisItem()};
	
		}
	
		[JsonProperty("legend")]
		public HighchartComboMultiAxesResponsiveRulesItemChartOptionsLegend Legend{ get; set; }	[JsonProperty("yAxis")]
		public List<HighchartComboMultiAxesResponsiveRulesItemChartOptionsYAxisItem> YAxis{ get; set; }
	}
	
	
	public class HighchartComboMultiAxesResponsiveRulesItem: Highchart {
	
		public HighchartComboMultiAxesResponsiveRulesItem():base(){
			Condition=new HighchartComboMultiAxesResponsiveRulesItemCondition();
			ChartOptions=new HighchartComboMultiAxesResponsiveRulesItemChartOptions();
	
		}
	
		[JsonProperty("condition")]
		public HighchartComboMultiAxesResponsiveRulesItemCondition Condition{ get; set; }	[JsonProperty("chartOptions")]
		public HighchartComboMultiAxesResponsiveRulesItemChartOptions ChartOptions{ get; set; }
	}
	
	
	public class HighchartComboMultiAxesResponsive: Highchart {
	
		public HighchartComboMultiAxesResponsive():base(){
			Rules=new List<HighchartComboMultiAxesResponsiveRulesItem>{new HighchartComboMultiAxesResponsiveRulesItem()};
	
		}
	
		[JsonProperty("rules")]
		public List<HighchartComboMultiAxesResponsiveRulesItem> Rules{ get; set; }
	}
	
	
	public class HighchartComboMultiAxes: Highchart {
	
		public HighchartComboMultiAxes():base(){
			Chart=new HighchartComboMultiAxesChart();
			Title=new HighchartComboMultiAxesTitle();
			Subtitle=new HighchartComboMultiAxesSubtitle();
			XAxis=new List<HighchartComboMultiAxesXAxisItem>{new HighchartComboMultiAxesXAxisItem()};
			YAxis=new List<HighchartComboMultiAxesYAxisItem>{new HighchartComboMultiAxesYAxisItem(),new HighchartComboMultiAxesYAxisItem(),new HighchartComboMultiAxesYAxisItem()};
			Tooltip=new HighchartComboMultiAxesTooltip();
			Legend=new HighchartComboMultiAxesLegend();
			Series=new List<HighchartComboMultiAxesSeriesItem>{new HighchartComboMultiAxesSeriesItem(),new HighchartComboMultiAxesSeriesItem(),new HighchartComboMultiAxesSeriesItem()};
			Responsive=new HighchartComboMultiAxesResponsive();
	
		}
	
		[JsonProperty("chart")]
		public HighchartComboMultiAxesChart Chart{ get; set; }	[JsonProperty("title")]
		public HighchartComboMultiAxesTitle Title{ get; set; }	[JsonProperty("subtitle")]
		public HighchartComboMultiAxesSubtitle Subtitle{ get; set; }	[JsonProperty("xAxis")]
		public List<HighchartComboMultiAxesXAxisItem> XAxis{ get; set; }	[JsonProperty("yAxis")]
		public List<HighchartComboMultiAxesYAxisItem> YAxis{ get; set; }	[JsonProperty("tooltip")]
		public HighchartComboMultiAxesTooltip Tooltip{ get; set; }	[JsonProperty("legend")]
		public HighchartComboMultiAxesLegend Legend{ get; set; }	[JsonProperty("series")]
		public List<HighchartComboMultiAxesSeriesItem> Series{ get; set; }	[JsonProperty("responsive")]
		public HighchartComboMultiAxesResponsive Responsive{ get; set; }
	}
	
	
}