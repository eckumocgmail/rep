using System.Collections.Generic;

using Newtonsoft.Json;
using System;



namespace Highcharts.Models.HighchartPolar
{

	public class HighchartPolar : Highchart
	{

		[JsonProperty("chart")]
		public HighchartPolarChart Chart { get; set; }
		[JsonProperty("title")]
		public HighchartPolarTitle Title { get; set; }
		[JsonProperty("subtitle")]
		public HighchartPolarSubtitle Subtitle { get; set; }
		[JsonProperty("pane")]
		public HighchartPolarPane Pane { get; set; }
		[JsonProperty("xAxis")]
		public HighchartPolarXAxis XAxis { get; set; }
		[JsonProperty("yAxis")]
		public HighchartPolarYAxis YAxis { get; set; }
		[JsonProperty("plotOptions")]
		public HighchartPolarPlotOptions PlotOptions { get; set; }
		[JsonProperty("series")]
		public List<HighchartPolarSeriesItem> Series { get; set; }

		public HighchartPolar() : base()
		{
			Chart = new HighchartPolarChart();
			Title = new HighchartPolarTitle();
			Subtitle = new HighchartPolarSubtitle();
			Pane = new HighchartPolarPane();
			XAxis = new HighchartPolarXAxis();
			YAxis = new HighchartPolarYAxis();
			PlotOptions = new HighchartPolarPlotOptions();
			Series = new List<HighchartPolarSeriesItem> { new HighchartPolarSeriesItem(), new HighchartPolarSeriesItem(), new HighchartPolarSeriesItem() };

		}
	}

	public class HighchartPolarChartPolar: Highchart {
	
		public HighchartPolarChartPolar():base(){
	
		}
	
	
	}
	
	
	public class HighchartPolarChart: Highchart {

		[JsonProperty("polar")]
		public HighchartPolarChartPolar Polar { get; set; }

		 
		public HighchartPolarChart():base(){
			Polar=new HighchartPolarChartPolar();	
		}	
	}
	
	
	/// <summary>
	/// Заголовок
	/// </summary>
	public class HighchartPolarTitle: Highchart {

		[InputText("text")]
		[JsonProperty("text")]
		public string Text { get; set; }
		public HighchartPolarTitle():base(){
			Text="Highcharts Polar Chart";	
		}
	}
	


	/// <summary>
	/// Подзаголовок
	/// </summary>
	
	public class HighchartPolarSubtitle: Highchart {

		[InputText("text")]
		[JsonProperty("text")]
		public string Text { get; set; }
		public HighchartPolarSubtitle():base(){
			Text="Also known as Radar Chart";
	
		}	
	}
	
	
	public class HighchartPolarPane: Highchart {

		[JsonProperty("startAngle")]
		public object StartAngle { get; set; }

		[InputNumber("endAngle")]
		[JsonProperty("endAngle")]
		public float EndAngle { get; set; }


		public HighchartPolarPane():base(){
			StartAngle=null;
			EndAngle=360f;
	
		}

	}
	
	
	public class HighchartPolarXAxisLabels: Highchart {
	
		public HighchartPolarXAxisLabels():base(){
			Format="{value}°";
	
		}
	
		[InputText("format")]
		[JsonProperty("format")]
		public string Format{ get; set; }
	}
	
	
	public class HighchartPolarXAxis: Highchart {


		[InputNumber("tickInterval")]
		[JsonProperty("tickInterval")]
		public float TickInterval { get; set; }

		[JsonProperty("min")]
		public object Min { get; set; }

		[InputNumber("max")]
		[JsonProperty("max")]
		public float Max { get; set; }


		[JsonProperty("labels")]
		public HighchartPolarXAxisLabels Labels { get; set; }


		public HighchartPolarXAxis():base(){
			TickInterval=45f;
			Min=null;
			Max=360f;
			Labels=new HighchartPolarXAxisLabels();
	
		}
	

	}
	
	
	public class HighchartPolarYAxis: Highchart {


		[JsonProperty("min")]
		public object Min { get; set; }


		public HighchartPolarYAxis():base(){
			Min=null;
	
		}	
	}
	
	
	public class HighchartPolarPlotOptionsSeries: Highchart {
	
	
		[JsonProperty("pointStart")]
		public object PointStart{ get; set; }	

		[InputNumber("pointInterval")]
		[JsonProperty("pointInterval")]
		public float PointInterval{ get; set; }

		public HighchartPolarPlotOptionsSeries() : base()
		{
			PointStart = null;
			PointInterval = 45f;

		}
	}
	
	
	public class HighchartPolarPlotOptionsColumn: Highchart {
	
		
	
		[JsonProperty("pointPadding")]
		public object PointPadding{ get; set; }	

		[JsonProperty("groupPadding")]
		public object GroupPadding{ get; set; }


		public HighchartPolarPlotOptionsColumn() : base()
		{
			PointPadding = null;
			GroupPadding = null;
		}

	}
	
	
	public class HighchartPolarPlotOptions: Highchart {
	
		public HighchartPolarPlotOptions():base(){
			Series=new HighchartPolarPlotOptionsSeries();
			Column=new HighchartPolarPlotOptionsColumn();
	
		}
	
		[JsonProperty("series")]
		public HighchartPolarPlotOptionsSeries Series{ get; set; }	[JsonProperty("column")]
		public HighchartPolarPlotOptionsColumn Column{ get; set; }
	}
	
	
	public class HighchartPolarSeriesItem: Highchart {
	
		public HighchartPolarSeriesItem():base(){
			Type="column";
			Name="Column";
			Data=new List<Nullable<float>>{8f,7f,6f,5f,4f,3f,2f,1f};
			PointPlacement="between";
	
		}
	
		[InputText("type")]
		[JsonProperty("type")]
		public string Type{ get; set; }	[InputText("name")]
		[JsonProperty("name")]
		public string Name{ get; set; }	[JsonProperty("data")]
		public List<Nullable<float>> Data{ get; set; }	[InputText("pointPlacement")]
		[JsonProperty("pointPlacement")]
		public string PointPlacement{ get; set; }
	}
	
	
	
	
	
}