using SomTests.Extensions;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace SomTests.Models
{

    public class Map<T> where T : class
    { 
        public Neuron[,] outputs;
        public int iteration;
        public int length;
        public int dimensions;
        public Random rnd = new Random();
        public List<double[]> patterns = new List<double[]>();
        private List<DataPoint<T>> _data; 
        public Map(int dimensions, int length)
        {
            this.length = length;
            this.dimensions = dimensions;
        } 
        public void Initialise(List<DataPoint<T>> data)
        {
            _data = data;
            outputs = new Neuron[length, length];
            foreach (var dataItem in data)
                patterns.Add(dataItem.Values.ToArray());

            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    outputs[i, j] = new Neuron(i, j, length);
                    outputs[i, j].Weights = new double[dimensions];
                    for (int k = 0; k < dimensions; k++)
                        outputs[i, j].Weights[k] = rnd.NextDouble();
                }
            }
            patterns.NormilizePatterns(dimensions);  
        }

 
        public void Train(double maxError = 0.0000006d)
        {
            double currentError = double.MaxValue;
            while (currentError > maxError)
            {
                currentError = 0;
                ConcurrentQueue<double[]> TrainingSet = new ConcurrentQueue<double[]>();
                foreach (double[] pattern in patterns)
                    TrainingSet.Enqueue(pattern);

                Parallel.For(0, patterns.Count, (int i) =>
                { 
                        TrainingSet.TryDequeue(out double[] pattern); 
                        currentError += TrainPattern(pattern); 
                }); 

 
              
            }
        }

        public double TrainPattern(double[] pattern)
        {
            double error = 0;
            Neuron winner = Winner(pattern);
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    error += outputs[i, j].UpdateWeights(pattern, winner, iteration);
                }
            }
            iteration++;
            return Math.Abs(error / (length * length));
        }

      
        public IList<Neuron> DumpMesh()
        {
            var data = new List<Neuron>();
            for (int i = 0; i < patterns.Count; i++)
            {
                Neuron n = Winner(patterns[i]);
                n.Label = _data[i].Label;
                n.Data = _data[i];
                data.Add(n);
            }
            return data;
        }

        public Neuron Winner(double[] pattern)
        {
            Neuron winner = null;
            double min = double.MaxValue;
            for (int i = 0; i < length; i++)
                for (int j = 0; j < length; j++)
                {
                    double d = pattern.Distance(outputs[i, j].Weights); 
                    if (d < min)
                    {
                        min = d;
                        winner = outputs[i, j];
                    }
                }
            return winner;
        }

      


    }
}
