using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace Perceptron.Core.IO
{
    public class NetworkStorage
    {
        string cs = @".\network.db";
        public NetworkStorage()
        {
            
        }

        public void CreateDB()
        {
            using (var con = new SQLiteConnection(cs))
            using (var cmd = new SQLiteCommand(con))
            {
                con.Open();

                cmd.CommandText = "DROP TABLE IF EXISTS Layer";
                cmd.ExecuteNonQuery();

                cmd.CommandText = @"CREATE TABLE Layer(
                    id INTEGER PRIMARY KEY,
                    order INT)"; //-1:out 0:in , other:hiddenlayer
                cmd.ExecuteNonQuery();  
            }
        }


        /*
         * insert
         cmd.CommandText = "INSERT INTO cars(name, price) VALUES('Audi',52642)";
cmd.ExecuteNonQuery();
         
         */
    }
}
