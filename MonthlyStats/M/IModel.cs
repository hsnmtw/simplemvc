using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonthlyStats.M {
    public interface IModel {
        int ID { get; set; }
        string GetSource ();
        void      Create ();
        DataTable Read   (params KeyValuePair<string, object>[] whereParameters);
        void      Update ();
        void      Delete ();
    }
}
