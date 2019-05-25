using MonthlyStats.M;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonthlyStats.C {
    public class ReadingDetailController : AbstractController {
        public DataTable ReadView(ReadingDetailModel model, params string[] whereParameters) {
            return model.ReadView(whereParameters);
        }
    }
}
