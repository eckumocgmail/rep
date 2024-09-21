using System.Collections.Generic;

using Newtonsoft.Json;
using System;

namespace Highcharts.Models.HighchartPolarRadialBar
{
	public class HighchartPolarRadialBarChartInverted: Highchart {
	
		public HighchartPolarRadialBarChartInverted():base(){
	
		}
	
	
	}
	
	
	public class HighchartPolarRadialBarChartPolar: Highchart {
	
		public HighchartPolarRadialBarChartPolar():base(){
	
		}
	
	
	}
	
	
	public class HighchartPolarRadialBarChart: Highchart {
	
		public HighchartPolarRadialBarChart():base(){
			Type="column";
			Inverted=new HighchartPolarRadialBarChartInverted();
			Polar=new HighchartPolarRadialBarChartPolar();
	
		}
	
		[InputText("type")]
		[JsonProperty("type")]
		public string Type{ get; set; }	[JsonProperty("inverted")]
		public HighchartPolarRadialBarChartInverted Inverted{ get; set; }	[JsonProperty("polar")]
		public HighchartPolarRadialBarChartPolar Polar{ get; set; }
	}
	
	
	public class HighchartPolarRadialBarTitle: Highchart {
	
		public HighchartPolarRadialBarTitle():base(){
			Text="Winter Olympic medals per existing country (TOP 5)";
	
		}
	
		[InputText("text")]
		[JsonProperty("text")]
		public string Text{ get; set; }
	}
	
	
	public class HighchartPolarRadialBarTooltipOutside: Highchart {
	
		public HighchartPolarRadialBarTooltipOutside():base(){
	
		}
	
	
	}
	
	
	public class HighchartPolarRadialBarTooltip: Highchart {
	
		public HighchartPolarRadialBarTooltip():base(){
			Outside=new HighchartPolarRadialBarTooltipOutside();
	
		}
	
		[JsonProperty("outside")]
		public HighchartPolarRadialBarTooltipOutside Outside{ get; set; }
	}
	
	
	public class HighchartPolarRadialBarPane: Highchart {
	
		public HighchartPolarRadialBarPane():base(){
			Size="85%";
			InnerSize="20%";
			EndAngle=270f;
	
		}
	
		[InputText("size")]
		[JsonProperty("size")]
		public string Size{ get; set; }	[InputText("innerSize")]
		[JsonProperty("innerSize")]
		public string InnerSize{ get; set; }	[InputNumber("endAngle")]
		[JsonProperty("endAngle")]
		public float EndAngle{ get; set; }
	}
	
	
	public class HighchartPolarRadialBarXAxisLabelsUseHTML: Highchart {
	
		public HighchartPolarRadialBarXAxisLabelsUseHTML():base(){
	
		}
	
	
	}
	
	
	public class HighchartPolarRadialBarXAxisLabelsAllowOverlap: Highchart {
	
		public HighchartPolarRadialBarXAxisLabelsAllowOverlap():base(){
	
		}
	
	
	}
	
	
	public class HighchartPolarRadialBarXAxisLabelsStyle: Highchart {
	
		public HighchartPolarRadialBarXAxisLabelsStyle():base(){
			FontSize="13px";
	
		}
	
		[InputText("fontSize")]
		[JsonProperty("fontSize")]
		public string FontSize{ get; set; }
	}
	
	
	public class HighchartPolarRadialBarXAxisLabels: Highchart {
	
		public HighchartPolarRadialBarXAxisLabels():base(){
			Align="right";
			UseHTML=new HighchartPolarRadialBarXAxisLabelsUseHTML();
			AllowOverlap=new HighchartPolarRadialBarXAxisLabelsAllowOverlap();
			Step=1f;
			Y=3f;
			Style=new HighchartPolarRadialBarXAxisLabelsStyle();
	
		}
	
		[InputText("align")]
		[JsonProperty("align")]
		public string Align{ get; set; }	[JsonProperty("useHTML")]
		public HighchartPolarRadialBarXAxisLabelsUseHTML UseHTML{ get; set; }	[JsonProperty("allowOverlap")]
		public HighchartPolarRadialBarXAxisLabelsAllowOverlap AllowOverlap{ get; set; }	[InputNumber("step")]
		[JsonProperty("step")]
		public float Step{ get; set; }	[InputNumber("y")]
		[JsonProperty("y")]
		public float Y{ get; set; }	[JsonProperty("style")]
		public HighchartPolarRadialBarXAxisLabelsStyle Style{ get; set; }
	}
	
	
	public class HighchartPolarRadialBarXAxis: Highchart {
	
		public HighchartPolarRadialBarXAxis():base(){
			TickInterval=1f;
			Labels=new HighchartPolarRadialBarXAxisLabels();
			LineWidth=null;
			Categories=new List<string>{"Norway <span class='f16'><span id='flag' class='flag no'></span></span>","United States <span class='f16'><span id='flag' class='flag us'></span></span>","Germany <span class='f16'><span id='flag' class='flag de'></span></span>","Canada <span class='f16'><span id='flag' class='flag ca'></span></span>","Austria <span class='f16'><span id='flag' class='flag at'></span></span>"};
	
		}
	
		[InputNumber("tickInterval")]
		[JsonProperty("tickInterval")]
		public float TickInterval{ get; set; }	[JsonProperty("labels")]
		public HighchartPolarRadialBarXAxisLabels Labels{ get; set; }	[JsonProperty("lineWidth")]
		public object LineWidth{ get; set; }	[JsonProperty("categories")]
		public List<string> Categories{ get; set; }
	}
	
	
	public class HighchartPolarRadialBarYAxisCrosshairEnabled: Highchart {
	
