using System.Collections.Generic;

using Newtonsoft.Json;
using System;

namespace Highcharts.Models.Highchart3dScatterDraggable
{
	public class Highchart3dScatterDraggableChartOptions3dEnabled: Highchart {
	
		public Highchart3dScatterDraggableChartOptions3dEnabled():base(){
	
		}
	
	
	}
	
	
	public class Highchart3dScatterDraggableChartOptions3dFrameBottom: Highchart {
	
		public Highchart3dScatterDraggableChartOptions3dFrameBottom():base(){
			Size=1f;
			Color="rgba(0,0,0,0.02)";
	
		}
	
		[InputNumber("size")]
		[JsonProperty("size")]
		public float Size{ get; set; }	[InputText("color")]
		[JsonProperty("color")]
		public string Color{ get; set; }
	}
	
	
	public class Highchart3dScatterDraggableChartOptions3dFrameBack: Highchart {
	
		public Highchart3dScatterDraggableChartOptions3dFrameBack():base(){
			Size=1f;
			Color="rgba(0,0,0,0.04)";
	
		}
	
		[InputNumber("size")]
		[JsonProperty("size")]
		public float Size{ get; set; }	[InputText("color")]
		[JsonProperty("color")]
		public string Color{ get; set; }
	}
	
	
	public class Highchart3dScatterDraggableChartOptions3dFrameSide: Highchart {
	
		public Highchart3dScatterDraggableChartOptions3dFrameSide():base(){
			Size=1f;
			Color="rgba(0,0,0,0.06)";
	
		}
	
		[InputNumber("size")]
		[JsonProperty("size")]
		public float Size{ get; set; }	[InputText("color")]
		[JsonProperty("color")]
		public string Color{ get; set; }
	}
	
	
	public class Highchart3dScatterDraggableChartOptions3dFrame: Highchart {
	
		public Highchart3dScatterDraggableChartOptions3dFrame():base(){
			Bottom=new Highchart3dScatterDraggableChartOptions3dFrameBottom();
			Back=new Highchart3dScatterDraggableChartOptions3dFrameBack();
			Side=new Highchart3dScatterDraggableChartOptions3dFrameSide();
	
		}
	
		[JsonProperty("bottom")]
		public Highchart3dScatterDraggableChartOptions3dFrameBottom Bottom{ get; set; }	[JsonProperty("back")]
		public Highchart3dScatterDraggableChartOptions3dFrameBack Back{ get; set; }	[JsonProperty("side")]
		public Highchart3dScatterDraggableChartOptions3dFrameSide Side{ get; set; }
	}
	
	
	public class Highchart3dScatterDraggableChartOptions3d: Highchart {
	
		public Highchart3dScatterDraggableChartOptions3d():base(){
			Enabled=new Highchart3dScatterDraggableChartOptions3dEnabled();
			Alpha=10f;
			Beta=30f;
			Depth=250f;
			ViewDistance=5f;
			FitToPlot=null;
			Frame=new Highchart3dScatterDraggableChartOptions3dFrame();
	
		}
	
		[JsonProperty("enabled")]
		public Highchart3dScatterDraggableChartOptions3dEnabled Enabled{ get; set; }	[InputNumber("alpha")]
		[JsonProperty("alpha")]
		public float Alpha{ get; set; }	[InputNumber("beta")]
		[JsonProperty("beta")]
		public float Beta{ get; set; }	[InputNumber("depth")]
		[JsonProperty("depth")]
		public float Depth{ get; set; }	[InputNumber("viewDistance")]
		[JsonProperty("viewDistance")]
		public float ViewDistance{ get; set; }	[JsonProperty("fitToPlot")]
		public object FitToPlot{ get; set; }	[JsonProperty("frame")]
		public Highchart3dScatterDraggableChartOptions3dFrame Frame{ get; set; }
	}
	
	
	public class Highchart3dScatterDraggableChart: Highchart {
	
		public Highchart3dScatterDraggableChart():base(){
			RenderTo="container";
			Margin=100f;
			Type="scatter3d";
			Animation=null;
			Options3d=new Highchart3dScatterDraggableChartOptions3d();
	
		}
	
