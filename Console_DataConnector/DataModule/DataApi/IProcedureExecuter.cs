using Console_DataConnector.DataModule.DataCommon.Metadata;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_DataConnector.DataModule.DataApi
{
    public interface IProcedureExecuter
    {
        public string GetName();
        public string GetDescription();
        public ProcedureMetadata GetMetaData();
        public Dictionary<string, ParameterMetadata> GetInputMetaData();
        public Dictionary<string, ParameterMetadata> GetOutputMetaData();
        public Func<Dictionary<string, object>, object> GetExecFunc<TResult>() where TResult : class;

    }
}
