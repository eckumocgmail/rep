using System.Collections.Generic;

using Newtonsoft.Json;
using System;

namespace Highcharts.Models.Highchart3dColumnStackingGrouping
{
	public class Highchart3dColumnStackingGroupingChartOptions3dEnabled: Highchart {
	
		public Highchart3dColumnStackingGroupingChartOptions3dEnabled():base(){
	
		}
	
	
	}
	
	
	public class Highchart3dColumnStackingGroupingChartOptions3d: Highchart {
	
		public Highchart3dColumnStackingGroupingChartOptions3d():base(){
			Enabled=new Highchart3dColumnStackingGroupingChartOptions3dEnabled();
			Alpha=15f;
			Beta=15f;
			ViewDistance=25f;
			Depth=40f;
	
		}
	
		[JsonProperty("enabled")]
		public Highchart3dColumnStackingGroupingChartOptions3dEnabled Enabled{ get; set; }	[InputNumber("alpha")]
		[JsonProperty("alpha")]
		public float Alpha{ get; set; }	[InputNumber("beta")]
		[JsonProperty("beta")]
		public float Beta{ get; set; }	[InputNumber("viewDistance")]
		[JsonProperty("viewDistance")]
		public float ViewDistance{ get; set; }	[InputNumber("depth")]
		[JsonProperty("depth")]
		public float Depth{ get; set; }
	}
	
	
	public class Highchart3dColumnStackingGroupingChart: Highchart {
	
		public Highchart3dColumnStackingGroupingChart():base(){
			Type="column";
			Options3d=new Highchart3dColumnStackingGroupingChartOptions3d();
	
		}
	
		[InputText("type")]
		[JsonProperty("type")]
		public string Type{ get; set; }	[JsonProperty("options3d")]
		public Highchart3dColumnStackingGroupingChartOptions3d Options3d{ get; set; }
	}
	
	
	public class Highchart3dColumnStackingGroupingTitle: Highchart {
	
		public Highchart3dColumnStackingGroupingTitle():base(){
			Text="Затраты по категориям";
	
		}
	
		[InputText("text")]
		[JsonProperty("text")]
		public string Text{ get; set; }
	}
	
	
	public class Highchart3dColumnStackingGroupingXAxisLabelsSkew3d: Highchart {
	
		public Highchart3dColumnStackingGroupingXAxisLabelsSkew3d():base(){
	
		}
	
	
	}
	
	
	public class Highchart3dColumnStackingGroupingXAxisLabelsStyle: Highchart {
	
		public Highchart3dColumnStackingGroupingXAxisLabelsStyle():base(){
			FontSize="16px";
	
		}
	
		[InputText("fontSize")]
		[JsonProperty("fontSize")]
		public string FontSize{ get; set; }
	}
	
	
	public class Highchart3dColumnStackingGroupingXAxisLabels: Highchart {
	
		public Highchart3dColumnStackingGroupingXAxisLabels():base(){
			Skew3d=new Highchart3dColumnStackingGroupingXAxisLabelsSkew3d();
			Style=new Highchart3dColumnStackingGroupingXAxisLabelsStyle();
	
		}
	
		[JsonProperty("skew3d")]
		public Highchart3dColumnStackingGroupingXAxisLabelsSkew3d Skew3d{ get; set; }	[JsonProperty("style")]
		public Highchart3dColumnStackingGroupingXAxisLabelsStyle Style{ get; set; }
	}
	
	
	public class Highchart3dColumnStackingGroupingXAxis: Highchart {
	
		public Highchart3dColumnStackingGroupingXAxis():base(){
			Categories=new List<string>{"Apples","Oranges","Pears","Grapes","Bananas"};
			Labels=new Highchart3dColumnStackingGroupingXAxisLabels();
	
		}
	
		[JsonProperty("categories")]
		public List<string> Categories{ get; set; }	[JsonProperty("labels")]
		public Highchart3dColumnStackingGroupingXAxisLabels Labels{ get; set; }
	}
	
	
	public class Highchart3dColumnStackingGroupingYAxisTitleSkew3d: Highchart {
	
		public Highchart3dColumnStackingGroupingYAxisTitleSkew3d():base(){
	
		}
	
	
	}
	
	
	public class Highchart3dColumnStackingGroupingYAxisTitle: Highchart {
	
