using System.Collections.Generic;

using Newtonsoft.Json;
using System;

namespace Highcharts.Models.HighchartChartUpdate
{
	public class HighchartChartUpdateTitle: Highchart {
	
		public HighchartChartUpdateTitle():base(){
			Text="Chart.update";
	
		}
	
		[InputText("text")]
		[JsonProperty("text")]
		public string Text{ get; set; }
	}
	
	
	public class HighchartChartUpdateSubtitle: Highchart {
	
		public HighchartChartUpdateSubtitle():base(){
			Text="Plain";
	
		}
	
		[InputText("text")]
		[JsonProperty("text")]
		public string Text{ get; set; }
	}
	
	
	public class HighchartChartUpdateXAxis: Highchart {
	
		public HighchartChartUpdateXAxis():base(){
			Categories=new List<string>{"Jan","Feb","Mar","Apr","May","Jun","Jul","Aug","Sep","Oct","Nov","Dec"};
	
		}
	
		[JsonProperty("categories")]
		public List<string> Categories{ get; set; }
	}
	
	
	public class HighchartChartUpdateSeriesItemColorByPoint: Highchart {
	
		public HighchartChartUpdateSeriesItemColorByPoint():base(){
	
		}
	
	
	}
	
	
	public class HighchartChartUpdateSeriesItem: Highchart {
	
		public HighchartChartUpdateSeriesItem():base(){
			Type="column";
			ColorByPoint=new HighchartChartUpdateSeriesItemColorByPoint();
			Data=new List<Nullable<float>>{29.9f,71.5f,106.4f,129.2f,144f,176f,135.6f,148.5f,216.4f,194.1f,95.6f,54.4f};
			ShowInLegend=null;
	
		}
	
		[InputText("type")]
		[JsonProperty("type")]
		public string Type{ get; set; }	[JsonProperty("colorByPoint")]
		public HighchartChartUpdateSeriesItemColorByPoint ColorByPoint{ get; set; }	[JsonProperty("data")]
		public List<Nullable<float>> Data{ get; set; }	[JsonProperty("showInLegend")]
		public object ShowInLegend{ get; set; }
	}
	
	
	public class HighchartChartUpdate: Highchart {
	
		public HighchartChartUpdate():base(){
			Title=new HighchartChartUpdateTitle();
			Subtitle=new HighchartChartUpdateSubtitle();
			XAxis=new HighchartChartUpdateXAxis();
			Series=new List<HighchartChartUpdateSeriesItem>{new HighchartChartUpdateSeriesItem()};
	
		}
	
		[JsonProperty("title")]
		public HighchartChartUpdateTitle Title{ get; set; }	[JsonProperty("subtitle")]
		public HighchartChartUpdateSubtitle Subtitle{ get; set; }	[JsonProperty("xAxis")]
		public HighchartChartUpdateXAxis XAxis{ get; set; }	[JsonProperty("series")]
		public List<HighchartChartUpdateSeriesItem> Series{ get; set; }
	}
	
	
}