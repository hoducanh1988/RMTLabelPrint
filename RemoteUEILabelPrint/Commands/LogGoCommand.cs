using RemoteUEILabelPrint.Base;
using RemoteUEILabelPrint.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.IO;

namespace RemoteUEILabelPrint.Commands {

    public class LogGoCommand : ICommand {
        private LogViewModel lvm;
        public LogGoCommand(LogViewModel _lvm) {
            lvm = _lvm;
        }

        #region ICommand Members

        public event EventHandler CanExecuteChanged {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        //enable button save setting
        public bool CanExecute(object parameter) {
            return true;
        }

        //save setting
        public void Execute(object parameter) {
            switch(lvm) {
                case var a when lvm.Log.isSelectLog == true: {
                        string dir = $"{AppDomain.CurrentDomain.BaseDirectory}Log\\{myGlobal.loginViewModel.Login.scriptName.Replace(".xlsx", "").Replace(".xls", "").Trim()}";
                        if (Directory.Exists(dir) == false) Directory.CreateDirectory(dir);
                        Process.Start(dir);
                        break;
                    }
                case var a when lvm.Log.isSelectSetting == true: {
                        Process.Start(AppDomain.CurrentDomain.BaseDirectory);
                        break;
                    }
                case var a when lvm.Log.isSelectLayout == true: {
                        string dir = $"{AppDomain.CurrentDomain.BaseDirectory}Layouts";
                        if (Directory.Exists(dir) == false) Directory.CreateDirectory(dir);
                        Process.Start(dir);
                        break;
                    }
                case var a when lvm.Log.isSelectScript == true: {
                        string dir = $"{AppDomain.CurrentDomain.BaseDirectory}Scripts";
                        if (Directory.Exists(dir) == false) Directory.CreateDirectory(dir);
                        Process.Start(dir);
                        break;
                    }
                case var a when lvm.Log.isSelectReference == true: {
                        string dir = $"{AppDomain.CurrentDomain.BaseDirectory}References";
                        if (Directory.Exists(dir) == false) Directory.CreateDirectory(dir);
                        Process.Start(dir);
                        break;
                    }
                case var a when lvm.Log.isSelectLib == true: {
                        string dir = $"{AppDomain.CurrentDomain.BaseDirectory}Libs";
                        if (Directory.Exists(dir) == false) Directory.CreateDirectory(dir);
                        Process.Start(dir);
                        break;
                    }
                default: {
                        System.Windows.MessageBox.Show("Vui lòng chọn loại log cần truy vấn.", "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                        break;
                    }
            }
        }

        #endregion

    }
}
