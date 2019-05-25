using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonthlyStats.DB {
    public interface IMonthlyDBConnection {
        void Execute(string sql,params object[] parameters);
        DataTable Read(string sql, params object[] parameters);

        void Open();
        void Close();
    }
}
