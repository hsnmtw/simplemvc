using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonthlyStats.M;

namespace MonthlyStats.C {
    public class AbstractController : IController {
        public void Delete(IModel model) {
            model.Delete();
        }

        public DataTable Read(IModel model, params string[] whereParameters) {
            return model.Read((
                from string prop 
                in whereParameters 
                select new KeyValuePair<string,object>(prop,model.GetType().GetProperty(prop).GetValue(model))
                ).ToArray());
        }

        public void Save(IModel model) {
            if (model.ID == 0) model.Create();
            else model.Update();
        }
    }
}