		[InputText("renderTo")]
		[JsonProperty("renderTo")]
		public string RenderTo{ get; set; }	[InputNumber("margin")]
		[JsonProperty("margin")]
		public float Margin{ get; set; }	[InputText("type")]
		[JsonProperty("type")]
		public string Type{ get; set; }	[JsonProperty("animation")]
		public object Animation{ get; set; }	[JsonProperty("options3d")]
		public Highchart3dScatterDraggableChartOptions3d Options3d{ get; set; }
	}
	
	
	public class Highchart3dScatterDraggableTitle: Highchart {
	
		public Highchart3dScatterDraggableTitle():base(){
			Text="Draggable box";
	
		}
	
		[InputText("text")]
		[JsonProperty("text")]
		public string Text{ get; set; }
	}
	
	
	public class Highchart3dScatterDraggableSubtitle: Highchart {
	
		public Highchart3dScatterDraggableSubtitle():base(){
			Text="Click and drag the plot area to rotate in space";
	
		}
	
		[InputText("text")]
		[JsonProperty("text")]
		public string Text{ get; set; }
	}
	
	
	public class Highchart3dScatterDraggablePlotOptionsScatter: Highchart {
	
		public Highchart3dScatterDraggablePlotOptionsScatter():base(){
			ChartWidth=10f;
			ChartHeight=10f;
			Depth=10f;
	
		}
	
		[InputNumber("width")]
		[JsonProperty("width")]
		public float ChartWidth{ get; set; }	

		[InputNumber("height")]
		[JsonProperty("height")]
		public float ChartHeight{ get; set; }
		
		[InputNumber("depth")]
		[JsonProperty("depth")]
		public float Depth{ get; set; }
	}
	
	
	public class Highchart3dScatterDraggablePlotOptions: Highchart {
	
		public Highchart3dScatterDraggablePlotOptions():base(){
			Scatter=new Highchart3dScatterDraggablePlotOptionsScatter();
	
		}
	
		[JsonProperty("scatter")]
		public Highchart3dScatterDraggablePlotOptionsScatter Scatter{ get; set; }
	}
	
	
	public class Highchart3dScatterDraggableYAxis: Highchart {
	
		public Highchart3dScatterDraggableYAxis():base(){
			Min=null;
			Max=10f;
			Title=null;
	
		}
	
		[JsonProperty("min")]
		public object Min{ get; set; }	[InputNumber("max")]
		[JsonProperty("max")]
		public float Max{ get; set; }	[JsonProperty("title")]
		public object Title{ get; set; }
	}
	
	
	public class Highchart3dScatterDraggableXAxis: Highchart {
	
		public Highchart3dScatterDraggableXAxis():base(){
			Min=null;
			Max=10f;
			GridLineWidth=1f;
	
		}
	
		[JsonProperty("min")]
		public object Min{ get; set; }	[InputNumber("max")]
		[JsonProperty("max")]
		public float Max{ get; set; }	[InputNumber("gridLineWidth")]
		[JsonProperty("gridLineWidth")]
		public float GridLineWidth{ get; set; }
	}
	
	
	public class Highchart3dScatterDraggableZAxis: Highchart {
	
		public Highchart3dScatterDraggableZAxis():base(){
			Min=null;
			Max=10f;
			ShowFirstLabel=null;
	
		}
	
		[JsonProperty("min")]
		public object Min{ get; set; }	[InputNumber("max")]
		[JsonProperty("max")]
		public float Max{ get; set; }	[JsonProperty("showFirstLabel")]
		public object ShowFirstLabel{ get; set; }
	}
	
	
	public class Highchart3dScatterDraggableLegend: Highchart {
	
		public Highchart3dScatterDraggableLegend():base(){
			Enabled=null;
	
		}
	
		[JsonProperty("enabled")]
		public object Enabled{ get; set; }
	}
	
	
	public class Highchart3dScatterDraggableSeriesItemColorByPoint: Highchart {
	
		public Highchart3dScatterDraggableSeriesItemColorByPoint():base(){
	
		}
	
	
	}
	
	
	public class Highchart3dScatterDraggableSeriesItemAccessibilityExposeAsGroupOnly: Highchart {
	
		public Highchart3dScatterDraggableSeriesItemAccessibilityExposeAsGroupOnly():base(){
	
		}
	
	
	}
	
	
	public class Highchart3dScatterDraggableSeriesItemAccessibility: Highchart {
	
