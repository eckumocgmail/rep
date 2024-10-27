using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;


//Загрузка данных начинаетя с этой таблицы"
[Label("Входящая информация")]
public class BusinessData : BaseEntity
{

    [NotNullNotEmpty("Показатель является обязательным полем")]
    [InputDictionary(nameof(BusinessIndicator) + ",Name")]
    public int BusinessIndicatorID { get; set; }

    [JsonIgnore]
    [Newtonsoft.Json.JsonIgnore]
    public virtual BusinessIndicator BusinessIndicator { get; set; }


    [NotNullNotEmpty("Набор данных является обязательным полем")]
    [InputDictionary(nameof(BusinessDataset) + ",Name")]
    public int BusinessDatasetID { get; set; }
    [JsonIgnore]
    [Newtonsoft.Json.JsonIgnore]
    public virtual BusinessDataset BusinessDataset { get; set; }


    [NotNullNotEmpty("Периодичность является обязательным полем")]
    [InputDictionary(nameof(BusinessGranularities) + ",Name")]
    public int GranularityID { get; set; }


    [NotNullNotEmpty("Обьект мониторинга является обязательным полем")]
    [InputDictionary(nameof(BusinessResource) + ",Name")]
    public int BusinessResourceID { get; set; }
    [JsonIgnore]
    [Newtonsoft.Json.JsonIgnore]
    public virtual BusinessResource BusinessResource { get; set; }

    [NotNullNotEmpty("Начало периода")]
    [InputDate()]    
    public DateTime BeginDate { get; set; }

    [InputNumber()]
    [NotNullNotEmpty()]
    public float? IndValue { get; set; }

    [NotInput()]
    public DateTime Changed { get; set; } = DateTime.Now;
}

