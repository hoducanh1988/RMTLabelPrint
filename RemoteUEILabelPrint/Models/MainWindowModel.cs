using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteUEILabelPrint.Models {
    public class MainWindowModel : INotifyPropertyChanged {

        public string appName {
            get => "Phần mềm in tem cho sản phẩm Remote UEI";
        }
        private string _station;
        public string stationName {
            get { return _station;  }
            set {
                _station = value;
                OnPropertyChanged(nameof(stationName));
            }
        }
        private string _app_version;
        public string appVersion {
            get { return _app_version; }
            set {
                _app_version = value;
                OnPropertyChanged(nameof(appVersion));
            }
        }
        private string _app_buildtime;
        public string appBuildTime {
            get { return _app_buildtime; }
            set {
                _app_buildtime = value;
                OnPropertyChanged(nameof(appBuildTime));
            }
        }
        public string appCopyRight {
            get => "Copyright Of VNPT Technology 2021";
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
