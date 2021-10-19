using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RemoteUEILabelPrint.Base.Function {
    
    public class LibraryHelper {

        CSharpCodeProvider provider;
        CompilerParameters parameters;
        Assembly assembly;
        string lib_file = "", code = "", name_space = "", name_class = "";
        List<string> list_method = new List<string>();

        public LibraryHelper(string _lib_file) {
            this.provider = new CSharpCodeProvider();
            this.parameters = new CompilerParameters();
            this.lib_file = _lib_file;
        }

        public bool addLibrary() {
            try {
                // Reference to System.Drawing library
                parameters.ReferencedAssemblies.Add("System.Drawing.dll");
                parameters.ReferencedAssemblies.Add("System.Core.dll");
                parameters.ReferencedAssemblies.Add("System.dll");
                return true;
            }
            catch (Exception ex) {
                myGlobal.printViewModel.printModel.logSystem += $"{ex.ToString()}\n";
                return false; 
            }
        }

        public bool compilerSourceCode() {
            try {
                // True - memory generation, false - external file generation
                parameters.GenerateInMemory = true;
                parameters.GenerateExecutable = true;
                code = File.ReadAllText(lib_file);
                CompilerResults results = provider.CompileAssemblyFromSource(parameters, this.code);

                if (results.Errors.HasErrors) {
                    StringBuilder sb = new StringBuilder();
                    foreach (CompilerError error in results.Errors) {
                        sb.AppendLine(String.Format("Error ({0}): {1}", error.ErrorNumber, error.ErrorText));
                    }
                    throw new Exception(sb.ToString());
                }

                assembly = results.CompiledAssembly;
                return true;
            }
            catch (Exception ex) {
                myGlobal.printViewModel.printModel.logSystem += $"{ex.ToString()}\n";
                System.Windows.MessageBox.Show(ex.ToString(), "Error Compiler", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                return false;
            }
        }

        public bool loadMethodFromLib() {

            //read source code
            code = File.ReadAllText(this.lib_file);
            string[] buffer = code.Split('\n');

            //read namespace and class
            name_space = buffer.Where(x => x.ToLower().Contains("namespace")).FirstOrDefault().Replace("namespace", "").Replace("{", "").Replace("\r", "").Trim();
            name_class = buffer.Where(x => x.ToLower().Contains("class")).FirstOrDefault().Replace("class", "").Replace("public", "").Replace("{", "").Replace("\r", "").Trim();

            //read function name
            foreach (var x in buffer) {
                if (x.Contains("public ") && x.Contains("(")) {
                    string f = x.Split('(')[0];
                    string[] bff = f.Split(' ');
                    f = bff[bff.Length - 1].Trim();
                    list_method.Add(f);
                }
            }
            return list_method.Count > 0;
        }

        public object callMethod(string method_name, params object[] parameters) {
            try {
                var func = list_method.Where(x => x.ToLower().Equals(method_name.ToLower())).FirstOrDefault();
                string function_name = func.Split('(')[0].Trim();
                Type program = assembly.GetType($"{name_space}.{name_class}");
                object classInstance = Activator.CreateInstance(program, null);
                MethodInfo method = program.GetMethod(function_name);
                object data = method.Invoke(classInstance, parameters);
                program = null;
                method = null;
                classInstance = null;
                GC.Collect();
 
                return data;
            }
            catch (Exception ex) {
                myGlobal.printViewModel.printModel.logSystem += $"{ex.ToString()}\n";
                return null;
            }
        }


    }
}
