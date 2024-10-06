using System.Collections.Generic;

using Newtonsoft.Json;
using System;

namespace Highcharts.Models.HighchartAnnotations
{
	public class HighchartAnnotationsChartPanning: Highchart {
	
		public HighchartAnnotationsChartPanning():base(){
	
		}
	
	
	}
	
	
	public class HighchartAnnotationsChartScrollablePlotArea: Highchart {
	
		public HighchartAnnotationsChartScrollablePlotArea():base(){
			MinWidth=600f;
	
		}
	
		[InputNumber("minWidth")]
		[JsonProperty("minWidth")]
		public float MinWidth{ get; set; }
	}
	
	
	public class HighchartAnnotationsChart: Highchart {
	
		public HighchartAnnotationsChart():base(){
			Type="area";
			ZoomType="x";
			Panning=new HighchartAnnotationsChartPanning();
			PanKey="shift";
			ScrollablePlotArea=new HighchartAnnotationsChartScrollablePlotArea();
	
		}
	
		[InputText("type")]
		[JsonProperty("type")]
		public string Type{ get; set; }	[InputText("zoomType")]
		[JsonProperty("zoomType")]
		public string ZoomType{ get; set; }	[JsonProperty("panning")]
		public HighchartAnnotationsChartPanning Panning{ get; set; }	[InputText("panKey")]
		[JsonProperty("panKey")]
		public string PanKey{ get; set; }	[JsonProperty("scrollablePlotArea")]
		public HighchartAnnotationsChartScrollablePlotArea ScrollablePlotArea{ get; set; }
	}
	
	
	public class HighchartAnnotationsCaption: Highchart {
	
		public HighchartAnnotationsCaption():base(){
			Text="This chart uses the Highcharts Annotations feature to place labels at various points of interest. The labels are responsive and will be hidden to avoid overlap on small screens.";
	
		}
	
		[InputText("text")]
		[JsonProperty("text")]
		public string Text{ get; set; }
	}
	
	
	public class HighchartAnnotationsTitle: Highchart {
	
		public HighchartAnnotationsTitle():base(){
			Text="2017 Tour de France Stage 8: Dole - Station des Rousses";
	
		}
	
		[InputText("text")]
		[JsonProperty("text")]
		public string Text{ get; set; }
	}
	
	
	public class HighchartAnnotationsAccessibility: Highchart {
	
		public HighchartAnnotationsAccessibility():base(){
			Description="Image description: An annotated line graph illustrates the 8th stage of the 2017 Tour de France cycling race from the start point in Dole to the finish line at Station des Rousses. Altitude is plotted on the Y-axis at increments of 500m and distance is plotted on the X-axis in increments of 25 kilometers. The line graph is interactive, and the user can trace the altitude level at every 100-meter point along the stage. The graph is shaded below the data line to visualize the mountainous altitudes encountered on the 187.5-kilometre stage. The three largest climbs are highlighted at Col de la Joux, Côte de Viry and the final 11.7-kilometer, 6.4% gradient climb to Montée de la Combe de Laisia Les Molunes which peaks at 1200 meters above sea level. The stage passes through the villages of Arbois, Montrond, Bonlieu, Chassal and Saint-Claude along the route.";
	
		}
	
		[InputText("description")]
		[JsonProperty("description")]
		public string Description{ get; set; }
	}
	
	
	public class HighchartAnnotationsCredits: Highchart {
	
		public HighchartAnnotationsCredits():base(){
			Enabled=null;
	
		}
	
		[JsonProperty("enabled")]
		public object Enabled{ get; set; }
	}
	
	
	public class HighchartAnnotationsAnnotationsItemLabelOptions: Highchart {
	
		public HighchartAnnotationsAnnotationsItemLabelOptions():base(){
			BackgroundColor="rgba(255,255,255,0.5)";
			VerticalAlign="top";
			Y=15f;
	
		}
	
		[InputText("backgroundColor")]
		[JsonProperty("backgroundColor")]
		public string BackgroundColor{ get; set; }	[InputText("verticalAlign")]
		[JsonProperty("verticalAlign")]
		public string VerticalAlign{ get; set; }	[InputNumber("y")]
		[JsonProperty("y")]
		public float Y{ get; set; }
	}
	
	
	public class HighchartAnnotationsAnnotationsItemLabelsItemPoint: Highchart {
	
		public HighchartAnnotationsAnnotationsItemLabelsItemPoint():base(){
			XAxis=null;
			YAxis=null;
			X=27.98f;
			Y=255f;
	
		}
	
		[JsonProperty("xAxis")]
		public object XAxis{ get; set; }	[JsonProperty("yAxis")]
		public object YAxis{ get; set; }	[InputNumber("x")]
		[JsonProperty("x")]
		public float X{ get; set; }	[InputNumber("y")]
		[JsonProperty("y")]
		public float Y{ get; set; }
	}
	
	
	public class HighchartAnnotationsAnnotationsItemLabelsItem: Highchart {
	
		public HighchartAnnotationsAnnotationsItemLabelsItem():base(){
			Point=new HighchartAnnotationsAnnotationsItemLabelsItemPoint();
			Text="Arbois";
	
		}
	
		[JsonProperty("point")]
		public HighchartAnnotationsAnnotationsItemLabelsItemPoint Point{ get; set; }	[InputText("text")]
		[JsonProperty("text")]
		public string Text{ get; set; }
	}
	
	
	public class HighchartAnnotationsAnnotationsItem: Highchart {
	
		public HighchartAnnotationsAnnotationsItem():base(){
			LabelOptions=new HighchartAnnotationsAnnotationsItemLabelOptions();
			Labels=new List<HighchartAnnotationsAnnotationsItemLabelsItem>{new HighchartAnnotationsAnnotationsItemLabelsItem(),new HighchartAnnotationsAnnotationsItemLabelsItem(),new HighchartAnnotationsAnnotationsItemLabelsItem(),new HighchartAnnotationsAnnotationsItemLabelsItem(),new HighchartAnnotationsAnnotationsItemLabelsItem(),new HighchartAnnotationsAnnotationsItemLabelsItem()};
	
		}
	
		[JsonProperty("labelOptions")]
		public HighchartAnnotationsAnnotationsItemLabelOptions LabelOptions{ get; set; }	[JsonProperty("labels")]
		public List<HighchartAnnotationsAnnotationsItemLabelsItem> Labels{ get; set; }
	}
	
	
	public class HighchartAnnotationsXAxisLabels: Highchart {
	
		public HighchartAnnotationsXAxisLabels():base(){
			Format="{value} km";
	
		}
	
		[InputText("format")]
		[JsonProperty("format")]
		public string Format{ get; set; }
	}
	
	
	public class HighchartAnnotationsXAxisTitle: Highchart {
	
		public HighchartAnnotationsXAxisTitle():base(){
			Text="Distance";
	
		}
	
		[InputText("text")]
		[JsonProperty("text")]
		public string Text{ get; set; }
	}
	
	
	public class HighchartAnnotationsXAxisAccessibility: Highchart {
	
		public HighchartAnnotationsXAxisAccessibility():base(){
			RangeDescription="Range: 0 to 187.8km.";
	
		}
	
		[InputText("rangeDescription")]
		[JsonProperty("rangeDescription")]
		public string RangeDescription{ get; set; }
	}
	
	
	public class HighchartAnnotationsXAxis: Highchart {
	
		public HighchartAnnotationsXAxis():base(){
			Labels=new HighchartAnnotationsXAxisLabels();
			MinRange=5f;
			Title=new HighchartAnnotationsXAxisTitle();
			Accessibility=new HighchartAnnotationsXAxisAccessibility();
	
		}
	
		[JsonProperty("labels")]
		public HighchartAnnotationsXAxisLabels Labels{ get; set; }	[InputNumber("minRange")]
		[JsonProperty("minRange")]
		public float MinRange{ get; set; }	[JsonProperty("title")]
		public HighchartAnnotationsXAxisTitle Title{ get; set; }	[JsonProperty("accessibility")]
		public HighchartAnnotationsXAxisAccessibility Accessibility{ get; set; }
	}
	
	
	public class HighchartAnnotationsYAxisStartOnTick: Highchart {
	
		public HighchartAnnotationsYAxisStartOnTick():base(){
	
		}
	
	
	}
	
	
	public class HighchartAnnotationsYAxisTitle: Highchart {
	
		public HighchartAnnotationsYAxisTitle():base(){
			Text=null;
	
		}
	
		[JsonProperty("text")]
		public object Text{ get; set; }
	}
	
	
	public class HighchartAnnotationsYAxisLabels: Highchart {
	
		public HighchartAnnotationsYAxisLabels():base(){
			Format="{value} m";
	
		}
	
		[InputText("format")]
		[JsonProperty("format")]
		public string Format{ get; set; }
	}
	
	
	public class HighchartAnnotationsYAxis: Highchart {
	
		public HighchartAnnotationsYAxis():base(){
			StartOnTick=new HighchartAnnotationsYAxisStartOnTick();
			EndOnTick=null;
			MaxPadding=0.35f;
			Title=new HighchartAnnotationsYAxisTitle();
			Labels=new HighchartAnnotationsYAxisLabels();
	
		}
	
		[JsonProperty("startOnTick")]
		public HighchartAnnotationsYAxisStartOnTick StartOnTick{ get; set; }	[JsonProperty("endOnTick")]
		public object EndOnTick{ get; set; }	[InputNumber("maxPadding")]
		[JsonProperty("maxPadding")]
		public float MaxPadding{ get; set; }	[JsonProperty("title")]
		public HighchartAnnotationsYAxisTitle Title{ get; set; }	[JsonProperty("labels")]
		public HighchartAnnotationsYAxisLabels Labels{ get; set; }
	}
	
	
	public class HighchartAnnotationsTooltipShared: Highchart {
	
		public HighchartAnnotationsTooltipShared():base(){
	
		}
	
	
	}
	
	
	public class HighchartAnnotationsTooltip: Highchart {
	
		public HighchartAnnotationsTooltip():base(){
			HeaderFormat="Distance: {point.x:.1f} km<br>";
			PointFormat="{point.y} m a. s. l.";
			Shared=new HighchartAnnotationsTooltipShared();
	
		}
	
		[InputText("headerFormat")]
		[JsonProperty("headerFormat")]
		public string HeaderFormat{ get; set; }	[InputText("pointFormat")]
		[JsonProperty("pointFormat")]
		public string PointFormat{ get; set; }	[JsonProperty("shared")]
		public HighchartAnnotationsTooltipShared Shared{ get; set; }
	}
	
	
	public class HighchartAnnotationsLegend: Highchart {
	
