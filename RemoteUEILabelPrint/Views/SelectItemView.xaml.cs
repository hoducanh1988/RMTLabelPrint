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
using RemoteUEILabelPrint.ViewModels;

namespace RemoteUEILabelPrint.Views {
    /// <summary>
    /// Interaction logic for SelectItemView.xaml
    /// </summary>
    public partial class SelectItemView : UserControl {
        SelectItemViewModel sivm;

        public SelectItemView(SelectItemViewModel _sivm) {
            InitializeComponent();
            sivm = _sivm;
            this.DataContext = sivm;
            this.Icon.Source = new BitmapImage(new Uri(sivm.selectItem.imagePath, UriKind.Absolute));
        }

    }
}