		public Highchart3dScatterDraggableSeriesItemAccessibility():base(){
			ExposeAsGroupOnly=new Highchart3dScatterDraggableSeriesItemAccessibilityExposeAsGroupOnly();
	
		}
	
		[JsonProperty("exposeAsGroupOnly")]
		public Highchart3dScatterDraggableSeriesItemAccessibilityExposeAsGroupOnly ExposeAsGroupOnly{ get; set; }
	}
	
	
	public class Highchart3dScatterDraggableSeriesItem: Highchart {
	
		public Highchart3dScatterDraggableSeriesItem():base(){
			Name="Data";
			ColorByPoint=new Highchart3dScatterDraggableSeriesItemColorByPoint();
			Accessibility=new Highchart3dScatterDraggableSeriesItemAccessibility();
			Data=new List<List<Nullable<float>>>{new List<Nullable<float>>{1f,6f,5f},new List<Nullable<float>>{8f,7f,9f},new List<Nullable<float>>{1f,3f,4f},new List<Nullable<float>>{4f,6f,8f},new List<Nullable<float>>{5f,7f,7f},new List<Nullable<float>>{6f,9f,6f},new List<Nullable<float>>{7f,0,5f},new List<Nullable<float>>{2f,3f,3f},new List<Nullable<float>>{3f,9f,8f},new List<Nullable<float>>{3f,6f,5f},new List<Nullable<float>>{4f,9f,4f},new List<Nullable<float>>{2f,3f,3f},new List<Nullable<float>>{6f,9f,9f},new List<Nullable<float>>{0,7f,0},new List<Nullable<float>>{7f,7f,9f},new List<Nullable<float>>{7f,2f,9f},new List<Nullable<float>>{0,6f,2f},new List<Nullable<float>>{4f,6f,7f},new List<Nullable<float>>{3f,7f,7f},new List<Nullable<float>>{0,1f,7f},new List<Nullable<float>>{2f,8f,6f},new List<Nullable<float>>{2f,3f,7f},new List<Nullable<float>>{6f,4f,8f},new List<Nullable<float>>{3f,5f,9f},new List<Nullable<float>>{7f,9f,5f},new List<Nullable<float>>{3f,1f,7f},new List<Nullable<float>>{4f,4f,2f},new List<Nullable<float>>{3f,6f,2f},new List<Nullable<float>>{3f,1f,6f},new List<Nullable<float>>{6f,8f,5f},new List<Nullable<float>>{6f,6f,7f},new List<Nullable<float>>{4f,1f,1f},new List<Nullable<float>>{7f,2f,7f},new List<Nullable<float>>{7f,7f,0},new List<Nullable<float>>{8f,8f,9f},new List<Nullable<float>>{9f,4f,1f},new List<Nullable<float>>{8f,3f,4f},new List<Nullable<float>>{9f,8f,9f},new List<Nullable<float>>{3f,5f,3f},new List<Nullable<float>>{0,2f,4f},new List<Nullable<float>>{6f,0,2f},new List<Nullable<float>>{2f,1f,3f},new List<Nullable<float>>{5f,8f,9f},new List<Nullable<float>>{2f,1f,1f},new List<Nullable<float>>{9f,7f,6f},new List<Nullable<float>>{3f,0,2f},new List<Nullable<float>>{9f,9f,0},new List<Nullable<float>>{3f,4f,8f},new List<Nullable<float>>{2f,6f,1f},new List<Nullable<float>>{8f,9f,2f},new List<Nullable<float>>{7f,6f,5f},new List<Nullable<float>>{6f,3f,1f},new List<Nullable<float>>{9f,3f,1f},new List<Nullable<float>>{8f,9f,3f},new List<Nullable<float>>{9f,1f,0},new List<Nullable<float>>{3f,8f,7f},new List<Nullable<float>>{8f,0,0},new List<Nullable<float>>{4f,9f,7f},new List<Nullable<float>>{8f,6f,2f},new List<Nullable<float>>{4f,3f,0},new List<Nullable<float>>{2f,3f,5f},new List<Nullable<float>>{9f,1f,4f},new List<Nullable<float>>{1f,1f,4f},new List<Nullable<float>>{6f,0,2f},new List<Nullable<float>>{6f,1f,6f},new List<Nullable<float>>{3f,8f,8f},new List<Nullable<float>>{8f,8f,7f},new List<Nullable<float>>{5f,5f,0},new List<Nullable<float>>{3f,9f,6f},new List<Nullable<float>>{5f,4f,3f},new List<Nullable<float>>{6f,8f,3f},new List<Nullable<float>>{0,1f,5f},new List<Nullable<float>>{6f,7f,3f},new List<Nullable<float>>{8f,3f,2f},new List<Nullable<float>>{3f,8f,3f},new List<Nullable<float>>{2f,1f,6f},new List<Nullable<float>>{4f,6f,7f},new List<Nullable<float>>{8f,9f,9f},new List<Nullable<float>>{5f,4f,2f},new List<Nullable<float>>{6f,1f,3f},new List<Nullable<float>>{6f,9f,5f},new List<Nullable<float>>{4f,8f,2f},new List<Nullable<float>>{9f,7f,4f},new List<Nullable<float>>{5f,4f,2f},new List<Nullable<float>>{9f,6f,1f},new List<Nullable<float>>{2f,7f,3f},new List<Nullable<float>>{4f,5f,4f},new List<Nullable<float>>{6f,8f,1f},new List<Nullable<float>>{3f,4f,0},new List<Nullable<float>>{2f,2f,6f},new List<Nullable<float>>{5f,1f,2f},new List<Nullable<float>>{9f,9f,7f},new List<Nullable<float>>{6f,9f,9f},new List<Nullable<float>>{8f,4f,3f},new List<Nullable<float>>{4f,1f,7f},new List<Nullable<float>>{6f,2f,5f},new List<Nullable<float>>{0,4f,9f},new List<Nullable<float>>{3f,5f,9f},new List<Nullable<float>>{6f,9f,1f},new List<Nullable<float>>{1f,9f,2f}};
	
		}
	
