using System.Collections.Generic;

using Newtonsoft.Json;
using System;

namespace Highcharts.Models.HighchartColumnStackedAndGrouped
{
	public class HighchartColumnStackedAndGroupedChart: Highchart {
	
		public HighchartColumnStackedAndGroupedChart():base(){
			Type="column";
	
		}
	
		[InputText("type")]
		[JsonProperty("type")]
		public string Type{ get; set; }
	}
	
	
	public class HighchartColumnStackedAndGroupedTitle: Highchart {
	
		public HighchartColumnStackedAndGroupedTitle():base(){
			Text="Total fruit consumption, grouped by gender";
	
		}
	
		[InputText("text")]
		[JsonProperty("text")]
		public string Text{ get; set; }
	}
	
	
	public class HighchartColumnStackedAndGroupedXAxis: Highchart {
	
		public HighchartColumnStackedAndGroupedXAxis():base(){
			Categories=new List<string>{"Apples","Oranges","Pears","Grapes","Bananas"};
	
		}
	
		[JsonProperty("categories")]
		public List<string> Categories{ get; set; }
	}
	
	
	public class HighchartColumnStackedAndGroupedYAxisTitle: Highchart {
	
		public HighchartColumnStackedAndGroupedYAxisTitle():base(){
			Text="Number of fruits";
	
		}
	
		[InputText("text")]
		[JsonProperty("text")]
		public string Text{ get; set; }
	}
	
	
	public class HighchartColumnStackedAndGroupedYAxis: Highchart {
	
		public HighchartColumnStackedAndGroupedYAxis():base(){
			AllowDecimals=null;
			Min=null;
			Title=new HighchartColumnStackedAndGroupedYAxisTitle();
	
		}
	
		[JsonProperty("allowDecimals")]
		public object AllowDecimals{ get; set; }	[JsonProperty("min")]
		public object Min{ get; set; }	[JsonProperty("title")]
		public HighchartColumnStackedAndGroupedYAxisTitle Title{ get; set; }
	}
	
	
	public class HighchartColumnStackedAndGroupedTooltip: Highchart {
	
		public HighchartColumnStackedAndGroupedTooltip():base(){
			Formatter=()=>{};
	
		}
	
		[JsonProperty("formatter")]
		[JsonIgnore()]
		public Action Formatter{ get; set; }
	}
	
	
	public class HighchartColumnStackedAndGroupedPlotOptionsColumn: Highchart {
	
		public HighchartColumnStackedAndGroupedPlotOptionsColumn():base(){
			Stacking="normal";
	
		}
	
		[InputText("stacking")]
		[JsonProperty("stacking")]
		public string Stacking{ get; set; }
	}
	
	
	public class HighchartColumnStackedAndGroupedPlotOptions: Highchart {
	
		public HighchartColumnStackedAndGroupedPlotOptions():base(){
			Column=new HighchartColumnStackedAndGroupedPlotOptionsColumn();
	
		}
	
		[JsonProperty("column")]
		public HighchartColumnStackedAndGroupedPlotOptionsColumn Column{ get; set; }
	}
	
	
	public class HighchartColumnStackedAndGroupedSeriesItem: Highchart {
	
		public HighchartColumnStackedAndGroupedSeriesItem():base(){
			Name="John";
			Data=new List<Nullable<float>>{5f,3f,4f,7f,2f};
			Stack="male";
	
		}
	
		[InputText("name")]
		[JsonProperty("name")]
		public string Name{ get; set; }	[JsonProperty("data")]
		public List<Nullable<float>> Data{ get; set; }	[InputText("stack")]
		[JsonProperty("stack")]
		public string Stack{ get; set; }
	}
	
	
	public class HighchartColumnStackedAndGrouped: Highchart {
	
		public HighchartColumnStackedAndGrouped():base(){
			Chart=new HighchartColumnStackedAndGroupedChart();
			Title=new HighchartColumnStackedAndGroupedTitle();
			XAxis=new HighchartColumnStackedAndGroupedXAxis();
			YAxis=new HighchartColumnStackedAndGroupedYAxis();
			Tooltip=new HighchartColumnStackedAndGroupedTooltip();
			PlotOptions=new HighchartColumnStackedAndGroupedPlotOptions();
			Series=new List<HighchartColumnStackedAndGroupedSeriesItem>{new HighchartColumnStackedAndGroupedSeriesItem(),new HighchartColumnStackedAndGroupedSeriesItem(),new HighchartColumnStackedAndGroupedSeriesItem(),new HighchartColumnStackedAndGroupedSeriesItem()};
	
		}
	
		[JsonProperty("chart")]
		public HighchartColumnStackedAndGroupedChart Chart{ get; set; }	[JsonProperty("title")]
		public HighchartColumnStackedAndGroupedTitle Title{ get; set; }	[JsonProperty("xAxis")]
		public HighchartColumnStackedAndGroupedXAxis XAxis{ get; set; }	[JsonProperty("yAxis")]
		public HighchartColumnStackedAndGroupedYAxis YAxis{ get; set; }	[JsonProperty("tooltip")]
		public HighchartColumnStackedAndGroupedTooltip Tooltip{ get; set; }	[JsonProperty("plotOptions")]
		public HighchartColumnStackedAndGroupedPlotOptions PlotOptions{ get; set; }	[JsonProperty("series")]
		public List<HighchartColumnStackedAndGroupedSeriesItem> Series{ get; set; }
	}
	
	
}