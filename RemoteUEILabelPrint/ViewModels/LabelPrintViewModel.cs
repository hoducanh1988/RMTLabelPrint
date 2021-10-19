using RemoteUEILabelPrint.Commands;
using RemoteUEILabelPrint.Models;
using RemoteUEILabelPrint.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.IO;
using RemoteUEILabelPrint.Base;

namespace RemoteUEILabelPrint.ViewModels {

    public class LabelPrintViewModel {

        public LabelPrintViewModel() {
            _lpm = new LabelPrintModel();

            PrintTest = new LabelPrintTestCommand(this);
            PrintLabel = new LabelPrintLabelCommand(this);
            SetDefault = new LabelPrintSetDefaultCommand(this);
        }

        private LabelPrintModel _lpm;
        public LabelPrintModel printModel {
            get => _lpm;
        }

        public ICommand PrintTest {
            get;
            private set;
        }
        public ICommand PrintLabel {
            get;
            private set;
        }
        public ICommand SetDefault {
            get;
            private set;
        }

        #region load input

        public void loadInputControll(StackPanel sp) {
            if (myGlobal.mainViewModel.collectionInput.Count > 0) {
                sp.Children.Clear();
                foreach (var item in myGlobal.mainViewModel.collectionInput) {
                    string s = item.inputType.ToLower();
                    switch (s) {
                        case string a when s.Contains("direct"): {
                                InputTextBoxView itbv = new InputTextBoxView(item);
                                sp.Children.Add(itbv);
                                break;
                            }
                        case string b when s.Contains("auto"): {
                                item.isEnable = false;
                                InputTextBoxView itbv = new InputTextBoxView(item);
                                sp.Children.Add(itbv);
                                break;
                            }
                        case string c when s.Contains("indexer"): {
                                item.isEnable = false;
                                InputTextBoxView itbv = new InputTextBoxView(item);
                                sp.Children.Add(itbv);
                                break;
                            }
                        case string d when s.Contains("reference"): {
                                InputComboboxView icbv = new InputComboboxView(item);
                                item.collectionData = new System.Windows.Data.CollectionView(File.ReadAllLines($"{AppDomain.CurrentDomain.BaseDirectory}References\\{item.referenceFile}"));
                                sp.Children.Add(icbv);
                                break;
                            }

                    }
                }
            }
        }

        #endregion

        #region load output



        #endregion


    }
}
