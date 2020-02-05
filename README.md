# Perceptron-test

https://www.youtube.com/watch?v=IHZwWFHWa-w

multilayer Perceptron

2 hidden layer
16 neuron per hidden layer


public class Logger
    {
        List<string> _logContainer;
        public Logger()
        {
            _logContainer = new List<String>();
        }

        /// <summary>
        /// Logs the specified line into the file.
        /// </summary>
        /// <param name="line">The line.</param>
        public void Log(string line)
        {
            string formatedLine = $"{DateTimeOffset.UtcNow.ToLocalTime().TimeOfDay} : {line}";
            _logContainer.Add(formatedLine);
            Console.WriteLine(formatedLine);
        }
        /// <summary>
        /// Prints this instance into the text file.
        /// </summary>
        public void Print()
        {
            Directory.CreateDirectory(Path.GetDirectoryName(Directory.GetCurrentDirectory() + "\\logs\\"));
            File.WriteAllLines($".\\logs\\log-{DateTimeOffset.UtcNow.ToUnixTimeSeconds()}.txt", _logContainer);
        }
    }

https://fr.wikipedia.org/wiki/Fonction_d%27activation

Feed forward
Backpropagation


Logguer !
=> epoch

No adaptative random for seeding !
Activation function => different selon la couche (wtf !)
extraire un max d'hyperparamètres
- learning rate (alpha)
-rate scheduling (gradient descent)

https://ef.readthedocs.io/en/staging/platforms/netcore/new-db-sqlite.html

https://blog.maskalik.com/asp-net/sqlite-simple-database-with-dapper/


.mode column
.headers on

INSERT INTO Neuron (LayerId,LocalIndex,Bias) Values (1,0,0.5);


"D:\Quentin_Data\Documents\03-projets\PerceptronNumber\Perceptron.Runner\bin\Debug\network.sqlite"


TRAINING SET LABEL FILE (train-labels-idx1-ubyte):
[offset] [type]          [value]          [description]
0000     32 bit integer  0x00000801(2049) magic number (MSB first)
0004     32 bit integer  60000            number of items
0008     unsigned byte   ??               label
0009     unsigned byte   ??               label
........
xxxx     unsigned byte   ??               label
The labels values are 0 to 9.

TRAINING SET IMAGE FILE (train-images-idx3-ubyte):
[offset] [type]          [value]          [description]
0000     32 bit integer  0x00000803(2051) magic number
0004     32 bit integer  60000            number of images
0008     32 bit integer  28               number of rows
0012     32 bit integer  28               number of columns
0016     unsigned byte   ??               pixel
0017     unsigned byte   ??               pixel
........
xxxx     unsigned byte   ??               pixel
Pixels are organized row-wise. Pixel values are 0 to 255. 0 means background (white), 255 means foreground (black).

TEST SET LABEL FILE (t10k-labels-idx1-ubyte):
[offset] [type]          [value]          [description]
0000     32 bit integer  0x00000801(2049) magic number (MSB first)
0004     32 bit integer  10000            number of items
0008     unsigned byte   ??               label
0009     unsigned byte   ??               label
........
xxxx     unsigned byte   ??               label
The labels values are 0 to 9.

TEST SET IMAGE FILE (t10k-images-idx3-ubyte):
[offset] [type]          [value]          [description]
0000     32 bit integer  0x00000803(2051) magic number
0004     32 bit integer  10000            number of images
0008     32 bit integer  28               number of rows
0012     32 bit integer  28               number of columns
0016     unsigned byte   ??               pixel
0017     unsigned byte   ??               pixel
........
xxxx     unsigned byte   ??               pixel
Pixels are organized row-wise. Pixel values are 0 to 255. 0 means background (white), 255 means foreground (black).