using System;


// Its Simple example of Constructer Dependancy Injection implemented 
// which has interface ,Client , service clase which involved in creating operation of the Sqlite . 

namespace SQLite.DependancyInjection
{
    public class DataInterClient
    {

        private IDataInterface  _idataInteface; 

        public DataInterClient(IDataInterface idataInteface )
        {
            this._idataInteface = idataInteface ;
        }

       

        public Object Start()
        {
            Console.WriteLine("Service Started");
            return this._idataInteface.createSuccess();
        }
    }
}
