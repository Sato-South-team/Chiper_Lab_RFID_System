using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Chiper_Lab_RFID_System
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class UserCreation : ContentPage
	{
		public UserCreation ()
		{
			InitializeComponent();
		}
        CommonClass.SoundsPlay obj = DependencyService.Get<CommonClass.SoundsPlay>();
        private void BtnSave_Clicked(object sender, EventArgs e)
        {
            try
            {
                if(ControlValidation())
                {
                    string str = txtUserName.Text + "," + txtUserID.Text + "," + txtConfiPassword.Text;
                    CommonClass.CommonMethods.WriteFile(CommonClass.CommonVariable.Path + "/UserList.csv", str);
                    Toast.MakeText(Android.App.Application.Context, "DATA SAVED", ToastLength.Long).Show(); ;
                    obj.PlaySound("Success");
                    Clear();
                }
            }
            catch(Exception ex)
            {
                obj.PlaySound("Error");
                DisplayAlert("Error", ex.Message, "OK");
            }
        }
        private void Clear()
        {
            
            txtUserName.Text = "";
            txtUserID.Text = "";
            txtPassword.Text = "";
            txtConfiPassword.Text = "";
            txtUserName.Focus();
        }
        private void BtnClear_Clicked(object sender, EventArgs e)
        {
            try
            {
                Clear();
                obj.PlaySound("Error");
                DisplayAlert("Error", "1233", "OK");
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
                App.Current.MainPage = new Login();
            }
            catch (Exception ex)
            {
                obj.PlaySound("Error");
                DisplayAlert("Error", ex.Message, "OK");
            }
        }
        private bool ControlValidation()
        {
            if (txtUserName.Text == null)
            {
                Toast.MakeText(Android.App.Application.Context, "PLEASE ENTER USERNAME", ToastLength.Long).Show();
                obj.PlaySound("Error");
                txtUserName.Focus();
                return false;
            }
            if (txtUserID.Text == null)
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
                txtPassword.Focus();
                return false;
            }
            if (txtConfiPassword.Text == null)
            {
                Toast.MakeText(Android.App.Application.Context, "PLEASE ENTER CONFIRM PASSWORD", ToastLength.Long).Show();
                obj.PlaySound("Error");
                txtConfiPassword.Text = "";
                txtConfiPassword.Focus();
                return false;
            }
            if (txtConfiPassword.Text != txtPassword.Text)
            {
                Toast.MakeText(Android.App.Application.Context, "PASSWORD IS MISSMATCHING", ToastLength.Long).Show();
                obj.PlaySound("Error");
                txtConfiPassword.Text = "";
                txtConfiPassword.Focus();
                return false;
            }
            return true;
        }
        private void ContentPage_Appearing(object sender, EventArgs e)
        {
            try
            {
                if (!File.Exists(CommonClass.CommonVariable.Path + "/UserList.csv"))
                {
                    string str = "UserName" + "," + "UserID" + "," + "Password";
                    CommonClass.CommonMethods.WriteFile(CommonClass.CommonVariable.Path + "/UserList.csv", str);
                }
                txtUserName.Focus();
            }
            catch (Exception ex)
            {
                obj.PlaySound("Error");
                DisplayAlert("Error", ex.Message, "OK");
            }
        }
    }
}