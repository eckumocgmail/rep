using System.Collections.Generic;

using Newtonsoft.Json;
using System;

namespace Highcharts.Models.HighchartCombo
{
	public class HighchartComboTitle: Highchart {
	
		public HighchartComboTitle():base(){
			Text="Combination chart";
	
		}
	
		[InputText("text")]
		[JsonProperty("text")]
		public string Text{ get; set; }
	}
	
	
	public class HighchartComboXAxis: Highchart {
	
		public HighchartComboXAxis():base(){
			Categories=new List<string>{"Apples","Oranges","Pears","Bananas","Plums"};
	
		}
	
		[JsonProperty("categories")]
		public List<string> Categories{ get; set; }
	}
	
	
	public class HighchartComboLabelsItemsItemStyle: Highchart {
	
		public HighchartComboLabelsItemsItemStyle():base(){
			FromLeft="50px";
			FromTop="18px";
			FrontColor="black";
	
		}
	
		[InputText("left")]
		[JsonProperty("left")]
		public string FromLeft{ get; set; }	[InputText("top")]


		[JsonProperty("top")]
		public string FromTop{ get; set; }	[InputText("color")]


		[JsonProperty("color")]
		public string FrontColor{ get; set; }
	}
	
	
	public class HighchartComboLabelsItemsItem: Highchart {
	
		public HighchartComboLabelsItemsItem():base(){
			Html="Total fruit consumption";
			Style=new HighchartComboLabelsItemsItemStyle();
	
		}
	
		[InputText("html")]
		[JsonProperty("html")]
		public string Html{ get; set; }	[JsonProperty("style")]
		public HighchartComboLabelsItemsItemStyle Style{ get; set; }
	}
	
	
	public class HighchartComboLabels: Highchart {
	
		public HighchartComboLabels():base(){
			Items=new List<HighchartComboLabelsItemsItem>{new HighchartComboLabelsItemsItem()};
	
		}
	
		[JsonProperty("items")]
		public List<HighchartComboLabelsItemsItem> Items{ get; set; }
	}
	
	
	public class HighchartComboSeriesItem: Highchart {
	
		public HighchartComboSeriesItem():base(){
			Type="column";
			Name="Jane";
			Data=new List<Nullable<float>>{3f,2f,1f,3f,4f};
	
		}
	
		[InputText("type")]
		[JsonProperty("type")]
		public string Type{ get; set; }	[InputText("name")]
		[JsonProperty("name")]
		public string Name{ get; set; }	[JsonProperty("data")]
		public List<Nullable<float>> Data{ get; set; }
	}
	
	
	public class HighchartCombo: Highchart {
	
		public HighchartCombo():base(){
			Title=new HighchartComboTitle();
			XAxis=new HighchartComboXAxis();
			Labels=new HighchartComboLabels();
			Series=new List<HighchartComboSeriesItem>{new HighchartComboSeriesItem(),new HighchartComboSeriesItem(),new HighchartComboSeriesItem(),new HighchartComboSeriesItem(),new HighchartComboSeriesItem()};
	
		}
	
		[JsonProperty("title")]
		public HighchartComboTitle Title{ get; set; }	[JsonProperty("xAxis")]
		public HighchartComboXAxis XAxis{ get; set; }	[JsonProperty("labels")]
		public HighchartComboLabels Labels{ get; set; }	[JsonProperty("series")]
		public List<HighchartComboSeriesItem> Series{ get; set; }
	}
	
	
}