using System.Collections.Generic;

using Newtonsoft.Json;
using System;

namespace Highcharts.Models.Highchart3dColumnInteractive
{
	public class Highchart3dColumnInteractiveChartOptions3dEnabled: Highchart {
	
		public Highchart3dColumnInteractiveChartOptions3dEnabled():base(){
	
		}
	
	
	}
	
	
	public class Highchart3dColumnInteractiveChartOptions3d: Highchart {
	
		public Highchart3dColumnInteractiveChartOptions3d():base(){
			Enabled=new Highchart3dColumnInteractiveChartOptions3dEnabled();
			Alpha=15f;
			Beta=15f;
			Depth=50f;
			ViewDistance=25f;
	
		}
	
		[JsonProperty("enabled")]
		public Highchart3dColumnInteractiveChartOptions3dEnabled Enabled{ get; set; }	[InputNumber("alpha")]
		[JsonProperty("alpha")]
		public float Alpha{ get; set; }	[InputNumber("beta")]
		[JsonProperty("beta")]
		public float Beta{ get; set; }	[InputNumber("depth")]
		[JsonProperty("depth")]
		public float Depth{ get; set; }	[InputNumber("viewDistance")]
		[JsonProperty("viewDistance")]
		public float ViewDistance{ get; set; }
	}
	
	
	public class Highchart3dColumnInteractiveChart: Highchart {
	
		public Highchart3dColumnInteractiveChart():base(){
			RenderTo="container";
			Type="column";
			Options3d=new Highchart3dColumnInteractiveChartOptions3d();
	
		}
	
		[InputText("renderTo")]
		[JsonProperty("renderTo")]
		public string RenderTo{ get; set; }	[InputText("type")]
		[JsonProperty("type")]
		public string Type{ get; set; }	[JsonProperty("options3d")]
		public Highchart3dColumnInteractiveChartOptions3d Options3d{ get; set; }
	}
	
	
	public class Highchart3dColumnInteractiveTitle: Highchart {
	
		public Highchart3dColumnInteractiveTitle():base(){
			Text="Chart rotation demo";
	
		}
	
		[InputText("text")]
		[JsonProperty("text")]
		public string Text{ get; set; }
	}
	
	
	public class Highchart3dColumnInteractiveSubtitle: Highchart {
	
		public Highchart3dColumnInteractiveSubtitle():base(){
			Text="Test options by dragging the sliders below";
	
		}
	
		[InputText("text")]
		[JsonProperty("text")]
		public string Text{ get; set; }
	}
	
	
	public class Highchart3dColumnInteractivePlotOptionsColumn: Highchart {
	
		public Highchart3dColumnInteractivePlotOptionsColumn():base(){
			Depth=25f;
	
		}
	
		[InputNumber("depth")]
		[JsonProperty("depth")]
		public float Depth{ get; set; }
	}
	
	
	public class Highchart3dColumnInteractivePlotOptions: Highchart {
	
		public Highchart3dColumnInteractivePlotOptions():base(){
			Column=new Highchart3dColumnInteractivePlotOptionsColumn();
	
		}
	
		[JsonProperty("column")]
		public Highchart3dColumnInteractivePlotOptionsColumn Column{ get; set; }
	}
	
	
	public class Highchart3dColumnInteractiveSeriesItem: Highchart {
	
		public Highchart3dColumnInteractiveSeriesItem():base(){
			Data=new List<Nullable<float>>{29.9f,71.5f,106.4f,129.2f,144f,176f,135.6f,148.5f,216.4f,194.1f,95.6f,54.4f};
	
		}
	
		[JsonProperty("data")]
		public List<Nullable<float>> Data{ get; set; }
	}
	
	
	public class Highchart3dColumnInteractive: Highchart {
	
		public Highchart3dColumnInteractive():base(){
			Chart=new Highchart3dColumnInteractiveChart();
			Title=new Highchart3dColumnInteractiveTitle();
			Subtitle=new Highchart3dColumnInteractiveSubtitle();
			PlotOptions=new Highchart3dColumnInteractivePlotOptions();
			Series=new List<Highchart3dColumnInteractiveSeriesItem>{new Highchart3dColumnInteractiveSeriesItem()};
	
		}
	
		[JsonProperty("chart")]
		public Highchart3dColumnInteractiveChart Chart{ get; set; }	[JsonProperty("title")]
		public Highchart3dColumnInteractiveTitle Title{ get; set; }	[JsonProperty("subtitle")]
		public Highchart3dColumnInteractiveSubtitle Subtitle{ get; set; }	[JsonProperty("plotOptions")]
		public Highchart3dColumnInteractivePlotOptions PlotOptions{ get; set; }	[JsonProperty("series")]
		public List<Highchart3dColumnInteractiveSeriesItem> Series{ get; set; }
	}
	
	
}