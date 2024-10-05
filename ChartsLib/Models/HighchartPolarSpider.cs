using System.Collections.Generic;

using Newtonsoft.Json;
using System;

namespace Highcharts.Models.HighchartPolarSpider
{
	public class HighchartPolarSpiderChartPolar: Highchart {
	
		public HighchartPolarSpiderChartPolar():base(){
	
		}
	
	
	}
	
	
	public class HighchartPolarSpiderChart: Highchart {
	
		public HighchartPolarSpiderChart():base(){
			Polar=new HighchartPolarSpiderChartPolar();
			Type="line";
	
		}
	
		[JsonProperty("polar")]
		public HighchartPolarSpiderChartPolar Polar{ get; set; }	[InputText("type")]
		[JsonProperty("type")]
		public string Type{ get; set; }
	}
	
	
	public class HighchartPolarSpiderAccessibility: Highchart {
	
		public HighchartPolarSpiderAccessibility():base(){
			Description="A spiderweb chart compares the allocated budget against actual spending within an organization. The spider chart has six spokes. Each spoke represents one of the 6 departments within the organization: sales, marketing, development, customer support, information technology and administration. The chart is interactive, and each data point is displayed upon hovering. The chart clearly shows that 4 of the 6 departments have overspent their budget with Marketing responsible for the greatest overspend of $20,000. The allocated budget and actual spending data points for each department are as follows: Sales. Budget equals $43,000; spending equals $50,000. Marketing. Budget equals $19,000; spending equals $39,000. Development. Budget equals $60,000; spending equals $42,000. Customer support. Budget equals $35,000; spending equals $31,000. Information technology. Budget equals $17,000; spending equals $26,000. Administration. Budget equals $10,000; spending equals $14,000.";
	
		}
	
		[InputText("description")]
		[JsonProperty("description")]
		public string Description{ get; set; }
	}
	
	
	public class HighchartPolarSpiderTitle: Highchart {
	
		public HighchartPolarSpiderTitle():base(){
			Text="Budget vs spending";
			X=-80f;
	
		}
	
		[InputText("text")]
		[JsonProperty("text")]
		public string Text{ get; set; }	[InputNumber("x")]
		[JsonProperty("x")]
		public float X{ get; set; }
	}
	
	
	public class HighchartPolarSpiderPane: Highchart {
	
		public HighchartPolarSpiderPane():base(){
			Size="80%";
	
		}
	
		[InputText("size")]
		[JsonProperty("size")]
		public string Size{ get; set; }
	}
	
	
	public class HighchartPolarSpiderXAxis: Highchart {
	
		public HighchartPolarSpiderXAxis():base(){
			Categories=new List<string>{"Sales","Marketing","Development","Customer Support","Information Technology","Administration"};
			TickmarkPlacement="on";
			LineWidth=null;
	
		}
	
		[JsonProperty("categories")]
		public List<string> Categories{ get; set; }	[InputText("tickmarkPlacement")]
		[JsonProperty("tickmarkPlacement")]
		public string TickmarkPlacement{ get; set; }	[JsonProperty("lineWidth")]
		public object LineWidth{ get; set; }
	}
	
	
	public class HighchartPolarSpiderYAxis: Highchart {
	
		public HighchartPolarSpiderYAxis():base(){
			GridLineInterpolation="polygon";
			LineWidth=null;
			Min=null;
	
		}
	
		[InputText("gridLineInterpolation")]
		[JsonProperty("gridLineInterpolation")]
		public string GridLineInterpolation{ get; set; }	[JsonProperty("lineWidth")]
		public object LineWidth{ get; set; }	[JsonProperty("min")]
		public object Min{ get; set; }
	}
	
	
	public class HighchartPolarSpiderTooltipShared: Highchart {
	
		public HighchartPolarSpiderTooltipShared():base(){
	
		}
	
	
	}
	
	
	public class HighchartPolarSpiderTooltip: Highchart {
	
		public HighchartPolarSpiderTooltip():base(){
			Shared=new HighchartPolarSpiderTooltipShared();
			PointFormat="<span style='color:{series.color}'>{series.name}: <b>${point.y:,.0f}</b><br/>";
	
		}
	
		[JsonProperty("shared")]
		public HighchartPolarSpiderTooltipShared Shared{ get; set; }	[InputText("pointFormat")]
		[JsonProperty("pointFormat")]
		public string PointFormat{ get; set; }
	}
	
	
	public class HighchartPolarSpiderLegend: Highchart {
	
		public HighchartPolarSpiderLegend():base(){
			Align="right";
			VerticalAlign="middle";
			Layout="vertical";
	
		}
	
		[InputText("align")]
		[JsonProperty("align")]
		public string Align{ get; set; }	[InputText("verticalAlign")]
		[JsonProperty("verticalAlign")]
		public string VerticalAlign{ get; set; }	[InputText("layout")]
		[JsonProperty("layout")]
		public string Layout{ get; set; }
	}
	
	
	public class HighchartPolarSpiderSeriesItem: Highchart {
	
		public HighchartPolarSpiderSeriesItem():base(){
			Name="Allocated Budget";
			Data=new List<Nullable<float>>{43000f,19000f,60000f,35000f,17000f,10000f};
			PointPlacement="on";
	
		}
	
