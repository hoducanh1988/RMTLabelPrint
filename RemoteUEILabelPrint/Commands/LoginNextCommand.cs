using RemoteUEILabelPrint.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RemoteUEILabelPrint.Commands {
    public class LoginNextCommand : ICommand {
        private LoginViewModel _lvm;
        public LoginNextCommand(LoginViewModel lvm) {
            _lvm = lvm;
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
           
        }

        #endregion

    }
}
