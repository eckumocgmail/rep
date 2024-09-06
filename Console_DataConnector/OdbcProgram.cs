using Console_DataConnector.DataModule.DataODBC.Connectors;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_DataConnector
{
    [Label("Система управления ODBC")]
    public class OdbcProgram
    {
        private OdbcDriverManager driverManager = new();
        private OdbcDataSource odbcDatasource = new OdbcDataSource("ASpbMarketPlace", "root", "sgdf1423");
        private OdbcDatabaseManager odbcManager = new OdbcDatabaseManager(new OdbcDataSource("ASpbMarketPlace", "root", "sgdf1423"));

        [Label("Меню управления драйверами ODBC")]
        public void StartOdbcDriverManagerMenu() 
            => ConsoleProgram<OdbcDriverManager>.RunInteractive(driverManager);
 
        [Label("Меню управления ODBC источником")]
        public void StartOdbcDatasourceMenu(string datasource, string username, string password)
            => ConsoleProgram<OdbcDataSource>.RunInteractive(new OdbcDataSource(datasource, username, password));
       
        [Label("Выполнение программы")]
        public static void Run(ref string[] args)
        {             
            ConsoleProgram<OdbcProgram>.RunInteractive<OdbcProgram>(ref args);
        }
    }
}
