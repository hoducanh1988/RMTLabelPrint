using RemoteUEILabelPrint.Base;
using RemoteUEILabelPrint.Base.Function;
using RemoteUEILabelPrint.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Input;

namespace RemoteUEILabelPrint.Commands {
    public class LabelPrintLabelCommand : ICommand {

        private LabelPrintViewModel _lpvm;
        public LabelPrintLabelCommand(LabelPrintViewModel lpvm) {
            _lpvm = lpvm;
        }

        #region ICommand Members

        public event EventHandler CanExecuteChanged {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        //enable button
        public bool CanExecute(object parameter) {
            return true;
        }

        //check input valid
        private bool checkInputValid(out string msg) {
            msg = "";
            try {
                bool ret = true;
                foreach (var item in myGlobal.mainViewModel.collectionInput) {
                    string s = item.formatData;
                    if (string.IsNullOrEmpty(s) || string.IsNullOrWhiteSpace(s)) { }
                    else {
                        switch (s) {
                            case var a when s.Contains(">="): {
                                    int value = 0;
                                    string data = s.Replace(">=", "").Trim();
                                    var ref_item = myGlobal.mainViewModel.collectionInput.Where(x => x.variableName.Equals(data.Replace("!", "").Trim())).FirstOrDefault();
                                    value = data.Contains("!") ? int.Parse(ref_item.defaultValue) : int.Parse(data);
                                    bool r = int.Parse(item.defaultValue) >= value;
                                    if (!r) {
                                        ret = false;
                                        msg += $"{item.variableName} phải >= {ref_item.variableName}.\n";
                                    }
                                    break;
                                }
                            case var b when s.Contains("<="): {
                                    int value = 0;
                                    string data = s.Replace("<=", "").Trim();
                                    var ref_item = myGlobal.mainViewModel.collectionInput.Where(x => x.variableName.Equals(data.Replace("!", "").Trim())).FirstOrDefault();
                                    value = data.Contains("!") ? int.Parse(ref_item.defaultValue) : int.Parse(data);
                                    bool r = int.Parse(item.defaultValue) <= value && int.Parse(item.defaultValue) > 0;
                                    if (!r) {
                                        ret = false;
                                        msg += $"{item.variableName} phải <= {ref_item.variableName}.\n";
                                    }
                                    break;
                                }
                            case var c when s.Contains(">"): {
                                    int value = 0;
                                    string data = s.Replace(">", "").Trim();
                                    var ref_item = myGlobal.mainViewModel.collectionInput.Where(x => x.variableName.Equals(data.Replace("!", "").Trim())).FirstOrDefault();
                                    value = data.Contains("!") ? int.Parse(ref_item.defaultValue) : int.Parse(data);
                                    bool r = int.Parse(item.defaultValue) > value;
                                    if (!r) {
                                        ret = false;
                                        msg += $"{item.variableName} phải > {ref_item.variableName}.\n";
                                    }
                                    break;
                                }
                            case var d when s.Contains("<"): {
                                    int value = 0;
                                    string data = s.Replace("<", "").Trim();
                                    var ref_item = myGlobal.mainViewModel.collectionInput.Where(x => x.variableName.Equals(data.Replace("!", "").Trim())).FirstOrDefault();
                                    value = data.Contains("!") ? int.Parse(ref_item.defaultValue) : int.Parse(data);
                                    bool r = int.Parse(item.defaultValue) < value && int.Parse(item.defaultValue) > 0;
                                    if (!r) {
                                        ret = false;
                                        msg += $"{item.variableName} phải < {ref_item.variableName}.\n";
                                    }
                                    break;
                                }
                            case var e when s.Contains("="): {
                                    int value = 0;
                                    string data = s.Replace("=", "").Trim();
                                    var ref_item = myGlobal.mainViewModel.collectionInput.Where(x => x.variableName.Equals(data.Replace("!", "").Trim())).FirstOrDefault();
                                    value = data.Contains("!") ? int.Parse(ref_item.defaultValue) : int.Parse(data);
                                    bool r = int.Parse(item.defaultValue) == value;
                                    if (!r) {
                                        ret = false;
                                        msg += $"{item.variableName} phải = {ref_item.variableName}.\n";
                                    }
                                    break;
                                }
                            case var f when s.ToLower().Trim().Contains("#null"): {
                                    bool r = string.IsNullOrEmpty(item.defaultValue) || string.IsNullOrWhiteSpace(item.defaultValue);
                                    if (r) {
                                        ret = false;
                                        msg += $"{item.variableName} phải # null.\n";
                                    }
                                    break;
                                }
                            default: break;
                        }
                    }
                }

                return ret;
            }
            catch (Exception ex) {
                myGlobal.printViewModel.printModel.logSystem += $"{ex.ToString()}\n";
                return false;
            }

        }

        //save
        public void Execute(object parameter) {
            bool is_running = false;
            Thread t = new Thread(new ThreadStart(() => {
                var model = myGlobal.printViewModel.printModel;
                is_running = true;
                Thread z = new Thread(new ThreadStart(() => {
                    int c = 0;
                    while (is_running) {
                        c++;
                        Thread.Sleep(500);
                        model.totalTime = UtilityPack.Converter.myConverter.intToTimeSpan(c * 500);
                    }
                }));
                z.IsBackground = true;
                z.Start();

                LogHelper log = new LogHelper();
                var testing = myGlobal.printViewModel.printModel;
                var setting = myGlobal.settingViewModel.Setting;
                testing.logSystem = "";

                try {
                    //check input valid or not
                    string msg;
                    bool r_input = checkInputValid(out msg);
                    if (!r_input) {
                        System.Windows.MessageBox.Show($"Lỗi nhập sai:\n{msg}", "Kiểm tra đầu vào", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                        return;
                    }

                    //in tem
                    testing.logSystem += $"{DateTime.Now}, Chuẩn bị in tem...\n";
                    model.totalResult = "Waiting...";
                    model.btnPrintContent = "Printing...";
                    if (myGlobal.mainViewModel.collectionOutput == null || myGlobal.mainViewModel.collectionOutput.Count == 0) return;
                    List<BartenderHelper.ItemVariable> list_var = new List<BartenderHelper.ItemVariable>();

                    var idx_item = myGlobal.mainViewModel.collectionInput.Where(x => x.inputType.ToLower().Equals("indexer")).FirstOrDefault();
                    if (idx_item != null) {
                        int start = int.Parse(idx_item.defaultValue.Split('~')[0].Trim());
                        int end = int.Parse(idx_item.defaultValue.Split('~')[1].Trim());
                        model.pdValue = 0;
                        model.pdMax = end - start + 1;
                        testing.logSystem += $"...box bắt đầu: {start}, box kết thúc: {end}\n";
                        testing.logSystem += $"...file layout: {setting.fileLayout}\n...printer: {setting.printerName}\n...copies: {setting.Copies}\n";

                        for (int i = start; i <= end; i++) {
                            model.pdValue++;
                            testing.logSystem += $"{DateTime.Now}, index: {i}\n";
                            idx_item.defaultValue = i.ToString();

                            testing.logSystem += $"thiết lập giá trị biến layout bartender:\n";
                            foreach (var item in myGlobal.mainViewModel.collectionOutput) {
                                Support.setOutputValue(item);
                                var iv = new BartenderHelper.ItemVariable() { Name = item.variableName, Value = item.defaultValue };
                                list_var.Add(iv);
                                testing.logSystem += $"...variable name: {item.variableName}, variable value: {item.defaultValue}\n";
                            }

                            bool r = false;
                            testing.logSystem += $"{DateTime.Now}, in tem:\n";
                            try {
                                BartenderHelper btd = new BartenderHelper();
                                btd.printLabel($"{AppDomain.CurrentDomain.BaseDirectory}Layouts\\{setting.fileLayout}", setting.printerName, setting.Copies, list_var.ToArray());
                                btd.Dispose();
                                r = true;
                                testing.logSystem += $"...kết quả: Passed\n";
                            }
                            catch (Exception ex) {
                                testing.logSystem += $"...kết quả: Failed\n";
                                testing.logSystem += $"{ex.ToString()}\n";
                                r = false;
                            }

                            //save log total
                            log.saveLogTotal(r);
                        }
                    }
                    else {
                        //
                    }

                    model.totalResult = "Passed";
                    System.Windows.MessageBox.Show("Success.", "Print Label", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                }
                catch (Exception ex) {
                    model.totalResult = "Failed";
                    testing.logSystem += $"{ex.ToString()}\n";
                    System.Windows.MessageBox.Show($"Error.\n" + ex.Message.ToString(), "Print Label", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                }
                finally {
                    model.btnPrintContent = "Print";
                    is_running = false;
                    //update default value
                    string f = $"{AppDomain.CurrentDomain.BaseDirectory}Scripts\\{myGlobal.loginViewModel.Login.scriptName}";
                    ExcelHelper e = new ExcelHelper(f);
                    e.writeInputDefaultValue("Input");

                    testing.logSystem += $"{DateTime.Now}, kết thúc in tem.\n";
                    testing.logSystem += $"{DateTime.Now}, tổng thời gian: {testing.totalTime}\n";
                    testing.logSystem += $"{DateTime.Now}, kết quả: {testing.totalResult}\n";

                    //save log detail
                    log.saveLogDetail();
                }
            }));
            t.IsBackground = true;
            t.Start();


        }

        #endregion

    }
}
