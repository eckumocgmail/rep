using System.Collections.Generic;

using Newtonsoft.Json;
using System;

namespace Highcharts.Models.HighchartColumnNegative
{
	public class HighchartColumnNegativeChart: Highchart {
	
		public HighchartColumnNegativeChart():base(){
			Type="column";
	
		}
	
		[InputText("type")]
		[JsonProperty("type")]
		public string Type{ get; set; }
	}
	
	
	public class HighchartColumnNegativeTitle: Highchart {
	
		public HighchartColumnNegativeTitle():base(){
			Text="Column chart with negative values";
	
		}
	
		[InputText("text")]
		[JsonProperty("text")]
		public string Text{ get; set; }
	}
	
	
	public class HighchartColumnNegativeXAxis: Highchart {
	
		public HighchartColumnNegativeXAxis():base(){
			Categories=new List<string>{"Apples","Oranges","Pears","Grapes","Bananas"};
	
		}
	
		[JsonProperty("categories")]
		public List<string> Categories{ get; set; }
	}
	
	
	public class HighchartColumnNegativeCredits: Highchart {
	
		public HighchartColumnNegativeCredits():base(){
			Enabled=null;
	
		}
	
		[JsonProperty("enabled")]
		public object Enabled{ get; set; }
	}
	
	
	public class HighchartColumnNegativeSeriesItem: Highchart {
	
		public HighchartColumnNegativeSeriesItem():base(){
			Name="John";
			Data=new List<Nullable<float>>{5f,3f,4f,7f,2f};
	
		}
	
		[InputText("name")]
		[JsonProperty("name")]
		public string Name{ get; set; }	[JsonProperty("data")]
		public List<Nullable<float>> Data{ get; set; }
	}
	
	
	public class HighchartColumnNegative: Highchart {
	
		public HighchartColumnNegative():base(){
			Chart=new HighchartColumnNegativeChart();
			Title=new HighchartColumnNegativeTitle();
			XAxis=new HighchartColumnNegativeXAxis();
			Credits=new HighchartColumnNegativeCredits();
			Series=new List<HighchartColumnNegativeSeriesItem>{new HighchartColumnNegativeSeriesItem(),new HighchartColumnNegativeSeriesItem(),new HighchartColumnNegativeSeriesItem()};
	
		}
	
		[JsonProperty("chart")]
		public HighchartColumnNegativeChart Chart{ get; set; }	[JsonProperty("title")]
		public HighchartColumnNegativeTitle Title{ get; set; }	[JsonProperty("xAxis")]
		public HighchartColumnNegativeXAxis XAxis{ get; set; }	[JsonProperty("credits")]
		public HighchartColumnNegativeCredits Credits{ get; set; }	[JsonProperty("series")]
		public List<HighchartColumnNegativeSeriesItem> Series{ get; set; }
	}
	
	
}