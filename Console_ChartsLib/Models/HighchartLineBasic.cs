using System.Collections.Generic;

using Newtonsoft.Json;
using System;

namespace Highcharts.Models.HighchartLineBasic
{
	public class HighchartLineBasicTitle: Highchart {
	
		public HighchartLineBasicTitle():base(){
			Text="Solar Employment Growth by Sector, 2010-2016";
	
		}
	
		[InputText("text")]
		[JsonProperty("text")]
		public string Text{ get; set; }
	}
	
	
	public class HighchartLineBasicSubtitle: Highchart {

		[InputText("text")]
		[JsonProperty("text")]
		public string Text { get; set; }

		public HighchartLineBasicSubtitle():base(){
			Text="Source: thesolarfoundation.com";
	
		}
	}
	
	
	public class HighchartLineBasicYAxisTitle: Highchart {

		[InputText("text")]
		[JsonProperty("text")]
		public string Text { get; set; }

		public HighchartLineBasicYAxisTitle():base(){
			Text="Number of Employees";
	
		}	
	}
	
	
	public class HighchartLineBasicYAxis: Highchart {

		[JsonProperty("title")]
		public HighchartLineBasicYAxisTitle Title { get; set; }


		public HighchartLineBasicYAxis():base(){
			Title=new HighchartLineBasicYAxisTitle();
	
		}
	

	}
	
	
	public class HighchartLineBasicXAxisAccessibility: Highchart {
	
		public HighchartLineBasicXAxisAccessibility():base(){
			RangeDescription="Range: 2010 to 2017";
	
		}
	
		[InputText("rangeDescription")]
		[JsonProperty("rangeDescription")]
		public string RangeDescription{ get; set; }
	}
	
	
	public class HighchartLineBasicXAxis: Highchart {
	
		public HighchartLineBasicXAxis():base(){
			Accessibility=new HighchartLineBasicXAxisAccessibility();
	
		}
	
		[JsonProperty("accessibility")]
		public HighchartLineBasicXAxisAccessibility Accessibility{ get; set; }
	}
	
	
	public class HighchartLineBasicLegend: Highchart {
	
		public HighchartLineBasicLegend():base(){
			Layout="vertical";
			Align="right";
			VerticalAlign="middle";
	
		}
	
		[InputText("layout")]
		[JsonProperty("layout")]
		public string Layout{ get; set; }	[InputText("align")]
		[JsonProperty("align")]
		public string Align{ get; set; }	[InputText("verticalAlign")]
		[JsonProperty("verticalAlign")]
		public string VerticalAlign{ get; set; }
	}
	
	
	public class HighchartLineBasicPlotOptionsSeriesLabel: Highchart {
	
		public HighchartLineBasicPlotOptionsSeriesLabel():base(){
			ConnectorAllowed=null;
	
		}
	
		[JsonProperty("connectorAllowed")]
		public object ConnectorAllowed{ get; set; }
	}
	
	
	public class HighchartLineBasicPlotOptionsSeries: Highchart {
	
		public HighchartLineBasicPlotOptionsSeries():base(){
			Label=new HighchartLineBasicPlotOptionsSeriesLabel();
			PointStart=2010f;
	
		}
	
		[JsonProperty("label")]
		public HighchartLineBasicPlotOptionsSeriesLabel Label{ get; set; }	[InputNumber("pointStart")]
		[JsonProperty("pointStart")]
		public float PointStart{ get; set; }
	}
	
	
	public class HighchartLineBasicPlotOptions: Highchart {
	
		public HighchartLineBasicPlotOptions():base(){
			Series=new HighchartLineBasicPlotOptionsSeries();
	
		}
	
		[JsonProperty("series")]
		public HighchartLineBasicPlotOptionsSeries Series{ get; set; }
	}
	
	
	public class HighchartLineBasicSeriesItem: Highchart {
	
		public HighchartLineBasicSeriesItem():base(){
			Name="Installation";
			Data=new List<Nullable<float>>{43934f,52503f,57177f,69658f,97031f,119931f,137133f,154175f};
	
		}
	
