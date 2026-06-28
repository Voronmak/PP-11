using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace пп_11.Models
{
    internal class Role
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string? Discribe { get; set; }

        public Role(int id, string name, string discribe) 
        { 
            Id = id;
            Name = name;
            Discribe = discribe; 
        }

        public Role() { }
    }
}
