using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Com.Cipherlab.Rfidapi;
using Android.Content;
using Com.Cipherlab.Rfid;

namespace Chiper_Lab_RFID_System.Droid
{
    [Activity(Label = "Asset_Tracking_System", Icon = "@drawable/RFID", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        IntentFilter filter;
        Receiver myDataReceiver;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            //TabLayoutResource = Resource.Layout.Tabbar;
            //ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            CommonClass.CommonVariable.m_RM  = RfidManager.InitInstance(this);
            myDataReceiver = new Receiver();

            filter = new IntentFilter();
            filter.AddAction(GeneralString.IntentRFIDSERVICECONNECTED);
            filter.AddAction(GeneralString.IntentRFIDSERVICETAGDATA);
            RegisterReceiver(myDataReceiver, filter);
            LoadApplication(new App());
        }
        //protected override void OnResume()
        //{
        //    base.OnResume();
        //    if (Variable.m_RM.ConnectionStatus)
        //    {
        //        new InitTask(this).Execute();
        //    }
        //    else
        //    {
        //        Toast.MakeText(Android.App.Application.Context, "Start failuer", ToastLength.Long).Show();
        //    }
        //}
        //protected override void OnPause()
        //{
        //    base.OnPause();
        //    Variable.m_RM.Release();
        //    UnregisterReceiver(myDataReceiver);

        // //   TagReading.handler.Stop();
        //}
        //private class InitTask : AsyncTask<Java.Lang.Void, Java.Lang.Void, string[]>
        //{
        //    MainActivity mainActivity;
        //    public InitTask(MainActivity _mainActivity)
        //    {
        //        mainActivity = _mainActivity;
        //    }

        //    protected override string[] RunInBackground(params Java.Lang.Void[] @params)
        //    {
        //        return null;
        //    }
        //    //后台要执行的任务

        //    //开始执行任务
        //    protected override void OnPreExecute()
        //    {
        //        Variable.ProDialg = new ProgressDialog(mainActivity);
        //        Variable.ProDialg.SetMessage("init.....");
        //        Variable.ProDialg.Show();
        //    }
        //}
    
    public override bool OnKeyDown(Keycode keyCode, KeyEvent e)
        {
            if (e.KeyCode.GetHashCode() == 545)
            {
                if (e.RepeatCount == 0)
                {
                    if (CommonClass.CommonVariable.m_RM.ConnectionStatus)
                    {
                        MainPage.handler.Start();
                        return true;
                    }
                    else
                        Toast.MakeText(Android.App.Application.Context, "Start failuer", ToastLength.Long).Show();
                }
            }
            return base.OnKeyDown(keyCode, e);
        }
        protected override void OnDestroy()
        {
            base.OnDestroy();

            // ***************************************************//
            // 在關閉此APP前可以先註銷之前的Broadcast Receiver
            // ***************************************************//
            UnregisterReceiver(myDataReceiver);

            // Reader Release
            CommonClass.CommonVariable.m_RM.Release();
        }
    }
}