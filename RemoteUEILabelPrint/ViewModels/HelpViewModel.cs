using RemoteUEILabelPrint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteUEILabelPrint.ViewModels {
    public class HelpViewModel {

        public HelpViewModel() {
            _help = new HelpModel();
        }

        //binding setting name
        HelpModel _help;
        public HelpModel Help {
            get { return _help; }
        }

    }
}
