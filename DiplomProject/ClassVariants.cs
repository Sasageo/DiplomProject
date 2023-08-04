using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomProject
{
    public class ClassVariants
    {
        public ClassVariants(int id, string name, double point)
        {
            this.Id = id;
            this.Name = name;
            this.Point = point;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public double Point { get; set; }

    }
}
