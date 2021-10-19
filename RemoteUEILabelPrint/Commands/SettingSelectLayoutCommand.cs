using RemoteUEILabelPrint.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Win32;

namespace RemoteUEILabelPrint.Commands {
    public class SettingSelectLayoutCommand : ICommand {

        private SettingViewModel _svm;
        public SettingSelectLayoutCommand(SettingViewModel svm) {
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
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory + "Layouts";
            openFileDialog.Filter = "*.btw|*.btw";

            if (openFileDialog.ShowDialog() == true) {
                _svm.Setting.fileLayout = openFileDialog.SafeFileName;
            }
        }

        #endregion

    }
}
