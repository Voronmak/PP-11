using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace пп_11.Models
{
    internal class User
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public bool Status { get; set; }

        public DateTime DateCreate { get; set; }

        public int IdRole { get; set; }

        [ForeignKey(nameof(IdRole))]
        public Role? Role { get; set; }

        public User(int id, string name, string password, bool status, DateTime dateCreate, int idRole)
        {
            Id = id;
            Name = name;
            Password = password;
            Status = status;
            DateCreate = dateCreate;
            IdRole = idRole;
        }
        public User() { }
    }
}