		public HighchartAnnotationsLegend():base(){
			Enabled=null;
	
		}
	
		[JsonProperty("enabled")]
		public object Enabled{ get; set; }
	}
	
	
	public class HighchartAnnotationsSeriesItemAccessibilityKeyboardNavigation: Highchart {
	
		public HighchartAnnotationsSeriesItemAccessibilityKeyboardNavigation():base(){
			Enabled=null;
	
		}
	
		[JsonProperty("enabled")]
		public object Enabled{ get; set; }
	}
	
	
	public class HighchartAnnotationsSeriesItemAccessibility: Highchart {
	
		public HighchartAnnotationsSeriesItemAccessibility():base(){
			KeyboardNavigation=new HighchartAnnotationsSeriesItemAccessibilityKeyboardNavigation();
	
		}
	
		[JsonProperty("keyboardNavigation")]
		public HighchartAnnotationsSeriesItemAccessibilityKeyboardNavigation KeyboardNavigation{ get; set; }
	}
	
	
	public class HighchartAnnotationsSeriesItemMarker: Highchart {
	
		public HighchartAnnotationsSeriesItemMarker():base(){
			Enabled=null;
	
		}
	
		[JsonProperty("enabled")]
		public object Enabled{ get; set; }
	}
	
	
	public class HighchartAnnotationsSeriesItem: Highchart {
	
