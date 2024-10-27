using System;
using System.Collections.Generic;
using System.Text;


[Label("Паралельность(периодичность)")]

public class BusinessGranularities : DictionaryTable 
{


    [UniqValue()]
    public string Code { get; set; }

}