using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteUEILabelPrint.Models {
    public class SelectItemModel : INotifyPropertyChanged {

        string _img_path;
        public string imagePath {
            get { return _img_path; }
            set {
                _img_path = value;
                OnPropertyChanged(nameof(imagePath));
            }
        }
        string _item_name;
        public string itemName {
            get { return _item_name; }
            set {
                _item_name = value;
                OnPropertyChanged(nameof(itemName));
            }
        }
        bool _is_clicked;
        public bool isClicked {
            get { return _is_clicked; }
            set {
                _is_clicked = value;
                OnPropertyChanged(nameof(isClicked));
            }
        }

        public SelectItemModel() {
            imagePath = "";
            itemName = "";
            isClicked = false;
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
