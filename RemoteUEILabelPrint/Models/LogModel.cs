using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteUEILabelPrint.Models {
    public class LogModel : INotifyPropertyChanged {

        public LogModel() {
            isSelectLog = false;
            isSelectSetting = false;
            isSelectLayout = false;
            isSelectScript = false;
            isSelectReference = false;
        }

        bool _is_select_log;
        public bool isSelectLog {
            get { return _is_select_log; }
            set {
                _is_select_log = value;
                OnPropertyChanged(nameof(isSelectLog));
            }
        }
        bool _is_select_setting;
        public bool isSelectSetting {
            get { return _is_select_setting; }
            set {
                _is_select_setting = value;
                OnPropertyChanged(nameof(isSelectSetting));
            }
        }
        bool _is_select_layout;
        public bool isSelectLayout {
            get { return _is_select_layout; }
            set {
                _is_select_layout = value;
                OnPropertyChanged(nameof(isSelectLayout));
            }
        }
        bool _is_select_script;
        public bool isSelectScript {
            get { return _is_select_script; }
            set {
                _is_select_script = value;
                OnPropertyChanged(nameof(isSelectScript));
            }
        }
        bool _is_select_reference;
        public bool isSelectReference {
            get { return _is_select_reference; }
            set {
                _is_select_reference = value;
                OnPropertyChanged(nameof(isSelectReference));
            }
        }
        bool _is_select_lib;
        public bool isSelectLib {
            get { return _is_select_lib; }
            set {
                _is_select_lib = value;
                OnPropertyChanged(nameof(isSelectLib));
            }
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
