using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExportChartToPDF
{
    public class ViewModel
    {
        public List<Model> Data { get; set; }

        public ViewModel()
        {
            Data = new List<Model>()
            {
            new Model { Name = "David", Height = 170 },
            new Model { Name = "Michael", Height = 96 },
            new Model { Name = "Steve", Height = 65 },
            new Model { Name = "Joel", Height = 182 },
            new Model { Name = "Bob", Height = 134 }
            };
        }
    }

    public class Model
    {
        public string Name { get; set; }
        public double Height { get; set; }
    }
}
