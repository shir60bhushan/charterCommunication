using System;
using System.IO;



namespace SQLite.DependancyInjection
{
    public class DataInterService :IDataInterface
    {
        public DataInterService()
        {
        }

        public object createSuccess()
        {
            string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "user.db3");
            var db = new SQLiteConnection(dpPath);
            var data = db.Table<LoginTable>();

            return data;
        }

        public Object start (Object data)
        {
            return data;
        }
    }
}
