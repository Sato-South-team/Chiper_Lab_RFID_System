using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Com.Cipherlab.Rfid;

namespace Chiper_Lab_RFID_System.Droid
{

    public class Receiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            //Toast.MakeText(context, "Received intent!", ToastLength.Short).Show();
            if (intent.Action == GeneralString.IntentRFIDSERVICETAGDATA)
            {
                //sDataStr = "";
                // 把intent裡夾帶的資料取出來
                //if (Variable.Reading == "true")
                //{
                    int type = intent.GetIntExtra(GeneralString.ExtraDataType, -1);
                    int response = intent.GetIntExtra(GeneralString.ExtraResponse, -1);
                    double data_rssi = intent.GetDoubleExtra(GeneralString.ExtraDataRssi, 0);

                    String PC = intent.GetStringExtra(GeneralString.ExtraPc);
                    String EPC = intent.GetStringExtra(GeneralString.ExtraEpc);
                    String TID = intent.GetStringExtra(GeneralString.ExtraTid);
                    String ReadData = intent.GetStringExtra(GeneralString.EXTRAReadData);
                    int EPC_length = intent.GetIntExtra(GeneralString.ExtraEpcLength, 0);
                    int TID_length = intent.GetIntExtra(GeneralString.ExtraTidLength, 0);
                    int ReadData_length = intent.GetIntExtra(GeneralString.EXTRAReadDataLENGTH, 0);

                //Log.Debug(TAG, "++++ [Intent_RFIDSERVICE_TAG_DATA] ++++");
                //Log.Debug(TAG, "[Intent_RFIDSERVICE_TAG_DATA] type=" + type + ", response=" + response + ", data_rssi=" + data_rssi);
                //Log.Debug(TAG, "[Intent_RFIDSERVICE_TAG_DATA] PC=" + PC);
                //Log.Debug(TAG, "[Intent_RFIDSERVICE_TAG_DATA] EPC=" + EPC);
                //Log.Debug(TAG, "[Intent_RFIDSERVICE_TAG_DATA] EPC_length=" + EPC_length);
                //Log.Debug(TAG, "[Intent_RFIDSERVICE_TAG_DATA] TID=" + TID);
                //Log.Debug(TAG, "[Intent_RFIDSERVICE_TAG_DATA] TID_length=" + TID_length);
                //Log.Debug(TAG, "[Intent_RFIDSERVICE_TAG_DATA] ReadData=" + ReadData);
                //Log.Debug(TAG, "[Intent_RFIDSERVICE_TAG_DATA] ReadData_length=" + ReadData_length);

                //Scan_Data.Text += EPC + "\n";
                CommonClass.CommonVariable.EPC = EPC;
                  //  Toast.MakeText(context, EPC, ToastLength.Short).Show();
                //}
            }

            //else if (intent.Action == GeneralString.IntentRFIDSERVICECONNECTED)
            //{
            //    String packname = intent.GetStringExtra("PackageName");
                

            //    NotificationParams gettings_NotificationParams = new NotificationParams();
            //   Variable.m_RM.GetNotification(gettings_NotificationParams);
            //    if (gettings_NotificationParams.ReaderBeep == BeepType.Mute)
            //        spinner.SetSelection(0);
            //    else if (gettings_NotificationParams.ReaderBeep == BeepType.Default)
            //        spinner.SetSelection(1);
            //    else if (gettings_NotificationParams.ReaderBeep == BeepType.Ringtone1)
            //        spinner.SetSelection(2);
            //    else if (gettings_NotificationParams.ReaderBeep == BeepType.Ringtone2)
            //        spinner.SetSelection(3);
            //    else if (gettings_NotificationParams.ReaderBeep == BeepType.Ringtone3)
            //        spinner.SetSelection(4);
            //    else if (gettings_NotificationParams.ReaderBeep == BeepType.Ringtone4)
            //        spinner.SetSelection(5);
            //}
        }
    }
}