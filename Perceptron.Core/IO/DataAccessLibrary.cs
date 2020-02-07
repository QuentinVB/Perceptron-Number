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
    //make it partial
    public class DataAccessLibrary<T> where T : IComparable<T>
    {
        public static string DbFile
        {
            get { return Environment.CurrentDirectory + "\\network.sqlite"; }
        }

        public static SQLiteConnection SimpleDbConnection()
        {
            return new SQLiteConnection("Data Source=" + DbFile);
        }

        //NOTE : SQLITE RowID is NOT 0 based !

        public void CreateDatabase()
        {
            using (var con = SimpleDbConnection())
            {
                con.Open();

                //layer
                con.Execute("DROP TABLE IF EXISTS Layer");
                con.Execute("CREATE TABLE Layer(" +
                    "Id INTEGER PRIMARY KEY,"+
                    "NeuronQuantity INT," +
                    "Depth INT)"); //-1:out 0:in , other:hiddenlayer

                //Neuron
                //foreign key
                con.Execute("DROP TABLE IF EXISTS Neuron");
                con.Execute("CREATE TABLE Neuron(" +
                    "Id INTEGER PRIMARY KEY," +
                    "LayerId INT NOT NULL," +
                    "LocalIndex INT NOT NULL," +
                    "Bias REAL," +
                    "FOREIGN KEY(LayerId) REFERENCES Layer(Id))");

                //link
                //foreign key
                con.Execute("DROP TABLE IF EXISTS Link");
                con.Execute("CREATE TABLE Link(" +
                    "Id INTEGER PRIMARY KEY," +
                    "InputNeuronId INT," +
                    "OutputNeuronId INT," +
                    "Weight REAL," +
                    "FOREIGN KEY(InputNeuronId) REFERENCES Neuron(Id)," +
                    "FOREIGN KEY(OutputNeuronId) REFERENCES Neuron(Id))");
            }
        }

        //Make it async : https://docs.microsoft.com/fr-fr/dotnet/csharp/programming-guide/concepts/async/
        public void SaveNetwork(Network<T> network)
        {
            for (int i = 0; i < network.Layers.Length; i++)
            {
                SaveLayer(network.Layers[i], i);
            }  
        }

        public void SaveLayer(Layer layer,int depth)
        {
            if (!File.Exists(DbFile))
            {
                CreateDatabase();
            }

            //TODO : secure data  
            using (SQLiteConnection cnn = SimpleDbConnection())
            {
                cnn.Open();
                //string sql = "INSERT INTO Layer (Depth) Values (@depth);";
                
                int layerId = cnn.QueryFirst<int>(
                    @"INSERT INTO Layer 
                    ( NeuronQuantity, Depth ) VALUES 
                    ( @NeuronQuantity, @Depth );
                    select last_insert_rowid()", new { NeuronQuantity=layer.Neurons.Length ,Depth=depth });

                //var affectedRows = cnn.Execute(sql, new { depth });
                //Console.WriteLine(affectedRows);

                //add neurons
                /*using (var transaction = cnn.BeginTransaction())
                {*/
                    string sql2 =
                    @"INSERT INTO Neuron 
                    (LayerId,LocalIndex,Bias) VALUES 
                    (@LayerId,@LocalIndex,@Biais); select last_insert_rowid()";

                    //https://sql.sh/cours/insert-into
                    for (int i = 0; i < layer.Neurons.Length; i++)
                    {
                        var param = new { LayerId = layerId, LocalIndex = i, Biais = layer.Neurons[i].Bias };
                        int outputneuronid = cnn.QueryFirst<int>(sql2, param);
                        if (layer.Neurons[i].ParentsLink.Length > 0) SaveParentNeuronLink(cnn, layer.Neurons[i], outputneuronid, layerId);

                    }
                /*
                transaction.Commit();
            }

            if(depth>0)
            {
                for (int i = 0; i < layer.Neurons.Length; i++)
                {
                    if (layer.Neurons[i].ParentsLink.Length > 0) SaveParentNeuronLink(cnn,layer.Neurons[i], outputneuronid, layerId);
                }
            }
             */

            }

        }

        public void SaveParentNeuronLink(SQLiteConnection connexion, Neuron neuron, int outputneuronid, int layerId)
        {
            if (layerId - 1 < 0) throw new ArgumentOutOfRangeException();
            //TODO : secure data  

            using (var transaction = connexion.BeginTransaction())
            {
                //add link
                string sql1 =
                    @"INSERT INTO Link 
                (InputNeuronId,OutputNeuronId,Weight) VALUES 
                (@InputNeuronId,@OutputNeuronId,@Weight)";

                string sql2 =
                    @"SELECT Id 
                FROM neuron 
                WHERE LayerId = @LayerID AND LocalIndex = @LocalIndex";

                //https://sql.sh/cours/insert-into
                for (int i = 0; i < neuron.ParentsLink.Length; i++)
                {
                    int parentNeuronDbId = connexion.QueryFirst<int>(sql2, new { LayerID = layerId - 1, LocalIndex = i });

                    var param = new { InputNeuronId = parentNeuronDbId, OutputNeuronId = outputneuronid, Weight = neuron.ParentsLink[i].Weight };
                    connexion.Execute(sql1, param, transaction: transaction);
                }
                transaction.Commit();
            }

        }

        public class NeuronViewModel
        {
                public int Id;
                public int LayerId;
                public int LocalIndex;
                public float Bias;            
            }
        public class LayerViewModel
        {
            public int Id;
            public int NeuronQuantity;
        }

        public class LinkViewModel
        {
            public int Id;
            public int InputNeuronId;
            public int OutputNeuronId;
            public float Weight;

        }

        //Restore
        public Network<T> RestoreNetwork(Network<T> emptyNetwork)
        {
            if (!File.Exists(DbFile)) throw new FileNotFoundException();

            for (int i = 0; i < emptyNetwork.Layers.Length; i++)
            {
                RestoreLayer(emptyNetwork.Layers[i], i);
            }

            return emptyNetwork;
        }
        public Layer RestoreLayer(Layer layer, int depth)
        {
            if (!File.Exists(DbFile)) throw new FileNotFoundException();

            for (int i = 0; i < layer.Neurons.Length; i++)
            {
                var neuronData = RestoreNeuronData(depth + 1, i);

                layer.Neurons[i].Bias = neuronData.Bias;

                if(depth>0) RestoreParentsLinkData(layer.Neurons[i], neuronData.Id);
            }
            return layer;
        }
        public NeuronViewModel RestoreNeuronData(int layerId, int localIndex)
        {
            if (!File.Exists(DbFile)) throw new FileNotFoundException();
            NeuronViewModel neuron;

            using (SQLiteConnection cnn = SimpleDbConnection())
            {
                cnn.Open();

                string sql = "SELECT * FROM neuron WHERE LayerId = @LayerID AND LocalIndex = @LocalIndex";

                neuron = cnn.QueryFirst<NeuronViewModel>(sql, new { LayerID = layerId, LocalIndex = localIndex });
            }

            return neuron;
        }
        public Neuron RestoreParentsLinkData(Neuron neuron, int neuronId)
        {
            if (!File.Exists(DbFile)) throw new FileNotFoundException();
            using (SQLiteConnection cnn = SimpleDbConnection())
            {
                cnn.Open();

                string sql = "SELECT * FROM link WHERE OutputNeuronId = @OutputNeuronId;";

                var linkData = cnn.Query<LinkViewModel>(sql, new { OutputNeuronId = neuronId }).ToList();

                if (linkData.Count != neuron.ParentsLink.Length) throw new InvalidDataException();

                for (int i = 0; i < neuron.ParentsLink.Length; i++)
                {
                    neuron.ParentsLink[i].Weight = linkData[i].Weight;
                }
            }

            return neuron;
        }
        
        //update
        public void UpdateNetwork(Network<T> network)
        {
            if (!File.Exists(DbFile)) throw new FileNotFoundException();

            for (int i = 0; i < network.Layers.Length; i++)
            {
                UpdateLayers(network.Layers[i], i);
            }

        }

        public void UpdateLayers(Layer layer, int depth)
        {
            if (!File.Exists(DbFile)) throw new FileNotFoundException();
            //TODO : secure data  
            using (SQLiteConnection cnn = SimpleDbConnection())
            {
                cnn.Open();
                //string sql = "INSERT INTO Layer (Depth) Values (@depth);";

                for (int i = 0; i < layer.Neurons.Length; i++)
                {
                    string sql = "UPDATE Neuron SET Bias = @Bias WHERE LayerId = @LayerID AND LocalIndex = @LocalIndex;";
                    cnn.Execute(
                        sql,
                        new { LayerID = depth + 1, LocalIndex = i, Bias = layer.Neurons[i].Bias }
                        );


                    if (layer.Neurons[i].ParentsLink.Length > 0) UpdateParentNeuronLink(cnn, layer.Neurons[i], depth +1 , i);

                }




            }
        }

        public void UpdateParentNeuronLink(SQLiteConnection connexion, Neuron neuron, int outputlayerId, int outputLocalId )
        {
            if (!File.Exists(DbFile)) throw new FileNotFoundException();

            if (outputlayerId - 1 < 0) throw new ArgumentOutOfRangeException();
            int inputLayerId = outputlayerId - 1;
            //TODO : secure data  

            using (var transaction = connexion.BeginTransaction())
            {
                //add link
                string sql = @"UPDATE Link 
                                SET Weight = @Weight 
                                WHERE Id = id in (SELECT id FROM Neuron WHERE
                                    (OutputNeuronId = id in (SELECT id FROM neuron WHERE LayerId = @InputLayerId AND LocalIndex = @InputLocalId ))
                                    AND 
                                    (InputNeuronId = id in (SELECT id FROM neuron WHERE LayerId = @OutputlayerId AND LocalIndex = @OutputLocalId)));";

                //https://sql.sh/cours/insert-into
                for (int i = 0; i < neuron.ParentsLink.Length; i++)
                {
                    var param = new { 
                        Weight = neuron.ParentsLink[i].Weight, 
                        InputLayerId = inputLayerId, 
                        OutputlayerId = outputlayerId, 
                        InputLocalId = i,
                        OutputLocalId = outputLocalId
                    };
                    connexion.Execute(
                                sql,
                                param,
                                transaction: transaction);

                }
                transaction.Commit();
            }    
        }

        //Recreate from db

        //Update

        /*
         * insert
         cmd.CommandText = "INSERT INTO cars(name, price) VALUES('Audi',52642)";
cmd.ExecuteNonQuery();
         
         */
    }
}
