using RemoteUEILabelPrint.Commands;
using RemoteUEILabelPrint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using System.IO;

namespace RemoteUEILabelPrint.ViewModels {
    public class LoginViewModel {

        public LoginViewModel() {
            _login = new LoginModel();
            NextCommand = new LoginNextCommand(this);

            List<string> listScript = new List<string>();
            var dir = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + "Scripts");
            var fs = dir.GetFiles("*.xlsx");
            if (fs.Length > 0) {
                foreach (var f in fs) {
                    listScript.Add(f.Name);
                }
            }
            _collectionScripts = new CollectionView(listScript);
        }

        private LoginModel _login;
        public LoginModel Login {
            get => _login;
        }

        //command
        public ICommand NextCommand {
            get;
            private set;
        }

        //collection
        readonly CollectionView _collectionScripts;
        public CollectionView CollectionScripts {
            get { return _collectionScripts; }
        }

    }
}
