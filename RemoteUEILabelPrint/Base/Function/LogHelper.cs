using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RemoteUEILabelPrint.Base.Function {
    public class LogHelper {

        string dir = AppDomain.CurrentDomain.BaseDirectory + "Log";
        string dir_total = "", file_total = "", file_sys = "";

        public LogHelper() {
            dir_total = $"{dir}\\{myGlobal.loginViewModel.Login.scriptName.Replace(".xlsx", "").Replace(".xls", "").Trim()}";
            if (Directory.Exists(dir_total) == false) Directory.CreateDirectory(dir_total);
        }

        public bool saveLogTotal(bool result) {
            try {
                file_total = $"{dir_total}\\{DateTime.Now.ToString("yyyy-MM-dd")}.csv";
                string file_title = "DateTimeCreated";
                string file_content = $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}";

                foreach (var item in myGlobal.mainViewModel.collectionOutput) {
                    file_title += $",\"{item.variableName}\"";
                    file_content += $",\"{item.defaultValue}\"";
                }

                file_title += $",\"Result\"";
                file_content += $",\"{(result ? "Passed" : "Failed" )}\"";

                bool r = File.Exists(file_total);
                using (var sw = new StreamWriter(file_total, true, Encoding.Unicode)) {
                    if (!r) sw.WriteLine(file_title);
                    sw.WriteLine(file_content);
                }

                return true;
            } 
            catch  (Exception ex) {
                myGlobal.printViewModel.printModel.logSystem += $"{ex.ToString()}\n";
                return false; }
        }

        public bool saveLogDetail() {
            try {
                file_sys = $"{dir_total}\\{DateTime.Now.ToString("yyyy-MM-dd")}.txt";
                using (var sw = new StreamWriter(file_sys, true, Encoding.Unicode)) {
                    sw.WriteLine(myGlobal.printViewModel.printModel.logSystem);
                }
                return true;
            } catch  (Exception ex) {
                myGlobal.printViewModel.printModel.logSystem += $"{ex.ToString()}\n";
                return false; }
        }


    }
}
