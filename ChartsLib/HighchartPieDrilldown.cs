using System.Collections.Generic;

using Newtonsoft.Json;
using System;

namespace Highcharts.Models.HighchartPieDrilldown
{
	public class HighchartPieDrilldownChart: Highchart {
	
		public HighchartPieDrilldownChart():base(){
			Type="pie";
	
		}
	
		[InputText("type")]
		[JsonProperty("type")]
		public string Type{ get; set; }
	}
	
	
	public class HighchartPieDrilldownTitle: Highchart {
	
		public HighchartPieDrilldownTitle():base(){
			Text="Browser market shares. January, 2018";
	
		}
	
		[InputText("text")]
		[JsonProperty("text")]
		public string Text{ get; set; }
	}
	
	
	public class HighchartPieDrilldownSubtitle: Highchart {
	
		public HighchartPieDrilldownSubtitle():base(){
			Text="Click the slices to view versions. Source: <a href='http://statcounter.com' target='_blank'>statcounter.com</a>";
	
		}
	
		[InputText("text")]
		[JsonProperty("text")]
		public string Text{ get; set; }
	}
	
	
	public class HighchartPieDrilldownAccessibilityAnnounceNewDataEnabled: Highchart {
	
		public HighchartPieDrilldownAccessibilityAnnounceNewDataEnabled():base(){
	
		}
	
	
	}
	
	
	public class HighchartPieDrilldownAccessibilityAnnounceNewData: Highchart {
	
		public HighchartPieDrilldownAccessibilityAnnounceNewData():base(){
			Enabled=new HighchartPieDrilldownAccessibilityAnnounceNewDataEnabled();
	
		}
	
		[JsonProperty("enabled")]
		public HighchartPieDrilldownAccessibilityAnnounceNewDataEnabled Enabled{ get; set; }
	}
	
	
	public class HighchartPieDrilldownAccessibilityPoint: Highchart {
	
		public HighchartPieDrilldownAccessibilityPoint():base(){
			ValueSuffix="%";
	
		}
	
		[InputText("valueSuffix")]
		[JsonProperty("valueSuffix")]
		public string ValueSuffix{ get; set; }
	}
	
	
	public class HighchartPieDrilldownAccessibility: Highchart {
	
		public HighchartPieDrilldownAccessibility():base(){
			AnnounceNewData=new HighchartPieDrilldownAccessibilityAnnounceNewData();
			Point=new HighchartPieDrilldownAccessibilityPoint();
	
		}
	
		[JsonProperty("announceNewData")]
		public HighchartPieDrilldownAccessibilityAnnounceNewData AnnounceNewData{ get; set; }	[JsonProperty("point")]
		public HighchartPieDrilldownAccessibilityPoint Point{ get; set; }
	}
	
	
	public class HighchartPieDrilldownPlotOptionsSeriesDataLabelsEnabled: Highchart {
	
		public HighchartPieDrilldownPlotOptionsSeriesDataLabelsEnabled():base(){
	
		}
	
	
	}
	
	
	public class HighchartPieDrilldownPlotOptionsSeriesDataLabels: Highchart {
	
		public HighchartPieDrilldownPlotOptionsSeriesDataLabels():base(){
			Enabled=new HighchartPieDrilldownPlotOptionsSeriesDataLabelsEnabled();
			Format="{point.name}: {point.y:.1f}%";
	
		}
	
		[JsonProperty("enabled")]
		public HighchartPieDrilldownPlotOptionsSeriesDataLabelsEnabled Enabled{ get; set; }	[InputText("format")]
		[JsonProperty("format")]
		public string Format{ get; set; }
	}
	
	
	public class HighchartPieDrilldownPlotOptionsSeries: Highchart {
	
		public HighchartPieDrilldownPlotOptionsSeries():base(){
			DataLabels=new HighchartPieDrilldownPlotOptionsSeriesDataLabels();
	
		}
	
		[JsonProperty("dataLabels")]
		public HighchartPieDrilldownPlotOptionsSeriesDataLabels DataLabels{ get; set; }
	}
	
	
	public class HighchartPieDrilldownPlotOptions: Highchart {
	
		public HighchartPieDrilldownPlotOptions():base(){
			Series=new HighchartPieDrilldownPlotOptionsSeries();
	
		}
	
		[JsonProperty("series")]
		public HighchartPieDrilldownPlotOptionsSeries Series{ get; set; }
	}
	
	
	public class HighchartPieDrilldownTooltip: Highchart {
	
		public HighchartPieDrilldownTooltip():base(){
			HeaderFormat="<span style='font-size:11px'>{series.name}</span><br>";
			PointFormat="<span style='color:{point.color}'>{point.name}</span>: <b>{point.y:.2f}%</b> of total<br/>";
	
		}
	
		[InputText("headerFormat")]
		[JsonProperty("headerFormat")]
		public string HeaderFormat{ get; set; }	[InputText("pointFormat")]
		[JsonProperty("pointFormat")]
		public string PointFormat{ get; set; }
	}
	
	
	public class HighchartPieDrilldownSeriesItemColorByPoint: Highchart {
	
		public HighchartPieDrilldownSeriesItemColorByPoint():base(){
	
		}
	
	
	}
	
	
	public class HighchartPieDrilldownSeriesItemDataItem: Highchart {
	
