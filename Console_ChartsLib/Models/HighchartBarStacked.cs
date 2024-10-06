using System.Collections.Generic;

using Newtonsoft.Json;
using System;

namespace Highcharts.Models.HighchartBarStacked
{
	public class HighchartBarStackedChart: Highchart {
	
		public HighchartBarStackedChart():base(){
			Type="bar";
	
		}
	
		[InputText("type")]
		[JsonProperty("type")]
		public string Type{ get; set; }
	}
	
	
	public class HighchartBarStackedTitle: Highchart {
	
		public HighchartBarStackedTitle():base(){
			Text="Stacked bar chart";
	
		}
	
		[InputText("text")]
		[JsonProperty("text")]
		public string Text{ get; set; }
	}
	
	
	public class HighchartBarStackedXAxis: Highchart {
	
		public HighchartBarStackedXAxis():base(){
			Categories=new List<string>{"Apples","Oranges","Pears","Grapes","Bananas"};
	
		}
	
		[JsonProperty("categories")]
		public List<string> Categories{ get; set; }
	}
	
	
	public class HighchartBarStackedYAxisTitle: Highchart {
	
		public HighchartBarStackedYAxisTitle():base(){
			Text="Total fruit consumption";
	
		}
	
		[InputText("text")]
		[JsonProperty("text")]
		public string Text{ get; set; }
	}
	
	
	public class HighchartBarStackedYAxis: Highchart {
	
		public HighchartBarStackedYAxis():base(){
			Min=null;
			Title=new HighchartBarStackedYAxisTitle();
	
		}
	
		[JsonProperty("min")]
		public object Min{ get; set; }	[JsonProperty("title")]
		public HighchartBarStackedYAxisTitle Title{ get; set; }
	}
	
	
	public class HighchartBarStackedLegendReversed: Highchart {
	
		public HighchartBarStackedLegendReversed():base(){
	
		}
	
	
	}
	
	
	public class HighchartBarStackedLegend: Highchart {
	
		public HighchartBarStackedLegend():base(){
			Reversed=new HighchartBarStackedLegendReversed();
	
		}
	
		[JsonProperty("reversed")]
		public HighchartBarStackedLegendReversed Reversed{ get; set; }
	}
	
	
	public class HighchartBarStackedPlotOptionsSeries: Highchart {
	
		public HighchartBarStackedPlotOptionsSeries():base(){
			Stacking="normal";
	
		}
	
		[InputText("stacking")]
		[JsonProperty("stacking")]
		public string Stacking{ get; set; }
	}
	
	
	public class HighchartBarStackedPlotOptions: Highchart {
	
		public HighchartBarStackedPlotOptions():base(){
			Series=new HighchartBarStackedPlotOptionsSeries();
	
		}
	
		[JsonProperty("series")]
		public HighchartBarStackedPlotOptionsSeries Series{ get; set; }
	}
	
	
	public class HighchartBarStackedSeriesItem: Highchart {
	
		public HighchartBarStackedSeriesItem():base(){
			Name="John";
			Data=new List<Nullable<float>>{5f,3f,4f,7f,2f};
	
		}
	
		[InputText("name")]
		[JsonProperty("name")]
		public string Name{ get; set; }	[JsonProperty("data")]
		public List<Nullable<float>> Data{ get; set; }
	}
	
	
	public class HighchartBarStacked: Highchart {
	
		public HighchartBarStacked():base(){
			Chart=new HighchartBarStackedChart();
			Title=new HighchartBarStackedTitle();
			XAxis=new HighchartBarStackedXAxis();
			YAxis=new HighchartBarStackedYAxis();
			Legend=new HighchartBarStackedLegend();
			PlotOptions=new HighchartBarStackedPlotOptions();
			Series=new List<HighchartBarStackedSeriesItem>{new HighchartBarStackedSeriesItem(),new HighchartBarStackedSeriesItem(),new HighchartBarStackedSeriesItem()};
	
		}
	
		[JsonProperty("chart")]
		public HighchartBarStackedChart Chart{ get; set; }	[JsonProperty("title")]
		public HighchartBarStackedTitle Title{ get; set; }	[JsonProperty("xAxis")]
		public HighchartBarStackedXAxis XAxis{ get; set; }	[JsonProperty("yAxis")]
		public HighchartBarStackedYAxis YAxis{ get; set; }	[JsonProperty("legend")]
		public HighchartBarStackedLegend Legend{ get; set; }	[JsonProperty("plotOptions")]
		public HighchartBarStackedPlotOptions PlotOptions{ get; set; }	[JsonProperty("series")]
		public List<HighchartBarStackedSeriesItem> Series{ get; set; }
	}
	
	
}