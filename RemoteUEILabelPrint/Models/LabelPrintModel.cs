using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteUEILabelPrint.Models {
    public class LabelPrintModel : INotifyPropertyChanged {

        string _log_system;
        public string logSystem {
            get { return _log_system; }
            set {
                _log_system = value;
                OnPropertyChanged(nameof(logSystem));
            }
        }
        int _copies_value;
        public int copiesValue {
            get { return _copies_value; }
            set {
                _copies_value = value;
                OnPropertyChanged(nameof(copiesValue));
            }
        }
        int _copies_max;
        public int copiesMax {
            get { return _copies_max; }
            set {
                _copies_max = value;
                OnPropertyChanged(nameof(copiesMax));
            }
        }
        int _pd_value;
        public int pdValue {
            get { return _pd_value; }
            set {
                _pd_value = value;
                OnPropertyChanged(nameof(pdValue));
            }
        }
        int _pd_max;
        public int pdMax {
            get { return _pd_max; }
            set {
                _pd_max = value;
                OnPropertyChanged(nameof(pdMax));
            }
        }
        string _btn_print_content;
        public string btnPrintContent {
            get { return _btn_print_content; }
            set {
                _btn_print_content = value;
                OnPropertyChanged(nameof(btnPrintContent));
            }
        }
        string _total_time;
        public string totalTime {
            get { return _total_time; }
            set {
                _total_time = value;
                OnPropertyChanged(nameof(totalTime));
            }
        }
        string _total_result;
        public string totalResult {
            get { return _total_result; }
            set {
                _total_result = value;
                OnPropertyChanged(nameof(totalResult));
            }
        }


        public LabelPrintModel() {
            logSystem = "";
            copiesValue = 0; copiesMax = 1;
            pdValue = 0; pdMax = 1;
            btnPrintContent = "Print";
            totalTime = "00:00:00";
            totalResult = "--";
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
