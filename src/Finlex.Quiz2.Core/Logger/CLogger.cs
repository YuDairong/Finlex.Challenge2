using System;
using System.Diagnostics;
using System.IO;

namespace Finlex.Quiz2.Core.Logger
{
    public static class CLogger
    {
        private static bool isInitialized = false;
        private static string outputFileLocation = "UNKNOWN";
        private static string outputFileName = "UNKNOWN";

        public static void Info(string f_message, string f_source = "")
        {
            if (!isInitialized)
            {
                Init();
            }
            Log(f_source, f_message, ELogType.INFO);
        }

        public static void Warning(string f_message, string f_source = "")
        {
            if (!isInitialized)
            {
                Init();
            }
            Log(f_source, f_message, ELogType.WARNING);
        }

        public static void Error(string f_message, string f_source = "")
        {
            if (!isInitialized)
            {
                Init();
            }
            Log(f_source, f_message, ELogType.ERROR);
        }

        public static void InitLogFileName(string fileName)
        {
            outputFileName = fileName;
            outputFileLocation = GetFileTargetLocation();
            isInitialized = false;
        }

        private static void Init()
        {
            if (!isInitialized)
            {
                if (File.Exists(Path.Combine(outputFileLocation, outputFileName)))
                {
                    File.Delete(Path.Combine(outputFileLocation, outputFileName));
                }
                isInitialized = true;
            }
        }

        private static string GetFileTargetLocation()
        {
            var exePath = Directory.GetCurrentDirectory();
            var filePath = exePath;
            if (!Directory.Exists(filePath))
            {
                Console.WriteLine("[ERROR] [{0}]: directory {1} doesn't exist. " +
                            "\r\nthe default location is {2}",
                            new StackFrame().GetMethod().DeclaringType.Name, filePath, exePath);

                filePath = exePath;
            }

            return filePath;
        }

        private static void Log(string f_source, string f_message, ELogType f_logType)
        {
            var severity = ParseLogTypeToString(f_logType);
            if (string.IsNullOrWhiteSpace(f_source))
            {
                f_source = GetLogCaller();
            }

            LogWriter(severity, f_source, f_message);
        }

        private static string ParseLogTypeToString(ELogType f_logType)
        {
            var severity = "UKN";
            switch (f_logType)
            {
                case ELogType.INFO:
                    severity = "INF";
                    break;

                case ELogType.WARNING:
                    severity = "WRN";
                    break;

                case ELogType.ERROR:
                    severity = "ERR";
                    break;

                default: break;
            }
            return severity;
        }

        private static string GetLogCaller()
        {
            var source = "UNKNOWN";
            var index = 0;
            var sfInitial = new StackFrame(0);
            bool areClassNamesEqual;

            do
            {
                index++;

                var sfToCheck = new StackFrame(index);
                areClassNamesEqual = (sfInitial.GetMethod().DeclaringType.Name)  // Get Logger Class itself
                        == (sfToCheck.GetMethod().DeclaringType.Name);  // Get Class of the indexed frame

            } while (areClassNamesEqual);

            var sf = new StackTrace().GetFrame(index);
            if (null != sf)
            {
                source = sf.GetMethod().ReflectedType.Name;
            }

            return source;
        }

        private static void LogWriter(string f_logType, string f_source, string f_message)
        {
            try
            {
                using var sw = File.AppendText(Path.Combine(outputFileLocation, outputFileName));
                sw.WriteLine("[{0}]: {1}: {2}", f_logType, f_source, f_message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("The process failed: {0}", ex.ToString());
            }
        }
    }
}
