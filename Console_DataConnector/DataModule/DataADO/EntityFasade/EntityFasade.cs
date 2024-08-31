
using Console_DataConnector.DataModule.DataADO.ADOWebApiService;
using Console_DataConnector.DataModule.DataCommon.Metadata;
using DataCommon.DatabaseMetadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class EntityFasade: IEntityFasade
{ 
    private SqlServerWebApi _api;
    private Type _entity;
    private TableMetadata _metadata;

    public EntityFasade(SqlServerWebApi api, TableMetadata metadata, Type entity )
    {
        _api = api;
        _entity = entity;
        _metadata = metadata;
    }

    public override string ToString()
    {
        return _metadata.TableName.ToLower();
    }

    public int Count( ) 
    {
        return _api.GetSingleJObject($"SELECT COUNT(*) AS CNT FROM [{_metadata.TableSchema}].[{_metadata.TableName}]")["CNT"].ToString().ToInt();
    }

    public Task<TResultSet> Find<TResultSet>(int id) where TResultSet : class
    {
        return Task.Run(()=> {
            return _api.ExecuteQuery<TResultSet>($"SELECT * FROM [{_metadata.TableSchema}].[{_metadata.TableName}] WHERE {_metadata.PrimaryKey}={id}").FirstOrDefault();
        });
    }

    public Task<int> Delete(int id)
    {
        return Task.Run(() => {
            return _api.PrepareQuery($"DELETE FROM [{_metadata.TableSchema}].[{_metadata.TableName}] WHERE {_metadata.PrimaryKey}={id}");
        });
    }

    public async Task<int> Update(object model)
    {
        await Task.CompletedTask ;
        string Settings = $"";
            
        foreach (string key in _metadata.ColumnsMetadata.Keys)
        {
            if (key.ToLower()!="id")
            {
                string Constant = GetValueContant(GetValue(model, key), _metadata.ColumnsMetadata[key]);
                Settings += $"\t[{key}]={Constant},\n";
            }
        }
        if( Settings.EndsWith(",\n"))
        {
            Settings = Settings.Substring(0, Settings.Length - ",\n".Length) + "\n";
        }
        return _api.PrepareQuery($"UPDATE [{_metadata.TableSchema}].[{_metadata.TableName}]\n SET \n{Settings} WHERE {_metadata.PrimaryKey}={GetValue(model,_metadata.PrimaryKey)}");
      
    }

    private object GetValue(object model, string key)
    {
        
        return ReflectionService.GetValueFor(model, key);
    }

    public Task<int> Create(object model)
    {
        string Values = $"(";
        string Columns = $"(";
        foreach (string key in _metadata.ColumnsMetadata.Keys)
        {
            var f = IsUserInput(_metadata.ColumnsMetadata[key]);
            if (f)
            {
                var ckey = key.ToCapitalStyle();
                var val = model.GetValue(ckey);
                string Constant = GetValueContant(val, _metadata.ColumnsMetadata[key]);
                Columns += $"[{key}],";
                Values += $"{Constant},";
                this.Info(Columns);
                this.Info(Values);
            }                        
        }
        if (Columns.EndsWith(","))
        {
            Columns = Columns.Substring(0, Columns.Length - 1);
            Values = Values.Substring(0, Values.Length - 1);
        }
        Values += ")";
        Columns += ")";
        return Task.Run(() => {
            

            return _api.PrepareQuery($"INSERT INTO [{_metadata.TableSchema}].[{_metadata.TableName}] {Columns} VALUES {Values}");
        });
    }

    private bool IsUserInput(ColumnMetadata columnMetadata)
    {
        return columnMetadata.ColumnName != "Id";
    }

    /// <summary>
    /// Используется для передачи данных в SQL
    /// </summary>
    private string GetValueContant(object value, ColumnMetadata metadata)
    {
        if (value == null)
            return "NULL";
        switch(metadata.DataType.ToLower())
        {
            case "date":
            case "datetime":
                
                return ToDateString(value);
            

            //case "datetime2":
          //      break;
            case "ntext":
            case "text":

            case "nvarchar":
            case "nvarchar(max)":
            case "varchar":
                return value!=null?"'" + value   + "'": "";
            case "bit":
                return value is true ? "1" : "0";
            case "smallint":
            
            case "int":
            case "float":
                return "" + value.ToString().Replace(",", ".") + "";
            default: throw new NotImplementedException("Тип данных "+ metadata.DataType + " не поддерживается ");  
        }
    }

    private string ToDateString(object value)
    {
        string text = "'"+((DateTime)value).ToString("yyyy-MM-dd")+"'";
        return text;
    }

    public Task<object[]> GetAll()
    {
        return Task.Run(() => {
            return _api.ExecuteQuery($"SELECT * FROM [{_metadata.TableSchema}].[{_metadata.TableName}]",_entity).ToArray();
        });
    }

    public Task<object[]> Page(int page, int size)
    {
        throw new NotImplementedException();
    }

    public Task<object[]> Page(int page, int size, params string[] sorting)
    {
        throw new NotImplementedException();
    }

    public string GetEntityName()
    {
        return _metadata.TableName;
    }

    public Task<object> CreateNew()
    {
        return Task.Run(()=> {
            var record = _entity.New();
            Create(record).Wait();
            return record;
        });
    }

    public IEntityFasade<T> ToFasade<T>() where T : BaseEntity
    {
        return new EntityFasade<T>(this);
    }
}