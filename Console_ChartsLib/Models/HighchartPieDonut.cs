using System.Collections.Generic;

using Newtonsoft.Json;
using System;

namespace Highcharts.Models.HighchartPieDonut
{
	public class HighchartPieDonutChart: Highchart {
	
		public HighchartPieDonutChart():base(){
			Type="pie";
	
		}
	
		[InputText("type")]
		[JsonProperty("type")]
		public string Type{ get; set; }
	}
	
	
	public class HighchartPieDonutTitle: Highchart {
	
		public HighchartPieDonutTitle():base(){
			Text="Browser market share, January, 2018";
	
		}
	
		[InputText("text")]
		[JsonProperty("text")]
		public string Text{ get; set; }
	}
	
	
	public class HighchartPieDonutSubtitle: Highchart {
	
		public HighchartPieDonutSubtitle():base(){
			Text="Source: <a href='http://statcounter.com' target='_blank'>statcounter.com</a>";
	
		}
	
		[InputText("text")]
		[JsonProperty("text")]
		public string Text{ get; set; }
	}
	
	
	public class HighchartPieDonutPlotOptionsPie: Highchart {
	
		public HighchartPieDonutPlotOptionsPie():base(){
			Shadow=null;
			Center=new List<string>{"50%","50%"};
	
		}
	
		[JsonProperty("shadow")]
		public object Shadow{ get; set; }	[JsonProperty("center")]
		public List<string> Center{ get; set; }
	}
	
	
	public class HighchartPieDonutPlotOptions: Highchart {
	
		public HighchartPieDonutPlotOptions():base(){
			Pie=new HighchartPieDonutPlotOptionsPie();
	
		}
	
		[JsonProperty("pie")]
		public HighchartPieDonutPlotOptionsPie Pie{ get; set; }
	}
	
	
	public class HighchartPieDonutTooltip: Highchart {
	
		public HighchartPieDonutTooltip():base(){
			ValueSuffix="%";
	
		}
	
		[InputText("valueSuffix")]
		[JsonProperty("valueSuffix")]
		public string ValueSuffix{ get; set; }
	}
	
	
	public class HighchartPieDonutSeriesItemDataItem: Highchart {
	
		public HighchartPieDonutSeriesItemDataItem():base(){
			Name="Chrome";
			Y=62.74f;
			Color="#90ed7d";
	
		}
	
		[InputText("name")]
		[JsonProperty("name")]
		public string Name{ get; set; }	[InputNumber("y")]
		[JsonProperty("y")]
		public float Y{ get; set; }	[InputText("color")]
		[JsonProperty("color")]
		public string Color{ get; set; }
	}
	
	
	public class HighchartPieDonutSeriesItemDataLabels: Highchart {
	
		public HighchartPieDonutSeriesItemDataLabels():base(){
			Formatter=()=>{};
			Color="#ffffff";
			Distance=-30f;
	
		}
	
		[JsonProperty("formatter")]
		[JsonIgnore()]
		public Action Formatter{ get; set; }	

		
		[InputText("color")]
		[JsonProperty("color")]
		public string Color{ get; set; }

		
		[InputNumber("distance")]
		[JsonProperty("distance")]
		public float Distance{ get; set; }
	}
	
	
	public class HighchartPieDonutSeriesItem: Highchart {
	
		public HighchartPieDonutSeriesItem():base(){
			Name="Browsers";
			Data=new List<HighchartPieDonutSeriesItemDataItem>{new HighchartPieDonutSeriesItemDataItem(),new HighchartPieDonutSeriesItemDataItem(),new HighchartPieDonutSeriesItemDataItem(),new HighchartPieDonutSeriesItemDataItem(),new HighchartPieDonutSeriesItemDataItem(),new HighchartPieDonutSeriesItemDataItem(),new HighchartPieDonutSeriesItemDataItem()};
			Size="60%";
			DataLabels=new HighchartPieDonutSeriesItemDataLabels();
	
		}
	
