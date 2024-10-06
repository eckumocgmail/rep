using System.Collections.Generic;

using Newtonsoft.Json;
using System;

namespace Highcharts.Models.HighchartBarNegativeStack
{
	public class HighchartBarNegativeStackChart: Highchart {
	
		public HighchartBarNegativeStackChart():base(){
			Type="bar";
	
		}
	
		[InputText("type")]
		[JsonProperty("type")]
		public string Type{ get; set; }
	}
	
	
	public class HighchartBarNegativeStackTitle: Highchart {
	
		public HighchartBarNegativeStackTitle():base(){
			Text="Population pyramid for Germany, 2018";
	
		}
	
		[InputText("text")]
		[JsonProperty("text")]
		public string Text{ get; set; }
	}
	
	
	public class HighchartBarNegativeStackSubtitle: Highchart {
	
		public HighchartBarNegativeStackSubtitle():base(){
			Text="Source: <a href='http://populationpyramid.net/germany/2018/'>Population Pyramids of the World from 1950 to 2100</a>";
	
		}
	
		[InputText("text")]
		[JsonProperty("text")]
		public string Text{ get; set; }
	}
	
	
	public class HighchartBarNegativeStackAccessibilityPoint: Highchart {
	
		public HighchartBarNegativeStackAccessibilityPoint():base(){
			ValueDescriptionFormat="{index}. Age {xDescription}, {value}%.";
	
		}
	
		[InputText("valueDescriptionFormat")]
		[JsonProperty("valueDescriptionFormat")]
		public string ValueDescriptionFormat{ get; set; }
	}
	
	
	public class HighchartBarNegativeStackAccessibility: Highchart {
	
		public HighchartBarNegativeStackAccessibility():base(){
			Point=new HighchartBarNegativeStackAccessibilityPoint();
	
		}
	
		[JsonProperty("point")]
		public HighchartBarNegativeStackAccessibilityPoint Point{ get; set; }
	}
	
	
	public class HighchartBarNegativeStackXAxisItemLabels: Highchart {
	
		public HighchartBarNegativeStackXAxisItemLabels():base(){
			Step=1f;
	
		}
	
		[InputNumber("step")]
		[JsonProperty("step")]
		public float Step{ get; set; }
	}
	
	
	public class HighchartBarNegativeStackXAxisItemAccessibility: Highchart {
	
		public HighchartBarNegativeStackXAxisItemAccessibility():base(){
			Description="Age (male)";
	
		}
	
		[InputText("description")]
		[JsonProperty("description")]
		public string Description{ get; set; }
	}
	
	
	public class HighchartBarNegativeStackXAxisItem: Highchart {
	
		public HighchartBarNegativeStackXAxisItem():base(){
			Categories=new List<string>{"0-4","5-9","10-14","15-19","20-24","25-29","30-34","35-39","40-44","45-49","50-54","55-59","60-64","65-69","70-74","75-79","80-84","85-89","90-94","95-99","100 + "};
			Reversed=null;
			Labels=new HighchartBarNegativeStackXAxisItemLabels();
			Accessibility=new HighchartBarNegativeStackXAxisItemAccessibility();
	
		}
	
		[JsonProperty("categories")]
		public List<string> Categories{ get; set; }	[JsonProperty("reversed")]
		public object Reversed{ get; set; }	[JsonProperty("labels")]
		public HighchartBarNegativeStackXAxisItemLabels Labels{ get; set; }	[JsonProperty("accessibility")]
		public HighchartBarNegativeStackXAxisItemAccessibility Accessibility{ get; set; }
	}
	
	
	public class HighchartBarNegativeStackYAxisTitle: Highchart {
	
		public HighchartBarNegativeStackYAxisTitle():base(){
			Text=null;
	
		}
	
		[JsonProperty("text")]
		public object Text{ get; set; }
	}
	
	
	public class HighchartBarNegativeStackYAxisLabels: Highchart {
	
		public HighchartBarNegativeStackYAxisLabels():base(){
			Formatter=()=>{};
	
		}
	
		[JsonProperty("formatter")]
		[JsonIgnore()]
		public Action Formatter{ get; set; }
	}
	
	
	public class HighchartBarNegativeStackYAxisAccessibility: Highchart {
	
		public HighchartBarNegativeStackYAxisAccessibility():base(){
			Description="Percentage population";
			RangeDescription="Range: 0 to 5%";
	
		}
	
