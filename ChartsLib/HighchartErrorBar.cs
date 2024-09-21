using System.Collections.Generic;

using Newtonsoft.Json;
using System;

namespace Highcharts.Models.HighchartErrorBar
{
	public class HighchartErrorBarChart: Highchart {
	
		public HighchartErrorBarChart():base(){
			ZoomType="xy";
	
		}
	
		[InputText("zoomType")]
		[JsonProperty("zoomType")]
		public string ZoomType{ get; set; }
	}
	
	
	public class HighchartErrorBarTitle: Highchart {
	
		public HighchartErrorBarTitle():base(){
			Text="Temperature vs Rainfall";
	
		}
	
		[InputText("text")]
		[JsonProperty("text")]
		public string Text{ get; set; }
	}
	
	
	public class HighchartErrorBarXAxisItem: Highchart {
	
		public HighchartErrorBarXAxisItem():base(){
			Categories=new List<string>{"Jan","Feb","Mar","Apr","May","Jun","Jul","Aug","Sep","Oct","Nov","Dec"};
	
		}
	
		[JsonProperty("categories")]
		public List<string> Categories{ get; set; }
	}
	
	
	public class HighchartErrorBarYAxisItemLabelsStyle: Highchart {
	
		public HighchartErrorBarYAxisItemLabelsStyle():base(){
			Color="#434348";
	
		}
	
		[InputText("color")]
		[JsonProperty("color")]
		public string Color{ get; set; }
	}
	
	
	public class HighchartErrorBarYAxisItemLabels: Highchart {
	
		public HighchartErrorBarYAxisItemLabels():base(){
			Format="{value} °C";
			Style=new HighchartErrorBarYAxisItemLabelsStyle();
	
		}
	
		[InputText("format")]
		[JsonProperty("format")]
		public string Format{ get; set; }	[JsonProperty("style")]
		public HighchartErrorBarYAxisItemLabelsStyle Style{ get; set; }
	}
	
	
	public class HighchartErrorBarYAxisItemTitleStyle: Highchart {
	
		public HighchartErrorBarYAxisItemTitleStyle():base(){
			Color="#434348";
	
		}
	
		[InputText("color")]
		[JsonProperty("color")]
		public string Color{ get; set; }
	}
	
	
	public class HighchartErrorBarYAxisItemTitle: Highchart {
	
		public HighchartErrorBarYAxisItemTitle():base(){
			Text="Temperature";
			Style=new HighchartErrorBarYAxisItemTitleStyle();
	
		}
	
		[InputText("text")]
		[JsonProperty("text")]
		public string Text{ get; set; }	[JsonProperty("style")]
		public HighchartErrorBarYAxisItemTitleStyle Style{ get; set; }
	}
	
	
	public class HighchartErrorBarYAxisItem: Highchart {
	
		public HighchartErrorBarYAxisItem():base(){
			Labels=new HighchartErrorBarYAxisItemLabels();
			Title=new HighchartErrorBarYAxisItemTitle();
	
		}
	
		[JsonProperty("labels")]
		public HighchartErrorBarYAxisItemLabels Labels{ get; set; }	[JsonProperty("title")]
		public HighchartErrorBarYAxisItemTitle Title{ get; set; }
	}
	
	
	public class HighchartErrorBarTooltipShared: Highchart {
	
		public HighchartErrorBarTooltipShared():base(){
	
		}
	
	
	}
	
	
	public class HighchartErrorBarTooltip: Highchart {
	
		public HighchartErrorBarTooltip():base(){
			Shared=new HighchartErrorBarTooltipShared();
	
		}
	
		[JsonProperty("shared")]
		public HighchartErrorBarTooltipShared Shared{ get; set; }
	}
	
	
	public class HighchartErrorBarSeriesItemTooltip: Highchart {
	
		public HighchartErrorBarSeriesItemTooltip():base(){
			PointFormat="<span style='font-weight: bold; color: {series.color}'>{series.name}</span>: <b>{point.y:.1f} mm</b> ";
	
		}
	
		[InputText("pointFormat")]
		[JsonProperty("pointFormat")]
		public string PointFormat{ get; set; }
	}
	
	
	public class HighchartErrorBarSeriesItem: Highchart {
	
		public HighchartErrorBarSeriesItem():base(){
			Name="Rainfall";
			Type="column";
			YAxis=1f;
			Data=new List<Nullable<float>>{49.9f,71.5f,106.4f,129.2f,144f,176f,135.6f,148.5f,216.4f,194.1f,95.6f,54.4f};
			Tooltip=new HighchartErrorBarSeriesItemTooltip();
	
		}
	
		[InputText("name")]
		[JsonProperty("name")]
		public string Name{ get; set; }	[InputText("type")]
		[JsonProperty("type")]
		public string Type{ get; set; }	[InputNumber("yAxis")]
		[JsonProperty("yAxis")]
		public float YAxis{ get; set; }	[JsonProperty("data")]
		public List<Nullable<float>> Data{ get; set; }	[JsonProperty("tooltip")]
		public HighchartErrorBarSeriesItemTooltip Tooltip{ get; set; }
	}
	
	
	public class HighchartErrorBar: Highchart {
	
		public HighchartErrorBar():base(){
			Chart=new HighchartErrorBarChart();
			Title=new HighchartErrorBarTitle();
			XAxis=new List<HighchartErrorBarXAxisItem>{new HighchartErrorBarXAxisItem()};
			YAxis=new List<HighchartErrorBarYAxisItem>{new HighchartErrorBarYAxisItem(),new HighchartErrorBarYAxisItem()};
			Tooltip=new HighchartErrorBarTooltip();
			Series=new List<HighchartErrorBarSeriesItem>{new HighchartErrorBarSeriesItem(),new HighchartErrorBarSeriesItem(),new HighchartErrorBarSeriesItem(),new HighchartErrorBarSeriesItem()};
	
		}
	
		[JsonProperty("chart")]
		public HighchartErrorBarChart Chart{ get; set; }	[JsonProperty("title")]
		public HighchartErrorBarTitle Title{ get; set; }	[JsonProperty("xAxis")]
		public List<HighchartErrorBarXAxisItem> XAxis{ get; set; }	[JsonProperty("yAxis")]
		public List<HighchartErrorBarYAxisItem> YAxis{ get; set; }	[JsonProperty("tooltip")]
		public HighchartErrorBarTooltip Tooltip{ get; set; }	[JsonProperty("series")]
		public List<HighchartErrorBarSeriesItem> Series{ get; set; }
	}
	
	
}