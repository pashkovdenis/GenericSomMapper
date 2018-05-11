using System;
using System.Collections.Generic;
using System.Text;

namespace SomTests.Models
{
    public class DataPoint<T> where T : class
    {

        public DataPoint(T d, string title = "")
        {
            data = d;
            Values = new List<double>();
            Label = title;

        }

        public T data { get; }


        public string Label { set; get; }


        public List<double> Values { set; get; }
          
        public override string ToString() => $"{Label}";

    }
}
