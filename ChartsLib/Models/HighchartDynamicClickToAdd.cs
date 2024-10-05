using System.Collections.Generic;

using Newtonsoft.Json;
using System;

namespace Highcharts.Models.HighchartDynamicClickToAdd
{
	public class HighchartDynamicClickToAddChartEvents: Highchart {
	
		public HighchartDynamicClickToAddChartEvents():base(){
			Click=()=>{};
	
		}
	
		[JsonProperty("click")]
		[JsonIgnore()]
		public Action Click{ get; set; }
	}
	
	
	public class HighchartDynamicClickToAddChart: Highchart {
	
		public HighchartDynamicClickToAddChart():base(){
			Type="scatter";
			Margin=new List<Nullable<float>>{70f,50f,60f,80f};
			Events=new HighchartDynamicClickToAddChartEvents();
	
		}
	
		[InputText("type")]
		[JsonProperty("type")]
		public string Type{ get; set; }	[JsonProperty("margin")]
		public List<Nullable<float>> Margin{ get; set; }	[JsonProperty("events")]
		public HighchartDynamicClickToAddChartEvents Events{ get; set; }
	}
	
	
	public class HighchartDynamicClickToAddTitle: Highchart {
	
		public HighchartDynamicClickToAddTitle():base(){
			Text="User supplied data";
	
		}
	
		[InputText("text")]
		[JsonProperty("text")]
		public string Text{ get; set; }
	}
	
	
	public class HighchartDynamicClickToAddSubtitle: Highchart {
	
		public HighchartDynamicClickToAddSubtitle():base(){
			Text="Click the plot area to add a point. Click a point to remove it.";
	
		}
	
		[InputText("text")]
		[JsonProperty("text")]
		public string Text{ get; set; }
	}
	
	
	public class HighchartDynamicClickToAddAccessibilityAnnounceNewDataEnabled: Highchart {
	
		public HighchartDynamicClickToAddAccessibilityAnnounceNewDataEnabled():base(){
	
		}
	
	
	}
	
	
	public class HighchartDynamicClickToAddAccessibilityAnnounceNewData: Highchart {
	
		public HighchartDynamicClickToAddAccessibilityAnnounceNewData():base(){
			Enabled=new HighchartDynamicClickToAddAccessibilityAnnounceNewDataEnabled();
	
		}
	
		[JsonProperty("enabled")]
		public HighchartDynamicClickToAddAccessibilityAnnounceNewDataEnabled Enabled{ get; set; }
	}
	
	
	public class HighchartDynamicClickToAddAccessibility: Highchart {
	
		public HighchartDynamicClickToAddAccessibility():base(){
			AnnounceNewData=new HighchartDynamicClickToAddAccessibilityAnnounceNewData();
	
		}
	
		[JsonProperty("announceNewData")]
		public HighchartDynamicClickToAddAccessibilityAnnounceNewData AnnounceNewData{ get; set; }
	}
	
	
	public class HighchartDynamicClickToAddXAxis: Highchart {
	
		public HighchartDynamicClickToAddXAxis():base(){
			GridLineWidth=1f;
			MinPadding=0.2f;
			MaxPadding=0.2f;
			MaxZoom=60f;
	
		}
	
		[InputNumber("gridLineWidth")]
		[JsonProperty("gridLineWidth")]
		public float GridLineWidth{ get; set; }	[InputNumber("minPadding")]
		[JsonProperty("minPadding")]
		public float MinPadding{ get; set; }	[InputNumber("maxPadding")]
		[JsonProperty("maxPadding")]
		public float MaxPadding{ get; set; }	[InputNumber("maxZoom")]
		[JsonProperty("maxZoom")]
		public float MaxZoom{ get; set; }
	}
	
	
	public class HighchartDynamicClickToAddYAxisTitle: Highchart {
	
		public HighchartDynamicClickToAddYAxisTitle():base(){
			Text="Value";
	
		}
	
		[InputText("text")]
		[JsonProperty("text")]
		public string Text{ get; set; }
	}
	
	
	public class HighchartDynamicClickToAddYAxisPlotLinesItem: Highchart {
	
		public HighchartDynamicClickToAddYAxisPlotLinesItem():base(){
			Value=null;
			ChartWidth=1f;
			Color="#808080";
	
		}
	
		[JsonProperty("value")]
		public object Value{ get; set; }
		
		[InputNumber("width")]
		[JsonProperty("width")]
		public float ChartWidth{ get; set; }
		
		[InputText("color")]
		[JsonProperty("color")]
		public string Color{ get; set; }
	}
	
	
	public class HighchartDynamicClickToAddYAxis: Highchart {
	
		public HighchartDynamicClickToAddYAxis():base(){
			Title=new HighchartDynamicClickToAddYAxisTitle();
			MinPadding=0.2f;
			MaxPadding=0.2f;
			MaxZoom=60f;
			PlotLines=new List<HighchartDynamicClickToAddYAxisPlotLinesItem>{new HighchartDynamicClickToAddYAxisPlotLinesItem()};
	
		}
	
