using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Android.Widget;
using Chiper_Lab_RFID_System;

namespace Chiper_Lab_RFID_System
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Login : ContentPage
	{
        DataTable dt = new DataTable();
        CommonClass.SoundsPlay obj = DependencyService.Get<CommonClass.SoundsPlay>();
        public Login ()
		{
			InitializeComponent ();
		}
   
        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            try
            {
                App.Current.MainPage = new UserCreation();
            }
            catch (Exception ex)
            {
                obj.PlaySound("Error");
                DisplayAlert("Error", ex.Message, "OK");
            }
        }

        private void  CreateDirectory()
        {
            if (!Directory.Exists(CommonClass.CommonVariable.Path))
            {
                Directory.CreateDirectory(CommonClass.CommonVariable.Path);
            }
           

        }
        private void LoginList()
        {
            string Data = CommonClass.CommonMethods.ReadFile(CommonClass.CommonVariable.Path + "/UserList.csv");
            dt = CommonClass.CommonMethods.StringToDataTable(Data);
        }
        private void ContentPage_Appearing(object sender, EventArgs e)
        {
            try
            {
                CreateDirectory();
                LoginList();
                txtUserID.Focus();
            }
            catch (Exception ex)
            {
                obj.PlaySound("Error");
                DisplayAlert("Error", ex.Message, "OK");
            }
        }

        private void BtnExit_Clicked(object sender, EventArgs e)
        {
            try
            {
                var cachePath = System.IO.Path.GetTempPath();

                // If exist, delete the cache directory and everything in it recursivly
                if (System.IO.Directory.Exists(cachePath))
                    System.IO.Directory.Delete(cachePath, true);

                // If not exist, restore just the directory that was deleted
                if (!System.IO.Directory.Exists(cachePath))
                    System.IO.Directory.CreateDirectory(cachePath);
                System.Diagnostics.Process.GetCurrentProcess().Kill();

            }
            catch (Exception ex)
            {
                obj.PlaySound("Error");
                DisplayAlert("Error", ex.Message, "OK");
            }
        }
        private bool ControlValidation()
        {
            if(txtUserID.Text==null)
            {
                Toast.MakeText(Android.App.Application.Context, "PLEASE ENTER USERID", ToastLength.Long).Show();
                obj.PlaySound("Error");
                txtUserID.Focus();
                return false;
            }
            if (txtPassword.Text == null)
            {
                Toast.MakeText(Android.App.Application.Context, "PLEASE ENTER PASSWORD", ToastLength.Long).Show();
                obj.PlaySound("Error");
                txtPassword.Text = "";
                return false;
            }

            return true;
        }
        private void BtnLogin_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (ControlValidation())
                {
                    bool Flag = false;
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (dt.Rows[i]["UserID"].ToString() == txtUserID.Text && dt.Rows[i]["Password"].ToString() == txtPassword.Text)
                            {
                                App.Current.MainPage = new MainPage();
                                CommonClass.CommonVariable.UserID = txtUserID.Text;
                                CommonClass.CommonVariable.UserName = dt.Rows[i]["UserName"].ToString();
                                Flag = true;
                                return;
                            }

                        }
                        if (Flag == false)
                        {
                            Toast.MakeText(Android.App.Application.Context, "INVALID CREDENTIAL", ToastLength.Long).Show();
                            obj.PlaySound("Error");
                            txtUserID.Focus();
                         
                        }
                    }
                    else
                    {
                        Toast.MakeText(Android.App.Application.Context, "PLEASE CREATE USERS TO ACCESS THE APP", ToastLength.Long).Show();
                        obj.PlaySound("Error");
                        txtUserID.Text = "";
                        txtPassword.Text = "";
                        txtUserID.Focus();
                    }

                }
            }
            catch (Exception ex)
            {
                obj.PlaySound("Error");
                DisplayAlert("Error", ex.Message, "OK");
            }
        }

        private void TxtPassword_Completed(object sender, EventArgs e)
        {
            BtnLogin_Clicked(sender, e);
        }
    }
}