using System;
using System.IO;

namespace Ethereum_Test2.src.logger {
    static class Log {

        private static string _fileName;

        public static void StartLogger() {
            _fileName = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "//log.log";
            BeginningOfLog();
        }

        public static void StartLogger(string path) {
            _fileName = path+"log.log";
            BeginningOfLog();
        }

        public static void StartLogger(string path, string fileName) {
            _fileName = path + fileName;
            BeginningOfLog();
        }

        private static void BeginningOfLog() {
            using (StreamWriter fs = File.AppendText(_fileName)) {
                fs.WriteLine();
                fs.WriteLine();
                fs.WriteLine("======================================================");
                fs.WriteLine();
                fs.WriteLine("New Log Started: " + DateTime.Now.ToString());
                fs.WriteLine();
                fs.WriteLine("======================================================");
                fs.WriteLine();
            }
        }

        public static void InfoLog(string message) {
            LogMessage("INFO", message);
        }

        public static void ErrorLog(string message) {
            LogMessage("ERROR", message);
        }

        public static void WarningLog(string message) {
            LogMessage("WARNING", message);
        }

        private static void LogMessage(string type, string message) {
            using (StreamWriter fs = File.AppendText(_fileName)) {
                fs.WriteLine(type + " - " + DateTime.Now.ToString() + ": " + message);
            }
        }

        public static void ClearLog() {
            using (StreamWriter fs = File.CreateText(_fileName)) {
                ;
            }
        }

        public static string GetLog() {
            return File.ReadAllText(_fileName);
        }
    }
}
