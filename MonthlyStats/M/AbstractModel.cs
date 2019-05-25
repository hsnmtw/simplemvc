using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonthlyStats.M {
    public abstract class AbstractModel : IModel {

        private MonthlyStats.DB.IMonthlyDBConnection dbc;

        public AbstractModel() {
            this.dbc = MonthlyStats.DB.MonthlyDBConnection.Instance;
        }

        public int ID { get; set; }

        public void Create() {
            var source = GetSource();
            var columns = (from col in this.GetType().GetProperties() where !col.Name.ToUpper().Equals("ID") select $"{col.Name}");
            var values = (from col in columns select '?');
            string sql = string.Format("INSERT INTO [{0}] ({1}) VALUES ({2})",source,string.Join(",",columns),string.Join(",",values));
            object[] parameters = (from col in columns select this.GetType().GetProperty(col).GetValue(this)).ToArray();
            dbc.Execute(sql, parameters);
        }

        public void Delete() {
            var source = GetSource();
            var columns = "ID";
            var values = "?";
            string sql = string.Format("DELETE FROM [{0}] WHERE {1}={2}", source,  columns, values);
            object[] parameters = { ID };
            dbc.Execute(sql, parameters);
        }

        public abstract string GetSource();

        public DataTable Read(params KeyValuePair<string, object>[] whereParameters) {
            if (whereParameters.Count() == 0) return dbc.Read($"SELECT * FROM {GetSource()}");

            var source = GetSource();
            var columns = (from col in whereParameters select $"[{col.Key}]=?");
            var values = (from col in columns select '?');
            string sql = string.Format("SELECT * FROM [{0}] WHERE ({1})", source, string.Join(",", columns), string.Join(",", values));
            object[] parameters = (from col in whereParameters select col.Value).ToArray();
            return dbc.Read(sql, parameters);
        }

        public void Update() {
            var source = GetSource();
            var columns = (from col in this.GetType().GetProperties() where !col.Name.ToUpper().Equals("ID") select $"{col.Name}");
            var colval = (from col in columns select $"{col}=?");
            string sql = string.Format("UPDATE [{0}] SET {1} WHERE ID=?", source, string.Join(",",colval));
            object[] parameters = (from col in columns select this.GetType().GetProperty(col).GetValue(this)).Concat(new object[] { ID }).ToArray();
            dbc.Execute(sql, parameters);
        }
    }
}
