using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.IO;
using SQLite.DependancyInjection;
using System.Text.RegularExpressions;

namespace SQLite
{
    [Activity(Label = "Spectrum", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        EditText txtusername;
        EditText txtPassword;
        Button btncreate;
        Button btnsign;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            btnsign = FindViewById<Button>(Resource.Id.btnlogin);
            btncreate = FindViewById<Button>(Resource.Id.btnregister);
            txtusername = FindViewById<EditText>(Resource.Id.txtusername);
            txtPassword = FindViewById<EditText>(Resource.Id.txtpwd);



            btnsign.Click += Btnsign_Click;
            btncreate.Click += Btncreate_Click;

            CreateDB();
        }

		protected override void OnResume()
		{
            txtusername.Text = "" ;
            txtPassword.Text = "" ;
            base.OnResume();
		}

		private void Btncreate_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(RegisterActivity));
        }



        private void Btnsign_Click(object sender, EventArgs e)
        {
            try
            {
                
        // Implemented the Simple Example of Constructor Dependancy Injection for dealing with SQliteclases.

                SQLite.DependancyInjection.DataInterClient  dependancyClient = new SQLite.DependancyInjection.DataInterClient(new SQLite.DependancyInjection.DataInterService()) ;
                TableQuery<LoginTable> data= (TableQuery<LoginTable>)dependancyClient.Start();

   
                   foreach (var s in data)
                {
                    Console.WriteLine(s.username.ToString() + " " + s.password.ToString());
                }

                var data1 = data.Where(x => x.username == Regex.Replace(txtusername.Text, " ", "") && x.password ==Regex.Replace(txtPassword.Text, " ", "") ).FirstOrDefault();


                if (data1 != null)
                {
                    Toast.MakeText(this, "Login Success : ", ToastLength.Short).Show();
                    StartActivity(typeof(ListofUsersActivity));

                }
                else
                {
                    Toast.MakeText(this, "Username or Password invalid", ToastLength.Short).Show();
                }
            }
            catch (Exception ex)
            {
                //Toast.MakeText(this, ex.ToString(), ToastLength.Short).Show();
            }
        }

        // it creates the inline database SQlite database with user.db name

        public string CreateDB()
        {
            var output = "";
            output += "Creating Databse if it doesnt exists";
            string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "user.db3");
            var db = new SQLiteConnection(dpPath);
            output += "\n Database Created....";
            return output;
        }
    }
}

