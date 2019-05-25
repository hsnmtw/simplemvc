using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonthlyStats.M {
    public class KeysModel : AbstractModel {

        public string ELM { get; set; }

        public override string GetSource() => "KEYS";
    }
}
