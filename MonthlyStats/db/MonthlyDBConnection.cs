using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using System.Data;


namespace MonthlyStats.DB {
    public class MonthlyDBConnection : IMonthlyDBConnection {

        private DbProviderFactory dbProviderFactory;
        private DbConnectionStringBuilder csBuilder;
        private DbConnection connection;

        private static IMonthlyDBConnection instance = null;

        public static IMonthlyDBConnection Instance {
            get {
                if (instance == null) { instance = new MonthlyDBConnection(); }
                return instance;
            }
        }

        private MonthlyDBConnection() {
            dbProviderFactory = DbProviderFactories.GetFactory("System.Data.OleDb");
            csBuilder = new DbConnectionStringBuilder();
            csBuilder["Data Source"] = @"C:\Users\96650\source\repos\MonthlyStats\MonthlyStats\db\db.mdb";
            csBuilder["User Id"] = "Admin";
            csBuilder["Password"] = "";
            csBuilder["Provider"] = "Microsoft.Jet.OLEDB.4.0";
            connection = dbProviderFactory.CreateConnection();
            connection.ConnectionString = csBuilder.ConnectionString;
        }

        public void Close() {
            connection.Close();
        }

        public void Execute(string sql, params object[] parameters) {
            if (connection.State != ConnectionState.Open) { Open(); }
            var cmd = dbProviderFactory.CreateCommand();
            cmd.Connection = connection;
            cmd.CommandText = sql;
            if (parameters.Length > 0) {
                foreach (var par in parameters) {
                    var parameter = dbProviderFactory.CreateParameter();
                    parameter.Value = par;
                    cmd.Parameters.Add(parameter);
                }
                //cmd.Prepare();
            }
            cmd.ExecuteNonQuery();
        }

        public void Open() {
            connection.Open();
        }

        public DataTable Read(string sql, params object[] parameters) {
            if (connection.State != ConnectionState.Open) { Open(); }
            var cmd = dbProviderFactory.CreateCommand();
            cmd.Connection = connection;
            cmd.CommandText = sql;
            if (parameters.Length > 0) {
                foreach (var par in parameters) {
                    var parameter = dbProviderFactory.CreateParameter();
                    parameter.Value = par;
                    cmd.Parameters.Add(parameter);
                }
                //cmd.Prepare();
            }
            var table = new DataTable();
            using (var adaptor = dbProviderFactory.CreateDataAdapter()) {
                adaptor.SelectCommand = cmd;
                adaptor.Fill(table);
            }
            return table;
        }
    }
}
