using RemoteUEILabelPrint.Base.Function;
using RemoteUEILabelPrint.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteUEILabelPrint.Base {
    public class myGlobal {

        public static LoginViewModel loginViewModel = new LoginViewModel();
        public static MainWindowViewModel mainViewModel = null;
        public static SettingViewModel settingViewModel = null;
        public static LabelPrintViewModel printViewModel = null;
        public static LibraryHelper libr = new LibraryHelper(AppDomain.CurrentDomain.BaseDirectory + "Libs\\RemoteLibs.dll");
        public static string setting_file = "";
    }
}
