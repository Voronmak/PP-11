using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace пп_11.Models
{
    internal class Pravoobladateli
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public int INN { get; set; }

        public int ORGN { get; set; }

        public int KPP { get; set; }

        public int Phone { get; set; }

        public string Email { get; set; }

        public string Addres { get; set; }

       public int IdTypeOfPravoobladateli { get; set; }

       [ForeignKey(nameof(IdTypeOfPravoobladateli))]
       public TypeOfPravoobladateli? TypeOfPravoobladateli { get; set; } 

       public Pravoobladateli(int id, string name, int inn, int orgn, int kpp, int phone, string email, string addres, int idtypeofpravoobladateli)
        {
            Id = id;
            Name = name;
            INN = inn;
            ORGN = orgn;
            KPP = kpp;
            Phone = phone;
            Email = email;
            Addres = addres;
            IdTypeOfPravoobladateli = idtypeofpravoobladateli;
        }

        public Pravoobladateli() { }
    }
}
