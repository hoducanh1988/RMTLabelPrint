using RemoteUEILabelPrint.Commands;
using RemoteUEILabelPrint.Models;
using RemoteUEILabelPrint.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using UtilityPack.IO;

namespace RemoteUEILabelPrint.ViewModels {
    public class SettingViewModel {

        public SettingViewModel() {
            //load setting file
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + myGlobal.setting_file) == false) _setting = new SettingModel();
            else _setting = XmlHelper<SettingModel>.FromXmlFile(AppDomain.CurrentDomain.BaseDirectory + myGlobal.setting_file);

            List<int> listNumber = new List<int>();
            for (int i = 1; i <= 10; i++) listNumber.Add(i);
            _collectionCopies = new CollectionView(listNumber);

            List<string> listPrinter = new List<string>();
            foreach (string printer in System.Drawing.Printing.PrinterSettings.InstalledPrinters) {
                listPrinter.Add(printer);
            }
            _collectionPrinter = new CollectionView(listPrinter);

            SaveCommand = new SettingSaveCommand(this);
            SelectLayout = new SettingSelectLayoutCommand(this);
        }

        //binding setting name
        SettingModel _setting;
        public SettingModel Setting {
            get { return _setting; }
        }

        //command
        public ICommand SaveCommand {
            get;
            private set;
        }
        public ICommand SelectLayout {
            get;
            private set;
        }

        //collection
        readonly CollectionView _collectionCopies;
        public CollectionView CollectionCopies {
            get { return _collectionCopies; }
        }

        readonly CollectionView _collectionPrinter;
        public CollectionView CollectionPrinter {
            get { return _collectionPrinter; }
        }

    }
}
