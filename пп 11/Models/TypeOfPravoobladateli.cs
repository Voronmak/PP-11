using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace пп_11.Models
{
    internal class TypeOfPravoobladateli
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public TypeOfPravoobladateli(string name)
        {
            Name = name;
        }

        public TypeOfPravoobladateli() { } 
    }
}
