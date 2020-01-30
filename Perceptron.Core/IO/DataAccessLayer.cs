using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;
using Dapper;

namespace Perceptron.Core.IO
{
    public class DataAccessLayer
    {
        public static string DbFile
        {
            get { return Environment.CurrentDirectory + "\\network.sqlite"; }
        }

        public static SQLiteConnection SimpleDbConnection()
        {
            return new SQLiteConnection("Data Source=" + DbFile);
        }



        public void CreateDatabase()
        {
            using (var con = SimpleDbConnection())
            {
                con.Open();

                //layer
                con.Execute("DROP TABLE IF EXISTS Layer");
                con.Execute("CREATE TABLE Layer(" +
                    "Id INTEGER PRIMARY KEY,"+
                    "Depth INT)"); //-1:out 0:in , other:hiddenlayer
                
                //Neuron
                con.Execute("DROP TABLE IF EXISTS Neuron");
                con.Execute("CREATE TABLE Neuron(" +
                    "id INTEGER PRIMARY KEY," +
                    "LayerId INT" +
                    "Bias REAL," +
                    "FOREIGN KEY(LayerId) REFERENCES Layer(Id))");


                //link
                con.Execute("DROP TABLE IF EXISTS Link");
                con.Execute("CREATE TABLE Link(" +
                    "Id INTEGER PRIMARY KEY," +
                    "InputNeuronId INT" +
                    "OutputNeuronId INT" +
                    "Weight REAL)");


                //primary key link

            }
        }

        public void SaveLayer(Layer layer,int depth)
        {
            if (!File.Exists(DbFile))
            {
                CreateDatabase();
            }

            //TODO : secure data  
            using (var cnn = SimpleDbConnection())
            {
                cnn.Open();
                string sql = "INSERT INTO Layer (Depth) Values (@depth);";
                /*
                int layerId = cnn.Query<int>(
                    @"INSERT INTO Layer 
                    ( depth ) VALUES 
                    ( @depth );
                    select last_insert_rowid()", depth).First();
            */
                var affectedRows = cnn.Execute(sql, new { depth });
                Console.WriteLine(affectedRows);
            }
            //add neurons
        }


        /*
         * insert
         cmd.CommandText = "INSERT INTO cars(name, price) VALUES('Audi',52642)";
cmd.ExecuteNonQuery();
         
         */
    }
}
