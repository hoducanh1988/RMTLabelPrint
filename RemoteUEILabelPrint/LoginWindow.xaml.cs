using RemoteUEILabelPrint.Base;
using RemoteUEILabelPrint.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;

namespace RemoteUEILabelPrint {
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window {
        string script_file = AppDomain.CurrentDomain.BaseDirectory + "Script.ini";

        public LoginWindow() {
            InitializeComponent();
            this.DataContext = myGlobal.loginViewModel;
            myGlobal.libr.addLibrary();
            myGlobal.libr.compilerSourceCode();
            myGlobal.libr.loadMethodFromLib();
            myGlobal.loginViewModel.Login.scriptName = File.Exists(script_file) ? File.ReadAllText(script_file) : "" ;
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            if (string.IsNullOrEmpty(myGlobal.loginViewModel.Login.scriptName) == false && string.IsNullOrWhiteSpace(myGlobal.loginViewModel.Login.scriptName) == false) {
                myGlobal.setting_file = $"setting_{myGlobal.loginViewModel.Login.scriptName.Replace(".xlsx", "").Replace(".xls", "").Trim()}.xml";
                myGlobal.mainViewModel = new MainWindowViewModel();
                myGlobal.settingViewModel = new SettingViewModel();
                myGlobal.printViewModel = new LabelPrintViewModel();
                myGlobal.mainViewModel.mainModel.stationName = $"Tên Script: {myGlobal.loginViewModel.Login.scriptName}";
                File.WriteAllText(script_file, myGlobal.loginViewModel.Login.scriptName);
                MainWindow mw = new MainWindow();
                mw.Show();
                this.Close();
            }
            else {
                MessageBox.Show("Vui lòng chọn loại tem trước.", "Lỗi chọn loại tem", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }
    }
}
