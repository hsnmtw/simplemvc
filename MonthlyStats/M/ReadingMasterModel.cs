using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonthlyStats.M {
    public class ReadingMasterModel : AbstractModel {

        public DateTime? RD_DATE { get; set; }

        public override string GetSource() => "READINGS_MASTER";
    }
}
