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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RemoteUEILabelPrint.Views {
    /// <summary>
    /// Interaction logic for LabelPrintView.xaml
    /// </summary>
    public partial class LabelPrintView : UserControl {
        public LabelPrintView() {
            InitializeComponent();
            this.DataContext = myGlobal.printViewModel;
            myGlobal.printViewModel.loadInputControll(this.sp_Input);
        }
    }
}
