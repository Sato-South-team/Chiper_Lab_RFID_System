using Android.App;

using Android.OS;
using Android.Views;
using Android.Widget;

using System;
using System.Threading;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Chiper_Lab_RFID_System
{
    public partial class App :  Xamarin.Forms.Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {

            //if (Variable.uhfAPI == null)
            //{
            //    try
            //    {
            //        Variable.uhfAPI = RFIDWithUHF.Instance;
            //        Variable.uhfAPI.StopInventory();
            //    }
            //    catch
            //    {

            //    }
            //}
            //if (Variable.m_RM.ConnectionStatus)
            //{
            //    new InitTask(this).Execute();
            //}

        }

        private class InitTask : AsyncTask<Java.Lang.Void, Java.Lang.Void, string[]>
        {

            //MainActivity mainActivity;

            private App app;

            public InitTask(App app)
            {
                this.app = app;
            }

            protected override string[] RunInBackground(params Java.Lang.Void[] @params)
            {
                //throw new NotImplementedException ();
                return null;
            }

            //后台要执行的任务
            protected override Java.Lang.Object DoInBackground(params Java.Lang.Object[] @params)
            {
                string result = string.Empty;

                if (Variable.m_RM.ConnectionStatus)
                {
                    result = "OK";
                }
                return result;
            }
            protected override void OnPostExecute(Java.Lang.Object result)
            {
                Variable.ProDialg.Cancel();
                if (result.ToString() != "OK")
                    Toast.MakeText(Android.App.Application.Context, "Init failure!", ToastLength.Short).Show();
            }

            //开始执行任务
            protected override void OnPreExecute()
            {
            }
        }
    }
}
