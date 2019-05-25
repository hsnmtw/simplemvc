using MonthlyStats.M;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonthlyStats.C {
    public interface IController {
        void       Save  (IModel model);
        DataTable  Read  (IModel model,params string[] whereParameters);
        void       Delete(IModel model);
    }
}
