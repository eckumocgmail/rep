using System.Collections.Generic;

using Newtonsoft.Json;
using System;

namespace Highcharts.Models.HighchartAreaNegative
{
	public class HighchartAreaNegativeChart: Highchart {
	
		public HighchartAreaNegativeChart():base(){
			Type="area";
	
		}
	
		[InputText("type")]
		[JsonProperty("type")]
		public string Type{ get; set; }
	}
	
	
	public class HighchartAreaNegativeTitle: Highchart {
	
		public HighchartAreaNegativeTitle():base(){
			Text="Area chart with negative values";
	
		}
	
		[InputText("text")]
		[JsonProperty("text")]
		public string Text{ get; set; }
	}
	
	
	public class HighchartAreaNegativeXAxis: Highchart {
	
		public HighchartAreaNegativeXAxis():base(){
			Categories=new List<string>{"Apples","Oranges","Pears","Grapes","Bananas"};
	
		}
	
		[JsonProperty("categories")]
		public List<string> Categories{ get; set; }
	}
	
	
	public class HighchartAreaNegativeCredits: Highchart {
	
		public HighchartAreaNegativeCredits():base(){
			Enabled=null;
	
		}
	
		[JsonProperty("enabled")]
		public object Enabled{ get; set; }
	}
	
	
	public class HighchartAreaNegativeSeriesItem: Highchart {
	
		public HighchartAreaNegativeSeriesItem():base(){
			Name="John";
			Data=new List<Nullable<float>>{5f,3f,4f,7f,2f};
	
		}
	
		[InputText("name")]
		[JsonProperty("name")]
		public string Name{ get; set; }	[JsonProperty("data")]
		public List<Nullable<float>> Data{ get; set; }
	}
	
	
	public class HighchartAreaNegative: Highchart {
	
		public HighchartAreaNegative():base(){
			Chart=new HighchartAreaNegativeChart();
			Title=new HighchartAreaNegativeTitle();
			XAxis=new HighchartAreaNegativeXAxis();
			Credits=new HighchartAreaNegativeCredits();
			Series=new List<HighchartAreaNegativeSeriesItem>{new HighchartAreaNegativeSeriesItem(),new HighchartAreaNegativeSeriesItem(),new HighchartAreaNegativeSeriesItem()};
	
		}
	
		[JsonProperty("chart")]
		public HighchartAreaNegativeChart Chart{ get; set; }	[JsonProperty("title")]
		public HighchartAreaNegativeTitle Title{ get; set; }	[JsonProperty("xAxis")]
		public HighchartAreaNegativeXAxis XAxis{ get; set; }	[JsonProperty("credits")]
		public HighchartAreaNegativeCredits Credits{ get; set; }	[JsonProperty("series")]
		public List<HighchartAreaNegativeSeriesItem> Series{ get; set; }
	}
	
	
}