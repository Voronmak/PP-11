using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace пп_11.Models
{
    internal class TypeOfRight
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int CodeName { get; set; }

        public string? Dicribe { get; set; }

        public TypeOfRight(int id, string name, int codeName, string? dicribe)
        {
            Id = id;
            Name = name;
            CodeName = codeName;
            Dicribe = dicribe;
        }

        public TypeOfRight() { }
    }
}
