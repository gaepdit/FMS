using System;
using System.IO;
using Microsoft.Win32;

namespace FMS.Helpers
{
    public class DataExportMeta
    {
        public DateTime ExportDate { get; set; }

        public DataExportMeta(DateTime dataExportDate)
        {
            ExportDate = dataExportDate;
        }

        public string FileDateString => $"{ExportDate:yyyy-MM-dd-HH-mm-ss.FFF}";

        public string FileName => $"FMS_export_{FileDateString}.csv";

        public string FilePath => FileName;

        //public string DownloadPath
        //{
        //    get
        //    {
        //        string path = String.Empty;
        //        RegistryKey rKey = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Internet Explorer\Main");
        //        if (rKey != null)
        //            path = (String)rKey.GetValue("Default Download Directory");
        //        if (String.IsNullOrEmpty(path))
        //            path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
                
        //        return path;
        //    }
        //}
    }
}
