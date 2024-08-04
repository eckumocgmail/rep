using Console_DataConnector.DataModule.DataCommon.Metadata;
using System.Collections.Generic;

public static class DataInputExtensions
{

    public static Dictionary<string, string> Output(this ProcedureMetadata procmd, ref string[] args)
    {
        var arguments = new Dictionary<string, string>();
        foreach (var pmd in procmd.ParametersMetadata)
        {
            
            arguments[pmd.Key] = InputConsole.InputString($"Параметр {pmd.Key} типа {pmd.Value.DataType}",
                value => null, ref args);
        }

        return arguments;
    }
}