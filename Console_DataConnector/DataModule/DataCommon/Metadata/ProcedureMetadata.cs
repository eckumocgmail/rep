using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Console_DataConnector.DataModule.DataCommon.Metadata
{
    public class ProcedureMetadata: MyValidatableObject
    {
        /// <summary>
        /// Квалификатор процедуры идентичен имени базы данных
        /// </summary>        
        [NotNullNotEmpty()]
        public string ProcedureQualifier { get; set; }

        /// <summary>
        /// Схема данных
        /// </summary>
        [NotNullNotEmpty()]
        public string ProcedureOwner { get; set; }

        /// <summary>
        /// Имя процедуры
        /// </summary>    
        [NotNullNotEmpty()]
        public string ProcedureName { get; set; }

        /// <summary>
        /// Полное наименование процедуры
        /// </summary>
        public string FullName { get => $"[{ProcedureQualifier}].[{ProcedureOwner}].[{ProcedureName}]"; }


        /// <summary>
        /// Сведения о параметрах
        /// </summary>
        public IDictionary<string, ParameterMetadata> ParametersMetadata { get; set; } = new Dictionary<string, ParameterMetadata>();




    }
}