		public Highchart3dColumnStackingGroupingYAxisTitle():base(){
			Text="Number of fruits";
			Skew3d=new Highchart3dColumnStackingGroupingYAxisTitleSkew3d();
	
		}
	
		[InputText("text")]
		[JsonProperty("text")]
		public string Text{ get; set; }	[JsonProperty("skew3d")]
		public Highchart3dColumnStackingGroupingYAxisTitleSkew3d Skew3d{ get; set; }
	}
	
	
	public class Highchart3dColumnStackingGroupingYAxis: Highchart {
	
		public Highchart3dColumnStackingGroupingYAxis():base(){
			AllowDecimals=null;
			Min=null;
			Title=new Highchart3dColumnStackingGroupingYAxisTitle();
	
		}
	
		[JsonProperty("allowDecimals")]
		public object AllowDecimals{ get; set; }	[JsonProperty("min")]
		public object Min{ get; set; }	[JsonProperty("title")]
		public Highchart3dColumnStackingGroupingYAxisTitle Title{ get; set; }
	}
	
	
	public class Highchart3dColumnStackingGroupingTooltip: Highchart {
	
		public Highchart3dColumnStackingGroupingTooltip():base(){
			HeaderFormat="<b>{point.key}</b><br>";
			PointFormat="<span style='color:{series.color}'>●</span> {series.name}: {point.y} / {point.stackTotal}";
	
		}
	
		[InputText("headerFormat")]
		[JsonProperty("headerFormat")]
		public string HeaderFormat{ get; set; }	[InputText("pointFormat")]
		[JsonProperty("pointFormat")]
		public string PointFormat{ get; set; }
	}
	
	
	public class Highchart3dColumnStackingGroupingPlotOptionsColumn: Highchart {
	
		public Highchart3dColumnStackingGroupingPlotOptionsColumn():base(){
			Stacking="normal";
			Depth=40f;
	
		}
	
		[InputText("stacking")]
		[JsonProperty("stacking")]
		public string Stacking{ get; set; }	[InputNumber("depth")]
		[JsonProperty("depth")]
		public float Depth{ get; set; }
	}
	
	
	public class Highchart3dColumnStackingGroupingPlotOptions: Highchart {
	
		public Highchart3dColumnStackingGroupingPlotOptions():base(){
			Column=new Highchart3dColumnStackingGroupingPlotOptionsColumn();
	
		}
	
		[JsonProperty("column")]
		public Highchart3dColumnStackingGroupingPlotOptionsColumn Column{ get; set; }
	}
	
	
	public class Highchart3dColumnStackingGroupingSeriesItem: Highchart {
	
		public Highchart3dColumnStackingGroupingSeriesItem():base(){
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
	
	
	public class Highchart3dColumnStackingGrouping: Highchart {
	
		public Highchart3dColumnStackingGrouping():base(){
			Chart=new Highchart3dColumnStackingGroupingChart();
			Title=new Highchart3dColumnStackingGroupingTitle();
			XAxis=new Highchart3dColumnStackingGroupingXAxis();
			YAxis=new Highchart3dColumnStackingGroupingYAxis();
			Tooltip=new Highchart3dColumnStackingGroupingTooltip();
			PlotOptions=new Highchart3dColumnStackingGroupingPlotOptions();
			Series=new List<Highchart3dColumnStackingGroupingSeriesItem>{new Highchart3dColumnStackingGroupingSeriesItem(),new Highchart3dColumnStackingGroupingSeriesItem(),new Highchart3dColumnStackingGroupingSeriesItem(),new Highchart3dColumnStackingGroupingSeriesItem()};
	
		}
	
		[JsonProperty("chart")]
		public Highchart3dColumnStackingGroupingChart Chart{ get; set; }	[JsonProperty("title")]
		public Highchart3dColumnStackingGroupingTitle Title{ get; set; }	[JsonProperty("xAxis")]
		public Highchart3dColumnStackingGroupingXAxis XAxis{ get; set; }	[JsonProperty("yAxis")]
		public Highchart3dColumnStackingGroupingYAxis YAxis{ get; set; }	[JsonProperty("tooltip")]
		public Highchart3dColumnStackingGroupingTooltip Tooltip{ get; set; }	[JsonProperty("plotOptions")]
		public Highchart3dColumnStackingGroupingPlotOptions PlotOptions{ get; set; }	[JsonProperty("series")]
		public List<Highchart3dColumnStackingGroupingSeriesItem> Series{ get; set; }
	}
	
	
}