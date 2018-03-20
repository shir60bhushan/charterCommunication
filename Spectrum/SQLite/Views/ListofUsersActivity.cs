
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;


namespace SQLite
{
    [Activity(Label = "ListofUsersActivity")]
    public class ListofUsersActivity : Activity
    {

        private List<String> listNames;
        private ListView lstView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ListView);

            lstView = FindViewById<ListView>(Resource.Id.lvList);

            listNames = new List<string>();

            listNames.Add("user1");
            listNames.Add("XX");
            listNames.Add("abc");
            listNames.Add("TT");
            listNames.Add("aws");
            listNames.Add("bhussk");

            try
            {
                string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "user.db3");
                var db = new SQLiteConnection(dpPath);
                var data = db.Table<LoginTable>();


                listNames.Clear();

                foreach (var s in data)
                {
                    Console.WriteLine(s.username.ToString() + " " + s.password.ToString());
                    listNames.Add(s.username);
                }
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.ToString(), ToastLength.Short).Show();
            }


            ArrayAdapter<String> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, listNames);
            lstView.Adapter = adapter;


            // Create your application here
        }
    }
}
