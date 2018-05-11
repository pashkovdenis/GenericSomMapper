using SomTests.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SomTests
{
    class Program
    {
        static void Main(string[] args)
        {

            StreamReader reader = File.OpenText(AppDomain.CurrentDomain.BaseDirectory+"/data.csv");
            reader.ReadLine();   
            var data = new List<DataPoint<string>>();  

            while (!reader.EndOfStream)
            {
                string[] line = reader.ReadLine().Split(',');
                var d = new DataPoint<string>(line[0], line[0]); 
                for (int i = 0; i < line.Count()-1 ; i++)
                    d.Values.Add(double.Parse(line[i + 1])); 
                data.Add(d);
            }
            reader.Close();  

            var map = new Map<string>(3, 25);

            map.Initialise(data); 
            map.Train(); 
            var mesh = map.DumpMesh();

            Console.WriteLine("--------------------------------"); 
            foreach (var n in mesh)
            {
                Console.WriteLine("{0},{1},{2}", n.Label, n.X, n.Y);
            }


            Console.WriteLine("EOF"); 


            Console.ReadLine(); 
        }
    }
}
