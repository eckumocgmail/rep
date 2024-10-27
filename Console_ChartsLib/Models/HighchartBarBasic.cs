using System.Collections.Generic;

using Newtonsoft.Json;
using System;

namespace Highcharts.Models.HighchartBarBasic
{
	public class HighchartBarBasicChart: Highchart {
	
		public HighchartBarBasicChart():base(){
			Type="bar";
	
		}
	
		[InputText("type")]
		[JsonProperty("type")]
		public string Type{ get; set; }
	}
	
	
	public class HighchartBarBasicTitle: Highchart {
	
		public HighchartBarBasicTitle():base(){
			Text="Historic World Population by Region";
	
		}
	
		[InputText("text")]
		[JsonProperty("text")]
		public string Text{ get; set; }
	}
	
	
	public class HighchartBarBasicSubtitle: Highchart {
	
		public HighchartBarBasicSubtitle():base(){
			Text="Source: <a>Wikipedia.org</a>";
	
		}
	
		[InputText("text")]
		[JsonProperty("text")]
		public string Text{ get; set; }
	}
	
	
	public class HighchartBarBasicXAxisTitle: Highchart {
	
		public HighchartBarBasicXAxisTitle():base(){
			Text=null;
	
		}
	
		[JsonProperty("text")]
		public object Text{ get; set; }
	}
	
	
	public class HighchartBarBasicXAxis: Highchart {
	
		public HighchartBarBasicXAxis():base(){
			Categories=new List<string>{"Africa","America","Asia","Europe","Oceania"};
			Title=new HighchartBarBasicXAxisTitle();
	
		}
	
		[JsonProperty("categories")]
		public List<string> Categories{ get; set; }	[JsonProperty("title")]
		public HighchartBarBasicXAxisTitle Title{ get; set; }
	}
	
	
	public class HighchartBarBasicYAxisTitle: Highchart {
	
		public HighchartBarBasicYAxisTitle():base(){
			Text="Population (millions)";
			Align="high";
	
		}
	
		[InputText("text")]
		[JsonProperty("text")]
		public string Text{ get; set; }	[InputText("align")]
		[JsonProperty("align")]
		public string Align{ get; set; }
	}
	
	
	public class HighchartBarBasicYAxisLabels: Highchart {
	
		public HighchartBarBasicYAxisLabels():base(){
			Overflow="justify";
	
		}
	
		[InputText("overflow")]
		[JsonProperty("overflow")]
		public string Overflow{ get; set; }
	}
	
	
	public class HighchartBarBasicYAxis: Highchart {
	
		public HighchartBarBasicYAxis():base(){
			Min=null;
			Title=new HighchartBarBasicYAxisTitle();
			Labels=new HighchartBarBasicYAxisLabels();
	
		}
	
		[JsonProperty("min")]
		public object Min{ get; set; }	[JsonProperty("title")]
		public HighchartBarBasicYAxisTitle Title{ get; set; }	[JsonProperty("labels")]
		public HighchartBarBasicYAxisLabels Labels{ get; set; }
	}
	
	
	public class HighchartBarBasicTooltip: Highchart {
	
		public HighchartBarBasicTooltip():base(){
			ValueSuffix=" millions";
	
		}
	
		[InputText("valueSuffix")]
		[JsonProperty("valueSuffix")]
		public string ValueSuffix{ get; set; }
	}
	
	
	public class HighchartBarBasicPlotOptionsBarDataLabelsEnabled: Highchart {
	
		public HighchartBarBasicPlotOptionsBarDataLabelsEnabled():base(){
	
		}
	
	
	}
	
	
	public class HighchartBarBasicPlotOptionsBarDataLabels: Highchart {
	
		public HighchartBarBasicPlotOptionsBarDataLabels():base(){
			Enabled=new HighchartBarBasicPlotOptionsBarDataLabelsEnabled();
	
		}
	
		[JsonProperty("enabled")]
		public HighchartBarBasicPlotOptionsBarDataLabelsEnabled Enabled{ get; set; }
	}
	
	
	public class HighchartBarBasicPlotOptionsBar: Highchart {
	
		public HighchartBarBasicPlotOptionsBar():base(){
			DataLabels=new HighchartBarBasicPlotOptionsBarDataLabels();
	
		}
	
