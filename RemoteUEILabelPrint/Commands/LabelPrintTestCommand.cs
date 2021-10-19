using RemoteUEILabelPrint.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using RemoteUEILabelPrint.Base;
using RemoteUEILabelPrint.Base.Function;

namespace RemoteUEILabelPrint.Commands {
    public class LabelPrintTestCommand : ICommand {

        private LabelPrintViewModel _lpvm;
        public LabelPrintTestCommand(LabelPrintViewModel lpvm) {
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
            try {
                if (myGlobal.mainViewModel.collectionOutput == null || myGlobal.mainViewModel.collectionOutput.Count == 0) return;
                List<BartenderHelper.ItemVariable> list_var = new List<BartenderHelper.ItemVariable>();

                foreach (var item in myGlobal.mainViewModel.collectionOutput) {
                    var iv = new BartenderHelper.ItemVariable() { Name = item.variableName, Value = item.defaultValue };
                    list_var.Add(iv);
                }

                var setting = myGlobal.settingViewModel.Setting;

                BartenderHelper btd = new BartenderHelper();
                btd.printLabel($"{AppDomain.CurrentDomain.BaseDirectory}Layouts\\{setting.fileLayout}", setting.printerName, "1", list_var.ToArray());
                btd.Dispose();
                System.Windows.MessageBox.Show("Success.", "Print Test", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
            } catch (Exception ex) {
                myGlobal.printViewModel.printModel.logSystem += $"{ex.ToString()}\n";
                System.Windows.MessageBox.Show($"Error.\n" + ex.Message.ToString() , "Print Test", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }

        }

        #endregion

    }
}