		[InputText("name")]
		[JsonProperty("name")]
		public string Name{ get; set; }	[JsonProperty("colorByPoint")]
		public Highchart3dScatterDraggableSeriesItemColorByPoint ColorByPoint{ get; set; }	[JsonProperty("accessibility")]
		public Highchart3dScatterDraggableSeriesItemAccessibility Accessibility{ get; set; }	[JsonProperty("data")]
		public List<List<Nullable<float>>> Data{ get; set; }
	}
	
	
	public class Highchart3dScatterDraggable: Highchart {
	
		public Highchart3dScatterDraggable():base(){
			Chart=new Highchart3dScatterDraggableChart();
			Title=new Highchart3dScatterDraggableTitle();
			Subtitle=new Highchart3dScatterDraggableSubtitle();
			PlotOptions=new Highchart3dScatterDraggablePlotOptions();
			YAxis=new Highchart3dScatterDraggableYAxis();
			XAxis=new Highchart3dScatterDraggableXAxis();
			ZAxis=new Highchart3dScatterDraggableZAxis();
			Legend=new Highchart3dScatterDraggableLegend();
			Series=new List<Highchart3dScatterDraggableSeriesItem>{new Highchart3dScatterDraggableSeriesItem()};
	
		}
	
		[JsonProperty("chart")]
		public Highchart3dScatterDraggableChart Chart{ get; set; }	[JsonProperty("title")]
		public Highchart3dScatterDraggableTitle Title{ get; set; }	[JsonProperty("subtitle")]
		public Highchart3dScatterDraggableSubtitle Subtitle{ get; set; }	[JsonProperty("plotOptions")]
		public Highchart3dScatterDraggablePlotOptions PlotOptions{ get; set; }	[JsonProperty("yAxis")]
		public Highchart3dScatterDraggableYAxis YAxis{ get; set; }	[JsonProperty("xAxis")]
		public Highchart3dScatterDraggableXAxis XAxis{ get; set; }	[JsonProperty("zAxis")]
		public Highchart3dScatterDraggableZAxis ZAxis{ get; set; }	[JsonProperty("legend")]
		public Highchart3dScatterDraggableLegend Legend{ get; set; }	[JsonProperty("series")]
		public List<Highchart3dScatterDraggableSeriesItem> Series{ get; set; }
	}
	
	
}