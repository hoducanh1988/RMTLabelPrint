using RemoteUEILabelPrint.Base;
using RemoteUEILabelPrint.Models;
using RemoteUEILabelPrint.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UtilityPack.IO;

namespace RemoteUEILabelPrint.Commands {
    public class SettingSaveCommand : ICommand {

        private SettingViewModel _svm;
        public SettingSaveCommand(SettingViewModel svm) {
            _svm = svm;
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
            XmlHelper<SettingModel>.ToXmlFile(_svm.Setting, AppDomain.CurrentDomain.BaseDirectory + myGlobal.setting_file);
            System.Windows.MessageBox.Show("Sucess!", "Save Setting", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
        }

        #endregion
    }
}
