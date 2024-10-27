using Console_DataConnector.DataModule.DataApi;
using Console_DataConnector.DataModule.DataODBC.Connectors;

using Newtonsoft.Json;

public class ApiOdbc
{
    public static void UseOdbc(WebApplication app)
    {
        var odbc = new OdbcSqlServerDataSource("DSN=" + "ASpbMarketPlace" + ";UId=" + "root" + ";PWD=" + "sgdf1423" + ";");
        odbc.EnsureIsValide();
        var dm = new OdbcDatabaseManager(odbc);

        string html = "";
        var tables = dm.GetTables();
        tables.Select(t => html += $@"<a href=""https://localhost:7243/{t}"">{t}</a>").ToList();
        app.MapGet($"/nav", () => html);
        app.MapGet($"/tables", () => JsonConvert.SerializeObject(dm.GetTables()));
        foreach (var table in tables)
        {
            MapCrud(app, dm, table);
            MapOneToMany(app, dm, dm.GetTableManager(table), table);
        }
    }

    private static void MapOneToMany(WebApplication app, OdbcDatabaseManager dm, ITableManager tm, string table)
    {
        //TODO: add foreight table to route
        app.MapGet($"/{table}" + "/{" + table + "Id:int}" + $"/{"CarsModels"}", (int id) => JsonConvert.SerializeObject(dm.GetTableManager("CarsModels").SelectAll().Where(item => id.ToString() == item.Value<string>("BrandId"))));
        app.MapPut($"/{table}" + "/{" + table + "Id:int}" + $"/{"CarsModels"}" + "/{json}", (string json) => tm.Update(JsonConvert.DeserializeObject<Dictionary<string, object>>(json)));
        app.MapPost($"/{table}" + "/{" + table + "Id:int}" + $"/{"CarsModels"}" + "/{json}", (string json) => tm.Create(JsonConvert.DeserializeObject<Dictionary<string, object>>(json)));
        app.MapDelete($"/{table}" + "/{" + table + "Id:int}" + $"/{"CarsModels"}" + "/{id:int}", (int id) => tm.Delete(id));
        app.MapGet($"/{table}" + "/{" + table + "Id:int}" + $"/{"CarsModels"}" + "/{id:int}", (int id) => JsonConvert.SerializeObject(tm.Select(id)));
    }

    private static void MapCrud(WebApplication app, OdbcDatabaseManager dm, string table)
    {
        try
        {
            var tm = dm.GetTableManager(table);

            app.MapGet($"/{table}", () => JsonConvert.SerializeObject(tm.SelectAll()));
            app.MapPut($"/{table}" + "/{json}", (string json) => tm.Update(JsonConvert.DeserializeObject<Dictionary<string, object>>(json)));
            app.MapPost($"/{table}" + "/{json}", (string json) => tm.Create(JsonConvert.DeserializeObject<Dictionary<string, object>>(json)));
            app.MapDelete($"/{table}" + "/{" + table + "Id:int}", (int id) => tm.Delete(id));
            app.MapGet($"/{table}" + "/{" + table + "Id:int}", (int id) => JsonConvert.SerializeObject(tm.Select(id)));
        }
        catch (Exception ex)
        {
            app.Error($"Исключение при обработке таблицы {table}: {ex.ToDocument()}");
        }
    }
}