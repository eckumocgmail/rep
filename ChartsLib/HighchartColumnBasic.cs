using System.Collections.Generic;

using Newtonsoft.Json;
using System;

namespace Highcharts.Models.HighchartColumnBasic
{
	public class HighchartColumnBasicChart: Highchart {
	
		public HighchartColumnBasicChart():base(){
			Type="column";
	
		}
	
		[InputText("type")]
		[JsonProperty("type")]
		public string Type{ get; set; }
	}
	
	
	public class HighchartColumnBasicTitle: Highchart {
	
		public HighchartColumnBasicTitle():base(){
			Text="Monthly Average Rainfall";
	
		}
	
		[InputText("text")]
		[JsonProperty("text")]
		public string Text{ get; set; }
	}
	
	
	public class HighchartColumnBasicSubtitle: Highchart {
	
		public HighchartColumnBasicSubtitle():base(){
			Text="Source: WorldClimate.com";
	
		}
	
		[InputText("text")]
		[JsonProperty("text")]
		public string Text{ get; set; }
	}
	
	
	public class HighchartColumnBasicXAxisCrosshair: Highchart {
	
		public HighchartColumnBasicXAxisCrosshair():base(){
	
		}
	
	
	}
	
	
	public class HighchartColumnBasicXAxis: Highchart {
	
		public HighchartColumnBasicXAxis():base(){
			Categories=new List<string>{"Jan","Feb","Mar","Apr","May","Jun","Jul","Aug","Sep","Oct","Nov","Dec"};
			Crosshair=new HighchartColumnBasicXAxisCrosshair();
	
		}
	
		[JsonProperty("categories")]
		public List<string> Categories{ get; set; }	[JsonProperty("crosshair")]
		public HighchartColumnBasicXAxisCrosshair Crosshair{ get; set; }
	}
	
	
	public class HighchartColumnBasicYAxisTitle: Highchart {
	
		public HighchartColumnBasicYAxisTitle():base(){
			Text="Rainfall (mm)";
	
		}
	
		[InputText("text")]
		[JsonProperty("text")]
		public string Text{ get; set; }
	}
	
	
	public class HighchartColumnBasicYAxis: Highchart {
	
		public HighchartColumnBasicYAxis():base(){
			Min=null;
			Title=new HighchartColumnBasicYAxisTitle();
	
		}
	
		[JsonProperty("min")]
		public object Min{ get; set; }	[JsonProperty("title")]
		public HighchartColumnBasicYAxisTitle Title{ get; set; }
	}
	
	
	public class HighchartColumnBasicTooltipShared: Highchart {
	
		public HighchartColumnBasicTooltipShared():base(){
	
		}
	
	
	}
	
	
	public class HighchartColumnBasicTooltipUseHTML: Highchart {
	
		public HighchartColumnBasicTooltipUseHTML():base(){
	
		}
	
	
	}
	
	
	public class HighchartColumnBasicTooltip: Highchart {
	
		public HighchartColumnBasicTooltip():base(){
			HeaderFormat="<span style='font-size:10px'>{point.key}</span><table>";
			PointFormat="<tr><td style='color:{series.color};padding:0'>{series.name}: </td><td style='padding:0'><b>{point.y:.1f} mm</b></td></tr>";
			FooterFormat="</table>";
			Shared=new HighchartColumnBasicTooltipShared();
			UseHTML=new HighchartColumnBasicTooltipUseHTML();
	
		}
	
		[InputText("headerFormat")]
		[JsonProperty("headerFormat")]
		public string HeaderFormat{ get; set; }	[InputText("pointFormat")]
		[JsonProperty("pointFormat")]
		public string PointFormat{ get; set; }	[InputText("footerFormat")]
		[JsonProperty("footerFormat")]
		public string FooterFormat{ get; set; }	[JsonProperty("shared")]
		public HighchartColumnBasicTooltipShared Shared{ get; set; }	[JsonProperty("useHTML")]
		public HighchartColumnBasicTooltipUseHTML UseHTML{ get; set; }
	}
	
	
	public class HighchartColumnBasicPlotOptionsColumn: Highchart {
	
		public HighchartColumnBasicPlotOptionsColumn():base(){
			PointPadding=0.2f;
			BorderWidth=null;
	
		}
	
		[InputNumber("pointPadding")]
		[JsonProperty("pointPadding")]
		public float PointPadding{ get; set; }	[JsonProperty("borderWidth")]
		public object BorderWidth{ get; set; }
	}
	
	
	public class HighchartColumnBasicPlotOptions: Highchart {
	
		public HighchartColumnBasicPlotOptions():base(){
			Column=new HighchartColumnBasicPlotOptionsColumn();
	
		}
	
		[JsonProperty("column")]
		public HighchartColumnBasicPlotOptionsColumn Column{ get; set; }
	}
	
	
	public class HighchartColumnBasicSeriesItem: Highchart {
	
		public HighchartColumnBasicSeriesItem():base(){
			Name="Tokyo";
			Data=new List<Nullable<float>>{49.9f,71.5f,106.4f,129.2f,144f,176f,135.6f,148.5f,216.4f,194.1f,95.6f,54.4f};
	
		}
	
		[InputText("name")]
		[JsonProperty("name")]
		public string Name{ get; set; }	[JsonProperty("data")]
		public List<Nullable<float>> Data{ get; set; }
	}
	
	
	public class HighchartColumnBasic: Highchart {
	
		public HighchartColumnBasic():base(){
			Chart=new HighchartColumnBasicChart();
			Title=new HighchartColumnBasicTitle();
			Subtitle=new HighchartColumnBasicSubtitle();
			XAxis=new HighchartColumnBasicXAxis();
			YAxis=new HighchartColumnBasicYAxis();
			Tooltip=new HighchartColumnBasicTooltip();
			PlotOptions=new HighchartColumnBasicPlotOptions();
			Series=new List<HighchartColumnBasicSeriesItem>{new HighchartColumnBasicSeriesItem(),new HighchartColumnBasicSeriesItem(),new HighchartColumnBasicSeriesItem(),new HighchartColumnBasicSeriesItem()};
	
		}
	
		[JsonProperty("chart")]
		public HighchartColumnBasicChart Chart{ get; set; }	[JsonProperty("title")]
		public HighchartColumnBasicTitle Title{ get; set; }	[JsonProperty("subtitle")]
		public HighchartColumnBasicSubtitle Subtitle{ get; set; }	[JsonProperty("xAxis")]
		public HighchartColumnBasicXAxis XAxis{ get; set; }	[JsonProperty("yAxis")]
		public HighchartColumnBasicYAxis YAxis{ get; set; }	[JsonProperty("tooltip")]
		public HighchartColumnBasicTooltip Tooltip{ get; set; }	[JsonProperty("plotOptions")]
		public HighchartColumnBasicPlotOptions PlotOptions{ get; set; }	[JsonProperty("series")]
		public List<HighchartColumnBasicSeriesItem> Series{ get; set; }
	}
	
	
}