		[JsonProperty("title")]
		public HighchartDynamicClickToAddYAxisTitle Title{ get; set; }	[InputNumber("minPadding")]
		[JsonProperty("minPadding")]
		public float MinPadding{ get; set; }	[InputNumber("maxPadding")]
		[JsonProperty("maxPadding")]
		public float MaxPadding{ get; set; }	[InputNumber("maxZoom")]
		[JsonProperty("maxZoom")]
		public float MaxZoom{ get; set; }	[JsonProperty("plotLines")]
		public List<HighchartDynamicClickToAddYAxisPlotLinesItem> PlotLines{ get; set; }
	}
	
	
	public class HighchartDynamicClickToAddLegend: Highchart {
	
		public HighchartDynamicClickToAddLegend():base(){
			Enabled=null;
	
		}
	
		[JsonProperty("enabled")]
		public object Enabled{ get; set; }
	}
	
	
	public class HighchartDynamicClickToAddExporting: Highchart {
	
		public HighchartDynamicClickToAddExporting():base(){
			Enabled=null;
	
		}
	
		[JsonProperty("enabled")]
		public object Enabled{ get; set; }
	}
	
	
	public class HighchartDynamicClickToAddPlotOptionsSeriesPointEvents: Highchart {
	
		public HighchartDynamicClickToAddPlotOptionsSeriesPointEvents():base(){
			Click=()=>{};
	
		}
	
		[JsonProperty("click")]
		[JsonIgnore()]
		public Action Click{ get; set; }
	}
	
	
	public class HighchartDynamicClickToAddPlotOptionsSeriesPoint: Highchart {
	
		public HighchartDynamicClickToAddPlotOptionsSeriesPoint():base(){
			Events=new HighchartDynamicClickToAddPlotOptionsSeriesPointEvents();
	
		}
	
		[JsonProperty("events")]
		public HighchartDynamicClickToAddPlotOptionsSeriesPointEvents Events{ get; set; }
	}
	
	
	public class HighchartDynamicClickToAddPlotOptionsSeries: Highchart {
	
		public HighchartDynamicClickToAddPlotOptionsSeries():base(){
			LineWidth=1f;
			Point=new HighchartDynamicClickToAddPlotOptionsSeriesPoint();
	
		}
	
		[InputNumber("lineWidth")]
		[JsonProperty("lineWidth")]
		public float LineWidth{ get; set; }	[JsonProperty("point")]
		public HighchartDynamicClickToAddPlotOptionsSeriesPoint Point{ get; set; }
	}
	
	
	public class HighchartDynamicClickToAddPlotOptions: Highchart {
	
		public HighchartDynamicClickToAddPlotOptions():base(){
			Series=new HighchartDynamicClickToAddPlotOptionsSeries();
	
		}
	
		[JsonProperty("series")]
		public HighchartDynamicClickToAddPlotOptionsSeries Series{ get; set; }
	}
	
	
	public class HighchartDynamicClickToAddSeriesItem: Highchart {
	
		public HighchartDynamicClickToAddSeriesItem():base(){
			Data=new List<List<Nullable<float>>>{new List<Nullable<float>>{20f,20f},new List<Nullable<float>>{80f,80f}};
	
		}
	
		[JsonProperty("data")]
		public List<List<Nullable<float>>> Data{ get; set; }
	}
	
	
	public class HighchartDynamicClickToAdd: Highchart {
	
		public HighchartDynamicClickToAdd():base(){
			Chart=new HighchartDynamicClickToAddChart();
			Title=new HighchartDynamicClickToAddTitle();
			Subtitle=new HighchartDynamicClickToAddSubtitle();
			Accessibility=new HighchartDynamicClickToAddAccessibility();
			XAxis=new HighchartDynamicClickToAddXAxis();
			YAxis=new HighchartDynamicClickToAddYAxis();
			Legend=new HighchartDynamicClickToAddLegend();
			Exporting=new HighchartDynamicClickToAddExporting();
			PlotOptions=new HighchartDynamicClickToAddPlotOptions();
			Series=new List<HighchartDynamicClickToAddSeriesItem>{new HighchartDynamicClickToAddSeriesItem()};
	
		}
	
		[JsonProperty("chart")]
		public HighchartDynamicClickToAddChart Chart{ get; set; }	[JsonProperty("title")]
		public HighchartDynamicClickToAddTitle Title{ get; set; }	[JsonProperty("subtitle")]
		public HighchartDynamicClickToAddSubtitle Subtitle{ get; set; }	[JsonProperty("accessibility")]
		public HighchartDynamicClickToAddAccessibility Accessibility{ get; set; }	[JsonProperty("xAxis")]
		public HighchartDynamicClickToAddXAxis XAxis{ get; set; }	[JsonProperty("yAxis")]
		public HighchartDynamicClickToAddYAxis YAxis{ get; set; }	[JsonProperty("legend")]
		public HighchartDynamicClickToAddLegend Legend{ get; set; }	[JsonProperty("exporting")]
		public HighchartDynamicClickToAddExporting Exporting{ get; set; }	[JsonProperty("plotOptions")]
		public HighchartDynamicClickToAddPlotOptions PlotOptions{ get; set; }	[JsonProperty("series")]
		public List<HighchartDynamicClickToAddSeriesItem> Series{ get; set; }
	}
	
	
}