using RemoteUEILabelPrint.Base;
using RemoteUEILabelPrint.Base.Function;
using RemoteUEILabelPrint.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RemoteUEILabelPrint.Views {
    /// <summary>
    /// Interaction logic for InputComboboxView.xaml
    /// </summary>
    public partial class InputComboboxView : UserControl {
        public MainWindowViewModel.InputDataItem idi = null;
        Task t = null;
        CancellationTokenSource tokenSource2;
        CancellationToken ct;

        ~InputComboboxView() {
            if (t != null) {
                tokenSource2.Cancel();
                tokenSource2.Dispose();
            }
        }

        public InputComboboxView(MainWindowViewModel.InputDataItem _idi) {
            InitializeComponent();
            idi = _idi;
            this.DataContext = idi;

            tokenSource2 = new CancellationTokenSource();
            ct = tokenSource2.Token;
            t = new Task(() => {
                ct.ThrowIfCancellationRequested();
                string o = idi.defaultValue;
            RE:
                string n = idi.defaultValue;
                if (o != n) {
                    o = n;
                    changeOtherItem(idi.variableName);
                }
                Thread.Sleep(100);

                if (ct.IsCancellationRequested) ct.ThrowIfCancellationRequested();
                goto RE;
            }, tokenSource2.Token);
            t.Start();
        }

        private void changeOtherItem(string name) {
            foreach (var item in myGlobal.mainViewModel.collectionInput) {
                if (item.actualValue.Contains($"!{name}")) {
                    if (item.actualValue.Contains("#")) {
                        //get method name
                        string method_name = item.actualValue.Split('(')[0].Replace("#", "").Trim();
                        //get parameters
                        string[] parameter_name = item.actualValue.Split('(')[1].Replace(")", "").Trim().Split(',');
                        List<object> list_params_value = new List<object>();
                        if (parameter_name == null || parameter_name.Length == 0) list_params_value = null;

                        for (int i = 0; i < parameter_name.Length; i++) {
                            string s = parameter_name[i];
                            if (s.Contains("!") == false) list_params_value.Add(s);
                            else {
                                string data = myGlobal.mainViewModel.collectionInput.Where(x => x.variableName.Equals(s.Replace("!", ""))).FirstOrDefault().defaultValue;
                                list_params_value.Add(data);
                            }
                        }
                        item.defaultValue = (string)myGlobal.libr.callMethod(method_name, list_params_value.ToArray());
                    }
                }
            }
        }
    }
}