		public HighchartAnnotationsSeriesItem():base(){
			Accessibility=new HighchartAnnotationsSeriesItemAccessibility();
			Data=new List<List<object>>{new List<object>{null,{225}},new List<object>{{0.1},{226}},new List<object>{{0.2},{228}},new List<object>{{0.3},{228}},new List<object>{{0.4},{229}},new List<object>{{0.5},{229}},new List<object>{{0.6},{230}},new List<object>{{0.7},{234}},new List<object>{{0.8},{235}},new List<object>{{0.9},{236}},new List<object>{{1},{235}},new List<object>{{1.1},{232}},new List<object>{{1.2},{228}},new List<object>{{1.3},{223}},new List<object>{{1.4},{218}},new List<object>{{1.5},{214}},new List<object>{{1.6},{207}},new List<object>{{1.7},{202}},new List<object>{{1.8},{198}},new List<object>{{1.9},{196}},new List<object>{{2},{197}},new List<object>{{2.1},{200}},new List<object>{{2.2},{205}},new List<object>{{2.3},{206}},new List<object>{{2.4},{210}},new List<object>{{2.5},{210}},new List<object>{{2.6},{210}},new List<object>{{2.7},{209}},new List<object>{{2.8},{208}},new List<object>{{2.9},{207}},new List<object>{{3},{209}},new List<object>{{3.1},{208}},new List<object>{{3.2},{207}},new List<object>{{3.3},{207}},new List<object>{{3.4},{206}},new List<object>{{3.5},{206}},new List<object>{{3.6},{205}},new List<object>{{3.7},{201}},new List<object>{{3.8},{195}},new List<object>{{3.9},{191}},new List<object>{{4},{191}},new List<object>{{4.1},{195}},new List<object>{{4.2},{199}},new List<object>{{4.3},{203}},new List<object>{{4.4},{208}},new List<object>{{4.5},{208}},new List<object>{{4.6},{208}},new List<object>{{4.7},{208}},new List<object>{{4.8},{209}},new List<object>{{4.9},{207}},new List<object>{{5},{207}},new List<object>{{5.1},{208}},new List<object>{{5.2},{209}},new List<object>{{5.3},{208}},new List<object>{{5.4},{210}},new List<object>{{5.5},{209}},new List<object>{{5.6},{209}},new List<object>{{5.7},{206}},new List<object>{{5.8},{207}},new List<object>{{5.9},{209}},new List<object>{{6},{211}},new List<object>{{6.1},{206}},new List<object>{{6.2},{201}},new List<object>{{6.3},{199}},new List<object>{{6.4},{200}},new List<object>{{6.5},{202}},new List<object>{{6.6},{203}},new List<object>{{6.7},{202}},new List<object>{{6.8},{204}},new List<object>{{6.9},{206}},new List<object>{{7},{208}},new List<object>{{7.1},{205}},new List<object>{{7.2},{202}},new List<object>{{7.3},{198}},new List<object>{{7.4},{198}},new List<object>{{7.5},{198}},new List<object>{{7.6},{198}},new List<object>{{7.7},{198}},new List<object>{{7.8},{199}},new List<object>{{7.9},{197}},new List<object>{{8},{194}},new List<object>{{8.1},{194}},new List<object>{{8.2},{195}},new List<object>{{8.3},{195}},new List<object>{{8.4},{196}},new List<object>{{8.5},{192}},new List<object>{{8.6},{200}},new List<object>{{8.7},{197}},new List<object>{{8.8},{194}},new List<object>{{8.9},{194}},new List<object>{{9},{193}},new List<object>{{9.1},{192}},new List<object>{{9.2},{192}},new List<object>{{9.3},{193}},new List<object>{{9.4},{191}},new List<object>{{9.5},{191}},new List<object>{{9.6},{193}},new List<object>{{9.7},{193}},new List<object>{{9.8},{194}},new List<object>{{9.9},{192}},new List<object>{{10},{192}},new List<object>{{10.1},{192}},new List<object>{{10.2},{192}},new List<object>{{10.3},{192}},new List<object>{{10.4},{193}},new List<object>{{10.5},{193}},new List<object>{{10.6},{193}},new List<object>{{10.7},{193}},new List<object>{{10.8},{194}},new List<object>{{10.9},{194}},new List<object>{{11},{194}},new List<object>{{11.1},{195}},new List<object>{{11.2},{194}},new List<object>{{11.3},{194}},new List<object>{{11.4},{194}},new List<object>{{11.5},{194}},new List<object>{{11.6},{193}},new List<object>{{11.7},{194}},new List<object>{{11.8},{194}},new List<object>{{11.9},{194}},new List<object>{{12},{195}},new List<object>{{12.1},{195}},new List<object>{{12.2},{195}},new List<object>{{12.3},{197}},new List<object>{{12.4},{197}},new List<object>{{12.5},{197}},new List<object>{{12.6},{198}},new List<object>{{12.7},{201}},new List<object>{{12.8},{202}},new List<object>{{12.9},{203}},new List<object>{{13},{205}},new List<object>{{13.1},{205}},new List<object>{{13.2},{204}},new List<object>{{13.3},{210}},new List<object>{{13.4},{213}},new List<object>{{13.5},{212}},new List<object>{{13.6},{213}},new List<object>{{13.7},{213}},new List<object>{{13.8},{213}},new List<object>{{13.9},{214}},new List<object>{{14},{214}},new List<object>{{14.1},{212}},new List<object>{{14.2},{209}},new List<object>{{14.3},{207}},new List<object>{{14.4},{207}},new List<object>{{14.5},{208}},new List<object>{{14.6},{211}},new List<object>{{14.7},{215}},new List<object>{{14.8},{219}},new List<object>{{14.9},{219}},new List<object>{{15},{221}},new List<object>{{15.1},{224}},new List<object>{{15.2},{224}},new List<object>{{15.3},{225}},new List<object>{{15.4},{225}},new List<object>{{15.5},{225}},new List<object>{{15.6},{225}},new List<object>{{15.7},{225}},new List<object>{{15.8},{225}},new List<object>{{15.9},{226}},new List<object>{{16},{226}},new List<object>{{16.1},{227}},new List<object>{{16.2},{227}},new List<object>{{16.3},{230}},new List<object>{{16.4},{231}},new List<object>{{16.5},{231}},new List<object>{{16.6},{232}},new List<object>{{16.7},{230}},new List<object>{{16.8},{229}},new List<object>{{16.9},{228}},new List<object>{{17},{226}},new List<object>{{17.1},{226}},new List<object>{{17.2},{224}},new List<object>{{17.3},{223}},new List<object>{{17.4},{224}},new List<object>{{17.5},{223}},new List<object>{{17.6},{221}},new List<object>{{17.7},{220}},new List<object>{{17.8},{218}},new List<object>{{17.9},{217}},new List<object>{{18},{216}},new List<object>{{18.1},{216}},new List<object>{{18.2},{215}},new List<object>{{18.3},{214}},new List<object>{{18.4},{213}},new List<object>{{18.5},{212}},new List<object>{{18.6},{212}},new List<object>{{18.7},{213}},new List<object>{{18.8},{214}},new List<object>{{18.9},{215}},new List<object>{{19},{215}},new List<object>{{19.1},{216}},new List<object>{{19.2},{217}},new List<object>{{19.3},{216}},new List<object>{{19.4},{216}},new List<object>{{19.5},{217}},new List<object>{{19.6},{219}},new List<object>{{19.7},{218}},new List<object>{{19.8},{218}},new List<object>{{19.9},{220}},new List<object>{{20},{224}},new List<object>{{20.1},{224}},new List<object>{{20.2},{225}},new List<object>{{20.3},{224}},new List<object>{{20.4},{225}},new List<object>{{20.5},{228}},new List<object>{{20.6},{228}},new List<object>{{20.7},{227}},new List<object>{{20.8},{228}},new List<object>{{20.9},{228}},new List<object>{{21},{228}},new List<object>{{21.1},{229}},new List<object>{{21.2},{225}},new List<object>{{21.3},{223}},new List<object>{{21.4},{222}},new List<object>{{21.5},{222}},new List<object>{{21.6},{223}},new List<object>{{21.7},{224}},new List<object>{{21.8},{224}},new List<object>{{21.9},{226}},new List<object>{{22},{228}},new List<object>{{22.1},{233}},new List<object>{{22.2},{237}},new List<object>{{22.3},{237}},new List<object>{{22.4},{238}},new List<object>{{22.5},{236}},new List<object>{{22.6},{232}},new List<object>{{22.7},{232}},new List<object>{{22.8},{228}},new List<object>{{22.9},{227}},new List<object>{{23},{227}},new List<object>{{23.1},{227}},new List<object>{{23.2},{227}},new List<object>{{23.3},{226}},new List<object>{{23.4},{226}},new List<object>{{23.5},{226}},new List<object>{{23.6},{226}},new List<object>{{23.7},{226}},new List<object>{{23.8},{226}},new List<object>{{23.9},{226}},new List<object>{{24},{228}},new List<object>{{24.1},{228}},new List<object>{{24.2},{229}},new List<object>{{24.3},{230}},new List<object>{{24.4},{230}},new List<object>{{24.5},{233}},new List<object>{{24.6},{235}},new List<object>{{24.7},{234}},new List<object>{{24.8},{234}},new List<object>{{24.9},{233}},new List<object>{{25},{233}},new List<object>{{25.1},{232}},new List<object>{{25.2},{232}},new List<object>{{25.3},{232}},new List<object>{{25.4},{232}},new List<object>{{25.5},{234}},new List<object>{{25.6},{234}},new List<object>{{25.7},{234}},new List<object>{{25.8},{234}},new List<object>{{25.9},{237}},new List<object>{{26},{238}},new List<object>{{26.1},{238}},new List<object>{{26.2},{238}},new List<object>{{26.3},{240}},new List<object>{{26.4},{240}},new List<object>{{26.5},{244}},new List<object>{{26.6},{241}},new List<object>{{26.7},{241}},new List<object>{{26.8},{245}},new List<object>{{26.9},{254}},new List<object>{{27},{262}},new List<object>{{27.1},{255}},new List<object>{{27.2},{255}},new List<object>{{27.3},{251}},new List<object>{{27.4},{252}},new List<object>{{27.5},{253}},new List<object>{{27.6},{253}},new List<object>{{27.7},{251}},new List<object>{{27.8},{251}},new List<object>{{27.9},{254}},new List<object>{{28},{255}},new List<object>{{28.1},{257}},new List<object>{{28.2},{259}},new List<object>{{28.3},{259}},new List<object>{{28.4},{259}},new List<object>{{28.5},{260}},new List<object>{{28.6},{258}},new List<object>{{28.7},{258}},new List<object>{{28.8},{262}},new List<object>{{28.9},{260}},new List<object>{{29},{262}},new List<object>{{29.1},{266}},new List<object>{{29.2},{268}},new List<object>{{29.3},{270}},new List<object>{{29.4},{278}},new List<object>{{29.5},{276}},new List<object>{{29.6},{278}},new List<object>{{29.7},{282}},new List<object>{{29.8},{281}},new List<object>{{29.9},{284}},new List<object>{{30},{287}},new List<object>{{30.1},{292}},new List<object>{{30.2},{303}},new List<object>{{30.3},{309}},new List<object>{{30.4},{317}},new List<object>{{30.5},{324}},new List<object>{{30.6},{334}},new List<object>{{30.7},{334}},new List<object>{{30.8},{319}},new List<object>{{30.9},{321}},new List<object>{{31},{333}},new List<object>{{31.1},{337}},new List<object>{{31.2},{345}},new List<object>{{31.3},{352}},new List<object>{{31.4},{386}},new List<object>{{31.5},{398}},new List<object>{{31.6},{406}},new List<object>{{31.7},{416}},new List<object>{{31.8},{423}},new List<object>{{31.9},{425}},new List<object>{{32},{425}},new List<object>{{32.1},{424}},new List<object>{{32.2},{423}},new List<object>{{32.3},{421}},new List<object>{{32.4},{421}},new List<object>{{32.5},{422}},new List<object>{{32.6},{421}},new List<object>{{32.7},{421}},new List<object>{{32.8},{421}},new List<object>{{32.9},{421}},new List<object>{{33},{421}},new List<object>{{33.1},{423}},new List<object>{{33.2},{435}},new List<object>{{33.3},{450}},new List<object>{{33.4},{451}},new List<object>{{33.5},{452}},new List<object>{{33.6},{452}},new List<object>{{33.7},{452}},new List<object>{{33.8},{452}},new List<object>{{33.9},{455}},new List<object>{{34},{459}},new List<object>{{34.1},{465}},new List<object>{{34.2},{469}},new List<object>{{34.3},{473}},new List<object>{{34.4},{476}},new List<object>{{34.5},{480}},new List<object>{{34.6},{483}},new List<object>{{34.7},{487}},new List<object>{{34.8},{490}},new List<object>{{34.9},{494}},new List<object>{{35},{497}},new List<object>{{35.1},{502}},new List<object>{{35.2},{504}},new List<object>{{35.3},{507}},new List<object>{{35.4},{511}},new List<object>{{35.5},{514}},new List<object>{{35.6},{518}},new List<object>{{35.7},{521}},new List<object>{{35.8},{524}},new List<object>{{35.9},{527}},new List<object>{{36},{528}},new List<object>{{36.1},{525}},new List<object>{{36.2},{526}},new List<object>{{36.3},{528}},new List<object>{{36.4},{528}},new List<object>{{36.5},{528}},new List<object>{{36.6},{529}},new List<object>{{36.7},{528}},new List<object>{{36.8},{526}},new List<object>{{36.9},{525}},new List<object>{{37},{524}},new List<object>{{37.1},{524}},new List<object>{{37.2},{524}},new List<object>{{37.3},{525}},new List<object>{{37.4},{528}},new List<object>{{37.5},{528}},new List<object>{{37.6},{532}},new List<object>{{37.7},{534}},new List<object>{{37.8},{538}},new List<object>{{37.9},{540}},new List<object>{{38},{544}},new List<object>{{38.1},{546}},new List<object>{{38.2},{554}},new List<object>{{38.3},{555}},new List<object>{{38.4},{559}},new List<object>{{38.5},{566}},new List<object>{{38.6},{568}},new List<object>{{38.7},{571}},new List<object>{{38.8},{571}},new List<object>{{38.9},{570}},new List<object>{{39},{569}},new List<object>{{39.1},{567}},new List<object>{{39.2},{567}},new List<object>{{39.3},{566}},new List<object>{{39.4},{566}},new List<object>{{39.5},{566}},new List<object>{{39.6},{566}},new List<object>{{39.7},{566}},new List<object>{{39.8},{566}},new List<object>{{39.9},{565}},new List<object>{{40},{563}},new List<object>{{40.1},{562}},new List<object>{{40.2},{563}},new List<object>{{40.3},{563}},new List<object>{{40.4},{562}},new List<object>{{40.5},{562}},new List<object>{{40.6},{562}},new List<object>{{40.7},{561}},new List<object>{{40.8},{563}},new List<object>{{40.9},{561}},new List<object>{{41},{559}},new List<object>{{41.1},{559}},new List<object>{{41.2},{559}},new List<object>{{41.3},{558}},new List<object>{{41.4},{559}},new List<object>{{41.5},{560}},new List<object>{{41.6},{560}},new List<object>{{41.7},{560}},new List<object>{{41.8},{559}},new List<object>{{41.9},{557}},new List<object>{{42},{556}},new List<object>{{42.1},{555}},new List<object>{{42.2},{555}},new List<object>{{42.3},{556}},new List<object>{{42.4},{557}},new List<object>{{42.5},{557}},new List<object>{{42.6},{557}},new List<object>{{42.7},{557}},new List<object>{{42.8},{557}},new List<object>{{42.9},{557}},new List<object>{{43},{557}},new List<object>{{43.1},{557}},new List<object>{{43.2},{558}},new List<object>{{43.3},{559}},new List<object>{{43.4},{560}},new List<object>{{43.5},{563}},new List<object>{{43.6},{566}},new List<object>{{43.7},{570}},new List<object>{{43.8},{572}},new List<object>{{43.9},{575}},new List<object>{{44},{573}},new List<object>{{44.1},{576}},new List<object>{{44.2},{577}},new List<object>{{44.3},{579}},new List<object>{{44.4},{581}},new List<object>{{44.5},{584}},new List<object>{{44.6},{591}},new List<object>{{44.7},{593}},new List<object>{{44.8},{594}},new List<object>{{44.9},{596}},new List<object>{{45},{599}},new List<object>{{45.1},{601}},new List<object>{{45.2},{601}},new List<object>{{45.3},{604}},new List<object>{{45.4},{607}},new List<object>{{45.5},{607}},new List<object>{{45.6},{607}},new List<object>{{45.7},{607}},new List<object>{{45.8},{605}},new List<object>{{45.9},{607}},new List<object>{{46},{609}},new List<object>{{46.1},{612}},new List<object>{{46.2},{613}},new List<object>{{46.3},{614}},new List<object>{{46.4},{614}},new List<object>{{46.5},{614}},new List<object>{{46.6},{616}},new List<object>{{46.7},{616}},new List<object>{{46.8},{615}},new List<object>{{46.9},{615}},new List<object>{{47},{618}},new List<object>{{47.1},{617}},new List<object>{{47.2},{615}},new List<object>{{47.3},{614}},new List<object>{{47.4},{613}},new List<object>{{47.5},{613}},new List<object>{{47.6},{613}},new List<object>{{47.7},{613}},new List<object>{{47.8},{612}},new List<object>{{47.9},{612}},new List<object>{{48},{609}},new List<object>{{48.1},{610}},new List<object>{{48.2},{603}},new List<object>{{48.3},{598}},new List<object>{{48.4},{598}},new List<object>{{48.5},{596}},new List<object>{{48.6},{595}},new List<object>{{48.7},{593}},new List<object>{{48.8},{590}},new List<object>{{48.9},{587}},new List<object>{{49},{583}},new List<object>{{49.1},{580}},new List<object>{{49.2},{576}},new List<object>{{49.3},{569}},new List<object>{{49.4},{568}},new List<object>{{49.5},{566}},new List<object>{{49.6},{560}},new List<object>{{49.7},{559}},new List<object>{{49.8},{558}},new List<object>{{49.9},{562}},new List<object>{{50},{564}},new List<object>{{50.1},{563}},new List<object>{{50.2},{563}},new List<object>{{50.3},{567}},new List<object>{{50.4},{574}},new List<object>{{50.5},{577}},new List<object>{{50.6},{580}},new List<object>{{50.7},{581}},new List<object>{{50.8},{579}},new List<object>{{50.9},{579}},new List<object>{{51},{578}},new List<object>{{51.1},{574}},new List<object>{{51.2},{569}},new List<object>{{51.3},{564}},new List<object>{{51.4},{558}},new List<object>{{51.5},{554}},new List<object>{{51.6},{550}},new List<object>{{51.7},{543}},new List<object>{{51.8},{539}},new List<object>{{51.9},{536}},new List<object>{{52},{532}},new List<object>{{52.1},{530}},new List<object>{{52.2},{529}},new List<object>{{52.3},{528}},new List<object>{{52.4},{528}},new List<object>{{52.5},{528}},new List<object>{{52.6},{528}},new List<object>{{52.7},{527}},new List<object>{{52.8},{527}},new List<object>{{52.9},{528}},new List<object>{{53},{529}},new List<object>{{53.1},{528}},new List<object>{{53.2},{526}},new List<object>{{53.3},{526}},new List<object>{{53.4},{524}},new List<object>{{53.5},{519}},new List<object>{{53.6},{517}},new List<object>{{53.7},{517}},new List<object>{{53.8},{522}},new List<object>{{53.9},{521}},new List<object>{{54},{520}},new List<object>{{54.1},{518}},new List<object>{{54.2},{513}},new List<object>{{54.3},{518}},new List<object>{{54.4},{520}},new List<object>{{54.5},{523}},new List<object>{{54.6},{526}},new List<object>{{54.7},{522}},new List<object>{{54.8},{513}},new List<object>{{54.9},{512}},new List<object>{{55},{513}},new List<object>{{55.1},{513}},new List<object>{{55.2},{518}},new List<object>{{55.3},{522}},new List<object>{{55.4},{526}},new List<object>{{55.5},{526}},new List<object>{{55.6},{525}},new List<object>{{55.7},{522}},new List<object>{{55.8},{520}},new List<object>{{55.9},{519}},new List<object>{{56},{518}},new List<object>{{56.1},{518}},new List<object>{{56.2},{518}},new List<object>{{56.3},{517}},new List<object>{{56.4},{516}},new List<object>{{56.5},{517}},new List<object>{{56.6},{517}},new List<object>{{56.7},{517}},new List<object>{{56.8},{521}},new List<object>{{56.9},{522}},new List<object>{{57},{518}},new List<object>{{57.1},{517}},new List<object>{{57.2},{514}},new List<object>{{57.3},{515}},new List<object>{{57.4},{513}},new List<object>{{57.5},{511}},new List<object>{{57.6},{511}},new List<object>{{57.7},{511}},new List<object>{{57.8},{510}},new List<object>{{57.9},{510}},new List<object>{{58},{509}},new List<object>{{58.1},{509}},new List<object>{{58.2},{509}},new List<object>{{58.3},{509}},new List<object>{{58.4},{509}},new List<object>{{58.5},{509}},new List<object>{{58.6},{509}},new List<object>{{58.7},{509}},new List<object>{{58.8},{509}},new List<object>{{58.9},{510}},new List<object>{{59},{510}},new List<object>{{59.1},{521}},new List<object>{{59.2},{524}},new List<object>{{59.3},{528}},new List<object>{{59.4},{533}},new List<object>{{59.5},{539}},new List<object>{{59.6},{545}},new List<object>{{59.7},{551}},new List<object>{{59.8},{562}},new List<object>{{59.9},{572}},new List<object>{{60},{579}},new List<object>{{60.1},{585}},new List<object>{{60.2},{593}},new List<object>{{60.3},{596}},new List<object>{{60.4},{605}},new List<object>{{60.5},{617}},new List<object>{{60.6},{620}},new List<object>{{60.7},{627}},new List<object>{{60.8},{628}},new List<object>{{60.9},{627}},new List<object>{{61},{627}},new List<object>{{61.1},{628}},new List<object>{{61.2},{629}},new List<object>{{61.3},{632}},new List<object>{{61.4},{634}},new List<object>{{61.5},{638}},new List<object>{{61.6},{637}},new List<object>{{61.7},{638}},new List<object>{{61.8},{639}},new List<object>{{61.9},{640}},new List<object>{{62},{640}},new List<object>{{62.1},{639}},new List<object>{{62.2},{639}},new List<object>{{62.3},{637}},new List<object>{{62.4},{637}},new List<object>{{62.5},{636}},new List<object>{{62.6},{637}},new List<object>{{62.7},{636}},new List<object>{{62.8},{637}},new List<object>{{62.9},{635}},new List<object>{{63},{629}},new List<object>{{63.1},{626}},new List<object>{{63.2},{626}},new List<object>{{63.3},{623}},new List<object>{{63.4},{621}},new List<object>{{63.5},{621}},new List<object>{{63.6},{621}},new List<object>{{63.7},{622}},new List<object>{{63.8},{625}},new List<object>{{63.9},{626}},new List<object>{{64},{629}},new List<object>{{64.1},{631}},new List<object>{{64.2},{633}},new List<object>{{64.3},{631}},new List<object>{{64.4},{632}},new List<object>{{64.5},{634}},new List<object>{{64.6},{637}},new List<object>{{64.7},{637}},new List<object>{{64.8},{637}},new List<object>{{64.9},{637}},new List<object>{{65},{638}},new List<object>{{65.1},{641}},new List<object>{{65.2},{644}},new List<object>{{65.3},{646}},new List<object>{{65.4},{649}},new List<object>{{65.5},{648}},new List<object>{{65.6},{647}},new List<object>{{65.7},{647}},new List<object>{{65.8},{649}},new List<object>{{65.9},{650}},new List<object>{{66},{651}},new List<object>{{66.1},{654}},new List<object>{{66.2},{652}},new List<object>{{66.3},{651}},new List<object>{{66.4},{650}},new List<object>{{66.5},{650}},new List<object>{{66.6},{649}},new List<object>{{66.7},{648}},new List<object>{{66.8},{648}},new List<object>{{66.9},{648}},new List<object>{{67},{649}},new List<object>{{67.1},{650}},new List<object>{{67.2},{647}},new List<object>{{67.3},{642}},new List<object>{{67.4},{641}},new List<object>{{67.5},{638}},new List<object>{{67.6},{636}},new List<object>{{67.7},{635}},new List<object>{{67.8},{633}},new List<object>{{67.9},{636}},new List<object>{{68},{638}},new List<object>{{68.1},{639}},new List<object>{{68.2},{641}},new List<object>{{68.3},{643}},new List<object>{{68.4},{644}},new List<object>{{68.5},{645}},new List<object>{{68.6},{649}},new List<object>{{68.7},{651}},new List<object>{{68.8},{648}},new List<object>{{68.9},{645}},new List<object>{{69},{640}},new List<object>{{69.1},{637}},new List<object>{{69.2},{637}},new List<object>{{69.3},{637}},new List<object>{{69.4},{635}},new List<object>{{69.5},{630}},new List<object>{{69.6},{628}},new List<object>{{69.7},{625}},new List<object>{{69.8},{622}},new List<object>{{69.9},{620}},new List<object>{{70},{618}},new List<object>{{70.1},{613}},new List<object>{{70.2},{612}},new List<object>{{70.3},{608}},new List<object>{{70.4},{603}},new List<object>{{70.5},{599}},new List<object>{{70.6},{597}},new List<object>{{70.7},{591}},new List<object>{{70.8},{590}},new List<object>{{70.9},{587}},new List<object>{{71},{584}},new List<object>{{71.1},{584}},new List<object>{{71.2},{582}},new List<object>{{71.3},{574}},new List<object>{{71.4},{572}},new List<object>{{71.5},{570}},new List<object>{{71.6},{572}},new List<object>{{71.7},{573}},new List<object>{{71.8},{575}},new List<object>{{71.9},{578}},new List<object>{{72},{590}},new List<object>{{72.1},{595}},new List<object>{{72.2},{595}},new List<object>{{72.3},{579}},new List<object>{{72.4},{581}},new List<object>{{72.5},{583}},new List<object>{{72.6},{583}},new List<object>{{72.7},{583}},new List<object>{{72.8},{583}},new List<object>{{72.9},{580}},new List<object>{{73},{579}},new List<object>{{73.1},{584}},new List<object>{{73.2},{587}},new List<object>{{73.3},{594}},new List<object>{{73.4},{597}},new List<object>{{73.5},{597}},new List<object>{{73.6},{596}},new List<object>{{73.7},{593}},new List<object>{{73.8},{591}},new List<object>{{73.9},{596}},new List<object>{{74},{596}},new List<object>{{74.1},{598}},new List<object>{{74.2},{598}},new List<object>{{74.3},{595}},new List<object>{{74.4},{592}},new List<object>{{74.5},{592}},new List<object>{{74.6},{592}},new List<object>{{74.7},{594}},new List<object>{{74.8},{597}},new List<object>{{74.9},{600}},new List<object>{{75},{601}},new List<object>{{75.1},{605}},new List<object>{{75.2},{604}},new List<object>{{75.3},{604}},new List<object>{{75.4},{607}},new List<object>{{75.5},{607}},new List<object>{{75.6},{607}},new List<object>{{75.7},{604}},new List<object>{{75.8},{605}},new List<object>{{75.9},{608}},new List<object>{{76},{616}},new List<object>{{76.1},{618}},new List<object>{{76.2},{629}},new List<object>{{76.3},{633}},new List<object>{{76.4},{634}},new List<object>{{76.5},{637}},new List<object>{{76.6},{644}},new List<object>{{76.7},{650}},new List<object>{{76.8},{653}},new List<object>{{76.9},{653}},new List<object>{{77},{657}},new List<object>{{77.1},{664}},new List<object>{{77.2},{668}},new List<object>{{77.3},{668}},new List<object>{{77.4},{668}},new List<object>{{77.5},{672}},new List<object>{{77.6},{674}},new List<object>{{77.7},{679}},new List<object>{{77.8},{681}},new List<object>{{77.9},{689}},new List<object>{{78},{692}},new List<object>{{78.1},{704}},new List<object>{{78.2},{708}},new List<object>{{78.3},{714}},new List<object>{{78.4},{716}},new List<object>{{78.5},{719}},new List<object>{{78.6},{722}},new List<object>{{78.7},{729}},new List<object>{{78.8},{733}},new List<object>{{78.9},{735}},new List<object>{{79},{736}},new List<object>{{79.1},{737}},new List<object>{{79.2},{737}},new List<object>{{79.3},{737}},new List<object>{{79.4},{737}},new List<object>{{79.5},{736}},new List<object>{{79.6},{736}},new List<object>{{79.7},{736}},new List<object>{{79.8},{737}},new List<object>{{79.9},{737}},new List<object>{{80},{737}},new List<object>{{80.1},{737}},new List<object>{{80.2},{738}},new List<object>{{80.3},{739}},new List<object>{{80.4},{739}},new List<object>{{80.5},{739}},new List<object>{{80.6},{741}},new List<object>{{80.7},{741}},new List<object>{{80.8},{741}},new List<object>{{80.9},{741}},new List<object>{{81},{743}},new List<object>{{81.1},{744}},new List<object>{{81.2},{744}},new List<object>{{81.3},{744}},new List<object>{{81.4},{744}},new List<object>{{81.5},{746}},new List<object>{{81.6},{748}},new List<object>{{81.7},{757}},new List<object>{{81.8},{753}},new List<object>{{81.9},{752}},new List<object>{{82},{751}},new List<object>{{82.1},{748}},new List<object>{{82.2},{746}},new List<object>{{82.3},{756}},new List<object>{{82.4},{755}},new List<object>{{82.5},{748}},new List<object>{{82.6},{745}},new List<object>{{82.7},{749}},new List<object>{{82.8},{752}},new List<object>{{82.9},{753}},new List<object>{{83},{753}},new List<object>{{83.1},{755}},new List<object>{{83.2},{764}},new List<object>{{83.3},{766}},new List<object>{{83.4},{771}},new List<object>{{83.5},{774}},new List<object>{{83.6},{775}},new List<object>{{83.7},{777}},new List<object>{{83.8},{778}},new List<object>{{83.9},{778}},new List<object>{{84},{780}},new List<object>{{84.1},{780}},new List<object>{{84.2},{782}},new List<object>{{84.3},{783}},new List<object>{{84.4},{786}},new List<object>{{84.5},{791}},new List<object>{{84.6},{792}},new List<object>{{84.7},{787}},new List<object>{{84.8},{782}},new List<object>{{84.9},{780}},new List<object>{{85},{777}},new List<object>{{85.1},{777}},new List<object>{{85.2},{777}},new List<object>{{85.3},{777}},new List<object>{{85.4},{770}},new List<object>{{85.5},{768}},new List<object>{{85.6},{764}},new List<object>{{85.7},{763}},new List<object>{{85.8},{758}},new List<object>{{85.9},{757}},new List<object>{{86},{760}},new List<object>{{86.1},{759}},new List<object>{{86.2},{756}},new List<object>{{86.3},{751}},new List<object>{{86.4},{748}},new List<object>{{86.5},{748}},new List<object>{{86.6},{747}},new List<object>{{86.7},{746}},new List<object>{{86.8},{745}},new List<object>{{86.9},{746}},new List<object>{{87},{746}},new List<object>{{87.1},{747}},new List<object>{{87.2},{742}},new List<object>{{87.3},{738}},new List<object>{{87.4},{733}},new List<object>{{87.5},{730}},new List<object>{{87.6},{730}},new List<object>{{87.7},{727}},new List<object>{{87.8},{725}},new List<object>{{87.9},{722}},new List<object>{{88},{719}},new List<object>{{88.1},{718}},new List<object>{{88.2},{716}},new List<object>{{88.3},{712}},new List<object>{{88.4},{710}},new List<object>{{88.5},{708}},new List<object>{{88.6},{707}},new List<object>{{88.7},{705}},new List<object>{{88.8},{699}},new List<object>{{88.9},{695}},new List<object>{{89},{686}},new List<object>{{89.1},{674}},new List<object>{{89.2},{671}},new List<object>{{89.3},{670}},new List<object>{{89.4},{670}},new List<object>{{89.5},{669}},new List<object>{{89.6},{668}},new List<object>{{89.7},{669}},new List<object>{{89.8},{670}},new List<object>{{89.9},{672}},new List<object>{{90},{679}},new List<object>{{90.1},{681}},new List<object>{{90.2},{684}},new List<object>{{90.3},{689}},new List<object>{{90.4},{693}},new List<object>{{90.5},{697}},new List<object>{{90.6},{701}},new List<object>{{90.7},{705}},new List<object>{{90.8},{708}},new List<object>{{90.9},{709}},new List<object>{{91},{712}},new List<object>{{91.1},{714}},new List<object>{{91.2},{716}},new List<object>{{91.3},{706}},new List<object>{{91.4},{697}},new List<object>{{91.5},{689}},new List<object>{{91.6},{681}},new List<object>{{91.7},{677}},new List<object>{{91.8},{668}},new List<object>{{91.9},{663}},new List<object>{{92},{661}},new List<object>{{92.1},{653}},new List<object>{{92.2},{652}},new List<object>{{92.3},{650}},new List<object>{{92.4},{647}},new List<object>{{92.5},{646}},new List<object>{{92.6},{645}},new List<object>{{92.7},{642}},new List<object>{{92.8},{640}},new List<object>{{92.9},{640}},new List<object>{{93},{651}},new List<object>{{93.1},{648}},new List<object>{{93.2},{657}},new List<object>{{93.3},{660}},new List<object>{{93.4},{660}},new List<object>{{93.5},{662}},new List<object>{{93.6},{672}},new List<object>{{93.7},{675}},new List<object>{{93.8},{681}},new List<object>{{93.9},{685}},new List<object>{{94},{694}},new List<object>{{94.1},{700}},new List<object>{{94.2},{707}},new List<object>{{94.3},{716}},new List<object>{{94.4},{720}},new List<object>{{94.5},{723}},new List<object>{{94.6},{727}},new List<object>{{94.7},{727}},new List<object>{{94.8},{728}},new List<object>{{94.9},{727}},new List<object>{{95},{727}},new List<object>{{95.1},{727}},new List<object>{{95.2},{730}},new List<object>{{95.3},{735}},new List<object>{{95.4},{744}},new List<object>{{95.5},{749}},new List<object>{{95.6},{753}},new List<object>{{95.7},{759}},new List<object>{{95.8},{762}},new List<object>{{95.9},{767}},new List<object>{{96},{782}},new List<object>{{96.1},{781}},new List<object>{{96.2},{783}},new List<object>{{96.3},{785}},new List<object>{{96.4},{789}},new List<object>{{96.5},{796}},new List<object>{{96.6},{807}},new List<object>{{96.7},{813}},new List<object>{{96.8},{819}},new List<object>{{96.9},{822}},new List<object>{{97},{824}},new List<object>{{97.1},{826}},new List<object>{{97.2},{830}},new List<object>{{97.3},{832}},new List<object>{{97.4},{836}},new List<object>{{97.5},{838}},new List<object>{{97.6},{842}},new List<object>{{97.7},{847}},new List<object>{{97.8},{848}},new List<object>{{97.9},{854}},new List<object>{{98},{855}},new List<object>{{98.1},{858}},new List<object>{{98.2},{863}},new List<object>{{98.3},{870}},new List<object>{{98.4},{875}},new List<object>{{98.5},{883}},new List<object>{{98.6},{889}},new List<object>{{98.7},{896}},new List<object>{{98.8},{904}},new List<object>{{98.9},{910}},new List<object>{{99},{916}},new List<object>{{99.1},{922}},new List<object>{{99.2},{927}},new List<object>{{99.3},{931}},new List<object>{{99.4},{938}},new List<object>{{99.5},{941}},new List<object>{{99.6},{949}},new List<object>{{99.7},{954}},new List<object>{{99.8},{962}},new List<object>{{99.9},{967}},new List<object>{{100},{976}},new List<object>{{100.1},{983}},new List<object>{{100.2},{986}},new List<object>{{100.3},{992}},new List<object>{{100.4},{994}},new List<object>{{100.5},{999}},new List<object>{{100.6},{1004}},new List<object>{{100.7},{1006}},new List<object>{{100.8},{1007}},new List<object>{{100.9},{1009}},new List<object>{{101},{1012}},new List<object>{{101.1},{1016}},new List<object>{{101.2},{1019}},new List<object>{{101.3},{1021}},new List<object>{{101.4},{1025}},new List<object>{{101.5},{1025}},new List<object>{{101.6},{1020}},new List<object>{{101.7},{1017}},new List<object>{{101.8},{1009}},new List<object>{{101.9},{1003}},new List<object>{{102},{1000}},new List<object>{{102.1},{994}},new List<object>{{102.2},{989}},new List<object>{{102.3},{986}},new List<object>{{102.4},{979}},new List<object>{{102.5},{974}},new List<object>{{102.6},{972}},new List<object>{{102.7},{964}},new List<object>{{102.8},{961}},new List<object>{{102.9},{957}},new List<object>{{103},{952}},new List<object>{{103.1},{946}},new List<object>{{103.2},{944}},new List<object>{{103.3},{940}},new List<object>{{103.4},{936}},new List<object>{{103.5},{935}},new List<object>{{103.6},{934}},new List<object>{{103.7},{934}},new List<object>{{103.8},{934}},new List<object>{{103.9},{934}},new List<object>{{104},{933}},new List<object>{{104.1},{929}},new List<object>{{104.2},{922}},new List<object>{{104.3},{914}},new List<object>{{104.4},{906}},new List<object>{{104.5},{910}},new List<object>{{104.6},{906}},new List<object>{{104.7},{903}},new List<object>{{104.8},{895}},new List<object>{{104.9},{893}},new List<object>{{105},{891}},new List<object>{{105.1},{889}},new List<object>{{105.2},{889}},new List<object>{{105.3},{893}},new List<object>{{105.4},{899}},new List<object>{{105.5},{904}},new List<object>{{105.6},{906}},new List<object>{{105.7},{897}},new List<object>{{105.8},{883}},new List<object>{{105.9},{895}},new List<object>{{106},{898}},new List<object>{{106.1},{893}},new List<object>{{106.2},{895}},new List<object>{{106.3},{907}},new List<object>{{106.4},{916}},new List<object>{{106.5},{915}},new List<object>{{106.6},{920}},new List<object>{{106.7},{919}},new List<object>{{106.8},{917}},new List<object>{{106.9},{911}},new List<object>{{107},{904}},new List<object>{{107.1},{891}},new List<object>{{107.2},{894}},new List<object>{{107.3},{902}},new List<object>{{107.4},{900}},new List<object>{{107.5},{900}},new List<object>{{107.6},{898}},new List<object>{{107.7},{897}},new List<object>{{107.8},{897}},new List<object>{{107.9},{897}},new List<object>{{108},{909}},new List<object>{{108.1},{910}},new List<object>{{108.2},{906}},new List<object>{{108.3},{920}},new List<object>{{108.4},{901}},new List<object>{{108.5},{900}},new List<object>{{108.6},{895}},new List<object>{{108.7},{892}},new List<object>{{108.8},{887}},new List<object>{{108.9},{889}},new List<object>{{109},{904}},new List<object>{{109.1},{910}},new List<object>{{109.2},{910}},new List<object>{{109.3},{907}},new List<object>{{109.4},{906}},new List<object>{{109.5},{898}},new List<object>{{109.6},{908}},new List<object>{{109.7},{911}},new List<object>{{109.8},{920}},new List<object>{{109.9},{928}},new List<object>{{110},{939}},new List<object>{{110.1},{939}},new List<object>{{110.2},{935}},new List<object>{{110.3},{932}},new List<object>{{110.4},{926}},new List<object>{{110.5},{924}},new List<object>{{110.6},{919}},new List<object>{{110.7},{913}},new List<object>{{110.8},{909}},new List<object>{{110.9},{906}},new List<object>{{111},{901}},new List<object>{{111.1},{899}},new List<object>{{111.2},{899}},new List<object>{{111.3},{899}},new List<object>{{111.4},{898}},new List<object>{{111.5},{896}},new List<object>{{111.6},{895}},new List<object>{{111.7},{889}},new List<object>{{111.8},{887}},new List<object>{{111.9},{886}},new List<object>{{112},{881}},new List<object>{{112.1},{875}},new List<object>{{112.2},{872}},new List<object>{{112.3},{867}},new List<object>{{112.4},{856}},new List<object>{{112.5},{850}},new List<object>{{112.6},{842}},new List<object>{{112.7},{839}},new List<object>{{112.8},{836}},new List<object>{{112.9},{827}},new List<object>{{113},{822}},new List<object>{{113.1},{817}},new List<object>{{113.2},{809}},new List<object>{{113.3},{805}},new List<object>{{113.4},{802}},new List<object>{{113.5},{796}},new List<object>{{113.6},{793}},new List<object>{{113.7},{790}},new List<object>{{113.8},{786}},new List<object>{{113.9},{778}},new List<object>{{114},{770}},new List<object>{{114.1},{759}},new List<object>{{114.2},{754}},new List<object>{{114.3},{744}},new List<object>{{114.4},{744}},new List<object>{{114.5},{746}},new List<object>{{114.6},{741}},new List<object>{{114.7},{761}},new List<object>{{114.8},{759}},new List<object>{{114.9},{737}},new List<object>{{115},{722}},new List<object>{{115.1},{719}},new List<object>{{115.2},{720}},new List<object>{{115.3},{721}},new List<object>{{115.4},{722}},new List<object>{{115.5},{718}},new List<object>{{115.6},{713}},new List<object>{{115.7},{709}},new List<object>{{115.8},{706}},new List<object>{{115.9},{707}},new List<object>{{116},{699}},new List<object>{{116.1},{689}},new List<object>{{116.2},{685}},new List<object>{{116.3},{683}},new List<object>{{116.4},{669}},new List<object>{{116.5},{665}},new List<object>{{116.6},{661}},new List<object>{{116.7},{657}},new List<object>{{116.8},{653}},new List<object>{{116.9},{653}},new List<object>{{117},{647}},new List<object>{{117.1},{640}},new List<object>{{117.2},{638}},new List<object>{{117.3},{633}},new List<object>{{117.4},{628}},new List<object>{{117.5},{624}},new List<object>{{117.6},{618}},new List<object>{{117.7},{613}},new List<object>{{117.8},{607}},new List<object>{{117.9},{602}},new List<object>{{118},{598}},new List<object>{{118.1},{595}},new List<object>{{118.2},{595}},new List<object>{{118.3},{594}},new List<object>{{118.4},{602}},new List<object>{{118.5},{598}},new List<object>{{118.6},{598}},new List<object>{{118.7},{601}},new List<object>{{118.8},{605}},new List<object>{{118.9},{608}},new List<object>{{119},{612}},new List<object>{{119.1},{614}},new List<object>{{119.2},{611}},new List<object>{{119.3},{608}},new List<object>{{119.4},{611}},new List<object>{{119.5},{612}},new List<object>{{119.6},{614}},new List<object>{{119.7},{615}},new List<object>{{119.8},{613}},new List<object>{{119.9},{611}},new List<object>{{120},{602}},new List<object>{{120.1},{593}},new List<object>{{120.2},{588}},new List<object>{{120.3},{588}},new List<object>{{120.4},{586}},new List<object>{{120.5},{583}},new List<object>{{120.6},{579}},new List<object>{{120.7},{579}},new List<object>{{120.8},{578}},new List<object>{{120.9},{576}},new List<object>{{121},{575}},new List<object>{{121.1},{579}},new List<object>{{121.2},{574}},new List<object>{{121.3},{570}},new List<object>{{121.4},{565}},new List<object>{{121.5},{562}},new List<object>{{121.6},{560}},new List<object>{{121.7},{559}},new List<object>{{121.8},{556}},new List<object>{{121.9},{554}},new List<object>{{122},{546}},new List<object>{{122.1},{542}},new List<object>{{122.2},{536}},new List<object>{{122.3},{531}},new List<object>{{122.4},{529}},new List<object>{{122.5},{529}},new List<object>{{122.6},{518}},new List<object>{{122.7},{515}},new List<object>{{122.8},{515}},new List<object>{{122.9},{515}},new List<object>{{123},{514}},new List<object>{{123.1},{513}},new List<object>{{123.2},{506}},new List<object>{{123.3},{498}},new List<object>{{123.4},{496}},new List<object>{{123.5},{494}},new List<object>{{123.6},{483}},new List<object>{{123.7},{479}},new List<object>{{123.8},{476}},new List<object>{{123.9},{470}},new List<object>{{124},{466}},new List<object>{{124.1},{460}},new List<object>{{124.2},{457}},new List<object>{{124.3},{451}},new List<object>{{124.4},{445}},new List<object>{{124.5},{443}},new List<object>{{124.6},{435}},new List<object>{{124.7},{432}},new List<object>{{124.8},{426}},new List<object>{{124.9},{421}},new List<object>{{125},{418}},new List<object>{{125.1},{414}},new List<object>{{125.2},{408}},new List<object>{{125.3},{405}},new List<object>{{125.4},{403}},new List<object>{{125.5},{394}},new List<object>{{125.6},{386}},new List<object>{{125.7},{379}},new List<object>{{125.8},{361}},new List<object>{{125.9},{358}},new List<object>{{126},{366}},new List<object>{{126.1},{372}},new List<object>{{126.2},{372}},new List<object>{{126.3},{374}},new List<object>{{126.4},{379}},new List<object>{{126.5},{382}},new List<object>{{126.6},{385}},new List<object>{{126.7},{388}},new List<object>{{126.8},{390}},new List<object>{{126.9},{393}},new List<object>{{127},{394}},new List<object>{{127.1},{393}},new List<object>{{127.2},{391}},new List<object>{{127.3},{387}},new List<object>{{127.4},{382}},new List<object>{{127.5},{378}},new List<object>{{127.6},{374}},new List<object>{{127.7},{370}},new List<object>{{127.8},{367}},new List<object>{{127.9},{366}},new List<object>{{128},{364}},new List<object>{{128.1},{364}},new List<object>{{128.2},{362}},new List<object>{{128.3},{362}},new List<object>{{128.4},{360}},new List<object>{{128.5},{357}},new List<object>{{128.6},{354}},new List<object>{{128.7},{351}},new List<object>{{128.8},{350}},new List<object>{{128.9},{351}},new List<object>{{129},{350}},new List<object>{{129.1},{350}},new List<object>{{129.2},{351}},new List<object>{{129.3},{352}},new List<object>{{129.4},{352}},new List<object>{{129.5},{352}},new List<object>{{129.6},{351}},new List<object>{{129.7},{352}},new List<object>{{129.8},{352}},new List<object>{{129.9},{353}},new List<object>{{130},{348}},new List<object>{{130.1},{346}},new List<object>{{130.2},{344}},new List<object>{{130.3},{343}},new List<object>{{130.4},{343}},new List<object>{{130.5},{342}},new List<object>{{130.6},{342}},new List<object>{{130.7},{345}},new List<object>{{130.8},{349}},new List<object>{{130.9},{341}},new List<object>{{131},{345}},new List<object>{{131.1},{348}},new List<object>{{131.2},{364}},new List<object>{{131.3},{374}},new List<object>{{131.4},{388}},new List<object>{{131.5},{379}},new List<object>{{131.6},{380}},new List<object>{{131.7},{387}},new List<object>{{131.8},{394}},new List<object>{{131.9},{404}},new List<object>{{132},{411}},new List<object>{{132.1},{416}},new List<object>{{132.2},{426}},new List<object>{{132.3},{428}},new List<object>{{132.4},{430}},new List<object>{{132.5},{438}},new List<object>{{132.6},{447}},new List<object>{{132.7},{450}},new List<object>{{132.8},{454}},new List<object>{{132.9},{460}},new List<object>{{133},{468}},new List<object>{{133.1},{469}},new List<object>{{133.2},{474}},new List<object>{{133.3},{478}},new List<object>{{133.4},{485}},new List<object>{{133.5},{488}},new List<object>{{133.6},{494}},new List<object>{{133.7},{497}},new List<object>{{133.8},{502}},new List<object>{{133.9},{510}},new List<object>{{134},{513}},new List<object>{{134.1},{516}},new List<object>{{134.2},{523}},new List<object>{{134.3},{527}},new List<object>{{134.4},{531}},new List<object>{{134.5},{540}},new List<object>{{134.6},{544}},new List<object>{{134.7},{549}},new List<object>{{134.8},{554}},new List<object>{{134.9},{557}},new List<object>{{135},{564}},new List<object>{{135.1},{566}},new List<object>{{135.2},{571}},new List<object>{{135.3},{577}},new List<object>{{135.4},{581}},new List<object>{{135.5},{584}},new List<object>{{135.6},{591}},new List<object>{{135.7},{596}},new List<object>{{135.8},{600}},new List<object>{{135.9},{608}},new List<object>{{136},{610}},new List<object>{{136.1},{616}},new List<object>{{136.2},{621}},new List<object>{{136.3},{627}},new List<object>{{136.4},{632}},new List<object>{{136.5},{644}},new List<object>{{136.6},{649}},new List<object>{{136.7},{656}},new List<object>{{136.8},{660}},new List<object>{{136.9},{663}},new List<object>{{137},{668}},new List<object>{{137.1},{672}},new List<object>{{137.2},{674}},new List<object>{{137.3},{677}},new List<object>{{137.4},{680}},new List<object>{{137.5},{683}},new List<object>{{137.6},{689}},new List<object>{{137.7},{691}},new List<object>{{137.8},{697}},new List<object>{{137.9},{699}},new List<object>{{138},{702}},new List<object>{{138.1},{707}},new List<object>{{138.2},{712}},new List<object>{{138.3},{716}},new List<object>{{138.4},{720}},new List<object>{{138.5},{728}},new List<object>{{138.6},{731}},new List<object>{{138.7},{735}},new List<object>{{138.8},{740}},new List<object>{{138.9},{742}},new List<object>{{139},{746}},new List<object>{{139.1},{750}},new List<object>{{139.2},{752}},new List<object>{{139.3},{760}},new List<object>{{139.4},{760}},new List<object>{{139.5},{761}},new List<object>{{139.6},{757}},new List<object>{{139.7},{756}},new List<object>{{139.8},{755}},new List<object>{{139.9},{754}},new List<object>{{140},{755}},new List<object>{{140.1},{756}},new List<object>{{140.2},{753}},new List<object>{{140.3},{746}},new List<object>{{140.4},{743}},new List<object>{{140.5},{734}},new List<object>{{140.6},{740}},new List<object>{{140.7},{746}},new List<object>{{140.8},{748}},new List<object>{{140.9},{747}},new List<object>{{141},{743}},new List<object>{{141.1},{742}},new List<object>{{141.2},{742}},new List<object>{{141.3},{740}},new List<object>{{141.4},{739}},new List<object>{{141.5},{741}},new List<object>{{141.6},{757}},new List<object>{{141.7},{756}},new List<object>{{141.8},{751}},new List<object>{{141.9},{747}},new List<object>{{142},{743}},new List<object>{{142.1},{738}},new List<object>{{142.2},{739}},new List<object>{{142.3},{742}},new List<object>{{142.4},{749}},new List<object>{{142.5},{750}},new List<object>{{142.6},{756}},new List<object>{{142.7},{760}},new List<object>{{142.8},{762}},new List<object>{{142.9},{765}},new List<object>{{143},{771}},new List<object>{{143.1},{775}},new List<object>{{143.2},{786}},new List<object>{{143.3},{791}},new List<object>{{143.4},{797}},new List<object>{{143.5},{801}},new List<object>{{143.6},{801}},new List<object>{{143.7},{793}},new List<object>{{143.8},{786}},new List<object>{{143.9},{782}},new List<object>{{144},{780}},new List<object>{{144.1},{778}},new List<object>{{144.2},{776}},new List<object>{{144.3},{765}},new List<object>{{144.4},{757}},new List<object>{{144.5},{753}},new List<object>{{144.6},{750}},new List<object>{{144.7},{748}},new List<object>{{144.8},{745}},new List<object>{{144.9},{738}},new List<object>{{145},{735}},new List<object>{{145.1},{732}},new List<object>{{145.2},{726}},new List<object>{{145.3},{724}},new List<object>{{145.4},{720}},new List<object>{{145.5},{712}},new List<object>{{145.6},{710}},new List<object>{{145.7},{705}},new List<object>{{145.8},{697}},new List<object>{{145.9},{691}},new List<object>{{146},{686}},new List<object>{{146.1},{681}},new List<object>{{146.2},{679}},new List<object>{{146.3},{680}},new List<object>{{146.4},{678}},new List<object>{{146.5},{673}},new List<object>{{146.6},{665}},new List<object>{{146.7},{657}},new List<object>{{146.8},{655}},new List<object>{{146.9},{647}},new List<object>{{147},{640}},new List<object>{{147.1},{634}},new List<object>{{147.2},{621}},new List<object>{{147.3},{621}},new List<object>{{147.4},{613}},new List<object>{{147.5},{608}},new List<object>{{147.6},{598}},new List<object>{{147.7},{594}},new List<object>{{147.8},{588}},new List<object>{{147.9},{578}},new List<object>{{148},{565}},new List<object>{{148.1},{559}},new List<object>{{148.2},{558}},new List<object>{{148.3},{556}},new List<object>{{148.4},{556}},new List<object>{{148.5},{555}},new List<object>{{148.6},{556}},new List<object>{{148.7},{557}},new List<object>{{148.8},{557}},new List<object>{{148.9},{565}},new List<object>{{149},{570}},new List<object>{{149.1},{575}},new List<object>{{149.2},{583}},new List<object>{{149.3},{591}},new List<object>{{149.4},{599}},new List<object>{{149.5},{603}},new List<object>{{149.6},{609}},new List<object>{{149.7},{613}},new List<object>{{149.8},{618}},new List<object>{{149.9},{624}},new List<object>{{150},{630}},new List<object>{{150.1},{635}},new List<object>{{150.2},{643}},new List<object>{{150.3},{652}},new List<object>{{150.4},{658}},new List<object>{{150.5},{669}},new List<object>{{150.6},{680}},new List<object>{{150.7},{684}},new List<object>{{150.8},{692}},new List<object>{{150.9},{696}},new List<object>{{151},{698}},new List<object>{{151.1},{698}},new List<object>{{151.2},{696}},new List<object>{{151.3},{695}},new List<object>{{151.4},{696}},new List<object>{{151.5},{699}},new List<object>{{151.6},{701}},new List<object>{{151.7},{706}},new List<object>{{151.8},{707}},new List<object>{{151.9},{707}},new List<object>{{152},{703}},new List<object>{{152.1},{702}},new List<object>{{152.2},{700}},new List<object>{{152.3},{700}},new List<object>{{152.4},{705}},new List<object>{{152.5},{705}},new List<object>{{152.6},{715}},new List<object>{{152.7},{718}},new List<object>{{152.8},{721}},new List<object>{{152.9},{723}},new List<object>{{153},{725}},new List<object>{{153.1},{724}},new List<object>{{153.2},{722}},new List<object>{{153.3},{720}},new List<object>{{153.4},{716}},new List<object>{{153.5},{710}},new List<object>{{153.6},{700}},new List<object>{{153.7},{696}},new List<object>{{153.8},{691}},new List<object>{{153.9},{682}},new List<object>{{154},{676}},new List<object>{{154.1},{670}},new List<object>{{154.2},{664}},new List<object>{{154.3},{658}},new List<object>{{154.4},{648}},new List<object>{{154.5},{643}},new List<object>{{154.6},{645}},new List<object>{{154.7},{645}},new List<object>{{154.8},{646}},new List<object>{{154.9},{630}},new List<object>{{155},{625}},new List<object>{{155.1},{620}},new List<object>{{155.2},{614}},new List<object>{{155.3},{605}},new List<object>{{155.4},{600}},new List<object>{{155.5},{593}},new List<object>{{155.6},{587}},new List<object>{{155.7},{581}},new List<object>{{155.8},{576}},new List<object>{{155.9},{569}},new List<object>{{156},{566}},new List<object>{{156.1},{559}},new List<object>{{156.2},{557}},new List<object>{{156.3},{551}},new List<object>{{156.4},{548}},new List<object>{{156.5},{544}},new List<object>{{156.6},{542}},new List<object>{{156.7},{540}},new List<object>{{156.8},{537}},new List<object>{{156.9},{540}},new List<object>{{157},{542}},new List<object>{{157.1},{541}},new List<object>{{157.2},{540}},new List<object>{{157.3},{538}},new List<object>{{157.4},{536}},new List<object>{{157.5},{532}},new List<object>{{157.6},{523}},new List<object>{{157.7},{519}},new List<object>{{157.8},{515}},new List<object>{{157.9},{509}},new List<object>{{158},{503}},new List<object>{{158.1},{499}},new List<object>{{158.2},{491}},new List<object>{{158.3},{485}},new List<object>{{158.4},{478}},new List<object>{{158.5},{477}},new List<object>{{158.6},{474}},new List<object>{{158.7},{471}},new List<object>{{158.8},{469}},new List<object>{{158.9},{464}},new List<object>{{159},{462}},new List<object>{{159.1},{456}},new List<object>{{159.2},{454}},new List<object>{{159.3},{445}},new List<object>{{159.4},{424}},new List<object>{{159.5},{427}},new List<object>{{159.6},{425}},new List<object>{{159.7},{422}},new List<object>{{159.8},{419}},new List<object>{{159.9},{418}},new List<object>{{160},{417}},new List<object>{{160.1},{423}},new List<object>{{160.2},{436}},new List<object>{{160.3},{434}},new List<object>{{160.4},{426}},new List<object>{{160.5},{401}},new List<object>{{160.6},{417}},new List<object>{{160.7},{418}},new List<object>{{160.8},{419}},new List<object>{{160.9},{419}},new List<object>{{161},{412}},new List<object>{{161.1},{417}},new List<object>{{161.2},{425}},new List<object>{{161.3},{430}},new List<object>{{161.4},{432}},new List<object>{{161.5},{417}},new List<object>{{161.6},{423}},new List<object>{{161.7},{425}},new List<object>{{161.8},{409}},new List<object>{{161.9},{399}},new List<object>{{162},{427}},new List<object>{{162.1},{425}},new List<object>{{162.2},{414}},new List<object>{{162.3},{421}},new List<object>{{162.4},{424}},new List<object>{{162.5},{426}},new List<object>{{162.6},{417}},new List<object>{{162.7},{405}},new List<object>{{162.8},{411}},new List<object>{{162.9},{403}},new List<object>{{163},{405}},new List<object>{{163.1},{410}},new List<object>{{163.2},{412}},new List<object>{{163.3},{413}},new List<object>{{163.4},{415}},new List<object>{{163.5},{406}},new List<object>{{163.6},{407}},new List<object>{{163.7},{408}},new List<object>{{163.8},{412}},new List<object>{{163.9},{417}},new List<object>{{164},{426}},new List<object>{{164.1},{431}},new List<object>{{164.2},{482}},new List<object>{{164.3},{478}},new List<object>{{164.4},{471}},new List<object>{{164.5},{463}},new List<object>{{164.6},{457}},new List<object>{{164.7},{452}},new List<object>{{164.8},{456}},new List<object>{{164.9},{463}},new List<object>{{165},{471}},new List<object>{{165.1},{470}},new List<object>{{165.2},{471}},new List<object>{{165.3},{474}},new List<object>{{165.4},{494}},new List<object>{{165.5},{506}},new List<object>{{165.6},{515}},new List<object>{{165.7},{520}},new List<object>{{165.8},{534}},new List<object>{{165.9},{520}},new List<object>{{166},{536}},new List<object>{{166.1},{529}},new List<object>{{166.2},{524}},new List<object>{{166.3},{515}},new List<object>{{166.4},{520}},new List<object>{{166.5},{526}},new List<object>{{166.6},{531}},new List<object>{{166.7},{551}},new List<object>{{166.8},{553}},new List<object>{{166.9},{555}},new List<object>{{167},{559}},new List<object>{{167.1},{562}},new List<object>{{167.2},{564}},new List<object>{{167.3},{567}},new List<object>{{167.4},{571}},new List<object>{{167.5},{574}},new List<object>{{167.6},{576}},new List<object>{{167.7},{596}},new List<object>{{167.8},{607}},new List<object>{{167.9},{623}},new List<object>{{168},{645}},new List<object>{{168.1},{667}},new List<object>{{168.2},{685}},new List<object>{{168.3},{691}},new List<object>{{168.4},{709}},new List<object>{{168.5},{702}},new List<object>{{168.6},{692}},new List<object>{{168.7},{692}},new List<object>{{168.8},{696}},new List<object>{{168.9},{732}},new List<object>{{169},{746}},new List<object>{{169.1},{758}},new List<object>{{169.2},{761}},new List<object>{{169.3},{763}},new List<object>{{169.4},{765}},new List<object>{{169.5},{768}},new List<object>{{169.6},{783}},new List<object>{{169.7},{816}},new List<object>{{169.8},{815}},new List<object>{{169.9},{817}},new List<object>{{170},{821}},new List<object>{{170.1},{825}},new List<object>{{170.2},{827}},new List<object>{{170.3},{828}},new List<object>{{170.4},{830}},new List<object>{{170.5},{829}},new List<object>{{170.6},{827}},new List<object>{{170.7},{827}},new List<object>{{170.8},{828}},new List<object>{{170.9},{837}},new List<object>{{171},{845}},new List<object>{{171.1},{850}},new List<object>{{171.2},{856}},new List<object>{{171.3},{864}},new List<object>{{171.4},{867}},new List<object>{{171.5},{876}},new List<object>{{171.6},{880}},new List<object>{{171.7},{888}},new List<object>{{171.8},{900}},new List<object>{{171.9},{905}},new List<object>{{172},{910}},new List<object>{{172.1},{922}},new List<object>{{172.2},{925}},new List<object>{{172.3},{931}},new List<object>{{172.4},{939}},new List<object>{{172.5},{957}},new List<object>{{172.6},{966}},new List<object>{{172.7},{980}},new List<object>{{172.8},{991}},new List<object>{{172.9},{998}},new List<object>{{173},{1008}},new List<object>{{173.1},{1021}},new List<object>{{173.2},{1031}},new List<object>{{173.3},{1045}},new List<object>{{173.4},{1059}},new List<object>{{173.5},{1065}},new List<object>{{173.6},{1078}},new List<object>{{173.7},{1094}},new List<object>{{173.8},{1092}},new List<object>{{173.9},{1096}},new List<object>{{174},{1098}},new List<object>{{174.1},{1101}},new List<object>{{174.2},{1104}},new List<object>{{174.3},{1107}},new List<object>{{174.4},{1110}},new List<object>{{174.5},{1113}},new List<object>{{174.6},{1114}},new List<object>{{174.7},{1116}},new List<object>{{174.8},{1119}},new List<object>{{174.9},{1122}},new List<object>{{175},{1125}},new List<object>{{175.1},{1128}},new List<object>{{175.2},{1130}},new List<object>{{175.3},{1134}},new List<object>{{175.4},{1137}},new List<object>{{175.5},{1142}},new List<object>{{175.6},{1148}},new List<object>{{175.7},{1155}},new List<object>{{175.8},{1162}},new List<object>{{175.9},{1174}},new List<object>{{176},{1183}},new List<object>{{176.1},{1191}},new List<object>{{176.2},{1196}},new List<object>{{176.3},{1198}},new List<object>{{176.4},{1200}},new List<object>{{176.5},{1189}},new List<object>{{176.6},{1183}},new List<object>{{176.7},{1180}},new List<object>{{176.8},{1178}},new List<object>{{176.9},{1173}},new List<object>{{177},{1171}},new List<object>{{177.1},{1167}},new List<object>{{177.2},{1165}},new List<object>{{177.3},{1165}},new List<object>{{177.4},{1165}},new List<object>{{177.5},{1165}},new List<object>{{177.6},{1164}},new List<object>{{177.7},{1164}},new List<object>{{177.8},{1164}},new List<object>{{177.9},{1166}},new List<object>{{178},{1167}},new List<object>{{178.1},{1170}},new List<object>{{178.2},{1177}},new List<object>{{178.3},{1174}},new List<object>{{178.4},{1166}},new List<object>{{178.5},{1166}},new List<object>{{178.6},{1164}},new List<object>{{178.7},{1162}},new List<object>{{178.8},{1160}},new List<object>{{178.9},{1155}},new List<object>{{179},{1152}},new List<object>{{179.1},{1151}},new List<object>{{179.2},{1151}},new List<object>{{179.3},{1146}},new List<object>{{179.4},{1144}},new List<object>{{179.5},{1147}},new List<object>{{179.6},{1151}},new List<object>{{179.7},{1154}},new List<object>{{179.8},{1153}},new List<object>{{179.9},{1154}},new List<object>{{180},{1153}},new List<object>{{180.1},{1150}},new List<object>{{180.2},{1147}},new List<object>{{180.3},{1146}},new List<object>{{180.4},{1144}},new List<object>{{180.5},{1142}},new List<object>{{180.6},{1140}},new List<object>{{180.7},{1135}},new List<object>{{180.8},{1125}},new List<object>{{180.9},{1121}},new List<object>{{181},{1113}},new List<object>{{181.1},{1107}},new List<object>{{181.2},{1098}},new List<object>{{181.3},{1094}},new List<object>{{181.4},{1087}},new List<object>{{181.5},{1084}},new List<object>{{181.6},{1082}},new List<object>{{181.7},{1078}},new List<object>{{181.8},{1073}},new List<object>{{181.9},{1068}},new List<object>{{182},{1063}},new List<object>{{182.1},{1061}},new List<object>{{182.2},{1060}},new List<object>{{182.3},{1061}},new List<object>{{182.4},{1065}},new List<object>{{182.5},{1069}},new List<object>{{182.6},{1072}},new List<object>{{182.7},{1076}},new List<object>{{182.8},{1084}},new List<object>{{182.9},{1098}},new List<object>{{183},{1110}},new List<object>{{183.1},{1116}},new List<object>{{183.2},{1113}},new List<object>{{183.3},{1116}},new List<object>{{183.4},{1122}},new List<object>{{183.5},{1125}},new List<object>{{183.6},{1133}},new List<object>{{183.7},{1138}},new List<object>{{183.8},{1146}},new List<object>{{183.9},{1140}},new List<object>{{184},{1135}},new List<object>{{184.1},{1134}},new List<object>{{184.2},{1131}},new List<object>{{184.3},{1129}},new List<object>{{184.4},{1137}},new List<object>{{184.5},{1139}},new List<object>{{184.6},{1144}},new List<object>{{184.7},{1149}},new List<object>{{184.8},{1155}},new List<object>{{184.9},{1163}},new List<object>{{185},{1164}},new List<object>{{185.1},{1173}},new List<object>{{185.2},{1170}},new List<object>{{185.3},{1165}},new List<object>{{185.4},{1157}},new List<object>{{185.5},{1153}},new List<object>{{185.6},{1150}},new List<object>{{185.7},{1142}},new List<object>{{185.8},{1141}},new List<object>{{185.9},{1142}},new List<object>{{186},{1142}},new List<object>{{186.1},{1142}},new List<object>{{186.2},{1142}},new List<object>{{186.3},{1145}},new List<object>{{186.4},{1151}},new List<object>{{186.5},{1154}},new List<object>{{186.6},{1155}},new List<object>{{186.7},{1152}},new List<object>{{186.8},{1154}},new List<object>{{186.9},{1154}},new List<object>{{187},{1155}},new List<object>{{187.1},{1158}},new List<object>{{187.2},{1159}},new List<object>{{187.3},{1158}},new List<object>{{187.4},{1158}},new List<object>{{187.5},{1158}},new List<object>{{187.6},{1161}},new List<object>{{187.7},{1167}},new List<object>{{187.8},{1170}}};
			LineColor="#434348";
			Color="#90ed7d";
			FillOpacity=0.5f;
			Name="Elevation";
			Marker=new HighchartAnnotationsSeriesItemMarker();
			Threshold=null;
	
		}
	
