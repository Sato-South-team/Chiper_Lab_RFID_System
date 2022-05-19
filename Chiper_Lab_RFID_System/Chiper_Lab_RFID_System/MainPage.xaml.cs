using Android.OS;
using Android.Widget;
using Com.Cipherlab.Rfid;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Chiper_Lab_RFID_System
{
    public partial class MainPage : ContentPage
    {
        DataTable dt = new DataTable();
        public static UIHand handler;
        int i = 0;
        bool Flag = false;
        public MainPage()
        {
            InitializeComponent();
            dt.Columns.Add("Tags");
            dt.Columns.Add("Count");
            handler = new UIHand(this);

        }


        public void StopInventory()
        {
            if (btnScan.Text == "STOP READING")
            {
                Variable.m_RM.Release();
                btnScan.Text = "START READING";
                Flag = false;
            }
        }
        public void Tags_Reading()
        {
            try
            {
                if (btnScan.Text == "STOP READING")
                {
                    StopInventory();
                    Variable.EPC = "";
                    Variable.Reading = "false";// 停止识别
                    return;
                }
                if (btnScan.Text == "START READING")
                {
                    if (Variable.m_RM.ConnectionStatus)
                    {
                        btnScan.Text = "STOP READING";
                        Flag = true;
                        Variable.Reading = "true" ;// 停止识别
                        ContinuousRead();

                    }
                    else
                    {
                        Toast.MakeText(Android.App.Application.Context, "Start failuer", ToastLength.Long).Show();
                    }
                }
            }
            catch (Exception ex)
            {
                Toast.MakeText(Android.App.Application.Context, ex.Message.ToString(), ToastLength.Long).Show();
            }

        }
        private void ContinuousRead()
        {
            Thread th = new Thread(new ThreadStart(delegate
            {
                Thread.Sleep(100);
                while (Flag)
                {
                    try
                    {
                        Thread.Sleep(200);
                        int re = Variable.m_RM.RFIDDirectStartInventoryRound(InventoryType.EpcAndTid, 10);

                        Message msg = handler.ObtainMessage();
                       // string[] res = Variable.uhfAPI.ReadTagFromBuffer();//.ReadTagFormBuffer();
                        if (Variable.EPC != (null) && Variable.EPC != "")
                        {
                            string strEPC;
                            string strTid = "";
                            StringBuilder sb = new StringBuilder();
                            //if (res[0].Length != 0 && res[0] != "0000000000000000" && res[0] != "000000000000000000000000")
                            //{
                            //    strTid = "TID:" + res[0] + "\r\n";
                            //}
                            strEPC = "EPC:" + Variable.EPC + "@";
                            //sb.Append(strTid);
                            sb.Append(strEPC);
                            //sb.Append(res[2]);
                            i = i + 1;
                            msg.Obj = sb.ToString();
                            handler.SendMessage(msg);
                        }
                    }
                    catch (Exception ex)
                    {
                        Toast.MakeText(Android.App.Application.Context, ex.Message, ToastLength.Long).Show();
                        StopInventory();
                        return;
                    }

                }
            }));
            th.Start();
        }
        public class UIHand : Handler
        {
            MainPage mainpage;
            public UIHand(MainPage _ScanTags)
            {
                mainpage = _ScanTags;
            }
            public override void HandleMessage(Message msg)
            {
                try
                {
                    string result = msg.Obj + "";
                    string[] strs = result.Split('@');
                    int Duplicate = 0;
                    for (int i = 0; i < mainpage.dt.Rows.Count; i++)
                    {
                        if (mainpage.dt.Rows[i]["Tags"].ToString() == strs[0].ToString())
                        {
                            mainpage.dt.Rows[i]["Count"] = (Convert.ToInt32(mainpage.dt.Rows[i]["Count"]) + 1).ToString();
                            Duplicate = 1;
                            break;
                        }
                    }
                    if (Duplicate == 0)
                    {
                        mainpage.dt.Rows.Add(strs[0], "1");
                    }

                    mainpage.lblReadcount.Text = "Tags Count : " + mainpage.dt.Rows.Count.ToString();
                    mainpage.lblTagsCount.Text = "Read Count : " + mainpage.i.ToString();

                    mainpage.ListTag.ItemsSource = null;
                    mainpage.ListTag.ItemsSource = mainpage.dt.Rows;

                    //mainpage.sound.PlaySound("Success");
                }
                catch (Exception ex)
                {
                    Toast.MakeText(Android.App.Application.Context, ex.Message, ToastLength.Long).Show();
                    //mainpage.StopInventory();
                    return;
                }

            }

            public void Start()
            {
                mainpage.Tags_Reading();
            }
            public void Stop()
            {
                mainpage.StopInventory();
            }
        }
        private void Button_Clicked(object sender, EventArgs e)
        {
            Tags_Reading();
        }
        private void ContentPage_Appearing(object sender, EventArgs e)
        {
            CreateDirectory();
            if (!File.Exists(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/RFIDTags/Asset_Tags_Details.csv"))
            {
                string str = "AssetTags" + "," + "ScannedCount" + "," + "ScannedOn";
                StreamWriter SW = new StreamWriter(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/RFIDTags/Asset_Tags_Details.csv", true);
                SW.WriteLine(str);
                SW.Close();
            }
        }
        private void CreateDirectory()
        {
            if (!Directory.Exists(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/RFIDTags"))
            {
                Directory.CreateDirectory(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/RFIDTags");
            }
        }
        private void BtnSave_Clicked(object sender, EventArgs e)
        {
            try
            {
                StreamWriter SW = new StreamWriter(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/RFIDTags/Asset_Tags_Details.csv", true);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    SW.WriteLine(dt.Rows[i]["Tags"].ToString().Replace("\r\n", "-") + "," + dt.Rows[i]["Count"].ToString() + "," + System.DateTime.Now.ToString());
                }
                SW.Close();
                Toast.MakeText(Android.App.Application.Context, "SAVED SUCCESSFULLY", ToastLength.Long).Show();
                BtnClear_Clicked(null, null);
            }
            catch (Exception ex)
            {
                Toast.MakeText(Android.App.Application.Context, ex.Message, ToastLength.Long).Show();
            }
        }

        private void BtnClear_Clicked(object sender, EventArgs e)
        {
            try
            {
                ListTag.ItemsSource = null;
                dt.Rows.Clear();
                StopInventory();
                lblReadcount.Text = "Tags Count : 0";
                lblTagsCount.Text = "Total Read : 0";
                i = 0;
            }
            catch (Exception ex)
            {
                Toast.MakeText(Android.App.Application.Context, ex.Message, ToastLength.Long).Show();
            }
        }

        private void BtnExit_Clicked(object sender, EventArgs e)
        {
            try
            {
                StopInventory();
                //App.Current.MainPage = new MainPage();

            }
            catch (Exception ex)
            {
                Toast.MakeText(Android.App.Application.Context, ex.Message, ToastLength.Long).Show();
            }
        }
    }
}
