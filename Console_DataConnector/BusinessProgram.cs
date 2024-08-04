
using Console_DataConnector.DataModule.DataODBC.Connectors;

using System;
using System.Linq;

 
public class BusinessProgram
{
    public static void Run(string[] args)
    {

        var manager = new OdbcDriverManager();        
        var odbc = new OdbcDataSource("ASpbMarketPlace", "root", "sgdf1423");
        odbc.EnsureIsValide();
        var dm = new OdbcDatabaseManager(odbc);
        dm.discovery();       
        dm.GetKeywords().ToJsonOnScreen().WriteToConsole();
        
    }
}
 