		public HighchartPolarRadialBarYAxisCrosshairEnabled():base(){
	
		}
	
	
	}
	
	
	public class HighchartPolarRadialBarYAxisCrosshair: Highchart {
	
		public HighchartPolarRadialBarYAxisCrosshair():base(){
			Enabled=new HighchartPolarRadialBarYAxisCrosshairEnabled();
			Color="#333";
	
		}
	
		[JsonProperty("enabled")]
		public HighchartPolarRadialBarYAxisCrosshairEnabled Enabled{ get; set; }	[InputText("color")]
		[JsonProperty("color")]
		public string Color{ get; set; }
	}
	
	
	public class HighchartPolarRadialBarYAxisEndOnTick: Highchart {
	
		public HighchartPolarRadialBarYAxisEndOnTick():base(){
	
		}
	
	
	}
	
	
	public class HighchartPolarRadialBarYAxisShowLastLabel: Highchart {
	
		public HighchartPolarRadialBarYAxisShowLastLabel():base(){
	
		}
	
	
	}
	
	
	public class HighchartPolarRadialBarYAxis: Highchart {
	
		public HighchartPolarRadialBarYAxis():base(){
			Crosshair=new HighchartPolarRadialBarYAxisCrosshair();
			LineWidth=null;
			TickInterval=25f;
			ReversedStacks=null;
			EndOnTick=new HighchartPolarRadialBarYAxisEndOnTick();
			ShowLastLabel=new HighchartPolarRadialBarYAxisShowLastLabel();
	
		}
	
		[JsonProperty("crosshair")]
		public HighchartPolarRadialBarYAxisCrosshair Crosshair{ get; set; }	[JsonProperty("lineWidth")]
		public object LineWidth{ get; set; }	[InputNumber("tickInterval")]
		[JsonProperty("tickInterval")]
		public float TickInterval{ get; set; }	[JsonProperty("reversedStacks")]
		public object ReversedStacks{ get; set; }	[JsonProperty("endOnTick")]
		public HighchartPolarRadialBarYAxisEndOnTick EndOnTick{ get; set; }	[JsonProperty("showLastLabel")]
		public HighchartPolarRadialBarYAxisShowLastLabel ShowLastLabel{ get; set; }
	}
	
	
	public class HighchartPolarRadialBarPlotOptionsColumn: Highchart {
	
		public HighchartPolarRadialBarPlotOptionsColumn():base(){
			Stacking="normal";
			BorderWidth=null;
			PointPadding=null;
			GroupPadding=0.15f;
	
		}
	
		[InputText("stacking")]
		[JsonProperty("stacking")]
		public string Stacking{ get; set; }	[JsonProperty("borderWidth")]
		public object BorderWidth{ get; set; }	[JsonProperty("pointPadding")]
		public object PointPadding{ get; set; }	[InputNumber("groupPadding")]
		[JsonProperty("groupPadding")]
		public float GroupPadding{ get; set; }
	}
	
	
	public class HighchartPolarRadialBarPlotOptions: Highchart {
	
		public HighchartPolarRadialBarPlotOptions():base(){
			Column=new HighchartPolarRadialBarPlotOptionsColumn();
	
		}
	
		[JsonProperty("column")]
		public HighchartPolarRadialBarPlotOptionsColumn Column{ get; set; }
	}
	
	
	public class HighchartPolarRadialBarSeriesItem: Highchart {
	
		public HighchartPolarRadialBarSeriesItem():base(){
			Name="Gold medals";
			Data=new List<Nullable<float>>{132f,105f,92f,73f,64f};
	
		}
	
		[InputText("name")]
		[JsonProperty("name")]
		public string Name{ get; set; }	[JsonProperty("data")]
		public List<Nullable<float>> Data{ get; set; }
	}
	
	
	public class HighchartPolarRadialBar: Highchart {
	
		public HighchartPolarRadialBar():base(){
	 
			Colors=new List<string>{"#FFD700","#C0C0C0","#CD7F32"};
			Chart=new HighchartPolarRadialBarChart();
			Title=new HighchartPolarRadialBarTitle();
			Tooltip=new HighchartPolarRadialBarTooltip();
			Pane=new HighchartPolarRadialBarPane();
			XAxis=new HighchartPolarRadialBarXAxis();
			YAxis=new HighchartPolarRadialBarYAxis();
			PlotOptions=new HighchartPolarRadialBarPlotOptions();
			Series=new List<HighchartPolarRadialBarSeriesItem>{new HighchartPolarRadialBarSeriesItem(),new HighchartPolarRadialBarSeriesItem(),new HighchartPolarRadialBarSeriesItem()};
	
		}
	
		[JsonProperty("colors")]
		public List<string> Colors{ get; set; }	[JsonProperty("chart")]
		public HighchartPolarRadialBarChart Chart{ get; set; }	[JsonProperty("title")]
		public HighchartPolarRadialBarTitle Title{ get; set; }	[JsonProperty("tooltip")]
		public HighchartPolarRadialBarTooltip Tooltip{ get; set; }	[JsonProperty("pane")]
		public HighchartPolarRadialBarPane Pane{ get; set; }	[JsonProperty("xAxis")]
		public HighchartPolarRadialBarXAxis XAxis{ get; set; }	[JsonProperty("yAxis")]
		public HighchartPolarRadialBarYAxis YAxis{ get; set; }	[JsonProperty("plotOptions")]
		public HighchartPolarRadialBarPlotOptions PlotOptions{ get; set; }	[JsonProperty("series")]
		public List<HighchartPolarRadialBarSeriesItem> Series{ get; set; }
	}
	
	
}