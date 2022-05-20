using Android.Media;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;

namespace Chiper_Lab_RFID_System.CommonClass
{
    public class CommonMethods
    {
        private MediaPlayer Obj_Player;
        //  public static System.Media.SoundPlayer Obj_Player = new System.Media.SoundPlayer();
        public static void WriteFile(string Path, string Data)
        {
            StreamWriter SW = new StreamWriter(Path, true);
            SW.WriteLine(Data);
            SW.Close();
            // Obj_Player.
            // Obj_Player.Start();

        }
        public static string ReadFile(string Path)
        {
            string Data = "";
            if (File.Exists(Path))
            {
                StreamReader SR = new StreamReader(Path);
                Data = SR.ReadToEnd();
                SR.Close();
            }
            return Data;
        }
        public static DataTable StringToDataTable(string str)
        {
            DataTable dt = new DataTable();
            // str = str.Replace("\n", "&");
            if (str != "")
            {
                string[] str1 = str.Split('\n');

                string[] str2 = str1[0].Split(',');
                for (int i = 0; i < str2.Length; i++)
                {
                    if (str2[i].ToString() != " ")
                    {
                        dt.Columns.Add(str2[i].Split(':')[0]);
                    }
                }

                for (int j = 1; j < str1.Length; j++)
                {
                    string[] str3 = str1[j].Split(',');
                    if (str1[j].ToString() != "")
                    {
                        DataRow row = dt.NewRow();
                        for (int k = 0; k < str3.Length; k++)
                        {
                            if (str3[k] != "")
                                row[k] = str3[k];
                        }
                        dt.Rows.Add(row);
                    }
                }
            }

            return dt;
        }
    }
}
