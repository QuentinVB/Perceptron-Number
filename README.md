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