using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteUEILabelPrint.Base.Function {
    public class ExcelHelper {
        string file_full_name = "";

        public ExcelHelper(string file) {
            file_full_name = file;
        }

        public DataTable readFile(string sheet_name, string range) {
            try {
                string _connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + file_full_name + ";" + "Extended Properties='Excel 12.0 Xml;HDR=YES;IMEX=1;MAXSCANROWS=0'";
                using (OleDbConnection conn = new OleDbConnection(_connectionString)) {
                    DataTable dt = new DataTable();
                    conn.Open();
                    OleDbDataAdapter objDA = new System.Data.OleDb.OleDbDataAdapter($"select * from [{sheet_name}${range}]", conn);
                    DataSet excelDataSet = new DataSet();
                    objDA.Fill(excelDataSet);
                    dt = excelDataSet.Tables[0];

                    excelDataSet.Dispose();
                    objDA.Dispose();
                    conn.Dispose();
                    return dt;
                }
            }
            catch (Exception ex) {
                myGlobal.printViewModel.printModel.logSystem += $"{ex.ToString()}\n";
                System.Windows.MessageBox.Show(ex.ToString(), "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                return null;
            }
        }

        public bool writeInputDefaultValue(string sheet_name) {
            try {
                if (myGlobal.mainViewModel.collectionInput == null || myGlobal.mainViewModel.collectionInput.Count == 0) return false;
                string _connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + file_full_name + ";" + "Extended Properties='Excel 12.0 Xml;HDR=NO;'";
                using (OleDbConnection conn = new OleDbConnection(_connectionString)) {
                    conn.Open();
                    string column = "F";
                    int row = 1; 
                    foreach (var item in myGlobal.mainViewModel.collectionInput) {
                        row++;
                        string sql2 = String.Format("UPDATE [{0}${1}{2}:{1}{2}] SET F1='{3}'", sheet_name, column, row, item.defaultValue);
                        OleDbCommand objCmdSelect = new OleDbCommand(sql2, conn);
                        objCmdSelect.ExecuteNonQuery();
                    }

                    conn.Close();
                    return true;
                }
            }
            catch (Exception ex) {
                myGlobal.printViewModel.printModel.logSystem += $"{ex.ToString()}\n";
                System.Windows.MessageBox.Show(ex.ToString(), "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                return false;
            }
        }




    }
}
