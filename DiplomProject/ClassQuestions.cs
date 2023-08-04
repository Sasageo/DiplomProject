using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomProject
{
    public class ClassQuestions
    {
        public ClassQuestions(int id, string name, List<ClassVariants> list)
        {
            Variants = list;
            this.Id = id;
            this.Name = name;
        }
        public ClassQuestions(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ClassVariants> Variants { get; set; }
        public int Result { get; set; }
    }
}