		[JsonProperty("dataLabels")]
		public HighchartBarBasicPlotOptionsBarDataLabels DataLabels{ get; set; }
	}
	
	
	public class HighchartBarBasicPlotOptions: Highchart {
	
		public HighchartBarBasicPlotOptions():base(){
			Bar=new HighchartBarBasicPlotOptionsBar();
	
		}
	
		[JsonProperty("bar")]
		public HighchartBarBasicPlotOptionsBar Bar{ get; set; }
	}
	
	
	public class HighchartBarBasicLegendFloating: Highchart {
	
		public HighchartBarBasicLegendFloating():base(){
	
		}
	
	
	}
	
	
	public class HighchartBarBasicLegendShadow: Highchart {
	
		public HighchartBarBasicLegendShadow():base(){
	
		}
	
	
	}
	
	
	public class HighchartBarBasicLegend: Highchart {
	
		public HighchartBarBasicLegend():base(){
			Layout="vertical";
			Align="right";
			VerticalAlign="top";
			X=-40f;
			Y=80f;
			Floating=new HighchartBarBasicLegendFloating();
			BorderWidth=1f;
			BackgroundColor="#FFFFFF";
			Shadow=new HighchartBarBasicLegendShadow();
	
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
		public HighchartBarBasicLegendFloating Floating{ get; set; }	[InputNumber("borderWidth")]


		[JsonProperty("borderWidth")]
		public float BorderWidth{ get; set; }	
		
		[InputText("backgroundColor")]
		[JsonProperty("backgroundColor")]
		public string BackgroundColor{ get; set; }	[JsonProperty("shadow")]
		public HighchartBarBasicLegendShadow Shadow{ get; set; }
	}
	
	
	public class HighchartBarBasicCredits: Highchart {
	
		public HighchartBarBasicCredits():base(){
			Enabled=null;
	
		}
	
		[JsonProperty("enabled")]
		public object Enabled{ get; set; }
	}
	
	
	public class HighchartBarBasicSeriesItem: Highchart {
	
		public HighchartBarBasicSeriesItem():base(){
			Name="Year 1800";
			Data=new List<Nullable<float>>{107f,31f,635f,203f,2f};
	
		}
	
		[InputText("name")]
		[JsonProperty("name")]
		public string Name{ get; set; }	[JsonProperty("data")]
		public List<Nullable<float>> Data{ get; set; }
	}
	
	
	public class HighchartBarBasic: Highchart {
	
		public HighchartBarBasic():base(){
			Chart=new HighchartBarBasicChart();
			Title=new HighchartBarBasicTitle();
			Subtitle=new HighchartBarBasicSubtitle();
			XAxis=new HighchartBarBasicXAxis();
			YAxis=new HighchartBarBasicYAxis();
			Tooltip=new HighchartBarBasicTooltip();
			PlotOptions=new HighchartBarBasicPlotOptions();
			Legend=new HighchartBarBasicLegend();
			Credits=new HighchartBarBasicCredits();
			Series=new List<HighchartBarBasicSeriesItem>{new HighchartBarBasicSeriesItem(),new HighchartBarBasicSeriesItem(),new HighchartBarBasicSeriesItem(),new HighchartBarBasicSeriesItem()};
	
		}
	
		[JsonProperty("chart")]
		public HighchartBarBasicChart Chart{ get; set; }	[JsonProperty("title")]
		public HighchartBarBasicTitle Title{ get; set; }	[JsonProperty("subtitle")]
		public HighchartBarBasicSubtitle Subtitle{ get; set; }	[JsonProperty("xAxis")]
		public HighchartBarBasicXAxis XAxis{ get; set; }	[JsonProperty("yAxis")]
		public HighchartBarBasicYAxis YAxis{ get; set; }	[JsonProperty("tooltip")]
		public HighchartBarBasicTooltip Tooltip{ get; set; }	[JsonProperty("plotOptions")]
		public HighchartBarBasicPlotOptions PlotOptions{ get; set; }	[JsonProperty("legend")]
		public HighchartBarBasicLegend Legend{ get; set; }	[JsonProperty("credits")]
		public HighchartBarBasicCredits Credits{ get; set; }	[JsonProperty("series")]
		public List<HighchartBarBasicSeriesItem> Series { get; set; } = new();
	}
	
	
}