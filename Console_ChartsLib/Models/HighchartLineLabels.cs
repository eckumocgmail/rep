using System.Collections.Generic;

using Newtonsoft.Json;
using System;

namespace Highcharts.Models.HighchartLineLabels
{
	public class HighchartLineLabelsChart: Highchart {
	
		public HighchartLineLabelsChart():base(){
			Type="line";
	
		}
	
		[InputText("type")]
		[JsonProperty("type")]
		public string Type{ get; set; }
	}
	
	
	public class HighchartLineLabelsTitle: Highchart {
	
		public HighchartLineLabelsTitle():base(){
			Text="Monthly Average Temperature";
	
		}
	
		[InputText("text")]
		[JsonProperty("text")]
		public string Text{ get; set; }
	}
	
	
	public class HighchartLineLabelsSubtitle: Highchart {
	
		public HighchartLineLabelsSubtitle():base(){
			Text="Source: WorldClimate.com";
	
		}
	
		[InputText("text")]
		[JsonProperty("text")]
		public string Text{ get; set; }
	}
	
	
	public class HighchartLineLabelsXAxis: Highchart {
	
		public HighchartLineLabelsXAxis():base(){
			Categories=new List<string>{"Jan","Feb","Mar","Apr","May","Jun","Jul","Aug","Sep","Oct","Nov","Dec"};
	
		}
	
		[JsonProperty("categories")]
		public List<string> Categories{ get; set; }
	}
	
	
	public class HighchartLineLabelsYAxisTitle: Highchart {
	
		public HighchartLineLabelsYAxisTitle():base(){
			Text="Temperature (°C)";
	
		}
	
		[InputText("text")]
		[JsonProperty("text")]
		public string Text{ get; set; }
	}
	
	
	public class HighchartLineLabelsYAxis: Highchart {
	
		public HighchartLineLabelsYAxis():base(){
			Title=new HighchartLineLabelsYAxisTitle();
	
		}
	
		[JsonProperty("title")]
		public HighchartLineLabelsYAxisTitle Title{ get; set; }
	}
	
	
	public class HighchartLineLabelsPlotOptionsLineDataLabelsEnabled: Highchart {
	
		public HighchartLineLabelsPlotOptionsLineDataLabelsEnabled():base(){
	
		}
	
	
	}
	
	
	public class HighchartLineLabelsPlotOptionsLineDataLabels: Highchart {
	
		public HighchartLineLabelsPlotOptionsLineDataLabels():base(){
			Enabled=new HighchartLineLabelsPlotOptionsLineDataLabelsEnabled();
	
		}
	
		[JsonProperty("enabled")]
		public HighchartLineLabelsPlotOptionsLineDataLabelsEnabled Enabled{ get; set; }
	}
	
	
	public class HighchartLineLabelsPlotOptionsLine: Highchart {
	
		public HighchartLineLabelsPlotOptionsLine():base(){
			DataLabels=new HighchartLineLabelsPlotOptionsLineDataLabels();
			EnableMouseTracking=null;
	
		}
	
		[JsonProperty("dataLabels")]
		public HighchartLineLabelsPlotOptionsLineDataLabels DataLabels{ get; set; }	[JsonProperty("enableMouseTracking")]
		public object EnableMouseTracking{ get; set; }
	}
	
	
	public class HighchartLineLabelsPlotOptions: Highchart {
	
		public HighchartLineLabelsPlotOptions():base(){
			Line=new HighchartLineLabelsPlotOptionsLine();
	
		}
	
		[JsonProperty("line")]
		public HighchartLineLabelsPlotOptionsLine Line{ get; set; }
	}
	
	
	public class HighchartLineLabelsSeriesItem: Highchart {
	
		public HighchartLineLabelsSeriesItem():base(){
			Name="Tokyo";
			Data=new List<Nullable<float>>{7f,6.9f,9.5f,14.5f,18.4f,21.5f,25.2f,26.5f,23.3f,18.3f,13.9f,9.6f};
	
		}
	
		[InputText("name")]
		[JsonProperty("name")]
		public string Name{ get; set; }	[JsonProperty("data")]
		public List<Nullable<float>> Data{ get; set; }
	}
	
	
	public class HighchartLineLabels: Highchart {
	
		public HighchartLineLabels():base(){
			Chart=new HighchartLineLabelsChart();
			Title=new HighchartLineLabelsTitle();
			Subtitle=new HighchartLineLabelsSubtitle();
			XAxis=new HighchartLineLabelsXAxis();
			YAxis=new HighchartLineLabelsYAxis();
			PlotOptions=new HighchartLineLabelsPlotOptions();
			Series=new List<HighchartLineLabelsSeriesItem>{new HighchartLineLabelsSeriesItem(),new HighchartLineLabelsSeriesItem()};
	
		}
	
		[JsonProperty("chart")]
		public HighchartLineLabelsChart Chart{ get; set; }	[JsonProperty("title")]
		public HighchartLineLabelsTitle Title{ get; set; }	[JsonProperty("subtitle")]
		public HighchartLineLabelsSubtitle Subtitle{ get; set; }	[JsonProperty("xAxis")]
		public HighchartLineLabelsXAxis XAxis{ get; set; }	[JsonProperty("yAxis")]
		public HighchartLineLabelsYAxis YAxis{ get; set; }	[JsonProperty("plotOptions")]
		public HighchartLineLabelsPlotOptions PlotOptions{ get; set; }	[JsonProperty("series")]
		public List<HighchartLineLabelsSeriesItem> Series{ get; set; }
	}
	
	
}