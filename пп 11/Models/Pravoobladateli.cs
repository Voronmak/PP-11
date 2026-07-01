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
         
        public long INN { get; set; }

        public long ORGN { get; set; }

        public long KPP { get; set; }

        public long Phone { get; set; }

        public string Email { get; set; }

        public string Addres { get; set; }

       public int IdTypeOfPravoobladateli { get; set; }

       [ForeignKey(nameof(IdTypeOfPravoobladateli))]
       public TypeOfPravoobladateli? TypeOfPravoobladateli { get; set; } 

       public Pravoobladateli(string name, long inn, long orgn, long kpp, long phone, string email, string addres, int idtypeofpravoobladateli)
        {
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
