using AngleSharp.Dom;

using Console_DataConnector.DataModule.DataADO.ADODbMigBuilderService;
using Console_DataConnector.DataModule.DataApi;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Console_DataConnector.DataModule.DataADO.ADOWebApiService
{
    public class SqlServerWebApi : SqlServerMigBuilder, IWebApi
    {
        public HashSet<IEntityFasade> Services { get; set; } = new();
        public HashSet<IProcedureExecuter> Procedures { get; set; } = new();

        public SqlServerWebApi(): base()  
        {
            Services = new HashSet<IEntityFasade>();
            Init();
        }

        public SqlServerWebApi(string server, string database) : base(server, database)
        {
            Services = new HashSet<IEntityFasade>();
            Init();
        }

        public SqlServerWebApi(string server, string database, bool trustedConnection, string userID, string password) : base(server, database, trustedConnection, userID, password)
        {
            Services = new HashSet<IEntityFasade>();
            Init();
        }

        protected void Init()
        {
            foreach (var tmd in GetTablesMetadata().Values)
            {
                var tableName = tmd.TableName.ToSingleCount().ToLower();
                this.Info(tableName);
                this.Info(this.EntityTypes.Select(entity => entity.GetTypeName().ToLower().ToSingleCount()).ToJsonOnScreen());
                this.EntityTypes.First(entity => entity.GetTypeName().ToLower().ToSingleCount().Equals(tableName));
                Services.Add(new global::EntityFasade(this, tmd, typeof(BaseEntity)));
            }
            foreach(var pmd in GetProceduresMetadata())
            {
                Procedures.Add(new ProcedureExecuter(this, pmd.Value ));
            }
        }

        public async Task<Tuple<int, object>> Request(string url, Dictionary<string, string> args=null, Dictionary<string, string> headers = null, byte[] body=null)
        {
            this.Info(url);
            await Task.CompletedTask;
            var ids = url.Split('/').ToList();
            ids.RemoveAt(0);
            object p = new List<object>(Services);
            ((List<object>)p).AddRange(Procedures);
            var success = new List<string>();
            if(ids.Count()==0)
            {
                return new Tuple<int, object>(404, new
                {
                    success = success,
                    args = args,
                    available = ((List<object>)p).Select(p => p.ToString())
                }); 
            }
            foreach (var id in ids) 
            {
             
                bool completed = false;
                List<string> available = new();
                if(id.IsInt())
                {
                    success.Add(id);
                    p = await ((global::EntityFasade)p).Find<Dictionary<string,object>>(id.ToInt());
                    completed = true;
                    break;
                } 
                else
                if( Typing.IsCollectionType(p.GetType()) )
                {
                    available = new();
                    foreach (var item in (IEnumerable)p)
                    {
                        available.Add(item.ToString().ToLower());
                        if (item.ToString().ToLower() == id.ToLower())
                        {
                            success.Add(id);
                            p = item;
                            completed = true;
                            break;
                        }
                    }
                } 
                else
                {
                    available = new();
                    available.AddRange(p.GetType().GetProperties().Select(p => p.Name));
                    foreach (var prop in p.GetType().GetProperties())
                    {                        
                        if (prop.Name.ToString().ToLower() == id.ToLower())
                        {
                            success.Add(id);
                            p = prop.GetValue(p);
                            completed = true;
                            continue;
                        }
                    }
                    available.AddRange(p.GetType().GetMethods().Select(p => p.Name));
                    foreach (var prop in p.GetType().GetMethods())
                    {
                        if (prop.Name.ToString().ToLower() == id.ToLower())
                        {
                            success.Add(id);
                            Func<Dictionary<string,object>,object> ap = (parameters) => {                                                                
                                return prop.Invoke(p, parameters.Values.ToArray());
                            };
                            completed = true;
                            p = ap;
                            break;
                        }
                    }
                }
                if (completed)
                    continue;
                return new Tuple<int, object>(404, new { 
                    success = success,
                    failed = id,
                    args = args,

                    available = available
                });
            }
            if(p is IProcedureExecuter)
            {
                p = ((IProcedureExecuter)p).GetExecFunc<Dictionary<string,object>>();
            }
            if(p is Func<Dictionary<string, object>, object>)
            {
                var argsv = new Dictionary<string, object>(args.Select(kv => new KeyValuePair<string, object>(kv.Key, kv.Value)));
                try
                {                    
                    var exec = ((Func<Dictionary<string, object>, object>)p);
                    var result = exec(argsv);
                    return new Tuple<int, object>(200, new
                    {
                        success = success,
                        input = argsv,
                        data = result
                    });
                }
                catch (ArgumentException ex)
                {
                    return new Tuple<int, object>(422, new
                    {
                        success = success,
                        param = ex.ParamName,
                        args = args,
                        message = ex.Message
                    });
                }
                catch (Exception ex)
                {
                    return new Tuple<int, object>(500, new
                    {
                        args = args,
                        success = success,
                        message = ex.Message
                    });
                }
            }
            else if( p is global::EntityFasade)
            {
                var result = ((global::EntityFasade)p).GetAll().Result;                
                return new Tuple<int, object>(200, new
                {
                    args = args,
                    success = success,                    
                    data = result 
                });
            }
            else
            {
                return new Tuple<int, object>(404, new
                {
                    args = args,
                    success = success,
                    data = p
                });
            }
            return new Tuple<int, object>(200, p);
        }

        public bool match(string url, Dictionary<string, string> queryParams)
        {
            throw new NotImplementedException();
        }

        public bool can(string url, Dictionary<string, string> queryParams, Dictionary<string, string> headers)
        {
            throw new NotImplementedException();
        }
    }
}
