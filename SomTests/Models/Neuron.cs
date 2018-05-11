using System;
using System.Collections.Generic;
using System.Text;

namespace SomTests.Models
{


    /// <summary>
    /// Neuron :  pashkovdenis@gmail.com 
    /// </summary>
    public class Neuron
    {
        public double[] Weights;

        public int X;
        public int Y;

        private int length; 

        private double nf;
        public string Label { set; get; }
        public double Distance { set; get; } 
        public  object Data { set; get; }

        public Neuron(int x, int y, int length)
        {
            X = x;
            Y = y;
            this.length = length;
            nf = 1000 / Math.Log(length);
        } 

        private double Gauss(Neuron win, int it)
        {
            double distance = Math.Sqrt(Math.Pow(win.X - X, 2) + Math.Pow(win.Y - Y, 2));
            return Math.Exp(-Math.Pow(distance, 2) / (Math.Pow(Strength(it), 2)));
        } 

        private double LearningRate(int it) => Math.Exp(-it / 1000) * 0.1;


        private double Strength(int it) => Math.Exp(-it / nf) * length;


        public double UpdateWeights(double[] pattern, Neuron winner, int it)
        {
            double sum = 0;
            for (int i = 0; i < Weights.Length; i++)
                {
                    double delta = LearningRate(it) * Gauss(winner, it) * (pattern[i] - Weights[i]);
                    Weights[i] += delta;
                    sum += delta;
                }
            return sum / Weights.Length;
        }
        
        public override string ToString() => Label;
        
    }  


}
