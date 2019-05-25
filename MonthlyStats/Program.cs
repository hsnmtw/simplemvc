using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MonthlyStats {
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {

            new Utils.ConfigLoader();

            CultureInfo cultureInfo = CultureInfo.CreateSpecificCulture(Utils.ConfigLoader.CultureInfoGlobalization);

            cultureInfo.DateTimeFormat.ShortDatePattern = Utils.ConfigLoader.CultureInfoDateTimeFormatShortDatePattern;
            cultureInfo.DateTimeFormat.LongDatePattern = Utils.ConfigLoader.CultureInfoDateTimeFormatLongDatePattern;
            cultureInfo.DateTimeFormat.DateSeparator = Utils.ConfigLoader.CultureInfoDateTimeFormatDateSeparator;
            cultureInfo.DateTimeFormat.ShortTimePattern = Utils.ConfigLoader.CultureInfoDateTimeFormatShortTimePattern;
            cultureInfo.DateTimeFormat.LongTimePattern = Utils.ConfigLoader.CultureInfoDateTimeFormatLongTimePattern;
            cultureInfo.DateTimeFormat.TimeSeparator = Utils.ConfigLoader.CultureInfoDateTimeFormatTimeSeparator;


            cultureInfo.DateTimeFormat.Calendar = new GregorianCalendar();

            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;
            Application.CurrentCulture = cultureInfo;



            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Main());
        }
    }
}
