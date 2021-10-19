using RemoteUEILabelPrint.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteUEILabelPrint.Base.Function {

    public class Support {

        public static bool setOutputValue(MainWindowViewModel.OutputDataItem item) {
            try {
                switch (item.actualValue) {
                    case string a when item.actualValue.StartsWith("#"): {
                            //get method name
                            string method_name = item.actualValue.Split('(')[0].Replace("#", "").Trim();
                            //get parameters
                            string str = item.actualValue.Split('(')[1].Replace(")", "").Trim();
                            List<object> list_params_value = new List<object>();
                            if (str.Contains(",")) {
                                string[] parameter_name = str.Split(',');
                                for (int i = 0; i < parameter_name.Length; i++) {
                                    string s = parameter_name[i];
                                    if (s.Contains("!") == false) list_params_value.Add(s.ToString());
                                    else {
                                        var inp = myGlobal.mainViewModel.collectionInput.Where(x => x.variableName.Equals(s.Replace("!", ""))).FirstOrDefault();
                                        var oup = myGlobal.mainViewModel.collectionOutput.Where(x => x.variableName.Equals(s.Replace("!", ""))).FirstOrDefault();
                                        string data = inp != null ? inp.defaultValue : oup.defaultValue;
                                        list_params_value.Add(data);
                                    }
                                }
                            }
                            else {
                                list_params_value = null;
                            }
                            item.defaultValue = (string)myGlobal.libr.callMethod(method_name, list_params_value == null ? null : list_params_value.ToArray());
                            break;
                        }
                    case string b when item.actualValue.StartsWith("!"): {
                            string s = item.actualValue;
                            var inp = myGlobal.mainViewModel.collectionInput.Where(x => x.variableName.Equals(s.Replace("!", ""))).FirstOrDefault();
                            var oup = myGlobal.mainViewModel.collectionOutput.Where(x => x.variableName.Equals(s.Replace("!", ""))).FirstOrDefault();
                            item.defaultValue = inp != null ? inp.defaultValue : oup.defaultValue;
                            break;
                        }
                    default: {
                            item.defaultValue = item.actualValue;
                            break;
                        }
                }

                return true;
            } catch (Exception ex) {
                myGlobal.printViewModel.printModel.logSystem += $"{ex.ToString()}\n";
                return false;
            }
            
        }




    }
}
