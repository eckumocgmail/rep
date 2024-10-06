using System.Collections.Generic;

using Newtonsoft.Json;
using System;

namespace Highcharts.Models.HighchartColumnStackedPercent
{
	public class HighchartColumnStackedPercentChart: Highchart {
	
		public HighchartColumnStackedPercentChart():base(){
			Type="column";
	
		}
	
		[InputText("type")]
		[JsonProperty("type")]
		public string Type{ get; set; }
	}
	
	
	public class HighchartColumnStackedPercentTitle: Highchart {
	
		public HighchartColumnStackedPercentTitle():base(){
			Text="Stacked column chart";
	
		}
	
		[InputText("text")]
		[JsonProperty("text")]
		public string Text{ get; set; }
	}
	
	
	public class HighchartColumnStackedPercentXAxis: Highchart {
	
		public HighchartColumnStackedPercentXAxis():base(){
			Categories=new List<string>{"Apples","Oranges","Pears","Grapes","Bananas"};
	
		}
	
		[JsonProperty("categories")]
		public List<string> Categories{ get; set; }
	}
	
	
	public class HighchartColumnStackedPercentYAxisTitle: Highchart {
	
		public HighchartColumnStackedPercentYAxisTitle():base(){
			Text="Total fruit consumption";
	
		}
	
		[InputText("text")]
		[JsonProperty("text")]
		public string Text{ get; set; }
	}
	
	
	public class HighchartColumnStackedPercentYAxis: Highchart {
	
		public HighchartColumnStackedPercentYAxis():base(){
			Min=null;
			Title=new HighchartColumnStackedPercentYAxisTitle();
	
		}
	
		[JsonProperty("min")]
		public object Min{ get; set; }	[JsonProperty("title")]
		public HighchartColumnStackedPercentYAxisTitle Title{ get; set; }
	}
	
	
	public class HighchartColumnStackedPercentTooltipShared: Highchart {
	
		public HighchartColumnStackedPercentTooltipShared():base(){
	
		}
	
	
	}
	
	
	public class HighchartColumnStackedPercentTooltip: Highchart {
	
		public HighchartColumnStackedPercentTooltip():base(){
			PointFormat="<span style='color:{series.color}'>{series.name}</span>: <b>{point.y}</b> ({point.percentage:.0f}%)<br/>";
			Shared=new HighchartColumnStackedPercentTooltipShared();
	
		}
	
		[InputText("pointFormat")]
		[JsonProperty("pointFormat")]
		public string PointFormat{ get; set; }	[JsonProperty("shared")]
		public HighchartColumnStackedPercentTooltipShared Shared{ get; set; }
	}
	
	
	public class HighchartColumnStackedPercentPlotOptionsColumn: Highchart {
	
		public HighchartColumnStackedPercentPlotOptionsColumn():base(){
			Stacking="percent";
	
		}
	
		[InputText("stacking")]
		[JsonProperty("stacking")]
		public string Stacking{ get; set; }
	}
	
	
	public class HighchartColumnStackedPercentPlotOptions: Highchart {
	
		public HighchartColumnStackedPercentPlotOptions():base(){
			Column=new HighchartColumnStackedPercentPlotOptionsColumn();
	
		}
	
		[JsonProperty("column")]
		public HighchartColumnStackedPercentPlotOptionsColumn Column{ get; set; }
	}
	
	
	public class HighchartColumnStackedPercentSeriesItem: Highchart {
	
		public HighchartColumnStackedPercentSeriesItem():base(){
			Name="John";
			Data=new List<Nullable<float>>{5f,3f,4f,7f,2f};
	
		}
	
		[InputText("name")]
		[JsonProperty("name")]
		public string Name{ get; set; }	[JsonProperty("data")]
		public List<Nullable<float>> Data{ get; set; }
	}
	
	
	public class HighchartColumnStackedPercent: Highchart {
	
		public HighchartColumnStackedPercent():base(){
			Chart=new HighchartColumnStackedPercentChart();
			Title=new HighchartColumnStackedPercentTitle();
			XAxis=new HighchartColumnStackedPercentXAxis();
			YAxis=new HighchartColumnStackedPercentYAxis();
			Tooltip=new HighchartColumnStackedPercentTooltip();
			PlotOptions=new HighchartColumnStackedPercentPlotOptions();
			Series=new List<HighchartColumnStackedPercentSeriesItem>{new HighchartColumnStackedPercentSeriesItem(),new HighchartColumnStackedPercentSeriesItem(),new HighchartColumnStackedPercentSeriesItem()};
	
		}
	
		[JsonProperty("chart")]
		public HighchartColumnStackedPercentChart Chart{ get; set; }	[JsonProperty("title")]
		public HighchartColumnStackedPercentTitle Title{ get; set; }	[JsonProperty("xAxis")]
		public HighchartColumnStackedPercentXAxis XAxis{ get; set; }	[JsonProperty("yAxis")]
		public HighchartColumnStackedPercentYAxis YAxis{ get; set; }	[JsonProperty("tooltip")]
		public HighchartColumnStackedPercentTooltip Tooltip{ get; set; }	[JsonProperty("plotOptions")]
		public HighchartColumnStackedPercentPlotOptions PlotOptions{ get; set; }	[JsonProperty("series")]
		public List<HighchartColumnStackedPercentSeriesItem> Series{ get; set; }
	}
	
	
}