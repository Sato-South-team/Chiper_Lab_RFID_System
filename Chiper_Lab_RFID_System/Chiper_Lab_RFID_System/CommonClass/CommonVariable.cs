using Com.Cipherlab.Rfidapi;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chiper_Lab_RFID_System.CommonClass
{
    public class CommonVariable
    {
        public static string UserID = "";
        public static string UserName = "";
        public static RfidManager m_RM = null;
        //public static ProgressDialog ProDialg = null;
        public static string EPC = null;
        public static string Path = Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/Essilor";
    }
}