		[InputText("name")]
		[JsonProperty("name")]
		public string Name{ get; set; }	[JsonProperty("data")]
		public List<Nullable<float>> Data{ get; set; }	[InputText("pointPlacement")]
		[JsonProperty("pointPlacement")]
		public string PointPlacement{ get; set; }
	}
	
	
	public class HighchartPolarSpiderResponsiveRulesItemCondition: Highchart {
	
		public HighchartPolarSpiderResponsiveRulesItemCondition():base(){
			MaxWidth=500f;
	
		}
	
		[InputNumber("maxWidth")]
		[JsonProperty("maxWidth")]
		public float MaxWidth{ get; set; }
	}
	
	
	public class HighchartPolarSpiderResponsiveRulesItemChartOptionsLegend: Highchart {
	
		public HighchartPolarSpiderResponsiveRulesItemChartOptionsLegend():base(){
			Align="center";
			VerticalAlign="bottom";
			Layout="horizontal";
	
		}
	
		[InputText("align")]
		[JsonProperty("align")]
		public string Align{ get; set; }	[InputText("verticalAlign")]
		[JsonProperty("verticalAlign")]
		public string VerticalAlign{ get; set; }	[InputText("layout")]
		[JsonProperty("layout")]
		public string Layout{ get; set; }
	}
	
	
	public class HighchartPolarSpiderResponsiveRulesItemChartOptionsPane: Highchart {
	
		public HighchartPolarSpiderResponsiveRulesItemChartOptionsPane():base(){
			Size="70%";
	
		}
	
		[InputText("size")]
		[JsonProperty("size")]
		public string Size{ get; set; }
	}
	
	
	public class HighchartPolarSpiderResponsiveRulesItemChartOptions: Highchart {
	
		public HighchartPolarSpiderResponsiveRulesItemChartOptions():base(){
			Legend=new HighchartPolarSpiderResponsiveRulesItemChartOptionsLegend();
			Pane=new HighchartPolarSpiderResponsiveRulesItemChartOptionsPane();
	
		}
	
		[JsonProperty("legend")]
		public HighchartPolarSpiderResponsiveRulesItemChartOptionsLegend Legend{ get; set; }	[JsonProperty("pane")]
		public HighchartPolarSpiderResponsiveRulesItemChartOptionsPane Pane{ get; set; }
	}
	
	
	public class HighchartPolarSpiderResponsiveRulesItem: Highchart {
	
		public HighchartPolarSpiderResponsiveRulesItem():base(){
			Condition=new HighchartPolarSpiderResponsiveRulesItemCondition();
			ChartOptions=new HighchartPolarSpiderResponsiveRulesItemChartOptions();
	
		}
	
		[JsonProperty("condition")]
		public HighchartPolarSpiderResponsiveRulesItemCondition Condition{ get; set; }	[JsonProperty("chartOptions")]
		public HighchartPolarSpiderResponsiveRulesItemChartOptions ChartOptions{ get; set; }
	}
	
	
	public class HighchartPolarSpiderResponsive: Highchart {
	
		public HighchartPolarSpiderResponsive():base(){
			Rules=new List<HighchartPolarSpiderResponsiveRulesItem>{new HighchartPolarSpiderResponsiveRulesItem()};
	
		}
	
		[JsonProperty("rules")]
		public List<HighchartPolarSpiderResponsiveRulesItem> Rules{ get; set; }
	}
	
	
	public class HighchartPolarSpider: Highchart {
	
		public HighchartPolarSpider():base(){
			Chart=new HighchartPolarSpiderChart();
			Accessibility=new HighchartPolarSpiderAccessibility();
			Title=new HighchartPolarSpiderTitle();
			Pane=new HighchartPolarSpiderPane();
			XAxis=new HighchartPolarSpiderXAxis();
			YAxis=new HighchartPolarSpiderYAxis();
			Tooltip=new HighchartPolarSpiderTooltip();
			Legend=new HighchartPolarSpiderLegend();
			Series=new List<HighchartPolarSpiderSeriesItem>{new HighchartPolarSpiderSeriesItem(),new HighchartPolarSpiderSeriesItem()};
			Responsive=new HighchartPolarSpiderResponsive();
	
		}
	
		[JsonProperty("chart")]
		public HighchartPolarSpiderChart Chart{ get; set; }	[JsonProperty("accessibility")]
		public HighchartPolarSpiderAccessibility Accessibility{ get; set; }	[JsonProperty("title")]
		public HighchartPolarSpiderTitle Title{ get; set; }	[JsonProperty("pane")]
		public HighchartPolarSpiderPane Pane{ get; set; }	[JsonProperty("xAxis")]
		public HighchartPolarSpiderXAxis XAxis{ get; set; }	[JsonProperty("yAxis")]
		public HighchartPolarSpiderYAxis YAxis{ get; set; }	[JsonProperty("tooltip")]
		public HighchartPolarSpiderTooltip Tooltip{ get; set; }	[JsonProperty("legend")]
		public HighchartPolarSpiderLegend Legend{ get; set; }	[JsonProperty("series")]
		public List<HighchartPolarSpiderSeriesItem> Series{ get; set; }	[JsonProperty("responsive")]
		public HighchartPolarSpiderResponsive Responsive{ get; set; }
	}
	
	
}