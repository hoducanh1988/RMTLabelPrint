using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using RemoteUEILabelPrint.Models;
using RemoteUEILabelPrint.Views;
using RemoteUEILabelPrint.Commands;
using RemoteUEILabelPrint.Base.Function;
using System.Data;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Data;
using RemoteUEILabelPrint.Base;

namespace RemoteUEILabelPrint.ViewModels {
    public class MainWindowViewModel {

        public MainWindowViewModel() {
            _mwm = new MainWindowModel();
        }

        MainWindowModel _mwm;
        public MainWindowModel mainModel {
            get => _mwm;
        }

        #region commander

        #endregion


        #region load item controller

        class Item {
            public Item(string n, string m) {
                name = n;
                image_path = m;
            }
            public string name { get; set; }
            public string image_path { get; set; }
        }

        private List<Item> Items = new List<Item>() {
                new Item ("In tem", @"pack://application:,,,/Assets/Image/print.png"),
                new Item("Cài đặt tham số", @"pack://application:,,,/Assets/Image/setting.png"),
                new Item("Truy vấn dữ liệu", @"pack://application:,,,/Assets/Image/log.png"),
                new Item("Hướng dẫn sử dụng", @"pack://application:,,,/Assets/Image/help.png"),
                new Item("Thông tin phần mềm", @"pack://application:,,,/Assets/Image/about.png")
            };
        public List<SelectItemViewModel> listItemViewModel = new List<SelectItemViewModel>();
        public void addItemController(StackPanel sp_controller) {
            sp_controller.Children.Clear();
            listItemViewModel.Clear();
            foreach (var it in Items) {
                SelectItemViewModel sivm = new SelectItemViewModel();
                sivm.selectItem.itemName = it.name;
                sivm.selectItem.imagePath = it.image_path;
                listItemViewModel.Add(sivm);
                SelectItemView siv = new SelectItemView(sivm);
                sp_controller.Children.Add(siv);
            }
        }

        #endregion

        #region load script file excel

        private DataTable tableInput = null, tableOutput = null, tableAbout = null;
        public bool loadScriptData() {
            string f = $"{AppDomain.CurrentDomain.BaseDirectory}Scripts\\{myGlobal.loginViewModel.Login.scriptName}";
            tableInput = new ExcelHelper(f).readFile("Input", "A1:G");
            tableOutput = new ExcelHelper(f).readFile("Output", "A1:D");
            tableAbout = new ExcelHelper(f).readFile("About", "A1:F");

            if (tableInput != null) convertInputData();
            if (tableOutput != null) convertOutputData();
            if (tableAbout != null) convertAboutData();

            if (tableInput == null || tableOutput == null || tableAbout == null) return false;
            else return true;
        }

        #endregion

        #region Convert DataTable to Object

        public class InputDataItem : INotifyPropertyChanged {

            public InputDataItem() {
                variableName = variableType = formatData = inputType = referenceFile = defaultValue = actualValue = "";
                isEnable = true;
            }