		public HighchartPieDrilldownSeriesItemDataItem():base(){
			Name="Chrome";
			Y=62.74f;
			Drilldown="Chrome";
	
		}
	
		[InputText("name")]
		[JsonProperty("name")]
		public string Name{ get; set; }	[InputNumber("y")]
		[JsonProperty("y")]
		public float Y{ get; set; }	[InputText("drilldown")]
		[JsonProperty("drilldown")]
		public string Drilldown{ get; set; }
	}
	
	
	public class HighchartPieDrilldownSeriesItem: Highchart {
	
		public HighchartPieDrilldownSeriesItem():base(){
			Name="Browsers";
			ColorByPoint=new HighchartPieDrilldownSeriesItemColorByPoint();
			Data=new List<HighchartPieDrilldownSeriesItemDataItem>{new HighchartPieDrilldownSeriesItemDataItem(),new HighchartPieDrilldownSeriesItemDataItem(),new HighchartPieDrilldownSeriesItemDataItem(),new HighchartPieDrilldownSeriesItemDataItem(),new HighchartPieDrilldownSeriesItemDataItem(),new HighchartPieDrilldownSeriesItemDataItem(),new HighchartPieDrilldownSeriesItemDataItem()};
	
		}
	
		[InputText("name")]
		[JsonProperty("name")]
		public string Name{ get; set; }	[JsonProperty("colorByPoint")]
		public HighchartPieDrilldownSeriesItemColorByPoint ColorByPoint{ get; set; }	[JsonProperty("data")]
		public List<HighchartPieDrilldownSeriesItemDataItem> Data{ get; set; }
	}
	
	
	public class HighchartPieDrilldownDrilldownSeriesItem: Highchart {
	
		public HighchartPieDrilldownDrilldownSeriesItem():base(){
			Name="Chrome";
			Id="Chrome";
			Data=new List<List<string>>{new List<string>{"v65.0","0.1"},new List<string>{"v64.0","1.3"},new List<string>{"v63.0","53.02"},new List<string>{"v62.0","1.4"},new List<string>{"v61.0","0.88"},new List<string>{"v60.0","0.56"},new List<string>{"v59.0","0.45"},new List<string>{"v58.0","0.49"},new List<string>{"v57.0","0.32"},new List<string>{"v56.0","0.29"},new List<string>{"v55.0","0.79"},new List<string>{"v54.0","0.18"},new List<string>{"v51.0","0.13"},new List<string>{"v49.0","2.16"},new List<string>{"v48.0","0.13"},new List<string>{"v47.0","0.11"},new List<string>{"v43.0","0.17"},new List<string>{"v29.0","0.26"}};
	
		}
	
		[InputText("name")]
		[JsonProperty("name")]
		public string Name{ get; set; }	[InputText("id")]
		[JsonProperty("id")]
		public string Id{ get; set; }	[JsonProperty("data")]
		public List<List<string>> Data{ get; set; }
	}
	
	
	public class HighchartPieDrilldownDrilldown: Highchart {
	
		public HighchartPieDrilldownDrilldown():base(){
			Series=new List<HighchartPieDrilldownDrilldownSeriesItem>{new HighchartPieDrilldownDrilldownSeriesItem(),new HighchartPieDrilldownDrilldownSeriesItem(),new HighchartPieDrilldownDrilldownSeriesItem(),new HighchartPieDrilldownDrilldownSeriesItem(),new HighchartPieDrilldownDrilldownSeriesItem(),new HighchartPieDrilldownDrilldownSeriesItem()};
	
		}
	
		[JsonProperty("series")]
		public List<HighchartPieDrilldownDrilldownSeriesItem> Series{ get; set; }
	}
	
	
	public class HighchartPieDrilldown: Highchart {
	
		public HighchartPieDrilldown():base(){
			Chart=new HighchartPieDrilldownChart();
			Title=new HighchartPieDrilldownTitle();
			Subtitle=new HighchartPieDrilldownSubtitle();
			Accessibility=new HighchartPieDrilldownAccessibility();
			PlotOptions=new HighchartPieDrilldownPlotOptions();
			Tooltip=new HighchartPieDrilldownTooltip();
			Series=new List<HighchartPieDrilldownSeriesItem>{new HighchartPieDrilldownSeriesItem()};
			Drilldown=new HighchartPieDrilldownDrilldown();
	
		}
	
		[JsonProperty("chart")]
		public HighchartPieDrilldownChart Chart{ get; set; }	[JsonProperty("title")]
		public HighchartPieDrilldownTitle Title{ get; set; }	[JsonProperty("subtitle")]
		public HighchartPieDrilldownSubtitle Subtitle{ get; set; }	[JsonProperty("accessibility")]
		public HighchartPieDrilldownAccessibility Accessibility{ get; set; }	[JsonProperty("plotOptions")]
		public HighchartPieDrilldownPlotOptions PlotOptions{ get; set; }	[JsonProperty("tooltip")]
		public HighchartPieDrilldownTooltip Tooltip{ get; set; }	[JsonProperty("series")]
		public List<HighchartPieDrilldownSeriesItem> Series{ get; set; }	[JsonProperty("drilldown")]
		public HighchartPieDrilldownDrilldown Drilldown{ get; set; }
	}
	
	
}