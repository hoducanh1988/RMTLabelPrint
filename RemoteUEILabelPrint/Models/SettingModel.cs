using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteUEILabelPrint.Models {
    public class SettingModel : INotifyPropertyChanged {

        string _file_layout;
        public string fileLayout {
            get { return _file_layout; }
            set {
                _file_layout = value;
                OnPropertyChanged(nameof(fileLayout));
            }
        }
        string _printer_name;
        public string printerName {
            get { return _printer_name; }
            set {
                _printer_name = value;
                OnPropertyChanged(nameof(printerName));
            }
        }
        string _copies;
        public string Copies {
            get { return _copies; }
            set {
                _copies = value;
                OnPropertyChanged(nameof(Copies));
            }
        }

        public SettingModel() {
            fileLayout = "";
            printerName = "";
            Copies = "2";
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName) {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

    }
}