            string _vr_name;
            public string variableName {
                get { return _vr_name; }
                set {
                    _vr_name = value;
                    OnPropertyChanged(nameof(variableName));
                }
            }
            string _vr_type;
            public string variableType {
                get { return _vr_type; }
                set {
                    _vr_type = value;
                    OnPropertyChanged(nameof(variableType));
                }
            }
            string _fm_data;
            public string formatData {
                get { return _fm_data; }
                set {
                    _fm_data = value;
                    OnPropertyChanged(nameof(formatData));
                }
            }
            string _inp_type;
            public string inputType {
                get { return _inp_type; }
                set {
                    _inp_type = value;
                    OnPropertyChanged(nameof(inputType));
                }
            }
            string _ref_file;
            public string referenceFile {
                get { return _ref_file; }
                set {
                    _ref_file = value;
                    OnPropertyChanged(nameof(referenceFile));
                }
            }
            string _def_value;
            public string defaultValue {
                get { return _def_value; }
                set {
                    _def_value = value;
                    OnPropertyChanged(nameof(defaultValue));
                }
            }
            string act_value;
            public string actualValue {
                get { return act_value; }
                set {
                    act_value = value;
                    OnPropertyChanged(nameof(actualValue));
                }
            }
            CollectionView _collectionData;
            public CollectionView collectionData {
                get { return _collectionData; }
                set {
                    _collectionData = value;
                    OnPropertyChanged(nameof(collectionData));
                }
            }
            bool _is_enable;
            public bool isEnable {
                get { return _is_enable; }
                set {
                    _is_enable = value;
                    OnPropertyChanged(nameof(isEnable));
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
        public ObservableCollection<InputDataItem> collectionInput = null;
        void convertInputData() {
            if (tableInput.Rows.Count == 0) return;
            collectionInput = new ObservableCollection<InputDataItem>();
            for (int i = 0; i < tableInput.Rows.Count; i++) {
                if (string.IsNullOrEmpty(tableInput.Rows[i].ItemArray[0].ToString()) == false && string.IsNullOrWhiteSpace(tableInput.Rows[i].ItemArray[0].ToString()) == false) {
                    InputDataItem item = new InputDataItem() {
                        variableName = tableInput.Rows[i].ItemArray[0].ToString(),
                        variableType = tableInput.Rows[i].ItemArray[1].ToString(),
                        formatData = tableInput.Rows[i].ItemArray[2].ToString(),
                        inputType = tableInput.Rows[i].ItemArray[3].ToString(),
                        referenceFile = tableInput.Rows[i].ItemArray[4].ToString(),
                        defaultValue = tableInput.Rows[i].ItemArray[5].ToString(),
                        actualValue = tableInput.Rows[i].ItemArray[6].ToString()
                    };

                    collectionInput.Add(item);
                }
            }
        }

        public class OutputDataItem : INotifyPropertyChanged {

            public OutputDataItem() {
                variableName = variableType = defaultValue = actualValue = "";
            }

            string _vr_name;
            public string variableName {
                get { return _vr_name; }
                set {
                    _vr_name = value;
                    OnPropertyChanged(nameof(variableName));
                }
            }
            string _vr_type;
            public string variableType {
                get { return _vr_type; }
                set {
                    _vr_type = value;
                    OnPropertyChanged(nameof(variableType));
                }
            }
            string _def_value;
            public string defaultValue {
                get { return _def_value; }
                set {
                    _def_value = value;
                    OnPropertyChanged(nameof(defaultValue));
                }
            }
            string act_value;
            public string actualValue {
                get { return act_value; }
                set {
                    act_value = value;
                    OnPropertyChanged(nameof(actualValue));
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
        public ObservableCollection<OutputDataItem> collectionOutput = null;
        void convertOutputData() {
            if (tableOutput.Rows.Count == 0) return;
            collectionOutput = new ObservableCollection<OutputDataItem>();
            for (int i = 0; i < tableOutput.Rows.Count; i++) {
                if (string.IsNullOrEmpty(tableOutput.Rows[i].ItemArray[0].ToString()) == false && string.IsNullOrWhiteSpace(tableOutput.Rows[i].ItemArray[0].ToString()) == false) {
                    OutputDataItem item = new OutputDataItem() {
                        variableName = tableOutput.Rows[i].ItemArray[0].ToString(),
                        variableType = tableOutput.Rows[i].ItemArray[1].ToString(),
                        defaultValue = tableOutput.Rows[i].ItemArray[2].ToString(),
                        actualValue = tableOutput.Rows[i].ItemArray[3].ToString()
                    };
                    collectionOutput.Add(item);
                }
            }
        }

        public class AboutDataItem {
            public string ID { get; set; }
            public string Version { get; set; }
            public string Content { get; set; }
            public string Date { get; set; }
            public string changeType { get; set; }
            public string Person { get; set; }
        }
        public List<AboutDataItem> listAbout = null;
        void convertAboutData() {
            if (tableAbout.Rows.Count == 0) return;
            listAbout = new List<AboutDataItem>();
            for (int i = 0; i < tableAbout.Rows.Count; i++) {
                if (string.IsNullOrEmpty(tableAbout.Rows[i].ItemArray[0].ToString()) == false && string.IsNullOrWhiteSpace(tableAbout.Rows[i].ItemArray[0].ToString()) == false) {
                    AboutDataItem item = new AboutDataItem() {
                        ID = tableAbout.Rows[i].ItemArray[0].ToString(),
                        Version = tableAbout.Rows[i].ItemArray[1].ToString(),
                        Content = tableAbout.Rows[i].ItemArray[2].ToString(),
                        Date = tableAbout.Rows[i].ItemArray[3].ToString(),
                        changeType = tableAbout.Rows[i].ItemArray[4].ToString(),
                        Person = tableAbout.Rows[i].ItemArray[5].ToString(),
                    };
                    _mwm.appVersion = $"Version: {item.Version}";
                    _mwm.appBuildTime = $"Build time: {item.Date}";
                    listAbout.Add(item);
                }
            }
        }
        #endregion

    }
}