		[InputText("name")]
		[JsonProperty("name")]
		public string Name{ get; set; }	[JsonProperty("data")]
		public List<HighchartPieDonutSeriesItemDataItem> Data{ get; set; }	[InputText("size")]
		[JsonProperty("size")]
		public string Size{ get; set; }	[JsonProperty("dataLabels")]
		public HighchartPieDonutSeriesItemDataLabels DataLabels{ get; set; }
	}
	
	
	public class HighchartPieDonutResponsiveRulesItemCondition: Highchart {
	
		public HighchartPieDonutResponsiveRulesItemCondition():base(){
			MaxWidth=400f;
	
		}
	
		[InputNumber("maxWidth")]
		[JsonProperty("maxWidth")]
		public float MaxWidth{ get; set; }
	}
	
	
	public class HighchartPieDonutResponsiveRulesItemChartOptionsSeriesItem: Highchart {
	
		public HighchartPieDonutResponsiveRulesItemChartOptionsSeriesItem():base(){
	
		}
	
	
	}
	
	
	public class HighchartPieDonutResponsiveRulesItemChartOptions: Highchart {
	
		public HighchartPieDonutResponsiveRulesItemChartOptions():base(){
			Series=new List<HighchartPieDonutResponsiveRulesItemChartOptionsSeriesItem>{new HighchartPieDonutResponsiveRulesItemChartOptionsSeriesItem(),new HighchartPieDonutResponsiveRulesItemChartOptionsSeriesItem()};
	
		}
	
		[JsonProperty("series")]
		public List<HighchartPieDonutResponsiveRulesItemChartOptionsSeriesItem> Series{ get; set; }
	}
	
	
	public class HighchartPieDonutResponsiveRulesItem: Highchart {
	
		public HighchartPieDonutResponsiveRulesItem():base(){
			Condition=new HighchartPieDonutResponsiveRulesItemCondition();
			ChartOptions=new HighchartPieDonutResponsiveRulesItemChartOptions();
	
		}
	
		[JsonProperty("condition")]
		public HighchartPieDonutResponsiveRulesItemCondition Condition{ get; set; }	[JsonProperty("chartOptions")]
		public HighchartPieDonutResponsiveRulesItemChartOptions ChartOptions{ get; set; }
	}
	
	
	public class HighchartPieDonutResponsive: Highchart {
	
		public HighchartPieDonutResponsive():base(){
			Rules=new List<HighchartPieDonutResponsiveRulesItem>{new HighchartPieDonutResponsiveRulesItem()};
	
		}
	
		[JsonProperty("rules")]
		public List<HighchartPieDonutResponsiveRulesItem> Rules{ get; set; }
	}
	
	
	public class HighchartPieDonut: Highchart {
	
		public HighchartPieDonut():base(){
			Chart=new HighchartPieDonutChart();
			Title=new HighchartPieDonutTitle();
			Subtitle=new HighchartPieDonutSubtitle();
			PlotOptions=new HighchartPieDonutPlotOptions();
			Tooltip=new HighchartPieDonutTooltip();
			Series=new List<HighchartPieDonutSeriesItem>{new HighchartPieDonutSeriesItem(),new HighchartPieDonutSeriesItem()};
			Responsive=new HighchartPieDonutResponsive();
	
		}
	
		[JsonProperty("chart")]
		public HighchartPieDonutChart Chart{ get; set; }	[JsonProperty("title")]
		public HighchartPieDonutTitle Title{ get; set; }	[JsonProperty("subtitle")]
		public HighchartPieDonutSubtitle Subtitle{ get; set; }	[JsonProperty("plotOptions")]
		public HighchartPieDonutPlotOptions PlotOptions{ get; set; }	[JsonProperty("tooltip")]
		public HighchartPieDonutTooltip Tooltip{ get; set; }	[JsonProperty("series")]
		public List<HighchartPieDonutSeriesItem> Series{ get; set; }	[JsonProperty("responsive")]
		public HighchartPieDonutResponsive Responsive{ get; set; }
	}
	
	
}