		[InputText("name")]
		[JsonProperty("name")]
		public string Name{ get; set; }	[JsonProperty("data")]
		public List<Nullable<float>> Data{ get; set; }
	}
	
	
	public class HighchartLineBasicResponsiveRulesItemCondition: Highchart {
	
		public HighchartLineBasicResponsiveRulesItemCondition():base(){
			MaxWidth=500f;
	
		}
	
		[InputNumber("maxWidth")]
		[JsonProperty("maxWidth")]
		public float MaxWidth{ get; set; }
	}
	
	
	public class HighchartLineBasicResponsiveRulesItemChartOptionsLegend: Highchart {
	
		public HighchartLineBasicResponsiveRulesItemChartOptionsLegend():base(){
			Layout="horizontal";
			Align="center";
			VerticalAlign="bottom";
	
		}
	
		[InputText("layout")]
		[JsonProperty("layout")]
		public string Layout{ get; set; }	[InputText("align")]
		[JsonProperty("align")]
		public string Align{ get; set; }	[InputText("verticalAlign")]
		[JsonProperty("verticalAlign")]
		public string VerticalAlign{ get; set; }
	}
	
	
	public class HighchartLineBasicResponsiveRulesItemChartOptions: Highchart {
	
		public HighchartLineBasicResponsiveRulesItemChartOptions():base(){
			Legend=new HighchartLineBasicResponsiveRulesItemChartOptionsLegend();
	
		}
	
		[JsonProperty("legend")]
		public HighchartLineBasicResponsiveRulesItemChartOptionsLegend Legend{ get; set; }
	}
	
	
	public class HighchartLineBasicResponsiveRulesItem: Highchart {
	
		public HighchartLineBasicResponsiveRulesItem():base(){
			Condition=new HighchartLineBasicResponsiveRulesItemCondition();
			ChartOptions=new HighchartLineBasicResponsiveRulesItemChartOptions();
	
		}
	
		[JsonProperty("condition")]
		public HighchartLineBasicResponsiveRulesItemCondition Condition{ get; set; }	[JsonProperty("chartOptions")]
		public HighchartLineBasicResponsiveRulesItemChartOptions ChartOptions{ get; set; }
	}
	
	
	public class HighchartLineBasicResponsive: Highchart {
	
		public HighchartLineBasicResponsive():base(){
			Rules=new List<HighchartLineBasicResponsiveRulesItem>{new HighchartLineBasicResponsiveRulesItem()};
	
		}
	
		[JsonProperty("rules")]
		public List<HighchartLineBasicResponsiveRulesItem> Rules{ get; set; }
	}
	
	
	public class HighchartLineBasic: Highchart {
	
		public HighchartLineBasic():base(){
			Title=new HighchartLineBasicTitle();
			Subtitle=new HighchartLineBasicSubtitle();
			YAxis=new HighchartLineBasicYAxis();
			XAxis=new HighchartLineBasicXAxis();
			Legend=new HighchartLineBasicLegend();
			PlotOptions=new HighchartLineBasicPlotOptions();
			Series=new List<HighchartLineBasicSeriesItem>{new HighchartLineBasicSeriesItem(),new HighchartLineBasicSeriesItem(),new HighchartLineBasicSeriesItem(),new HighchartLineBasicSeriesItem(),new HighchartLineBasicSeriesItem()};
			Responsive=new HighchartLineBasicResponsive();
	
		}
	
		[JsonProperty("title")]
		public HighchartLineBasicTitle Title{ get; set; }	[JsonProperty("subtitle")]
		public HighchartLineBasicSubtitle Subtitle{ get; set; }	[JsonProperty("yAxis")]
		public HighchartLineBasicYAxis YAxis{ get; set; }	[JsonProperty("xAxis")]
		public HighchartLineBasicXAxis XAxis{ get; set; }	[JsonProperty("legend")]
		public HighchartLineBasicLegend Legend{ get; set; }	[JsonProperty("plotOptions")]
		public HighchartLineBasicPlotOptions PlotOptions{ get; set; }	[JsonProperty("series")]
		public List<HighchartLineBasicSeriesItem> Series{ get; set; }	[JsonProperty("responsive")]
		public HighchartLineBasicResponsive Responsive{ get; set; }
	}
	
	
}