		[JsonProperty("accessibility")]
		public HighchartAnnotationsSeriesItemAccessibility Accessibility{ get; set; }	[JsonProperty("data")]
		public List<List<object>> Data{ get; set; }	[InputText("lineColor")]
		[JsonProperty("lineColor")]
		public string LineColor{ get; set; }	[InputText("color")]
		[JsonProperty("color")]
		public string Color{ get; set; }	[InputNumber("fillOpacity")]
		[JsonProperty("fillOpacity")]
		public float FillOpacity{ get; set; }	[InputText("name")]
		[JsonProperty("name")]
		public string Name{ get; set; }	[JsonProperty("marker")]
		public HighchartAnnotationsSeriesItemMarker Marker{ get; set; }	[JsonProperty("threshold")]
		public object Threshold{ get; set; }
	}
	
	
	public class HighchartAnnotations: Highchart {
	
		public HighchartAnnotations():base(){
			Chart=new HighchartAnnotationsChart();
			Caption=new HighchartAnnotationsCaption();
			Title=new HighchartAnnotationsTitle();
			Accessibility=new HighchartAnnotationsAccessibility();
			Credits=new HighchartAnnotationsCredits();
			Annotations=new List<HighchartAnnotationsAnnotationsItem>{new HighchartAnnotationsAnnotationsItem(),new HighchartAnnotationsAnnotationsItem(),new HighchartAnnotationsAnnotationsItem()};
			XAxis=new HighchartAnnotationsXAxis();
			YAxis=new HighchartAnnotationsYAxis();
			Tooltip=new HighchartAnnotationsTooltip();
			Legend=new HighchartAnnotationsLegend();
			Series=new List<HighchartAnnotationsSeriesItem>{new HighchartAnnotationsSeriesItem()};
	
		}
	
		[JsonProperty("chart")]
		public HighchartAnnotationsChart Chart{ get; set; }	[JsonProperty("caption")]
		public HighchartAnnotationsCaption Caption{ get; set; }	[JsonProperty("title")]
		public HighchartAnnotationsTitle Title{ get; set; }	[JsonProperty("accessibility")]
		public HighchartAnnotationsAccessibility Accessibility{ get; set; }	[JsonProperty("credits")]
		public HighchartAnnotationsCredits Credits{ get; set; }	[JsonProperty("annotations")]
		public List<HighchartAnnotationsAnnotationsItem> Annotations{ get; set; }	[JsonProperty("xAxis")]
		public HighchartAnnotationsXAxis XAxis{ get; set; }	[JsonProperty("yAxis")]
		public HighchartAnnotationsYAxis YAxis{ get; set; }	[JsonProperty("tooltip")]
		public HighchartAnnotationsTooltip Tooltip{ get; set; }	[JsonProperty("legend")]
		public HighchartAnnotationsLegend Legend{ get; set; }	[JsonProperty("series")]
		public List<HighchartAnnotationsSeriesItem> Series{ get; set; }
	}
	
	
}