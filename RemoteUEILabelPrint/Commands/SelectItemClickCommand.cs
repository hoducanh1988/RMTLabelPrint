using RemoteUEILabelPrint.Base;
using RemoteUEILabelPrint.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RemoteUEILabelPrint.Commands {
    public class SelectItemClickCommand : ICommand {

        private SelectItemViewModel _sivm;
        public SelectItemClickCommand(SelectItemViewModel sivm) {
            _sivm = sivm;
        }

        #region ICommand Members

        public event EventHandler CanExecuteChanged {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter) {
            return true;
        }

        public void Execute(object parameter) {
            foreach (var item in myGlobal.mainViewModel.listItemViewModel) {
                item.selectItem.isClicked = false;
            }
            _sivm.selectItem.isClicked = true;
        }

        #endregion
    }
}
