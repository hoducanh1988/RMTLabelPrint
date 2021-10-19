using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using RemoteUEILabelPrint.Commands;
using RemoteUEILabelPrint.Models;

namespace RemoteUEILabelPrint.ViewModels {
    public class SelectItemViewModel {
        
        public SelectItemViewModel() {
            _sim = new SelectItemModel();
            Click = new SelectItemClickCommand(this);
        }

        private SelectItemModel _sim;
        public SelectItemModel selectItem {
            get { return _sim; }
        }

        #region commander

        public ICommand Click {
            get;
            private set;
        }

        #endregion
    }
}
