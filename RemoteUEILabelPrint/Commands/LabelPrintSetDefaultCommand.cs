using RemoteUEILabelPrint.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using RemoteUEILabelPrint.Base;

namespace RemoteUEILabelPrint.Commands {
    public class LabelPrintSetDefaultCommand : ICommand {

        private LabelPrintViewModel _lpvm;
        public LabelPrintSetDefaultCommand(LabelPrintViewModel lpvm) {
            _lpvm = lpvm;
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
            if (myGlobal.mainViewModel.collectionInput == null || myGlobal.mainViewModel.collectionInput.Count == 0) return;
            foreach (var item in myGlobal.mainViewModel.collectionInput) {
                item.defaultValue = "";
            }
            System.Windows.MessageBox.Show("Success.", "Set Default", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
        }

        #endregion

    }
}
