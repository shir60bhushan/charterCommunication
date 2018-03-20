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
using System.IO;
using System.Text.RegularExpressions;

namespace SQLite
{
    [Activity(Label = "RegisterActivity")]
    public class RegisterActivity : Activity
    {

        EditText txtusername;
        EditText txtPassword;
        Button btncreate;
      
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Newuser);
            // Create your application here
           
            btncreate = FindViewById<Button>(Resource.Id.btn_reg_create);
            txtusername = FindViewById<EditText>(Resource.Id.txt_reg_username);
            txtPassword = FindViewById<EditText>(Resource.Id.txt_reg_password);

            btncreate.Click += Btncreate_Click;
        }

        private void Btncreate_Click(object sender, EventArgs e)
        {
            try
            {
                //Regex pattern for password validation rules

                Boolean IsValid = false;
                const string passwordRegex = @"^(?=.*[A-Za-z])(?=.*\d)(?=.*[$@$!%*#?&])[A-Za-z\d$@$!%*#?&]{5,12}$"; 
                IsValid = Regex.IsMatch(txtPassword.Text, passwordRegex, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));

            
                // Here we take reference /instace of the Database and path to get the Data in the Login table .

                string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "user.db3");
            var db = new SQLiteConnection(dpPath);
            db.CreateTable<LoginTable>();
                LoginTable tbl = new LoginTable();
            tbl.username = txtusername.Text;
            tbl.password = txtPassword.Text;



                // If the Valid regex pattern is followed then the entry gets added with succes toast message

                if(IsValid)
                {
                    // The Entry is added to the data base and the activity is finished which takes to login screen again
                    db.Insert(tbl);
                    Toast.MakeText(this, "Record Added Successfully...,", ToastLength.Short).Show();
                    Finish(); 


                }
                else{
                    Toast.MakeText(this, "Enter proper format of password", ToastLength.Short).Show();
                }
            }
            catch(Exception ex)
            {
                Toast.MakeText(this, ex.ToString(), ToastLength.Short).Show();
            }
        }
    }
}