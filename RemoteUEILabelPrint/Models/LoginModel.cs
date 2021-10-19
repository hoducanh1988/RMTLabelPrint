using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteUEILabelPrint.Models {
    public class LoginModel : INotifyPropertyChanged {

        public string appName {
            get => "Phần mềm in tem remote uei";
        }
        public string appInfo {
            get => "Version RMTUEIVN0U0001 - Build time 14/05/2021 17:00 - Copyright of VNPT Technology 2021";
            }
        string _script_name;
        public string scriptName {
            get { return _script_name; }
            set {
                _script_name = value;
                OnPropertyChanged(nameof(scriptName));
            }
        }

        public LoginModel() {
            scriptName = "";
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
