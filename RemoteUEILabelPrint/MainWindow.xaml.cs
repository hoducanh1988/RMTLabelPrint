using RemoteUEILabelPrint.ViewModels;
using RemoteUEILabelPrint.Views;
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
using RemoteUEILabelPrint.Base;

namespace RemoteUEILabelPrint {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        Task t = null;
        CancellationTokenSource tokenSource2;
        CancellationToken ct;

        AboutView av;
        HelpView hv;
        LogView lv;
        SettingView sv;
        LabelPrintView lpv;

        public MainWindow() {
            InitializeComponent();
            this.DataContext = myGlobal.mainViewModel;
            myGlobal.mainViewModel.loadScriptData();

            tokenSource2 = new CancellationTokenSource();
            ct = tokenSource2.Token;
            t = new Task(() => {
                ct.ThrowIfCancellationRequested();
            RE:
                Thread.Sleep(250);
                foreach (var item in myGlobal.mainViewModel.listItemViewModel) {
                    if (item.selectItem.isClicked) {
                        var controller = GetItemClicked(item.selectItem.itemName);
                        this.Dispatcher.Invoke(new Action(() => {
                            if (this.grid_Main.Children.Contains(controller)) return;
                            this.grid_Main.Children.Clear();
                            this.grid_Main.Children.Add(controller);
                        }));
                    }
                }
                if (ct.IsCancellationRequested) ct.ThrowIfCancellationRequested();
                goto RE;
            }, tokenSource2.Token);
            t.Start();

            av = new AboutView();
            hv = new HelpView();
            lv = new LogView();
            sv = new SettingView();
            lpv = new LabelPrintView();

            myGlobal.mainViewModel.addItemController(this.sp_Controller);
        }


        private UserControl GetItemClicked(string item_name) {
            switch (item_name) {
                case "In tem": return lpv;
                case "Cài đặt tham số": return sv;
                case "Truy vấn dữ liệu": return lv;
                case "Hướng dẫn sử dụng": return hv;
                case "Thông tin phần mềm": return av;
                default: return null;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
            if (t != null) {
                tokenSource2.Cancel();
                tokenSource2.Dispose();
            }
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e) {
            if (e.LeftButton == MouseButtonState.Pressed) {
                this.DragMove();
            }
        }

        private void Label_MouseDown(object sender, MouseButtonEventArgs e) {
            if (e.LeftButton == MouseButtonState.Pressed) {
                this.Close();
            }
        }


    }
}