		[InputText("description")]
		[JsonProperty("description")]
		public string Description{ get; set; }	[InputText("rangeDescription")]
		[JsonProperty("rangeDescription")]
		public string RangeDescription{ get; set; }
	}
	
	
	public class HighchartBarNegativeStackYAxis: Highchart {
	
		public HighchartBarNegativeStackYAxis():base(){
			Title=new HighchartBarNegativeStackYAxisTitle();
			Labels=new HighchartBarNegativeStackYAxisLabels();
			Accessibility=new HighchartBarNegativeStackYAxisAccessibility();
	
		}
	
		[JsonProperty("title")]
		public HighchartBarNegativeStackYAxisTitle Title{ get; set; }	[JsonProperty("labels")]
		public HighchartBarNegativeStackYAxisLabels Labels{ get; set; }	[JsonProperty("accessibility")]
		public HighchartBarNegativeStackYAxisAccessibility Accessibility{ get; set; }
	}
	
	
	public class HighchartBarNegativeStackPlotOptionsSeries: Highchart {
	
		public HighchartBarNegativeStackPlotOptionsSeries():base(){
			Stacking="normal";
	
		}
	
		[InputText("stacking")]
		[JsonProperty("stacking")]
		public string Stacking{ get; set; }
	}
	
	
	public class HighchartBarNegativeStackPlotOptions: Highchart {
	
		public HighchartBarNegativeStackPlotOptions():base(){
			Series=new HighchartBarNegativeStackPlotOptionsSeries();
	
		}
	
		[JsonProperty("series")]
		public HighchartBarNegativeStackPlotOptionsSeries Series{ get; set; }
	}
	
	
	public class HighchartBarNegativeStackTooltip: Highchart {
	
		public HighchartBarNegativeStackTooltip():base(){
			Formatter=()=>{};
	
		}
	
		[JsonProperty("formatter")]
		[JsonIgnore()]
		public Action Formatter{ get; set; }
	}
	
	
	public class HighchartBarNegativeStackSeriesItem: Highchart {
	
		public HighchartBarNegativeStackSeriesItem():base(){
			Name="Male";
			Data=new List<Nullable<float>>{-2.2f,-2.1f,-2.2f,-2.4f,-2.7f,-3f,-3.3f,-3.2f,-2.9f,-3.5f,-4.4f,-4.1f,-3.4f,-2.7f,-2.3f,-2.2f,-1.6f,-0.6f,-0.3f,0,0};
	
		}
	
		[InputText("name")]
		[JsonProperty("name")]
		public string Name{ get; set; }	[JsonProperty("data")]
		public List<Nullable<float>> Data{ get; set; }
	}
	
	
	public class HighchartBarNegativeStack: Highchart {
	
		public HighchartBarNegativeStack():base(){
			Chart=new HighchartBarNegativeStackChart();
			Title=new HighchartBarNegativeStackTitle();
			Subtitle=new HighchartBarNegativeStackSubtitle();
			Accessibility=new HighchartBarNegativeStackAccessibility();
			XAxis=new List<HighchartBarNegativeStackXAxisItem>{new HighchartBarNegativeStackXAxisItem(),new HighchartBarNegativeStackXAxisItem()};
			YAxis=new HighchartBarNegativeStackYAxis();
			PlotOptions=new HighchartBarNegativeStackPlotOptions();
			Tooltip=new HighchartBarNegativeStackTooltip();
			Series=new List<HighchartBarNegativeStackSeriesItem>{new HighchartBarNegativeStackSeriesItem(),new HighchartBarNegativeStackSeriesItem()};
	
		}
	
		[JsonProperty("chart")]
		public HighchartBarNegativeStackChart Chart{ get; set; }	[JsonProperty("title")]
		public HighchartBarNegativeStackTitle Title{ get; set; }	[JsonProperty("subtitle")]
		public HighchartBarNegativeStackSubtitle Subtitle{ get; set; }	[JsonProperty("accessibility")]
		public HighchartBarNegativeStackAccessibility Accessibility{ get; set; }	[JsonProperty("xAxis")]
		public List<HighchartBarNegativeStackXAxisItem> XAxis{ get; set; }	[JsonProperty("yAxis")]
		public HighchartBarNegativeStackYAxis YAxis{ get; set; }	[JsonProperty("plotOptions")]
		public HighchartBarNegativeStackPlotOptions PlotOptions{ get; set; }	[JsonProperty("tooltip")]
		public HighchartBarNegativeStackTooltip Tooltip{ get; set; }	[JsonProperty("series")]
		public List<HighchartBarNegativeStackSeriesItem> Series{ get; set; }
	}
	
	
}