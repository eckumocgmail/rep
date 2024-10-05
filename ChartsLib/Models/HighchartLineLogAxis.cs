using System.Collections.Generic;

using Newtonsoft.Json;
using System;

namespace Highcharts.Models.HighchartLineLogAxis
{
	public class HighchartLineLogAxisTitle: Highchart {
	
		public HighchartLineLogAxisTitle():base(){
			Text="Logarithmic axis demo";
	
		}
	
		[InputText("text")]
		[JsonProperty("text")]
		public string Text{ get; set; }
	}
	
	
	public class HighchartLineLogAxisXAxisAccessibility: Highchart {
	
		public HighchartLineLogAxisXAxisAccessibility():base(){
			RangeDescription="Range: 1 to 10";
	
		}
	
		[InputText("rangeDescription")]
		[JsonProperty("rangeDescription")]
		public string RangeDescription{ get; set; }
	}
	
	
	public class HighchartLineLogAxisXAxis: Highchart {
	
		public HighchartLineLogAxisXAxis():base(){
			TickInterval=1f;
			Type="logarithmic";
			Accessibility=new HighchartLineLogAxisXAxisAccessibility();
	
		}
	
		[InputNumber("tickInterval")]
		[JsonProperty("tickInterval")]
		public float TickInterval{ get; set; }	[InputText("type")]
		[JsonProperty("type")]
		public string Type{ get; set; }	[JsonProperty("accessibility")]
		public HighchartLineLogAxisXAxisAccessibility Accessibility{ get; set; }
	}
	
	
	public class HighchartLineLogAxisYAxisAccessibility: Highchart {
	
		public HighchartLineLogAxisYAxisAccessibility():base(){
			RangeDescription="Range: 0.1 to 1000";
	
		}
	
		[InputText("rangeDescription")]
		[JsonProperty("rangeDescription")]
		public string RangeDescription{ get; set; }
	}
	
	
	public class HighchartLineLogAxisYAxis: Highchart {
	
		public HighchartLineLogAxisYAxis():base(){
			Type="logarithmic";
			MinorTickInterval=0.1f;
			Accessibility=new HighchartLineLogAxisYAxisAccessibility();
	
		}
	
		[InputText("type")]
		[JsonProperty("type")]
		public string Type{ get; set; }	[InputNumber("minorTickInterval")]
		[JsonProperty("minorTickInterval")]
		public float MinorTickInterval{ get; set; }	[JsonProperty("accessibility")]
		public HighchartLineLogAxisYAxisAccessibility Accessibility{ get; set; }
	}
	
	
	public class HighchartLineLogAxisTooltip: Highchart {
	
		public HighchartLineLogAxisTooltip():base(){
			HeaderFormat="<b>{series.name}</b><br />";
			PointFormat="x = {point.x}, y = {point.y}";
	
		}
	
		[InputText("headerFormat")]
		[JsonProperty("headerFormat")]
		public string HeaderFormat{ get; set; }	[InputText("pointFormat")]
		[JsonProperty("pointFormat")]
		public string PointFormat{ get; set; }
	}
	
	
	public class HighchartLineLogAxisSeriesItem: Highchart {
	
		public HighchartLineLogAxisSeriesItem():base(){
			Data=new List<Nullable<float>>{1f,2f,4f,8f,16f,32f,64f,128f,256f,512f};
			PointStart=1f;
	
		}
	
		[JsonProperty("data")]
		public List<Nullable<float>> Data{ get; set; }	[InputNumber("pointStart")]
		[JsonProperty("pointStart")]
		public float PointStart{ get; set; }
	}
	
	
	public class HighchartLineLogAxis: Highchart {
	
		public HighchartLineLogAxis():base(){
			Title=new HighchartLineLogAxisTitle();
			XAxis=new HighchartLineLogAxisXAxis();
			YAxis=new HighchartLineLogAxisYAxis();
			Tooltip=new HighchartLineLogAxisTooltip();
			Series=new List<HighchartLineLogAxisSeriesItem>{new HighchartLineLogAxisSeriesItem()};
	
		}
	
		[JsonProperty("title")]
		public HighchartLineLogAxisTitle Title{ get; set; }	[JsonProperty("xAxis")]
		public HighchartLineLogAxisXAxis XAxis{ get; set; }	[JsonProperty("yAxis")]
		public HighchartLineLogAxisYAxis YAxis{ get; set; }	[JsonProperty("tooltip")]
		public HighchartLineLogAxisTooltip Tooltip{ get; set; }	[JsonProperty("series")]
		public List<HighchartLineLogAxisSeriesItem> Series{ get; set; }
	}
	
	
}