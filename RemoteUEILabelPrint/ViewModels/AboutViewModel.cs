using RemoteUEILabelPrint.Base;
using RemoteUEILabelPrint.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteUEILabelPrint.ViewModels {

    public class AboutViewModel {

        public AboutViewModel() {
            if (myGlobal.mainViewModel.listAbout != null && myGlobal.mainViewModel.listAbout.Count > 0) {
                foreach (var item in myGlobal.mainViewModel.listAbout) {
                    _about.Add(new AboutModel() { ID = item.ID, Version = item.Version, Content = item.Content, Date = item.Date, ChangeType = item.changeType, Person = item.Person });
                }
            }
        }

        private ObservableCollection<AboutModel> _about = new ObservableCollection<AboutModel>();
        public ObservableCollection<AboutModel> About {
            get { return _about; }
            set { _about = value; }
        }
    }


}
