using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonthlyStats.M {
    public class ReadingDetailModel : AbstractModel {

        public int RD_ID { get; set; }
        public int ELM_ID { get; set; }
        public double RD_VALUE { get; set; }
        public override string GetSource() => "READINGS_DETAIL";

        public DataTable ReadView(params string[] whereParameters) {
            string sql = @"SELECT x.* FROM (SELECT a.ID,b.RD_DATE,c.ELM,a.RD_VALUE,a.RD_ID
                             FROM KEYS c,
                                  READINGS_MASTER b,
                                  READINGS_DETAIL a
                            WHERE a.RD_ID = b.ID
                              AND a.ELM_ID = c.ID) AS x";

            if (whereParameters.Length > 0) {
                sql = sql + $" WHERE {string.Join(" AND ", (from s in whereParameters select $"x.{s}=?").ToArray() )}";
            }

            return DB.MonthlyDBConnection.Instance.Read(sql, (
                from string prop
                in whereParameters
                select this.GetType().GetProperty(prop).GetValue(this)
                ).ToArray());
        }

    }
}
