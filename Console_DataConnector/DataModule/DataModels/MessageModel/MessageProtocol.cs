﻿
using Console_DataConnector.DataModule.DataCommon.DataTypes;
using System;
using System.Collections.Generic;

[Icon("message")]
[Label("Информационные характеристики сообщений")]
public class MessageProtocol : BusinessEntity<MessageProtocol>
{         
    

    [Label("Источник")]     
    [SingleSelectApi("BusinessFunction" + ",Name")]
    public int? FromID { get; set; }
  


    public int? FromBusinessFunctionID { get; set; }
    public int? ToBusinessFunctionID { get; set; }




    [Label("Приёмник")]
    [SingleSelectApi("BusinessFunction,Name")]
    public int? ToID { get; set; }
 


    [Label("Свойства")]
    public virtual List<MessageProperty> Properties { get; set; }


    public MessageProtocol()
    {
    }

    public Func<object,object> GetFromBusinessFunction() => throw new NotImplementedException();
    


    public Func<object, object> GetToBusinessFunction() => throw new NotImplementedException();
    

    public string GetInputTableName()
    {
        return "[" + RusEngTranslite.TransliteToLatine(this.Name).ToUpper() + "]";
    }

    
}
