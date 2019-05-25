using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonthlyStats.C {
    public static class ControllersFactory {
        public static IController GetController(Controllers controller) {
            switch (controller) {
                case Controllers.KEYS           : return new KeysController();
                case Controllers.READING_DETAIL : return new ReadingDetailController();
                case Controllers.READING_MASTER  : return new ReadingMasterController();
                default                         : return null;

            }
        }
    }
}
